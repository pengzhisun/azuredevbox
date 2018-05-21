//------------------------------------------------------------------------------
// <copyright file="DemoBase.cs" company="Pengzhi Sun">
// Copyright (c) Pengzhi Sun. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace Winl.AzureDevBox.ConsoleDemo.Demos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Newtonsoft.Json;
    using Winl.AzureDevBox.Extensions;

    /// <summary>
    /// Defines the demo base class.
    /// </summary>
    /// <seealso cref="IDemo" />
    public abstract class DemoBase : IDemo
    {
        #region Fields

        /// <summary>
        /// The flag indicate whether enable synchronous demo.
        /// </summary>
        private readonly bool enableSyncDemo;

        /// <summary>
        /// The flag indicate whether enable asynchronous demo.
        /// </summary>
        private readonly bool enableAsyncDemo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DemoBase"/> class.
        /// </summary>
        /// <param name="enableSyncDemo">if set to <c>true</c> [enable synchronous demo].</param>
        /// <param name="enableAsyncDemo">if set to <c>true</c> [enable asynchronous demo].</param>
        protected DemoBase(bool enableSyncDemo = false, bool enableAsyncDemo = false)
        {
            this.enableSyncDemo = enableSyncDemo;
            this.enableAsyncDemo = enableAsyncDemo;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the demo name.
        /// </summary>
        /// <value>
        /// The demo name.
        /// </value>
        public string Name => this.GetType().Name;

        #endregion

        #region Methods

        #region Methods => Public Methods

        /// <summary>
        /// Runs the demo.
        /// </summary>
        public void Run()
        {
            this.PrintMetadata();

            if (this.enableSyncDemo)
            {
                PrintMessageBlock("Run synchronous demo");

                this.RunDemo(() => this.InternalRun());
            }

            if (this.enableAsyncDemo)
            {
                PrintMessageBlock("Run asynchronous demo");

                this.RunDemo(() =>
                    this.InternalRunAsync().ConfigureAwait(false).GetAwaiter().GetResult());
            }
        }

        #endregion

        #region Methods => Protected Methods

        /// <summary>
        /// The internal run synchronous demo.
        /// </summary>
        protected virtual void InternalRun()
        {
        }

        /// <summary>
        /// The internal run asynchronous demo.
        /// </summary>
        /// <returns>The <see cref="Task.CompletedTask"/> by default.</returns>
        protected virtual Task InternalRunAsync()
        {
            return Task.CompletedTask;
        }

        protected void PrintObjectInfo(object obj)
            => Console.WriteLine(
                $"{obj.GetType().Name}\r\n{JsonConvert.SerializeObject(obj, Formatting.Indented)}");


        #endregion

        #region Methods => Private Methods

        /// <summary>
        /// Prints the debug messages block.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <param name="blockChar">The block decorator character.</param>
        private static void PrintMessagesBlock(
            IEnumerable<string> messages,
            char blockChar = '*')
        {
            const int MessagesBlockWidth = 60;

            Console.WriteLine(new string(blockChar, MessagesBlockWidth));

            foreach (string message in messages)
            {
                Console.WriteLine($"{blockChar} {message}");
            }

            Console.WriteLine(new string(blockChar, MessagesBlockWidth));
        }

        /// <summary>
        /// Prints the debug message block.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="blockChar">The block decorator character.</param>
        private static void PrintMessageBlock(
            string message,
            char blockChar = '*')
        {
            string[] messages = new[]
            {
                message
            };

            PrintMessagesBlock(messages, blockChar);
        }

        /// <summary>
        /// Runs the demo.
        /// </summary>
        /// <param name="demoAction">The demo action.</param>
        private void RunDemo(Action demoAction)
        {
            try
            {
                demoAction();
            }
            catch (Exception ex)
            {
                ConsoleColor previousColor = Console.ForegroundColor;
                try
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.GetDetail());
                }
                finally
                {
                    Console.ForegroundColor = previousColor;
                }
            }
        }

        /// <summary>
        /// Prints the demo metadata.
        /// </summary>
        private void PrintMetadata()
        {
            Type demotype = this.GetType();
            List<string> metadataMessages = new List<string>
            {
                $"Run demo [{demotype.Name}]",
                $"\t{nameof(this.enableSyncDemo)}: {this.enableSyncDemo}",
                $"\t{nameof(this.enableAsyncDemo)}: {enableAsyncDemo}",
            };

            PrintMessagesBlock(messages: metadataMessages, blockChar: '=');
        }

        #endregion

        #endregion
    }
}
