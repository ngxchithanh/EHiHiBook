using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace EHiHiBook.Models
{
    public class CategoryDetail
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Category Name Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charaters are allowed", MinimumLength = 3)]
        public string CategoryName { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }

    public class BookDetail
    {
        public int BookID { get; set; }
        [Required(ErrorMessage = "Please enter Book nam")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charaters are allwed", MinimumLength = 3)]
        public string BookName { get; set; }
        [Required]
        [Range(1, 50)]
        public Nullable<int> CategoryID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }

        [Required(ErrorMessage = "Please enter descriptions")]
        public Nullable<System.DateTime> Descriptions { get; set; }
        public string Images { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity")]
        public Nullable<int> Quantity { get; set; }
        [Required]
        [Range(typeof(decimal), "1", "200000", ErrorMessage = "invalid Price")]
        public Nullable<decimal> Price { get; set; }
        public SelectList Categories { get; set; }
    }
}