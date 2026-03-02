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
                // Auto-register new account
                var allAccounts = iAccountRepository.GetAll();
                short nextId = 1;
                if (allAccounts.Any())
                {
                    nextId = (short)(allAccounts.Max(a => a.AccountId) + 1);
                }

                account = new SystemAccount
                {
                    AccountId = nextId,
                    AccountEmail = email,
                    AccountName = email.Split('@')[0], // Use email prefix as name
                    AccountRole = 2, // Default to Lecturer
                    AccountPassword = Guid.NewGuid().ToString().Substring(0, 8) // Random password
                };

                iAccountRepository.Add(account);
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
        public IEnumerable<SystemAccount> GetAll()
        {
            return iAccountRepository.GetAll();
        }
    }
}
