using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AKS.Shared.Payroll.Models.Base
{
    public class Base
    {
        public string UserId { get; set; }
        public bool IsReadOnly { get; set; }
    }

    public class BaseST : Base
    {
        [DefaultValue(1)]
        [Display(Name = "Store")]
        public int StoreId { get; set; }
        public string StoreCode { get; set; }
        //public virtual Store Store { get; set; }
        public EntryStatus EntryStatus { get; set; }
    }



    public class Contact:Person
    {
        public int ContactId { get; set; }  
        [Phone]
        public string PhoneNumber { get; set; }
        [Phone]
        public string MobileNumber { get; set; }
         [EmailAddress]
        public string EMail { get; set; }
    }


    public class Person:Address
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName+" "+LastName; } }
        public string Title { get; set; }
        public Gender Gender { get; set; } // Enum Gender
        public DateTime DOB { get; set; }
    }

    public class Address
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
