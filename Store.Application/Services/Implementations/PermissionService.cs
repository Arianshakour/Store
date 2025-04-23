using Store.Application.Services.Interfaces;
using Store.Domain.Dtoes.AdminPanel.Permission;
using Store.Domain.Entities;
using Store.Domain.ViewModels;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Implementations
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionService(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public void AddRole(CreatePermissionDto create)
        {
            var role = new Role()
            {
                RoleTitle = create.RoleTitle
            };
            _permissionRepository.AddRole(role);
            _permissionRepository.Save();
        }

        public void DeleteRole(DeletePermissionDto delete)
        {
            var role = _permissionRepository.GetRole(delete.RoleId);
            if (role == null)
            {
                throw new NullReferenceException();
            }
            role.Dlt = true;
            _permissionRepository.UpdateRole(role);
            _permissionRepository.Save();
        }

        public DeletePermissionDto GetRoleByIdForDelete(int id)
        {
            var role = _permissionRepository.GetRole(id);
            if (role == null)
            {
                throw new NullReferenceException();
            }
            return new DeletePermissionDto()
            {
                RoleId = role.RoleId,
                RoleTitle = role.RoleTitle
            };
        }

        public EditPermissionDto GetRoleByIdForEdit(int id)
        {
            var role = _permissionRepository.GetRole(id);
            if (role == null)
            {
                throw new NullReferenceException();
            }
            return new EditPermissionDto()
            {
                RoleId = role.RoleId,
                RoleTitle = role.RoleTitle
            };
        }

        public RoleViewModel GetRoles()
        {
            var data = _permissionRepository.GetRoles();
            return new RoleViewModel()
            {
                roleList = data
            };
        }

        public void UpdateRole(EditPermissionDto edit)
        {
            var role = _permissionRepository.GetRole(edit.RoleId);
            if (role == null)
            {
                throw new NullReferenceException();
            }
            role.RoleTitle = edit.RoleTitle;
            _permissionRepository.UpdateRole(role);
            _permissionRepository.Save();
        }
    }
}
