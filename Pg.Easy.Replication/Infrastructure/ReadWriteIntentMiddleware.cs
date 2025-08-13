namespace Pg.Easy.Replication.Infrastructure
{
    public sealed class ReadWriteIntentMiddleware
    {
        private readonly RequestDelegate _next;
        private const string StickyCookieName = "rw_sticky";

        public ReadWriteIntentMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext ctx, IDataSourceSelector selector)
        {
            // простое правило: GET/HEAD -> реплика; остальное -> мастер
            var method = ctx.Request.Method.ToUpperInvariant();
            var strongRead = ctx.Request.Headers.TryGetValue("x-strong-read", out var v) && v == "true";
            var stickyExists = ctx.Request.Cookies.ContainsKey(StickyCookieName);

            if (method is "GET" or "HEAD" && !strongRead && !stickyExists)
            {
                selector.UseReplica();
            }
            else
            {
                selector.UsePrimary();
            }

            await _next(ctx);

            SetSticky(ctx.Response, method, 5);
        }

        public static void SetSticky(HttpResponse response, string method, int seconds)
        {
            if (method is "GET" || method is "HEAD" || response.StatusCode is not (>= 200 and < 400)) return;

            response.Cookies.Append(
            StickyCookieName,
            "true",
            new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Lax,
                MaxAge = TimeSpan.FromSeconds(seconds)
            });
        }
    }
}
