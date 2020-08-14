using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public static class ModelBuilderExtensions
    {

        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
               new Student
               {
                   Id = 1,
                   Name = "耿家楷",
                   ClassName = ClassNameEnum.SecondGrade,
                   Email = "1392122452@qq.com",
               },


               new Student
               {
                   Id = 2,
                   Name = "耿加林",
                   ClassName = ClassNameEnum.SecondGrade,
                   Email = "1014283424@qq.com",
               }
                   );

        }
    }
}
