using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniversityProject.Controllers;

namespace MSTestProjectForContoller
{
    [TestClass]
    public class TestDepartmentEdit
    {
        [TestMethod]
        public async Task CheckEditReturnsView()
        {
            using var dbContext = DbContextMocker.GetApplicationDbContext(nameof(CheckEditReturnsView));
            var controller = new DepartmentsController(dbContext);
            int editDeptId = DbContextMocker.SeedData_Departments[0].DepartmentId;
            var actionResult = await controller.Edit(editDeptId);
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
            var viewResult = actionResult as ViewResult;

            if (!string.IsNullOrEmpty(viewResult.ViewName))
            {
                Assert.AreEqual("Edit", viewResult.ViewName);
            }
            Assert.IsInstanceOfType(viewResult.Model,typeof(Department));
            var editDept = viewResult.Model as Department;
            Assert.IsNotNull(editDept);
            var expectedDept = DbContextMocker.SeedData_Departments
                .SingleOrDefault(e=> e.DepartmentId == editDeptId);
            Assert.AreEqual<int>(expectedDept.DepartmentId, editDept.DepartmentId,
                $"Department Id does not match ");
            Assert.AreEqual<string>(expectedDept.DepartmentName, expectedDept.DepartmentName,
                $"Department name does not match");
        }
        [TestMethod]
        public async Task CheckEditUpdateOk()
        {
            using var dbContext = DbContextMocker.GetApplicationDbContext(nameof(CheckEditUpdateOk));
            var controller = new DepartmentsController(dbContext);
            int editDeptId = DbContextMocker.SeedData_Departments[0].DepartmentId;
            var editDeptName = DbContextMocker.SeedData_Departments[0].DepartmentName;
            var changedDeptName = editDeptName.ToUpper();

            var actionResult = await controller.Edit(editDeptId);
            var editDept = (actionResult as ViewResult).Model as Department;
            editDept.DepartmentName = changedDeptName;
            actionResult = await controller.Edit(editDeptId,editDept);
            Assert.IsInstanceOfType(actionResult, typeof(RedirectToActionResult));

            Assert.AreEqual("Index", (actionResult as RedirectToActionResult).ActionName);

        }
        [TestMethod]
        public async Task CheckEditUpdatesWithInjectedModelStateError()
        {
            using var dbContext = DbContextMocker.GetApplicationDbContext("CheckEditUpdatesWithInjectedModelStateError");
            var controller = new DepartmentsController(dbContext);
            int editDeptId = DbContextMocker.SeedData_Departments[0].DepartmentId;
            var editDeptName = DbContextMocker.SeedData_Departments[0].DepartmentName;
            var changedDeptName = editDeptName.ToUpper();
            var actionResultEditGet = await controller.Edit(editDeptId);
             var editDept = (actionResultEditGet as ViewResult).Model as Department;
            editDept.DepartmentName= changedDeptName;
            controller.ModelState.AddModelError("DepartmentName", "Duplicate Department");
            var actionResultEditPost = await controller.Edit(editDeptId, editDept);
            Assert.IsInstanceOfType(actionResultEditPost, typeof(ActionResult));

            var actionResult = actionResultEditPost as ViewResult;
            if (!string.IsNullOrEmpty(actionResult.ViewName))
            {
                Assert.AreEqual("Edit",actionResult.ViewName);
            }
            Assert.IsInstanceOfType(actionResult.Model,typeof(Department));
            var modelstate = actionResult.ViewData.ModelState;
            Assert.AreEqual<bool>(false, modelstate.IsValid, "Model state doesnot report any error");
            Assert.IsTrue(modelstate.ContainsKey("DepartmentName"));
            Assert.AreEqual<string>("Duplicate Department", modelstate["TestError"].Errors[0].ErrorMessage,
                "Model Error for [TestError] is not the same as what was injected");

        }

    }
}
