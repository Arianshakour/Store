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
    public class PermissionRepository : IPermissionRepository
    {
        private readonly MyContext _context;
        public PermissionRepository(MyContext context)
        {
            _context = context;
        }

        public void AddRole(Role role)
        {
            _context.Roles.Add(role);
        }

        public void DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
        }

        public Role? GetRole(int id)
        {
            return _context.Roles.FirstOrDefault(x => x.RoleId == id);
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateRole(Role role)
        {
            _context.Roles.Update(role);
        }
    }
}
