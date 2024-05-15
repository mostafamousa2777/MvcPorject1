using Business_Layer.Interfaces;
using Business_Layer.Repoistory;
using DataAccess_Layer.Context;
using DataAccess_Layer.models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MvcPorject_PresentionLayer.MappingProfile;

namespace MvcPorject_PresentionLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<CompanydbContext>(option=>option
            .UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddScoped<IDepartmentRepo, DepartmentRepo>();
            builder.Services.AddScoped<IEmployeeRepoistory, EmployeeRepo>();
            builder.Services.AddScoped<IUniteOfWork, UnitOfWork>();

            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<CompanydbContext>().AddDefaultTokenProviders();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
