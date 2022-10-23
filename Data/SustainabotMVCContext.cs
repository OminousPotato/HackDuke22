using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SustainabotMVC.Models;

    public class SustainabotMVCContext : DbContext
    {
        public SustainabotMVCContext (DbContextOptions<SustainabotMVCContext> options)
            : base(options)
        {
        }

        public DbSet<SustainabotMVC.Models.ReviewImgFile> ReviewImgFile { get; set; } = default!;

        public DbSet<SustainabotMVC.Models.Review> Review { get; set; } = default!;
    }
