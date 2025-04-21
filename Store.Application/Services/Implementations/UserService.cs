using Microsoft.Win32;
using Store.Application.Services.Interfaces;
using Store.Domain.Common.Convertors;
using Store.Domain.Common.Generators;
using Store.Domain.Common.Security;
using Store.Domain.Dtoes.AdminPanel;
using Store.Domain.Dtoes.Login;
using Store.Domain.Dtoes.UserPanel;
using Store.Domain.Entities;
using Store.Domain.ViewModels;
using Store.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public int GetMojodiWallet(int id)
        {
            var varizi = _userRepository.VariziBeHesab(id);
            var bardasht = _userRepository.BardashtAzHesab(id);
            var mojodi = varizi.Sum() - bardasht.Sum();
            return mojodi;
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
                Wallet = GetMojodiWallet(id)
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

        public ChargeWalletDto GetWallet(int id)
        {
            return new ChargeWalletDto()
            {
                UserId = id,
                Balance = GetMojodiWallet(id)
            };
        }

        public WalletViewModel GetWalletUser(int id)
        {
            var wallets = _userRepository.GetWallets(id);
            return new WalletViewModel()
            {
                walletList = wallets
            };
            //return wallets.Select(x => new ListWalletDto()
            //{
            //    Amount = x.Amount,
            //    DateTime = x.CreateDate,
            //    Description = x.Description,
            //    Type = x.TypeId
            //}).ToList();
        }

        public void AddWallet(ChargeWalletDto charge)
        {
            var wal = new Wallet()
            {
                UserId = charge.UserId,
                Amount = charge.Amount,
                IsPay = false,
                CreateDate =DateTime.Now,
                TypeId = 1,
                Description = "شارژ حساب"
            };
            _userRepository.AddWallet(wal);
            _userRepository.Save();

        }

        public UserViewModel GetUsers(string searchValue, int page, int pageSize, int isDel)
        {
            var data = _userRepository.GetUsers(searchValue, isDel);
            int dataCount = data.Count();
            return new UserViewModel()
            {
                userList = data.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                pager = new Pager(dataCount,page,pageSize)
            };
        }

        public void AddUserForm(CreateUserDto create)
        {
            create.ImageName = Guid.NewGuid() + Path.GetExtension(create.imgUp.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SiteQaleb/UserAvatar", create.ImageName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                create.imgUp.CopyTo(stream);
            }
            //inja create.UserRoles yek listi az id haye role ha hast
            //pas bayad bere to table UserRole beshine
            //to list akharesh ke khob chon in userRoles list hast lazeme
            //goftam be ezaye har role id ke dari boro to table UserRole ydone jadid besaz
            var userRoles = create.UserRoles.Select(roleId => new UserRole { RoleId = roleId }).ToList();
            //injoori ham mishe nevesht mese paein bdoone LinQ
            //var userRoles = new List<UserRole>();
            //foreach (var roleId in create.UserRoles)
            //{
            //    userRoles.Add(new UserRole { RoleId = roleId });
            //}
            var user = new User()
            {
                UserName = create.UserName,
                Email = create.Email,
                ActiveCode = NameGenerator.GenerateUniqCode(),
                Password = PasswordHelper.EncodePasswordMd5(create.Password),
                IsActive = true,
                CreateOn = DateTime.Now,
                UserAvatar = create.ImageName,
                userRoles = userRoles
            };
            _userRepository.Add(user);
            _userRepository.Save();
        }

        public EditUserDto GetUserForEditAdmin(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new NullReferenceException();
            }
            return new EditUserDto()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                ImageName = user.UserAvatar,
                UserRoles = user.userRoles.Select(x => x.RoleId).ToList()
            };
            //baraye userrole injoori ham mishe nevesht
            //var userRoles = new List<int>();
            //foreach (var item in user.userRoles)
            //{
            //    userRoles.Add(item.role.RoleId);
            //}
            //return new EditUserDto()
            //{
            //    Id = user.Id,
            //    Email = user.Email,
            //    UserName = user.UserName,
            //    ImageName = user.UserAvatar,
            //    UserRoles = userRoles
            //};
        }

        public void EditUserAdmin(EditUserDto edit)
        {
            var user = _userRepository.GetUserById(edit.Id);
            if (user == null)
            {
                throw new NullReferenceException();
            }
            if (!string.IsNullOrEmpty(edit.Password))
            {
                user.Password = PasswordHelper.EncodePasswordMd5(edit.Password);
            }
            if (edit.imgUp != null)
            {
                if (edit.ImageName != "Defult.jpg")
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SiteQaleb/UserAvatar", edit.ImageName);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                edit.ImageName = Guid.NewGuid() + Path.GetExtension(edit.imgUp.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/SiteQaleb/UserAvatar", edit.ImageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    edit.imgUp.CopyTo(stream);
                }
            }
            user.UserAvatar = edit.ImageName;
            //baraye Role ha bayad aval hame Role ha pak beshe badesh dobare insert beshe
            _userRepository.DeleteUserRoles(user.Id);
            //hala insert
            user.userRoles = edit.UserRoles.Select(roleId => new UserRole
            {
                RoleId = roleId,
                UserId = user.Id
            }).ToList();
            _userRepository.UpdateUser(user);
            _userRepository.Save();
        }

        public DetailsUserDto GetUserForDetailsAdmin(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new NullReferenceException();
            }
            return new DetailsUserDto()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                ImageName = user.UserAvatar,
                UserRoles = user.userRoles.Select(x => x.RoleId).ToList()
            };
        }

        public DeleteUserDto GetUserForDeleteAdmin(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new NullReferenceException();
            }
            return new DeleteUserDto()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                UserRoles = user.userRoles.Select(x => x.RoleId).ToList()
            };
        }

        public void DeleteUserAdmin(DeleteUserDto delete)
        {
            var user = _userRepository.GetUserById(delete.Id);
            if (user == null)
            {
                throw new NullReferenceException();
            }
            user.Dlt = true;
            _userRepository.UpdateUser(user);
            _userRepository.Save();
        }
    }
}
