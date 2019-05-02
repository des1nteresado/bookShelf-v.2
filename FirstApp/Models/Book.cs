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
        [Display(Name = "Название")]
        public string Name { get; set; }
        // автор книги
        [Display(Name = "Автор")]
        public string Author { get; set; }
        // цена
        [Display(Name = "Цена")]
        public int Price { get; set; }
    }
}