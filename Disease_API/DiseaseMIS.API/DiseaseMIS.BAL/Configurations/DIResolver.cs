using DiseaseMIS.BAL.Services;
using DiseaseMIS.BAL.Services.User;
using Microsoft.Extensions.DependencyInjection;

namespace DiseaseMIS.BAL.Configurations
{
    public static class DIResolver
    {
        public static IServiceCollection DIBALResolver(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDistrictService, DistrictsService>();
            services.AddScoped<IInchargesService, InchargeService>();
            services.AddScoped<ILaboratoryService, LaboratoryService>();
            services.AddScoped<IInstitutesService, InstitutesService>();
            services.AddScoped<IAnimalService, AnimalsService>();
            services.AddScoped<IDiseaseService, DiseaseService>();
            services.AddScoped<ISamplesService, SamplesService>();
            services.AddScoped<IFormsService, FormsService>();
            return services;
        }
    }
}
