using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class User : BaseEntity 
    {
        public int ID {get; set;}
        public string firstName {get; set;}
        public string lastName {get; set;}
        public string email {get; set;}
        public string password {get; set;}
        public double balance {get; set;}
        public DateTime createdAt {get; set;}
        public DateTime updatedAt {get; set;}
        public List<Transaction> transactions {get; set;}
        public User()
        {
            transactions = new List<Transaction>();
            balance = 0;
            createdAt = DateTime.Now;
            updatedAt = DateTime.Now;
        }
    }
    public class UserRegister : BaseEntity
    {
        [Required(ErrorMessage = "First name is required")] 
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "First name can only contain alphabetic letters!")]       
        public string firstName {get; set;}

        [Required(ErrorMessage = "Last name is required")] 
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Last name can only contain alphabetic letters!")]
        public string lastName {get; set;}

        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email; please try again!")]
        public string email {get; set;}

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must contain at least 8 characters!")]
        public string password {get; set;}

        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Passwords do not match; please try again!")]
        public string confirmPassword {get; set;}
    }
    public class UserLogin : BaseEntity
    {
        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress)]
        public string email {get; set;}

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        public string password {get; set;}

    }
}