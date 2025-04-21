using Store.Domain.Dtoes.AdminPanel;
using Store.Domain.Dtoes.Login;
using Store.Domain.Dtoes.UserPanel;
using Store.Domain.Entities;
using Store.Domain.ViewModels;
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
        //wallet
        int GetMojodiWallet(int id);
        ChargeWalletDto GetWallet(int id);
        WalletViewModel GetWalletUser(int id);
        void AddWallet(ChargeWalletDto charge);


        UserViewModel GetUsers(string searchValue , int page , int pageSize,int isDel);
        void AddUserForm(CreateUserDto create);
        EditUserDto GetUserForEditAdmin(int id);
        void EditUserAdmin(EditUserDto edit);
        DetailsUserDto GetUserForDetailsAdmin(int id);
        DeleteUserDto GetUserForDeleteAdmin(int id);
        void DeleteUserAdmin(DeleteUserDto delete);
    }
}
