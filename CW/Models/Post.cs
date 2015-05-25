using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CW.Models
{
    public class Post
    {
        // ID 
        public int Id { get; set; }
        // Наименование поста
        [Required]
        [Display(Name = "Post name")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }
        // Описание поста
        [Required]
        [Display(Name = "Description")]
        [MaxLength(200, ErrorMessage = "Превышена максимальная длина записи")]
        public string Description { get; set; }
        // Статус поста
        [Display(Name = "Status")]
        public int Status { get; set; }
        // Файл картинки
        [Display(Name = "Picture file")]
        public string File { get; set; }
        
        // Внешний ключ Категория
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        // Внешний ключ Пользователь
        [Display(Name = "User")]
        public int? UserId { get; set; }
        public User User { get; set; }
        
        // Внешний ключ
        // ID жизненного цикла поста - обычное свойство
        public int LifecycleId { get; set; }
        // Ссылка на жизненный цикл поста - Навигационное свойство
        public Lifecycle Lifecycle { get; set; }
        
    }

    // Перечисление для статуса поста
    public enum PostStatus
    {
        Sent = 1,
        Approved = 2
    }
}