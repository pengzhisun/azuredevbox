//------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Winl.AzureDevBox.ConsoleDemo
{
    using System;
    using System.Diagnostics;
    using Winl.AzureDevBox.ConsoleDemo.Demos;

    /// <summary>
    /// Defines the console demo application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// The main entry point.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        public static void Main(string[] args)
        {
            RunDemo<ChecksDemo>();
        }

        /// <summary>
        /// Runs the demo.
        /// </summary>
        /// <typeparam name="TDemo">The type of the demo.</typeparam>
        private static void RunDemo<TDemo>()
            where TDemo : class, IDemo
        {
            TDemo demo = Activator.CreateInstance<TDemo>();
            demo.Run();
        }
    }
}
