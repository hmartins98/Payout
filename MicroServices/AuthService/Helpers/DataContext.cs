﻿using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<UserAuthentication> UserAuthentications { get; set; }
    }
}
