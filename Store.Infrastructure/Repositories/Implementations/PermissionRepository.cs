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
        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
