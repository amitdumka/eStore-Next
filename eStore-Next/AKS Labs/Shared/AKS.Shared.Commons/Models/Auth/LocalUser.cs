using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AKS.Shared.Commons.Models.Auth
{
    public class LocalUser
    {
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreationTime { get; set; }
        public LoginRole Role { get; set; }
        public string StoreId { get; set; }
        public string EmployeeId { get; set; }
        public string GuestName { get; set; }
    }
}
