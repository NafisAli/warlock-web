﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warlock.Models;

namespace Warlock.DataAccess.Repository.IRepository
{
    public interface IFactionRepository : IRepository<Faction>
    {
        void Update(Faction obj);
    }
}
