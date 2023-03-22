using SuperCarga.Application.Settings;

namespace SuperCarga.Api.Settings
{
    public static class Registration
    {
        public static IServiceCollection AddSettings(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var jwtConfig = configurationManager.GetSection("JwtConfig").Get<JwtConfig>();
            services.AddSingleton(jwtConfig);

            var imageStoreConfig = configurationManager.GetSection("ImageStoresConfig").Get<ImageStoresConfig>();
            services.AddSingleton(imageStoreConfig);

            return services;
        }
    }
}
