using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EHiHiBook.Models;
using EHiHiBook.Repository;
using System.Web.Mvc;

namespace EHiHiBook.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        SachEntities context = new SachEntities();

        public IEnumerable<Book> ListOfBooks { get; set; }

        public HomeIndexViewModel CreateModel()
        {
            //SqlParameter[] param = new SqlParameter[]{
            //    new SqlParameter("@search",search??(object)DBNull.Value)
            ////};
            //IEnumerable<Book> data = context.Database.SqlQuery<Book>().ToList();
            return new HomeIndexViewModel
            {
                ListOfBooks = _unitOfWork.GetRepositoryInstance<Book>().GetAllRecords()
            };
        }
    }
}