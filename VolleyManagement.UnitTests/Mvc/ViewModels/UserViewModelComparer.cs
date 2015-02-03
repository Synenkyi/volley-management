﻿namespace VolleyManagement.UnitTests.Mvc.ViewModels
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using VolleyManagement.Domain.Users;
    using VolleyManagement.Mvc.ViewModels.Users;

    /// <summary>
    /// Comparer for user objects.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class UserViewModelComparer : IComparer<UserViewModel>, IComparer
    {
        /// <summary>
        /// Compares two user objects.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A signed integer that indicates the relative values of users.</returns>
        public int Compare(UserViewModel x, UserViewModel y)
        {
            return AreEqual(x, y) ? 0 : 1;
        }

        /// <summary>
        /// Compares two user objects (non-generic implementation).
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>A signed integer that indicates the relative values of users.</returns>
        public int Compare(object x, object y)
        {
            UserViewModel firstUser = x as UserViewModel;
            UserViewModel secondUser = y as UserViewModel;

            if (firstUser == null)
            {
                return -1;
            }
            else if (secondUser == null)
            {
                return 1;
            }

            return Compare(firstUser, secondUser);
        }

        /// <summary>
        /// Finds out whether two user objects have the same properties.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns>True if given users have the same properties.</returns>
        private bool AreEqual(UserViewModel x, UserViewModel y)
        {
            return x.Id == y.Id &&
                x.FullName == y.FullName &&
                x.UserName == y.UserName &&
                x.Password == y.Password &&
                x.Email == y.Email &&
                x.CellPhone == y.CellPhone;
        }
    }
}
