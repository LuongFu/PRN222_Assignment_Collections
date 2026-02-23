using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class AccountDAO
    {
        private readonly FunewsManagementContext _context;

        public AccountDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        public SystemAccount Login(string email, string password)
        {
            return _context.SystemAccounts
                           .FirstOrDefault(a =>
                               a.AccountEmail == email &&
                               a.AccountPassword == password);
        }

        public SystemAccount GetAccountById(short accountID)
        {
            return _context.SystemAccounts
                     .FirstOrDefault(c => c.AccountId.Equals(accountID));
        }

        public SystemAccount GetAccountByEmail(string email)
        {
            return _context.SystemAccounts
                           .FirstOrDefault(c => c.AccountEmail == email);
        }
        public IEnumerable<SystemAccount> GetAll()
        {
            return _context.SystemAccounts.ToList();
        }
        public SystemAccount Add(SystemAccount account)
        {
            _context.SystemAccounts.Add(account);
            _context.SaveChanges();
            return account;
        }
        public SystemAccount Update(SystemAccount account)
        {
            _context.SystemAccounts.Update(account);
            _context.SaveChanges();
            return account;
        }
        public SystemAccount Delete(short accountId)
        {
            var account = _context.SystemAccounts.Find(accountId);
            if (account != null)
            {
                _context.SystemAccounts.Remove(account);
                _context.SaveChanges();
            }
            return account;
        }
    }
}
