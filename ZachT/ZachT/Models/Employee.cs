using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ZachT.Models
{
    public class Employee
    {
        [DisplayName("Employee ID")]
        [Required(ErrorMessage ="ID cannot empty")]
        [StringLength(7, ErrorMessage ="ID need 4 to 7 character", MinimumLength =4)]
        public int fEmpId { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name cannot empty")]
        public string fName { get; set; }

        [DisplayName("Gender")]
        public string fGender { get; set; }

        [DisplayName("Email")]
        [EmailAddress(ErrorMessage = "Email Format is wrong")]
        public string fMail { get; set; }

        [DisplayName("Salary")]
        [Range(23000,65000,ErrorMessage = "Salary")]
        public Nullable<int> fSalary { get; set; }

        [DisplayName("Employment Date")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> fEmploymentData { get; set; }

    }
}