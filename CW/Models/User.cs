using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CW.Models
{
    public class User
    {
        // ID 
        public int Id { get; set; }
        // Фамилия Имя Отчество
        [Required]
        [Display(Name = "Name")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }
        // Логин
        [Required]
        [Display(Name = "Login")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Login { get; set; }
        // Пароль
        [Required]
        [Display(Name = "Password")]
        //[MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Password { get; set; }
        // Статус
        [Required]
        [Display(Name = "Role")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}