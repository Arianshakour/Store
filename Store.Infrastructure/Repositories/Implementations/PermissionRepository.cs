using Microsoft.EntityFrameworkCore;
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

        public bool CheckPermission(int userId, int permissionId)
        {
            int uId = _context.Users.Where(x => x.Id == userId).Select(x=>x.Id).FirstOrDefault();
            if(uId == 0)
            {
                return false;
            }
            //hala mirim harchi role dare in id bala roleId hasho mirizim yja
            var uRolesId = _context.UserRoles.Where(x => x.UserId == uId).Select(x => x.RoleId).ToList();
            if (uRolesId == null)
            {
                return false;
            }
            //hala miri to jadval RolePermission ba tavajoh be vorodi permissionId , roleId haye onjaro ham migiri
            //ta in 2 ta listo moqayese koni
            var pRolesId = _context.RolePermissions.Where(x => x.PermissionId == permissionId)
                .Select(x => x.RoleId).ToList();
            //ba inteesect moshtarakao peida mikonim
            return uRolesId.Intersect(pRolesId).Any();
        }

        public void DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
        }

        public List<int> GetPermissionIdFromRoleId(int roleId)
        {
            return _context.RolePermissions.Where(x => x.RoleId == roleId).Select(x => x.PermissionId).ToList();
        }

        public List<Permission> GetPermissions()
        {
            return _context.Permissions.ToList();
        }

        public Role? GetRole(int id)
        {
            return _context.Roles.Include(x=>x.rolePermissions).FirstOrDefault(x => x.RoleId == id);
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
