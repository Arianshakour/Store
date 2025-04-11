using Store.Domain.Entities;
using Store.Infrastructure.Context;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly MyContext _context;
        public UserRepository(MyContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
        }

        public User? GetUser(string email, string pass)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email && x.Password == pass);
        }

        public User? GetUserByActiveCode(string code)
        {
            return _context.Users.FirstOrDefault(x => x.ActiveCode == code);
        }

        public User? GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(x => x.Email == email);
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User? GetUserByUserName(string username)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == username);
        }

        public bool IsExistEmail(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }

        public bool IsExistUserName(string userName)
        {
            return _context.Users.Any(x => x.UserName == userName);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
        }
    }
}
