using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Infrastructure.Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        List<Role> GetRoles();
        void AddRole(Role role);
        Role? GetRole(int id);
        void UpdateRole(Role role);
        void DeleteRole(Role role);
        void Save();

        //Permission

        List<Permission> GetPermissions();
        List<int> GetPermissionIdFromRoleId(int roleId);
    }
}
