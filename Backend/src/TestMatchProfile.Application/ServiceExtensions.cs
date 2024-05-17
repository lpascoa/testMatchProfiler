using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestMatchProfile.Application.Behaviours;
using TestMatchProfile.Application.Helpers;
using TestMatchProfile.Application.Interfaces;
using TestMatchProfile.Domain.Entities;

namespace TestMatchProfile.Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<IDataShapeHelper<Author>, DataShapeHelper<Author>>();
            services.AddScoped<IDataShapeHelper<LegalEntity>, DataShapeHelper<LegalEntity>>();
            services.AddScoped<IDataShapeHelper<LegalContract>, DataShapeHelper<LegalContract>>();

            services.AddScoped<IDataShapeHelper<Position>, DataShapeHelper<Position>>();
            services.AddScoped<IDataShapeHelper<Employee>, DataShapeHelper<Employee>>();
            services.AddScoped<IModelHelper, ModelHelper>();
        }
    }
}