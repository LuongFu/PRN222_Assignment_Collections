using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IAccountRepository
    {
        SystemAccount GetAccountById(short accountID);
        SystemAccount GetAccountByEmail(string email);
        SystemAccount Login(string email, string password);
        SystemAccount Add(SystemAccount account);
        SystemAccount Update(SystemAccount account);
        SystemAccount Delete(short accountID);
        IEnumerable<SystemAccount> GetAll();
    }
}
