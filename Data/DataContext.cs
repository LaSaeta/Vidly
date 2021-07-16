﻿using System.Data.Entity;
using Vidly.Models;

namespace Vidly.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("VidlyDataContext")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
    }
}