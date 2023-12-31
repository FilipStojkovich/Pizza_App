﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaApp.DataAccess.DbContext
{
	public class PizzaAppDbContext : IdentityDbContext<User>
	{
        public PizzaAppDbContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Order>()
				.HasMany(o => o.Pizzas)
				.WithOne(o => o.Order)
				.HasForeignKey(p => p.OrderId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
