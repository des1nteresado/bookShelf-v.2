using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FirstApp.Models
{
    public class Book
    {
        // ID книги
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        // название книги
        [Required(ErrorMessage = "Это поле должно быть установлено")]
        [StringLength(20, MinimumLength = 3)]
        [Display(Name = "Название")]
        public string Name { get; set; }
        // автор книги
        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }
        // цена
        [Required]
        [Display(Name = "Цена")]
        public int Price { get; set; }
    }
}