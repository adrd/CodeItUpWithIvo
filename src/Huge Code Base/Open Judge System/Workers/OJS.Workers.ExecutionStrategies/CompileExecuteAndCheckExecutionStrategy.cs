﻿namespace OJS.Workers.ExecutionStrategies
{
    using System;

    using OJS.Workers.Common;
    using OJS.Workers.Common.Models;
    using OJS.Workers.ExecutionStrategies.Models;
    using OJS.Workers.Executors;

    public class CompileExecuteAndCheckExecutionStrategy : BaseCompiledCodeExecutionStrategy
    {
        public CompileExecuteAndCheckExecutionStrategy(
            Func<CompilerType, string> getCompilerPathFunc,
            IProcessExecutorFactory processExecutorFactory,
            int baseTimeUsed,
            int baseMemoryUsed)
            : base(processExecutorFactory, baseTimeUsed, baseMemoryUsed)
            => this.GetCompilerPathFunc = getCompilerPathFunc;

        protected Func<CompilerType, string> GetCompilerPathFunc { get; }

        protected override IExecutionResult<TestResult> ExecuteAgainstTestsInput(
            IExecutionContext<TestsInputModel> executionContext,
            IExecutionResult<TestResult> result)
            => this.CompileExecuteAndCheck(
                executionContext,
                result,
                this.GetCompilerPathFunc,
                this.CreateExecutor(ProcessExecutorType.Restricted));

        protected override IExecutionResult<OutputResult> ExecuteAgainstSimpleInput(
            IExecutionContext<string> executionContext,
            IExecutionResult<OutputResult> result)
        {
            var compileResult = this.ExecuteCompiling(
                executionContext,
                this.GetCompilerPathFunc,
                result);

            if (!compileResult.IsCompiledSuccessfully)
            {
                return result;
            }

            var executor = this.CreateExecutor(ProcessExecutorType.Restricted);

            var processExecutionResult = executor.Execute(
                compileResult.OutputFile,
                executionContext.Input ?? string.Empty,
                executionContext.TimeLimit,
                executionContext.MemoryLimit,
                null,
                this.WorkingDirectory);

            result.Results.Add(this.GetOutputResult(processExecutionResult));

            return result;
        }
    }
}
