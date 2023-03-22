using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SuperCarga.Application.Registrations
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddValidators();

            return services;
        }

        private static void AddValidators(this IServiceCollection services)
        {
            var validators = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IValidator).IsAssignableFrom(t)).ToList();

            foreach (var validator in validators)
            {
                if (validator.IsAbstract)
                    continue;

                services.AddTransient(validator);
            }
                
        }
    }
}
