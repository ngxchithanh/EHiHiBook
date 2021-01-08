﻿using EHiHiBook.Models;
using EHiHiBook.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EHiHiBook.Repository
{
    public class GenericUnitOfWork : IDisposable
    {
        private SachEntities DBEntity = new SachEntities();
        public IRepository<EntityType> GetRepositoryInstance<EntityType>() where EntityType : class
        {
            return new GenericRepository<EntityType>(DBEntity);
        }

        public void SaveChanges()
        {
            DBEntity.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DBEntity.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

    }
}