using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Marginean_Daria_Lab2.Models;

namespace Marginean_Daria_Lab2.Data
{
    public class Marginean_Daria_Lab2Context : DbContext
    {
        public Marginean_Daria_Lab2Context (DbContextOptions<Marginean_Daria_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<Marginean_Daria_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<Marginean_Daria_Lab2.Models.Publisher> Publisher { get; set; } = default!;
    }
}