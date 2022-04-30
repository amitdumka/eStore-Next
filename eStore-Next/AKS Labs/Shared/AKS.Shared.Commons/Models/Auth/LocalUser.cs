using System.ComponentModel.DataAnnotations;

namespace AKS.Shared.Commons.Models.Auth
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
        public DateTime CreationTime { get; set; }
        public LoginRole Role { get; set; }
        public string StoreId { get; set; }
        public string EmployeeId { get; set; }
        public string GuestName { get; set; }
        public UserType UserType { get; set; }
    }
    public class LocalUser
    {
        public int UserId { get; set; }
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
        public bool IsEmployee { get; set; }
        public string GuestName { get; set; }
        public DateTime CreationTime { get; set; }
        public LoginRole Role { get; set; }

        public string StoreId { get; set; }
        public string EmployeeId { get; set; }
        
        public UserType UserType { get; set; }
    }
}
public enum UserType { Admin, Owner, StoreManager, Sales, Accountant, CA, Guest, PowerUser, Employees }