
using Ecommerce.API.mapping_profile;
using Ecommerce.Core.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Core.Models;
using Ecommerce.Infastructure.Data;
using Ecommerce.Infastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).UseLazyLoadingProxies();
            });

            //builder.Services.AddScoped(typeof(IProductsRepository), typeof(IProductsRepository));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            //builder.Services.AddScoped(typeof(RoleManager<IdentityRole>));
            //builder.Services.AddScoped(typeof(UserManager<LocalUser>));
            //builder.Services.AddScoped(typeof(SignInManager<IdentityUser>));


            builder.Services.AddAuthentication();


            //for createAsync
            builder.Services.AddIdentity<LocalUser, IdentityRole>(options =>
            { 
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;

            })
                .AddEntityFrameworkStores<ApplicationDbContext>();







            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(Mapping_Profile));
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                                              .Where(x => x.Value.Errors.Count > 0)
                                              .SelectMany(x => x.Value.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                    var validationResponse = new ApiValidationResponse(errors, 400);
                    return new BadRequestObjectResult(validationResponse);
                };
            });
            var app = builder.Build();
           




            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
