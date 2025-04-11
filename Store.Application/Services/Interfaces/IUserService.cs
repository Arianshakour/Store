using Store.Domain.Dtoes.Login;
using Store.Domain.Dtoes.UserPanel;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Interfaces
{
    public interface IUserService
    {
        bool IsExistUserName(string userName);
        bool IsExistEmail(string email);
        void AddUser(RegisterDto register);
        User? LoginMember(LoginDto login);
        User? GetUserByEmail(string email);
        bool ActiveAccount(string code);
        void UpdatePassUser(ResetPasswordDto reset);
        InformationUserDto GetUserInformation(int id);
        SideBarUserPanelDto GetSideBarUserPanelData(int id);
        EditInfoDto GetUserForEdit(int id);
        void UpdateUserInfo(EditInfoDto edit);
        ChangePassDto GetUserForChangePass(int id);
        void ChangePassUser(ChangePassDto change);
        bool IsCorrectPass(int id, string pass);
    }
}
