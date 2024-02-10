using Common;
using Identity.Application.Interfaces.IRepositories;
using Identity.Application.Interfaces.IServices;
using Identity.Application.Models;
using Identity.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        public UserService(IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        public async Task<TokenModel> RegisterUser(RegisterRequestModel registerRequestModel)
        {
            var userExist = await _userRepository.Get(registerRequestModel.Email);
            if (userExist != null)
            {
                throw new BadRequestException("Email is already registered with another user");
            }

            var user = new User();
            user.Email = registerRequestModel.Email;
            user.Name = registerRequestModel.Name;
            user.Password = _passwordService.Hash(registerRequestModel.Password);
            var data = await _userRepository.Add(user);

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, data.Id.ToString()),
                new Claim(ClaimTypes.Email, data.Email )
            };

            var token = _tokenService.GenerateAccessToken(claims);

            return new TokenModel() { Token = token };
        }

        public async Task<TokenModel> Login(LoginRequestModel loginRequestModel)
        {
            var user = await _userRepository.Get(loginRequestModel.Email);
            if (user == null)
            {
                throw new BadRequestException("Email is not exist, please register the account");
            }


            var isVerified = _passwordService.Verify(loginRequestModel.Password, user.Password);
            if (!isVerified)
            {
                throw new BadRequestException("Email or Password incorrect");
            }

            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email )
            };

            var token = _tokenService.GenerateAccessToken(claims);

            return new TokenModel() { Token = token };
        }
    }
}
