using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.Shared.Payroll.Models.Base
{
    class BaseModel
    {
        public int StoreId { get; set; }
        public string UserId { get; set; }
        public bool IsReadOnly { get; set; }
        public bool MarkedDeleted { get; set; }
    }
    class BaseM
    {
        public string UserId { get; set; }
        public bool IsReadOnly { get; set; }

    }

    class Contact:Person, Address
    {
        public int ContactId { get; set; }  
        [Phone]
        public string PhoneNumber { get; set; }
        [Phone]
        public string MobileNumber { get; set; }
        [Email]
        public string Email { get; set; }
    }


    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName+" "+LastName; } }
        public string Title { get; set; }
        public Gender Gender { get; set; } // Enum Gender
        public DateTime DOB { get; set; }
    }

    class Address
    {
        public string City { get; set; }
        public string State { get;set; }
        public string Country { get; set; }
        public string StreetName { get; set; }
        public string ZipCode { get; set; }
        public string AddressLine { get; set; }
    }

    public class City
    {
        public int CityId { get; set; }
        public string CityName { get; set; }   
        public int StateId { get; set; }
        public int CountryId { get; set; }
    }

    public class State
    {
        [Key]
        public string StateName { get; set; }
        public int CountryId { get; set; }
    }
    public class Country
    {
        [Key]
        public string CountryName { get; set; }
    }
    public enum Gender { Male, Female, Transgender}
}
