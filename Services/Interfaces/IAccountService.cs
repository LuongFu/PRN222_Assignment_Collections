using BusinessObjects;
using Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IAccountService
    {
        SystemAccount GetAccountById(short accountID);
        SystemAccount GetAccountByEmail(string email);
        SystemAccount Login(string email, string password);
        LoginResult LoginAdmin(string email, string password);
        ExternalLoginResult GoogleLogin(string email);
        SystemAccount Add(SystemAccount account);
        SystemAccount Update(SystemAccount account);
        SystemAccount Delete(short accountID);
        IEnumerable<SystemAccount> GetAll();
    }
}
