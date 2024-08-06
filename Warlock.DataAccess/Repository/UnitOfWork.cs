﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warlock.DataAccess.Data;
using Warlock.DataAccess.Repository.IRepository;

namespace Warlock.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public IFactionRepository Faction { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Faction = new FactionRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
