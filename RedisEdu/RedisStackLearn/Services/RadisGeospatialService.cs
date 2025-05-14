using StackExchange.Redis;

namespace RedisStackLearn.Services
{
    public class RadisGeospatialService(IConnectionMultiplexer connection)
    {
        private IDatabase db { get; set; } = connection.GetDatabase(0);

        public Task<bool> AddAsync(string key, (float x, float y) coords, RedisValue value) => 
            db.GeoAddAsync(key, coords.x, coords.y, value);

        public Task<GeoPosition?> GetPositionAsync(string key, string value) => db.GeoPositionAsync(key, value);

        public Task<double?> GetDistancesAsync(string key, string value1, string value2) => 
            db.GeoDistanceAsync(key, value1, value2, GeoUnit.Kilometers);

        public Task<GeoRadiusResult[]> GetIntoRadiusAsync(string key, string value, long radius, GeoUnit radiusType) => 
            db.GeoRadiusAsync(key, value, radius, radiusType);
    }
}
