using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace UniversityProject.Models
{
    public class MyIdentityUser
        :IdentityUser<Guid>
    {
        [Display(Name ="Display Name")]
        [Required]
        [MinLength(3)]
        [StringLength(60)]
        public string DisplayName { get; set; }    

        [Display(Name ="Date of Birth")]
        [Required]
        [PersonalData]
        [Column(TypeName ="smalldatetime")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name ="Is Admin User?")]
        [Required]
        public bool IsAdminUser { get; set; }


    }
}
