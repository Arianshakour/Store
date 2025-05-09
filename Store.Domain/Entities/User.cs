using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ActiveCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateOn { get; set; }
        public string? UserAvatar { get; set; }
        public bool Dlt { get; set; }

        //Relation

        public List<UserRole> userRoles { get; set; }
        public List<Wallet> wallets { get; set; }
        public List<Product> products { get; set; }
        public List<Order> orders { get; set; }
    }
}
