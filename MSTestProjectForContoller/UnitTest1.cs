using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniversityProject.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using UniversityProject.Models;

namespace MSTestProjectForContoller
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task CheckIndexReturnsView()
        {
            using var dbContext = DbContextMocker.GetApplicationDbContext("TestMethod1");
            var controller = new DepartmentsController(dbContext);

            var actionResult = await controller.Index();

            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
        }
        [TestMethod]
        public async Task CheckIndexReturnsData()
        {
            using var dbContext = DbContextMocker.GetApplicationDbContext("TestMethod1");
            var controller = new DepartmentsController(dbContext);

            var actionResult = await controller.Index();

            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));

            var result = actionResult as ViewResult;
            if (!string.IsNullOrEmpty(result.ViewName))
            {
                Assert.AreEqual("Index",result.ViewName);

            }
            Assert.IsInstanceOfType(result.Model, typeof(List<Department>));

            var departments = result.Model as List<Department>;
            Assert.AreEqual<int>(
                DbContextMocker.SeedData_Departments.Length,
                departments.Count,
                "Number of Rows returned doesnot match !");

            int ndx=0;
            foreach(Department dept in departments)
            {
                var exceptedDept = DbContextMocker.SeedData_Departments[ndx];
                Assert.AreEqual<int>(exceptedDept.DepartmentId,
                    dept.DepartmentId,
                    $"Department Name doesnot match for seeded data for row #{ndx}");
                Assert.AreEqual<string>(exceptedDept.DepartmentName,
                    dept.DepartmentName,
                    $"Department Name doesnot match for seeded data for row #{ndx}");
                ndx++;

            }
        }
    }

}
