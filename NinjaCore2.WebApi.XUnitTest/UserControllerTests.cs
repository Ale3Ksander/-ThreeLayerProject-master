using Microsoft.AspNetCore.Mvc;
using Moq;
using NinjaCore2.Data.Entities;
using NinjaCore2.Domain.Services.Abstract;
using NinjaCore2.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace NinjaCore2.WebApi.XUnitTest
{
    public class UserControllerTests
    {
        [Fact]
        public void Get_RequestActionResultAllUsers_GetActionResAndAllUsers()
        {
            var mock = new Mock<IUserService>();
            mock.Setup(service => service.GetUserList()).Returns(ListUser());
            var controller = new UserController(mock.Object);

            var result = controller.Get();
               
            var actionResult = Assert.IsType<ActionResult<IEnumerable<User>>>(result);           
            Assert.Equal(ListUser().Count, actionResult.Value.Count());          
        }

        private List<User> ListUser()
        {
            var users = new List<User>
            {
                new User { Id = 1, FirstName = "Bil", LastName = "Bon", Email = "I.M@email.com", BirthDate = new DateTime(1954, 2, 5) },
                new User { Id = 2, FirstName = "Tim", LastName = "Tom", Email = "T.T12@email.com", BirthDate = new DateTime(1900, 4, 12) },
                new User { Id = 3, FirstName = "Youi", LastName = "Rrr", Email = "dfdf23@gmail.com", BirthDate = new DateTime(2000, 8, 25) }
            };
            return users;
        }

        [Fact]
        public void Get_MethotGetWithNullParam_ReturnedNotFound()
        {
            int testUserId = 1;
            var mock = new Mock<IUserService>();
            mock.Setup(service => service.GetUser(testUserId)).Returns(null as User);
            var controller = new UserController(mock.Object);

            var result = controller.NotFound();

            Assert.IsType<NotFoundResult>(result);

            #region код для IActionResult          
            //var mock = new Mock<IUserService>();            
            //var controller = new UserController(mock.Object);            
            //User user = null;

            //var result = controller.Get(user.Id);            

            //Assert.IsType<NotFoundResult>(result);
            #endregion
        }

        [Fact]
        public void Get_MethotGetWithParam_ReturnedUserById()
        {
            int testUserId = 1;
            var mock = new Mock<IUserService>();
            mock.Setup(service => service.GetUser(testUserId))
                .Returns(ListUser().FirstOrDefault(p => p.Id == testUserId));
            var controller = new UserController(mock.Object);

            var result = controller.Get(testUserId);
            result = ListUser().FirstOrDefault(p => p.Id == testUserId);

            Assert.IsType<ActionResult<User>>(result);
            Assert.Equal(testUserId, result.Value.Id);
            

            #region  код для IActionResult 
            //int testUserId = 1;
            //var mock = new Mock<IUserService>();            
            //var controller = new UserController(mock.Object);
            //var user = ListUser().FirstOrDefault(p => p.Id == testUserId);

            //var result = controller.Get(user.Id);
            ////result = user;

            //Assert.IsType<ObjectResult>(result);
            ////Assert.Equal(testUserId, );              
            #endregion
        }

        [Fact]
        public void Post_MethodPostWithNullParam_ReturnedBadRequest()
        {
            var mock = new Mock<IUserService>();
            var controller = new UserController(mock.Object);

            var result = controller.Post(null);

            Assert.IsType<BadRequestResult>(result);

            #region  код для ActionResult 
            //ActionResult
            //var mock = new Mock<IUserService>();            
            //var controller = new UserController(mock.Object);            

            //var result = controller.BadRequest();

            //Assert.IsType<BadRequestResult>(result);   
            #endregion
        }

        [Fact]
        public void Post_MethodPostWithParam_ReturnedOkObjectResult()
        {
            var mock = new Mock<IUserService>();
            var controller = new UserController(mock.Object);
            var user = new User() { Id = 4, FirstName = "Bob", LastName = "Booo", Email = "B.B@email.com", BirthDate = new DateTime(1960, 11, 14) };

            var result = controller.Post(user);

            mock.Verify(repo => repo.Create(user));
            Assert.IsType<OkObjectResult>(result);

            #region  код для ActionResult            
            //var mock = new Mock<IUserService>();
            //var controller = new UserController(mock.Object);
            //var user = new User() { Id = 4, FirstName = "Bob", LastName = "Booo", Email = "B.B@email.com", BirthDate = new DateTime(1960, 11, 14) };

            //var result = controller.Post(user);      

            //mock.Verify(repo => repo.Create(user));
            //Assert.IsType<ActionResult<User>>(result);
            #endregion
        }

        [Fact]
        public void Put_MethodPutWithNullParam_ReturnedBadRequest()
        {
            var mock = new Mock<IUserService>();
            var controller = new UserController(mock.Object);

            var result = controller.Put(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Put_MethodPutWithNotAnyParam_ReturnedNotFound()
        {
            var mock = new Mock<IUserService>();            
            var controller = new UserController(mock.Object);
            var user = new User() { Id = 5, FirstName = "Boyy", LastName = "Boouuo", Email = "B.Byy@email.com", BirthDate = new DateTime(1990, 11, 14) };

            var result = controller.Put(user);

            Assert.IsType<NotFoundResult>(result);
        }

        //Метод PUT который сделает Update
        //[Fact]
        //public void Put_MethodPutWithRightParam_ReturnedUpdateUser()
        //{
        //    int idUpdateUser = 1;
        //    var mock = new Mock<IUserService>();
        //    var controller = new UserController(mock.Object);
        //    var user = ListUser().FirstOrDefault(p => p.Id == idUpdateUser);

        //    var result = controller.Put(user);

        //    mock.Verify(repo => repo.Update(user));
        //    Assert.IsType<OkResult>(result);

        //    #region  код для ActionResult 
        //    //ActionResult
        //    //var user = new User() { Id = 7, FirstName = "Bouyy", LastName = "Boouppuo", Email = "B.By2y@email.com", BirthDate = new DateTime(1890, 11, 14) };
        //    //ListUser().Add(user);

        //    //int idUpdateUser = 1;
        //    //var mock = new Mock<IUserService>();
        //    //mock.Setup(repo => repo.Update(ListUser().FirstOrDefault(p => p.Id == idUpdateUser)));
        //    //var controller = new UserController(mock.Object);
        //    //var user = ListUser().FirstOrDefault(p => p.Id == idUpdateUser);

        //    //var updateUser = user;
        //    //updateUser.FirstName = "Boi";
        //    //var user = new User() { Id = 5, FirstName = "Boyy", LastName = "Boouuo", Email = "B.Byy@email.com", BirthDate = new DateTime(1990, 11, 14) };
        //    //var user = ListUser().Fi

        //    //var result = controller.Put(user);

        //    //mock.Verify(repo => repo.Update(user));
        //    //Assert.IsType<OkResult>(result);
        //    #endregion
        //}

        [Fact]
        public void Delete_MethodDeleteWithNullParam_ReturnedNotFound()
        {            
            var mock = new Mock<IUserService>();
            var controller = new UserController(mock.Object);
            User deleteUser = new User();
            
            var result = controller.Delete(deleteUser.Id);
            
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_MethodDeleteWithRigthParam_ReturnedOkObjectResult()
        {
            int idDeleteUser = 2;
            var mock = new Mock<IUserService>();
            mock.Setup(service => service.GetUser(idDeleteUser))
                .Returns(ListUser().FirstOrDefault(p => p.Id == idDeleteUser));
            var controller = new UserController(mock.Object);

            var result = controller.Delete(idDeleteUser);            

            mock.Verify(repo => repo.Delete(idDeleteUser));
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
