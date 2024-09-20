using Athena.Application.Interface;
using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Domain.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Application.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IEncryptionService _encryptionService;

        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepositoryInterface, IJwtTokenService jwtTokenService, IEncryptionService encryptionService, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepositoryInterface;
            _jwtTokenService = jwtTokenService;
            _encryptionService = encryptionService;
            _appSettings = appSettings.Value;
        }

        public object GetById(int userId)
        {
            return _userRepository.GetById(userId);
        }

        public async Task<LoginDetailsModel> Login(string userName, string password, string application)
        {
            User user = await _userRepository.Login(userName, _encryptionService.Encrypt(password), application);
            if (user == null)
                return null;

            await _userRepository.AddLoginTrackerForUserId(userName);

            JWTTokenModel tokenDetails = _jwtTokenService.GetJwtToken(application).GetAwaiter().GetResult();
            return new LoginDetailsModel
            {
                Token = tokenDetails.Token,
                ExpiryInHours = tokenDetails.ExpiryInHours,
                UserId = user.UserId,
                UserSk = user.UserSk,
                GeneratedDateTime = tokenDetails.GeneratedDateTime
            };
        }
    }
}
