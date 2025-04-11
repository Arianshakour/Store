using Store.Application.Services.Interfaces;
using Store.Domain.Common.Convertors;
using Store.Domain.Common.Generators;
using Store.Domain.Common.Security;
using Store.Domain.Dtoes.Login;
using Store.Domain.Dtoes.UserPanel;
using Store.Domain.Entities;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ActiveAccount(string code)
        {
            var user = _userRepository.GetUserByActiveCode(code);
            if(user==null || user.IsActive)
            {
                return false;
            }
            user.IsActive = true;
            user.ActiveCode = NameGenerator.GenerateUniqCode();
            //active code ra avaz mikonim ke agar dobare ro link faalsazi zad peida nakone
            _userRepository.Save();
            return true;
        }

        public void AddUser(RegisterDto register)
        {
            var user = new User()
            {
                UserName = register.UserName,
                Email = FixedText.FixedEmail(register.Email),
                Password = PasswordHelper.EncodePasswordMd5(register.Password),
                ActiveCode = NameGenerator.GenerateUniqCode(),
                IsActive = false,
                CreateOn = DateTime.Now,
                UserAvatar = "Defult.jpg"
            };
            register.ActiveCode = user.ActiveCode;
            _userRepository.Add(user);
            _userRepository.Save();
        }

        public void ChangePassUser(ChangePassDto change)
        {
            var user = _userRepository.GetUserById(change.Id);
            if (user == null)
            {
                throw new Exception("کاربر یافت نشد.");
            }
            user.Password = PasswordHelper.EncodePasswordMd5(change.Password);
            _userRepository.UpdateUser(user);
            _userRepository.Save();
        }

        public SideBarUserPanelDto GetSideBarUserPanelData(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new Exception("کاربر یافت نشد.");
            }
            return new SideBarUserPanelDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                CreateOn = user.CreateOn,
                ImageName = user.UserAvatar
            };
            //inam balad bash ...
            //return _context.Users.Where(u => u.UserName == username).Select(u => new SideBarUserPanelViewModel()
            //{
            //    UserName = u.UserName,
            //    ImageName = u.UserAvatar,
            //    RegisterDate = u.RegisterDate
            //}).Single();
        }

        public User? GetUserByEmail(string email)
        {
            var x = FixedText.FixedEmail(email);
            var user = _userRepository.GetUserByEmail(x);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public ChangePassDto GetUserForChangePass(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new Exception("کاربر یافت نشد.");
            }
            return new ChangePassDto();
        }

        public EditInfoDto GetUserForEdit(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new Exception("کاربر یافت نشد.");
            }
            return new EditInfoDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                UserAvatar = user.UserAvatar
            };
        }

        public InformationUserDto GetUserInformation(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new Exception("کاربر یافت نشد.");
            }
            return new InformationUserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                CreateOn = user.CreateOn,
                Wallet = 0
            };
        }

        public bool IsCorrectPass(int id, string pass)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new Exception("کاربر یافت نشد.");
            }
            var passToHash = PasswordHelper.EncodePasswordMd5(pass);
            return passToHash == user.Password;
        }

        public bool IsExistEmail(string email)
        {
            var x = FixedText.FixedEmail(email);
            return _userRepository.IsExistEmail(x);
        }

        public bool IsExistUserName(string userName)
        {
            return _userRepository.IsExistUserName(userName);
        }

        public User? LoginMember(LoginDto login)
        {
            string email = FixedText.FixedEmail(login.Email);
            string pass = PasswordHelper.EncodePasswordMd5(login.Password);
            var user = _userRepository.GetUser(email, pass);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public void UpdatePassUser(ResetPasswordDto reset)
        {
            var user = GetUserByEmail(reset.Email);
            if (user == null)
            {
                throw new Exception("کاربر یافت نشد.");
            }
            user.Password = PasswordHelper.EncodePasswordMd5(reset.Password);
            _userRepository.UpdateUser(user);
            _userRepository.Save();
        }

        public void UpdateUserInfo(EditInfoDto edit)
        {
            var user = _userRepository.GetUserById(edit.Id);
            if (user == null)
            {
                throw new Exception("کاربر یافت نشد.");
            }
            user.Id = edit.Id;
            user.UserName = edit.UserName;
            user.Email = FixedText.FixedEmail(edit.Email);
            if(edit.imgUp != null)
            {
                if(edit.UserAvatar != "Defult.jpg")
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SiteQaleb/UserAvatar", edit.UserAvatar);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                edit.UserAvatar = Guid.NewGuid() + Path.GetExtension(edit.imgUp.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SiteQaleb/UserAvatar", edit.UserAvatar);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    edit.imgUp.CopyTo(stream);
                }
            }
            user.UserAvatar = edit.UserAvatar;
            _userRepository.UpdateUser(user);
            _userRepository.Save();
        }
    }
}
