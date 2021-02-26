using Microsoft.EntityFrameworkCore;
using Pointwest.Test.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pointwest.Test.DatabaseContext
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Applicant>().HasData(
                new Applicant
                {
                    //Id = 1,
                    Name = "Peter John Realisan Hugo",
                    Email = "johnayossulit@gmail.com",
                    Mobile = "09274862199",
                    PositionApplied = "Developer / API Developer",
                    Source = "Pointwest"
                }
                );
        }
    }
}
