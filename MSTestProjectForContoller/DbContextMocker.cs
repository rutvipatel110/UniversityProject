using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Models;
using UniversityProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MSTestProjectForContoller
{
    public static  class DbContextMocker
    {
        public static ApplicationDbContext GetApplicationDbContext(string databaseName)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            var option = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            var dbContext = new ApplicationDbContext(option);

            dbContext.SeedDepartmentData();

            return dbContext;


        }
        internal static readonly Department[] SeedData_Departments =
            {
                new Department
                {
                    DepartmentId = 1,
                    DepartmentName = "English Department"
                },
                new Department
                {
                    DepartmentId = 2,
                    DepartmentName = "Maths Department"
                }
            };

        private static void SeedDepartmentData(this ApplicationDbContext context)
        {
            context.Departments.AddRange(SeedData_Departments);
            context.SaveChanges();
        }
    }
}
