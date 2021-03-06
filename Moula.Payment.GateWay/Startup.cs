using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moula.Payment.Domain.AggregatesModel.PaymentAggerate;
using Moula.Payment.Domain.AggregatesModel.UserAggerate;
using Moula.Payment.GateWay.Application.Behaviours;
using Moula.Payment.GateWay.Application.Commands;
using Moula.Payment.GateWay.Application.Filters;
using Moula.Payment.GateWay.Application.Queries;
using Moula.Payment.GateWay.Application.Validations;
using Moula.Payment.GateWay.Application.ViewModels;
using Moula.Payment.Infrastructure;
using Moula.Payment.Infrastructure.Repositories;

namespace Moula.Payment.GateWay
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PaymentContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MoulaPaymentDb"), o => o.MigrationsAssembly("Moula.Payment.GateWay"));
            });

            services.AddControllers(options =>
                options.Filters.Add(new PaymentExceptionFilter()))
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreatePaymentCommandValidator>());

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            // Inject services
            services.AddTransient<IValidator<CreatePaymentCommand>, CreatePaymentCommandValidator>();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IPaymentQuery, PaymentQuery>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment Gateway API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
