using Microservices.AuthAPI.Models;
using Microservices.AuthAPI.Models.Dto;
using Microservices.AuthAPI.Service.IService;
using Microservices.Services.AuthAPI.Data;
using Microsoft.AspNetCore.Identity;

namespace Microservices.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthService(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string rolename)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
            if (user != null)
            { 
                if(!_roleManager.RoleExistsAsync(rolename).GetAwaiter().GetResult())
                {
                    IdentityRole role = new IdentityRole()
                    {
                        Name = rolename,
                    };
                    _roleManager.CreateAsync(role).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, rolename);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _dbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isValid = false;

            isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            // Generate Token - JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);
            UserDto userDTO = new()
            {
                Email = user.Email,
                ID = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDTO,
                Token = token,
            };
            return loginResponseDto;

        }

        public async Task<string> Register(ResgistrationRequestDto resgistrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = resgistrationRequestDto.Email,
                Name = resgistrationRequestDto.Name,
                Email = resgistrationRequestDto.Email,
                NormalizedEmail = resgistrationRequestDto.Email.ToUpper(),
                PhoneNumber = resgistrationRequestDto.PhoneNumber,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, resgistrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _dbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == resgistrationRequestDto.Email.ToLower());
                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        ID = userToReturn.Id,
                        Name = userToReturn.Name,
                        PhoneNumber = userToReturn.PhoneNumber,
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            //return new UserDto();
            return "Error Encountered";
        }
    }
}
