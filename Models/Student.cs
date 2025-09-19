using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Имя студента обязательно для заполнения")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно содержать от 2 до 50 символов")]
        [Display(Name = "Имя студента")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Возраст обязателен для заполнения")]
        [Range(16, 100, ErrorMessage = "Возраст должен быть от 16 до 100 лет")]
        [Display(Name = "Возраст")]
        public int Age { get; set; }
        
        [Required(ErrorMessage = "Группа обязательна для заполнения")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Группа должна содержать от 2 до 20 символов")]
        [Display(Name = "Группа")]
        public string Group { get; set; }
    }
}
