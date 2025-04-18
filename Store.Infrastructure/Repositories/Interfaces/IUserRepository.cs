using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);
        void Save();
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        User? GetUser(string email, string pass);
        User? GetUserByEmail(string email);
        User? GetUserById(int id);
        void UpdateUser(User user);
        User? GetUserByActiveCode(string code);
        User? GetUserByUserName(string username);
        List<int> VariziBeHesab(int id);
        List<int> BardashtAzHesab(int id);
        List<Wallet> GetWallets(int id);
        void AddWallet(Wallet wallet);


        List<User> GetUsers(string searchValue);
    }
}
