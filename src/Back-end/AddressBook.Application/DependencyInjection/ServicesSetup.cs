using AddressBook.Application.Infrastructure.Behaviors.LoggingBehavior;
using AddressBook.Application.Infrastructure.Behaviors.ValidationBehavior;
using AddressBook.Application.Infrastructure.Exceptions;
using AddressBook.Application.Infrastructure.Pagination.Services;
using AddressBook.Application.Infrastructure.Pagination.Services.Implementations;
using AdressBook.Persistance.Contexts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AddressBook.Application.DependencyInjection
{
    public static class ServicesSetup
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, string? connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name));
                options.EnableSensitiveDataLogging();
            });
            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ServicesSetup).Assembly);
                cfg.AddOpenBehavior(typeof(RequestResponseLoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Validation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            services.AddServices();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPaginationService, PaginationService>();

            return services;
        }
    }
}
