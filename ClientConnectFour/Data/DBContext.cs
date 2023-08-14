using ClientConnectFour.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConnectFour.Data
{
    public class DBContext : DbContext
    {
        public DBContext() : base("GameDatabase") { }

        public DbSet<GameView> GameViews { get; set; }
    }
}
