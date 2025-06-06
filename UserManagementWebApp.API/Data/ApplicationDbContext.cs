﻿using Microsoft.EntityFrameworkCore;
using UserManagementWebApp.API.Models.Domain;

namespace UserManagementWebApp.API.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<Permission> Permissions { get; set; }  
        public DbSet<Role> Roles { get; set; }
        public DbSet<User>  Users { get; set; }
    }
}
