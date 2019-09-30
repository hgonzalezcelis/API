using System;
using System.Collections.Generic;
using System.Linq;
using API;
using API.Controllers;
using API.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestApi
{
    [TestClass]
    public class UnitTest1
    {
        List<User> expectedUser;
        Mock<IUser> mockUserRepository;
        UsersController userController;

        [TestInitialize]
        public void InitializeTestData()
        {
            //Setup test data
            expectedUser = GetExpectedUser();
            //Arrange
            mockUserRepository = new Mock<IUser>();
            userController = new UsersController(mockUserRepository.Object);

            //Setup
            mockUserRepository.Setup(m => m.Get()).Returns(expectedUser);

            mockUserRepository.Setup(m => m.Add(It.IsAny<User>())).Returns(
                (User target) =>
                {
                    expectedUser.Add(target);

                    return true;
                });

            mockUserRepository.Setup(m => m.Update(It.IsAny<User>())).Returns(
               (User target) =>
               {
                   var user = expectedUser.Where(p => p.Id == target.Id).FirstOrDefault();

                   if (user == null)
                   {
                       return false;
                   }

                   user.Name = target.Name;
                   user.LastName = target.LastName;
                   user.Address = target.Address;
                   user.CreateDate = target.CreateDate;
                   user.UpdateDate = target.UpdateDate;

                   return true;
               });

            mockUserRepository.Setup(m => m.Delete(It.IsAny<int>())).Returns(
               (int userId) =>
               {
                   var user = expectedUser.Where(p => p.Id == userId).FirstOrDefault();

                   if (user == null)
                   {
                       return false;
                   }

                   expectedUser.Remove(user);

                   return true;
               });
        }

        [TestMethod]
        public void Get_All_User()
        {
            //Act
            //var actualUser = mockUserRepository.Object.GetUser();
            var actualUser = userController.GetUsers();

            //Assert
            Assert.AreSame(expectedUser, actualUser);
        }
        [TestMethod]
        public void Add_User()
        {
            //int userCount = mockUserRepository.Object.GetUser().Count;
            int userCount = userController.GetUsers().Count;

            Assert.AreEqual(2, userCount);

            //Prepare
            User newUser = GetNewUser("N3", "C3", "12 Avenida");
            //Act
            //mockUserRepository.Object.AddUser(newUser);
            userController.AddUser(newUser);

            //userCount = mockUserRepository.Object.GetUser().Count;
            userCount = userController.GetUsers().Count;
            //Assert
            Assert.AreEqual(3, userCount);
        }
        [TestMethod]
        public void Update_User()
        {
            User user = new User()
            {
                Id = 2,
                Name = "N22",//Changed Name
                LastName = "P2",
                Address = "22"
            };

            //mockUserRepository.Object.UpdateUser(user);
            userController.UpdateUser(user);

            // Verify the change
            //Assert.AreEqual("N22", mockUserRepository.Object.GetUser()[1].Name);
            Assert.AreEqual("N22", userController.GetUsers()[1].Name);
        }
        [TestMethod]
        public void Delete_User()
        {
            //Assert.AreEqual(2, mockUserRepository.Object.GetUser().Count);
            Assert.AreEqual(2, userController.GetUsers().Count);

            //mockUserRepository.Object.Delete(1);
            userController.Delete(1);

            // Verify the change
            //Assert.AreEqual(1, mockUserRepository.Object.GetUser().Count);
            Assert.AreEqual(1, userController.GetUsers().Count);
        }

        [TestCleanup]
        public void CleanUpTestData()
        {
            expectedUser = null;
            mockUserRepository = null;
        }

        #region HelperMethods
        private static List<User> GetExpectedUser()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = 1,
                    Name = "N1",
                    LastName = "C1",
                    Address = "11"
                },
                new User()
                {
                    Id = 2,
                    Name = "N2",
                    LastName = "C2",
                    Address = "11"
                }
            };
        }
        private static User GetNewUser(string name, string lastName, String address)
        {
            return new User()
            {
                Name = name,
                LastName = lastName,
                Address = address
            };
        }
        #endregion
    }
}

