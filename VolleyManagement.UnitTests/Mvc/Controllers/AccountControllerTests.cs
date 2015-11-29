﻿namespace VolleyManagement.UnitTests.Mvc.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using Comparers;
    using Contracts.Authentication;
    using Contracts.Authentication.Models;
    using Contracts.Authorization;
    using Domain.RolesAggregate;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Ninject;

    using Services.UserManager;
    using UI.Areas.Mvc.Controllers;
    using UI.Areas.Mvc.ViewModels.Users;
    using UI.Infrastructure;
    using ViewModels;

    [ExcludeFromCodeCoverage]
    [TestClass]
    public class AccountControllerTests
    {
        #region Consts

        private const int USER_ID = 2;
        private const string USER_NAME = "Jack";

        private readonly Role _adminRole = new Role() { Id = 1, Name = "Admin" };

        #endregion

        #region Fields

        private IKernel _kernel;

        private Mock<IVolleyUserManager<UserModel>> _userManagerMock = new Mock<IVolleyUserManager<UserModel>>();

        private Mock<IRolesService> _rolesServiceMock = new Mock<IRolesService>();

        private Mock<VolleyExceptionFilterAttribute> _exceptionFilter = new Mock<VolleyExceptionFilterAttribute>();

        private ControllerContext _controllerContext;

        #endregion

        #region Init

        [TestInitialize]
        public void TestInit()
        {
            _kernel = new StandardKernel();

            this._kernel.Bind<IVolleyUserManager<UserModel>>()
                   .ToConstant(this._userManagerMock.Object);
            this._kernel.Bind<IRolesService>()
                   .ToConstant(this._rolesServiceMock.Object);

            _controllerContext = GetControllerContext();
        }

        #endregion

        #region Tests

        /// <summary>
        /// Test for Details()
        /// </summary>
        [TestMethod]
        public void Details_UserExists_UserIsReturned()
        {
            // Arrange
            CustomFindByIdDefault();
            CustomRolesService();

            var sut = this._kernel.Get<AccountController>();
            sut.ControllerContext = _controllerContext;

            var expected = new UserMvcViewModelBuilder()
                    .WithId(USER_ID)
                    .WithUserName(USER_NAME)
                    .Build();

            // Act
            var actual = TestExtensions.GetModelAsync<UserViewModel>(sut.Details());

            // Assert
            TestHelper.AreEqual<UserViewModel>(expected, actual, new UserViewModelComparer());
        }

        /// <summary>
        /// Test for Edit()
        /// </summary>
        /// <returns>Asynchronous operation</returns>
        [TestMethod]
        public async Task EditPostAction_UserExists_UserUpdated()
        {
            // Arrange
            CustomFindByIdDefault();
            CustomRolesService();

            var userEditViewModel = new UserEditMvcViewModelBuilder()
                .WithId(USER_ID)
                .WithCellPhone("068-33-44-555")
                .WithEmail("example@ex.ua")
                .WithName("Vasya Petichkin")
                .Build();

            var sut = this._kernel.Get<AccountController>();
            sut.ControllerContext = _controllerContext;

            // Act
            await sut.Edit(userEditViewModel);

            // Assert
            _userManagerMock.Verify(um => um.UpdateAsync(It.IsAny<UserModel>()), Times.Once());
        }

        /// <summary>
        /// Test() edit method.
        /// </summary>
        /// <returns>Asynchronous operation</returns>
        [TestMethod]
        public async Task EditPostAction_UserIdPassed_ExceptionThrown()
        {
            // Arrange
            _userManagerMock.Setup(um => um.FindByIdAsync(USER_ID))
                            .Returns(Task.FromResult<UserModel>(null));
            CustomRolesService();

            var userEditViewModel = new UserEditMvcViewModelBuilder()
                .Build();

            var sut = this._kernel.Get<AccountController>();
            sut.ControllerContext = _controllerContext;

            // Act
            await sut.Edit(userEditViewModel);

            // Assert
            _userManagerMock.Verify(um => um.UpdateAsync(It.IsAny<UserModel>()), Times.Never());
        }

        #endregion

        private ControllerContext GetControllerContext()
        {
            var claim = new Claim("id", USER_ID.ToString());
            var identityMock = Mock.Of<ClaimsIdentity>(ci => ci.FindFirst(It.IsAny<string>()) == claim);
            var mockContext = Mock.Of<ControllerContext>(cc => cc.HttpContext.User.Identity == identityMock);
            return mockContext;
        }

        private void CustomFindByIdDefault()
        {
            _userManagerMock.Setup(um => um.FindByIdAsync(USER_ID))
                            .Returns(Task.FromResult(
                                new UserModelBuilder()
                                .WithId(USER_ID)
                                .WithUserName(USER_NAME)
                                .Build()));
        }

        private void CustomRolesService()
        {
            _rolesServiceMock.Setup(rs => rs.GetRole(It.IsAny<int>()))
                .Returns(_adminRole);
        }
    }
}