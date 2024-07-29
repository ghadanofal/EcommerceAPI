using Ecommerce.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.IRepositories
{
    public interface IUserRepository
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO LoginRequestDTO);
        Task<LocalUserDTO> Register(RegisterationRequestDTO RegisterationRequestDTO);
        bool IsUniqueUser(string Email);
    }
}
