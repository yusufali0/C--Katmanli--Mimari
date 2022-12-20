using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAB.ToDoApp.NTier.Business.Interfaces;
using YAB.ToDoApp.NTier.Business.Mappings.AutoMapper;
using YAB.ToDoApp.NTier.Business.Services;
using YAB.ToDoApp.NTier.Business.ValidationRules;
using YAB.ToDoAppNTier.DataAccess.Contexts;
using YAB.ToDoAppNTier.DataAccess.UnitOfWork;
using YAB.ToDoAppNTier.Dtos.WorkDtos;

namespace YAB.ToDoApp.NTier.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<ToDoContext>(opt =>
            {
                opt.UseSqlServer("server=(localdb)\\mssqllocaldb; database=ToDoDb; integrated security=true;");
                opt.LogTo(Console.WriteLine,LogLevel.Information);
            });

            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new WorkProfile());
            });

            var mapper = configuration.CreateMapper();

            services.AddSingleton(mapper);

            services.AddScoped<IUow, Uow>();
            services.AddScoped<IWorkService,WorkService>();

            services.AddTransient<IValidator<WorkCreateDto>,WorkCreateDtoValidator>();
            services.AddTransient<IValidator<WorkUpdateDto>,WorkUpdateDtoValidator>();
        }
    }
}
