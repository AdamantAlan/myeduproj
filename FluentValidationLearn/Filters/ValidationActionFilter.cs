using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace FluentValidationLearn.Filters
{
    public class ValidationActionFilter : IAsyncActionFilter
    {
        private const string VALIDATE_METHOD = "ValidateAsync";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var arg = context.ActionDescriptor.Parameters.Cast<ControllerParameterDescriptor>()
                .Where(p => p.ParameterInfo.GetCustomAttributes<ValidationAttribute>(false).Any())
                .Select(s => new { s.ParameterInfo.Name, s.ParameterInfo.ParameterType })
                .ToList();

            if (!arg.Any())
            {
                await next();
                return;
            }

            foreach (var a in arg)
            {
                if(!context.ActionArguments.TryGetValue(a.Name, out var objectToValidate))
                {
                    throw new ValidationException($"Параметр для валидации '{a.Name}' не указан");
                }

                var typeToValidate = a.ParameterType;
                var validatorType = typeof(IValidator<>).MakeGenericType(typeToValidate);
                var validator = context.HttpContext.RequestServices.GetService(validatorType);
                var validatorMethod = validatorType.GetMethod(VALIDATE_METHOD);

                if (validator is null || validatorMethod is null)
                {
                    await next();
                    return;
                }

                var validated = await (Task<ValidationResult>)validatorMethod.Invoke(validator, [objectToValidate, CancellationToken.None])!;

                if(!validated.IsValid)
                {
                    context.Result = new BadRequestObjectResult(string.Join('\n', validated.Errors.Select(s => s.ErrorMessage)));
                    return;
                }
            }

            await next();
        }
    }
}
