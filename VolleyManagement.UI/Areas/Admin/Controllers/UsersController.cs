﻿namespace VolleyManagement.UI.Areas.Admin.Controllers
{
    using System;
    using System.Web.Mvc;
    using Contracts;
    using Contracts.Exceptions;
    using Models;

    /// <summary>
    /// Provides User management
    /// </summary>
    public class UsersController : Controller
    {
        private const string URL_ADMIN_USERS = "https://localhost:44300/Admin/Users";
        private const string URL_ACTIVE_USERS = "https://localhost:44300/Admin/Users/ActiveUsers";
        private readonly IUserService _userService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersController"/> class.
        /// </summary>
        /// <param name="userService">User service</param>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all user list view.
        /// </summary>
        /// <returns> The <see cref="ActionResult"/>. </returns>
        public ActionResult Index()
        {
            var users = _userService.GetAllUsers().ConvertAll(UserViewModel.Initialize);
            return View(users);
        }

        /// <summary>
        /// Get all active user list view.
        /// </summary>
        /// <returns> The <see cref="ActionResult"/>. </returns>
        public ActionResult ActiveUsers()
        {
            var activeUsers = _userService.GetAllActiveUsers().ConvertAll(UserViewModel.Initialize);
            return View(activeUsers);
        }

        /// <summary>
        /// Get user's details view.
        /// </summary>
        /// <param name="id"> User Id. </param>
        /// <returns> The <see cref="ActionResult"/>. </returns>
        public ActionResult UserDetails(int id)
        {
            var user = _userService.GetUserDetails(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var result = UserViewModel.Map(user);

            return View(result);
        }

        /// <summary>
        /// Get user's details view.
        /// </summary>
        /// <param name="id"> User Id. </param>
        /// <param name="toBlock">block or unblock user</param>
        /// <param name="backTo"> return to url where button click </param>
        /// <returns> The <see cref="ActionResult"/>. </returns>
        public ActionResult ChangeUserBlocked(int id, bool toBlock, string backTo)
        {
            var user = _userService.GetUserDetails(id);
            var users = _userService.GetAllUsers().ConvertAll(UserViewModel.Initialize);
            var result = string.Empty;

            if (user == null)
            {
                return HttpNotFound();
            }

            try
            {
                _userService.ChangeUserBlocked(id, toBlock);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("ValidationError", ex.Message);
            }
            catch (MissingEntityException ex)
            {
                ModelState.AddModelError("ValidationError", ex.Message);
            }

            switch (backTo)
            {
                case URL_ADMIN_USERS:
                    result = "Index";
                    break;
                case URL_ACTIVE_USERS:
                    result = "ActiveUsers";
                    break;
                default:
                    result = "Index";
                    break;
            }

            return View(result, users);
        }
    }
}