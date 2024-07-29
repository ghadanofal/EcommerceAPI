using AutoMapper;
using Ecommerce.Core.DTO;
using Ecommerce.Core.IRepositories;
using Ecommerce.Core.IRepositories.IServices;
using Ecommerce.Core.Models;
using Ecommerce.Infastructure.Data;
using Ecommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Ecommerce.Infastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<LocalUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;
        private readonly SignInManager<LocalUser> signInManager;
        private readonly ITokenService tokenService;

        public UserRepository(ApplicationDbContext context, UserManager<LocalUser>userManager, 
                               RoleManager<IdentityRole>roleManager, IMapper mapper,
                               SignInManager<LocalUser>signInManager,
                               ITokenService tokenService)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }
        public bool IsUniqueUser(string email)
        {
            var result = context.LocalUsers.FirstOrDefault(e => e.Email == email);
            return result == null;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDTO.Email);

            var checkPasssword = await signInManager.CheckPasswordSignInAsync(user, loginRequestDTO.password, false);
            if (!checkPasssword.Succeeded)
            {
                return new LoginResponseDTO()
                {
                    user = null,
                    Token ="",
                    
                };
            }

            var role = await userManager.GetRolesAsync(user);
            return new LoginResponseDTO()
            {
                user = mapper.Map<LocalUserDTO>(user),
                Token = await tokenService.CreateTokenAsync(user),
                Role = role.FirstOrDefault(),
            };
        }

        public async Task<LocalUserDTO> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            var user = new LocalUser
            {
                UserName = registerationRequestDTO.Email.Split("@")[0],
                Email = registerationRequestDTO.Email,
                NormalizedEmail = registerationRequestDTO.Email.ToUpper(),
                FirstName = registerationRequestDTO.FName,
                LastName = registerationRequestDTO.LName,
                Address = registerationRequestDTO.Address

            };

            using (var transaction =  await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var result = await userManager.CreateAsync(user, registerationRequestDTO.Password);
                    if (result.Succeeded)
                    {

                        var role = await roleManager.RoleExistsAsync(registerationRequestDTO.Role);
                        if (!role)
                        {
                            throw new Exception($"The role {registerationRequestDTO.Role} dosen't exsist");
                        }
                        var userRoleResult = await userManager.AddToRoleAsync(user, registerationRequestDTO.Role);
                        if (userRoleResult.Succeeded)
                        {
                            await transaction.CommitAsync();
                            var userReturn = await context.LocalUsers.FirstOrDefaultAsync(u => u.Email == registerationRequestDTO.Email);
                            return mapper.Map<LocalUserDTO>(userReturn);
                        }
                        else
                        {
                            await transaction.RollbackAsync(); ///Rollback transaction if  adding to UserRole is fails
                            throw new Exception("Failed to add user to userRole");
                        }
                    }
                    else
                        {
                            throw new Exception("User Registration Failed");
                        }
                }
                catch (Exception)
                {
                    throw ;
                }
            }
            
            
        }
    }
}
