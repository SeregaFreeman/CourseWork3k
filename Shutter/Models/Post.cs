﻿using System;
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
        // Наименование поста
        [Required]
        [Display(Name = "Название поста")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }
        // Описание поста
        [Required]
        [Display(Name = "Описание")]
        [MaxLength(200, ErrorMessage = "Превышена максимальная длина записи")]
        public string Description { get; set; }
        // Комментарий к посту
        [Display(Name = "Комментарий")]
        [MaxLength(200, ErrorMessage = "Превышена максимальная длина записи")]
        public string Comment { get; set; }
        // Статус поста
        [Display(Name = "Status")]
        public int Status { get; set; }
        // Файл картинки
        [Display(Name = "Файл картинки")]
        public string File { get; set; }
        // Внешний ключ Категория
        [Display(Name = "Категория")]
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        // ID Пользователя - обычное свойство
        public int? UserId { get; set; }
        // Внешний ключ
        // ID жизненного цикла заявки - обычное свойство
        public int LifecycleId { get; set; }
        // Ссылка на жизненный цикл заявки - Навигационное свойство
        public Lifecycle Lifecycle { get; set; }
    }
    // Перечисление для статуса заявки
    public enum PostStatus
    {
        Posted = 1,
        Moderated = 2,
        Approved = 3
    }
}