using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Warlock.DataAccess.Data;
using Warlock.DataAccess.Repository.IRepository;
using Warlock.Models;

namespace Warlock.DataAccess.Repository
{
    public class FactionRepository : Repository<Faction>, IFactionRepository
    {
        private ApplicationDbContext _db;

        public FactionRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public void Update(Faction obj)
        {
            _db.Factions.Update(obj);
        }
    }
}
