﻿namespace OJS.Workers.ExecutionStrategies
{
    using System;
    using System.Threading.Tasks;

    using log4net;

    using OJS.Workers.Common;
    using OJS.Workers.Common.Exceptions;
    using OJS.Workers.Common.Helpers;
    using OJS.Workers.ExecutionStrategies.Models;

    public abstract class BaseExecutionStrategy : IExecutionStrategy
    {
        private readonly ILog logger;

        protected BaseExecutionStrategy() => this.logger = LogManager.GetLogger(typeof(BaseExecutionStrategy));

        protected string WorkingDirectory { get; set; }

        public IExecutionResult<TResult> Execute<TInput, TResult>(IExecutionContext<TInput> executionContext)
            where TResult : ISingleCodeRunResult, new()
            => this.InternalExecute(executionContext, new ExecutionResult<TResult>());

        public IExecutionResult<TResult> SafeExecute<TInput, TResult>(IExecutionContext<TInput> executionContext)
            where TResult : ISingleCodeRunResult, new()
        {
            this.WorkingDirectory = DirectoryHelpers.CreateTempDirectoryForExecutionStrategy();

            try
            {
                return this.Execute<TInput, TResult>(executionContext);
                // Catch logic is handled by the caller
            }
            finally
            {
                // Use another thread for deletion of the working directory,
                // because we don't want the execution flow to wait for the clean up
                Task.Run(() =>
                {
                    try
                    {
                        DirectoryHelpers.SafeDeleteDirectory(this.WorkingDirectory, true);
                    }
                    catch (Exception ex)
                    {
                        // Sometimes deletion of the working directory cannot be done,
                        // because the process did not released it yet, which is nondeterministic.
                        // Problems in the deletion of leftover files should not break the execution flow,
                        // because the execution is already completed and results are generated.
                        // Only log the exception and continue.
                        this.logger.Error("executionStrategy.SafeDeleteDirectory has thrown an exception:", ex);
                    }
                });
            }
        }

        protected virtual IExecutionResult<OutputResult> ExecuteAgainstSimpleInput(
            IExecutionContext<string> executionContext,
            IExecutionResult<OutputResult> result)
            => throw new DerivedImplementationNotFoundException();

        protected virtual IExecutionResult<TestResult> ExecuteAgainstTestsInput(
            IExecutionContext<TestsInputModel> executionContext,
            IExecutionResult<TestResult> result)
            => throw new DerivedImplementationNotFoundException();

        protected virtual IExecutionResult<TResult> InternalExecute<TInput, TResult>(
            IExecutionContext<TInput> executionContext,
            IExecutionResult<TResult> result)
            where TResult : ISingleCodeRunResult, new()
        {
            if (executionContext is IExecutionContext<string> stringInputExecutionContext &&
                result is IExecutionResult<OutputResult> outputResult)
            {
                return (IExecutionResult<TResult>)this.ExecuteAgainstSimpleInput(
                    stringInputExecutionContext,
                    outputResult);
            }

            if (executionContext is IExecutionContext<TestsInputModel> testsExecutionContext &&
                result is IExecutionResult<TestResult> testsResult)
            {
                return (IExecutionResult<TResult>)this.ExecuteAgainstTestsInput(
                    testsExecutionContext,
                    testsResult);
            }

            throw new InvalidExecutionContextException<TInput, TResult>(executionContext, result);
        }
    }
}
