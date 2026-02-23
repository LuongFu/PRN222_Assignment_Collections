using BusinessObjects;
using DataAccessObjects;

namespace Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDAO _accountDAO;

        public AccountRepository(AccountDAO accountDAO)
        {
            _accountDAO = accountDAO;
        }
        public SystemAccount GetAccountById(short accountId)
            => _accountDAO.GetAccountById(accountId);

        public SystemAccount GetAccountByEmail(string email)
            => _accountDAO.GetAccountByEmail(email);

        public SystemAccount Login(string email, string password)
            => _accountDAO.Login(email, password);
        public IEnumerable<SystemAccount> GetAll()
            => _accountDAO.GetAll();
        public SystemAccount Add(SystemAccount account)
            => _accountDAO.Add(account);
        public SystemAccount Update(SystemAccount account)
            => _accountDAO.Update(account);
        public SystemAccount Delete(short accountID)
            => _accountDAO.Delete(accountID);
    }
}
