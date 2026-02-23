using BusinessObjects;
using Microsoft.Extensions.Configuration;
using Repositories;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository iAccountRepository;
        private readonly IConfiguration _config;
        public AccountService(IAccountRepository accountRepository, IConfiguration config)
        {
            iAccountRepository = accountRepository;
            _config = config;
        }
        public LoginResult LoginAdmin(string email, string password)
        {
            var adminEmail = _config["AdminAccount:Email"];
            var adminPass = _config["AdminAccount:Password"];

            if (email == adminEmail && password == adminPass)
            {
                return new LoginResult
                {
                    IsSuccess = true,
                    Role = 0,
                    RedirectController = "Admin"
                };
            }

            var account = iAccountRepository.Login(email, password);
            if (account == null) return new LoginResult { IsSuccess = false };

            return new LoginResult
            {
                IsSuccess = true,
                Role = (int)account.AccountRole,
                UserId = account.AccountId,
                RedirectController = "Home"
            };
        }
        public ExternalLoginResult GoogleLogin(string email)
        {
            var account = iAccountRepository.GetAccountByEmail(email);

            if (account == null)
            {
                return new ExternalLoginResult
                {
                    IsSuccess = false,
                    Error = "Google account is not registered"
                };
            }

            return new ExternalLoginResult
            {
                IsSuccess = true,
                Role = (int)account.AccountRole!,
                UserId = account.AccountId
            };
        }

        public SystemAccount GetAccountById(short accountID)
        {
            return iAccountRepository.GetAccountById(accountID);
        }
        public SystemAccount GetAccountByEmail(string email)
        {
            return iAccountRepository.GetAccountByEmail(email);
        }
        public SystemAccount Login(string email, string password)
        {
            return iAccountRepository.Login(email, password);
        }
        public SystemAccount Add(SystemAccount account)
        {
            return iAccountRepository.Add(account);
        }
        public SystemAccount Update(SystemAccount account)
        {
            return iAccountRepository.Update(account);
        }
        public SystemAccount Delete(short accountID)
        {
            return iAccountRepository.Delete(accountID);
        }
    }
}
