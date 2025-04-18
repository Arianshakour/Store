using Store.Application.Services.Interfaces;
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
        public RoleViewModel GetRole()
        {
            var data = _permissionRepository.GetRoles();
            return new RoleViewModel()
            {
                roleList = data
            };
        }
    }
}
