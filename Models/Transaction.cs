using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccounts.Models
{
    public class Transaction : BaseEntity
    {
        public int ID {get; set;}
        public int userID {get; set;}
        public User User {get; set;}
        public double total {get; set;}
        public DateTime createdAt {get; set;}
        public DateTime updatedAt {get; set;}
        public Transaction()
        {
            createdAt = DateTime.Now;
            updatedAt = DateTime.Now;
        }
    }
    public class TransValidation : BaseEntity 
    {
        [Required]
        [RegularExpression(@"^-?[0-9]\d*(\.\d+)?$")]
        public double total {get; set;}
    }
}