﻿namespace VolleyManagement.Contracts.Authentication
{
    using Microsoft.AspNet.Identity;

    using Models;

    /// <summary>
    /// Stores Volley Users data
    /// </summary>
    public interface IVolleyUserStore
        : IUserStore<UserModel, int>,
          IUserEmailStore<UserModel, int>,
          IUserLoginStore<UserModel, int>
    {
    }
}