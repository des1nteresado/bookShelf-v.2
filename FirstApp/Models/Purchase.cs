using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FirstApp.Models
{
    public class Purchase
    {
        // ID покупки
        [HiddenInput(DisplayValue = false)]
        public int PurchaseId { get; set; }
        // имя и фамилия покупателя
        [Display(Name = "Инициалы")]
        public string Person { get; set; }
        // адрес покупателя
        [Display(Name = "Адрес")]
        public string Address { get; set; }
        // ID книги
        [HiddenInput(DisplayValue = false)]
        public int BookId { get; set; }
        // дата покупки
        [Display(Name = "Дата покупки")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}