﻿namespace VolleyManagement.UnitTests
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using VolleyManagement.UnitTests.Mvc.Comparers;

    /// <summary>
    /// Class for custom asserts.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class TestHelper
    {
        private const string COLLECTION_IS_NULL_MESSAGE = "One of the collections is null.";
        private const string COLLECTIONS_COUNT_UNEQUAL_MESSAGE = "Number of items in collections should match.";
        
        /// <summary>
        /// Test equals of two objects with specific comparer.
        /// </summary>
        /// <typeparam name="T">Type of object.</typeparam>
        /// <param name="expected">Expected result</param>
        /// <param name="actual">Actual result</param>
        /// <param name="comparer">Specific comparer</param>
        public static void AreEqual<T>(T expected, T actual, IComparer<T> comparer)
        {
            int equalsResult = 0;
            int compareResult = comparer.Compare(expected, actual);

            Assert.AreEqual(equalsResult, compareResult);
        }
        
        public static void AreEqual<T>(ICollection<T> expected, ICollection<T> actual, IComparer<T> comparer) =>
            AreEqual(expected, actual, comparer, string.Empty);

        public static void AreEqual<T>(ICollection<T> expected, ICollection<T> actual, string message) =>
            AreEqual(expected, actual, null, message);

        public static void AreEqual<T>(ICollection<T> expected, ICollection<T> actual, IComparer<T> comparer, string message)
        {
            if (expected == null || actual == null)
            {
                Assert.Fail(COLLECTION_IS_NULL_MESSAGE);
            }

            Assert.AreEqual(expected.Count, actual.Count, COLLECTIONS_COUNT_UNEQUAL_MESSAGE);

            using (var expectedEnumerator = expected.GetEnumerator())
            using (var actualEnumerator = actual.GetEnumerator())
            {
                string preparedErrorMessage;

                while (expectedEnumerator.MoveNext() && actualEnumerator.MoveNext())
                {
                    preparedErrorMessage = !string.IsNullOrEmpty(message) ? message
                        : $"[Item#{expectedEnumerator.Current.ToString()}] ";

                    if (comparer == null)
                    {
                        Assert.AreEqual(expectedEnumerator.Current, 
                            actualEnumerator.Current, 
                            preparedErrorMessage);
                    }
                    else
                    {
                        Assert.IsTrue(
                            comparer.Compare(expectedEnumerator.Current, actualEnumerator.Current) == 0,
                            preparedErrorMessage);
                    }
                }
            }
        }
    }
}
