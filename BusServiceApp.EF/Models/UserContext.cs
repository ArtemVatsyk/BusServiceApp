using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusServiceApp.EF.Models
{
    class UserContext : DbContext
    {
        private string _connectionString;

        public DbSet<User> Users { get; set; }

        public UserContext(string connectionString)
        {
            _connectionString = connectionString;
        }


    }
}
