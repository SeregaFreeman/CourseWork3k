using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shutter.Models
{
    public class Post
    {
        // ID 
        public int Id { get; set; }
        // Наименование заявки
        [Required]
        [Display(Name = "Название поста")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }
        // Описание
        [Required]
        [Display(Name = "Описание")]
        [MaxLength(200, ErrorMessage = "Превышена максимальная длина записи")]
        public string Description { get; set; }
        // Комментарий к заявке
        [Display(Name = "Комментарий")]
        [MaxLength(200, ErrorMessage = "Превышена максимальная длина записи")]
        public string Comment { get; set; }

        // Файл картинки
        //[Required]
        //file

        // Внешний ключ
        // ID Пользователя - обычное свойство
        public int? UserId { get; set; }
    }
}