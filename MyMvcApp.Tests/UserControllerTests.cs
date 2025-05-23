using Xunit;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MyMvcApp.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public void Index_ReturnsViewResult_WithUserList()
        {
            // Arrange
            var controller = new UserController();
            // Use reflection to clear and add to the static userlist
            var userListField = typeof(UserController).GetField("userlist", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic);
            var userList = userListField?.GetValue(null) as List<User>;
            userList?.Clear();
            userList?.Add(new User { Id = 1, Name = "Test User", Email = "test@example.com" });

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<User>>(viewResult.Model);
            Assert.Single(model);
            Assert.Equal("Test User", model[0].Name);
        }
    }
}
