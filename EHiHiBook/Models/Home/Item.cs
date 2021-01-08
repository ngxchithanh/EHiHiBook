using EHiHiBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHiHiBook.Models.Home
{
    public class Item
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}