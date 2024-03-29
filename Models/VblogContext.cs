﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using MyFirstBlog.Models;

namespace MyFirstBlog.Models
{
    public class VblogContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RegisterUser> RegisterUsers { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Article> Articles { get; set; }
    }
}