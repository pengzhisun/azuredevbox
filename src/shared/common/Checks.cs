// -----------------------------------------------------------------------
// <copyright file="Checks.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Winl.AzureDevBox
{
    using System.Diagnostics;
    using System.Threading;
    using Winl.AzureDevBox.Validations;

    public static class Checks
    {
        public static bool TraceEnabled { get; set; }

        public static IParameterCheck Parameter<TValue>(
            string name,
            TValue value)
        {
            Debug.Assert(
                !string.IsNullOrWhiteSpace(name),
                @"The parameter name shouldn't be null or empty or whitespace.");

            return ParameterCheckFactory.CreateInstance(name, value);
        }
    }
}