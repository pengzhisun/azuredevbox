//------------------------------------------------------------------------------
// <copyright file="ChecksDemo.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Winl.AzureDevBox.ConsoleDemo.Demos
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Winl.AzureDevBox.Validation;

    /// <summary>
    /// Defines the Checks demo;
    /// </summary>
    /// <seealso cref="DemoBase" />
    internal sealed class ChecksDemo : DemoBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ChecksDemo"/> class.
        /// </summary>
        public ChecksDemo()
            : base(enableSyncDemo: true, enableAsyncDemo: false)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// The internal run synchronous demo.
        /// </summary>
        protected override void InternalRun()
        {
            Checks.TraceEnabled = true;

            RunNotNullOrEmptyOrWhitespaceDemos();
        }

        private static void RunNotNullOrEmptyOrWhitespaceDemos()
        {
            RunCheckDemos(
                "dummy_str_param",
                new string[]
                {
                    null,
                    string.Empty,
                    "   ",
                    "dummy_str_value",
                },
                ck => ck.NotNullOrEmptyOrWhitespace());

            RunCheckDemos(
                "dummy_obj_param",
                new object[]
                {
                    123,
                    false,
                    DateTime.Now
                },
                ck => ck.NotNullOrEmptyOrWhitespace());
        }

        private static void RunCheckDemos<TValue>(
            string paramName,
            IEnumerable<TValue> paramValues,
            Action<IParameterCheck> checkAction)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            foreach (TValue paramValue in paramValues)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"> Checking parameter '{paramName}' with value: '{paramValue}'");

                try
                {
                    IParameterCheck checkInstance =
                        Checks.Parameter(paramName, paramValue);
                    checkAction.Invoke(checkInstance);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                }
            }

            Console.ForegroundColor = originalColor;
        }

        #endregion
    }
}
