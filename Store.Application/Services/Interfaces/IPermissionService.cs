using Store.Domain.Dtoes.AdminPanel.Permission;
using Store.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Interfaces
{
    public interface IPermissionService
    {
        RoleViewModel GetRoles();
        void AddRole(CreatePermissionDto create);
        EditPermissionDto GetRoleByIdForEdit(int id);
        void UpdateRole(EditPermissionDto edit);
        DeletePermissionDto GetRoleByIdForDelete(int id);
        void DeleteRole(DeletePermissionDto delete);

        //permission
        PermissionViewModel GetPermissions();
        List<int> GetPermissionIdFromRoleId(int roleId);

    }
}
