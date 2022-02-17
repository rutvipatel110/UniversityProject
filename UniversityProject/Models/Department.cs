using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UniversityProject.Models
{
    [Table("Departments")]
    public class Department
    {
        [Display(Name ="Department ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int DepartmentId { get; set; }

        [Display(Name ="Display Name")]
        [Required]
        [StringLength(100)]
        [Column("varchar")]
        public string DepartmentName { get; set; }
    }
}
