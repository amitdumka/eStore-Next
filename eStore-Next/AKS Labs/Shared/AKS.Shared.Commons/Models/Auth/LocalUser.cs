using System.ComponentModel.DataAnnotations;

namespace AKS.Shared.Commons.Models.Auth
{
    //    1	Admin @estore.in Admin   1	0	Amit Kumar	1	4	3
    //    2	Alok @eStore.in Alok    1	1	Alok Kumar	1	1	1
    //    3	Gita @eStore.in Geeta   1	1	Geetanjali Verma	1	11	4

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

//public enum LoginRole { Admin, StoreManager, Salesman, Accountant, RemoteAccountant, Member, PowerUser };
//insert Users Values('Admin@estore.in',1, 'Admin', 1,'2022-01-01',0,'ARD','',    'Amit Kumar',   0	);
//insert Users Values('Amit@estore.in',2, 'Dumka', 1,'2022-01-01',1,'ARD','',     'Amit Kumar',   0	);
//insert Users Values('Gita@eStore.in',3, 'Geeta' ,1, '2022-01-01',4,'ARD','',    'Geetanjali Verma', 3	);
//insert Users Values('Alok@eStore.in',4, 'Alok' , 1, '2022-01-01',2,'ARD', '',  'Alok Kumar'	,1	);
