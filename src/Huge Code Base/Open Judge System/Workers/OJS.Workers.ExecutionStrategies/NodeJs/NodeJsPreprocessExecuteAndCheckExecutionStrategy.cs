﻿namespace OJS.Workers.ExecutionStrategies.NodeJs
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using OJS.Workers.Common;
    using OJS.Workers.Common.Extensions;
    using OJS.Workers.Common.Helpers;
    using OJS.Workers.ExecutionStrategies.Models;
    using OJS.Workers.Executors;

    using static OJS.Workers.ExecutionStrategies.NodeJs.NodeJsConstants;

    public class NodeJsPreprocessExecuteAndCheckExecutionStrategy : BaseInterpretedCodeExecutionStrategy
    {
        protected const string UserInputPlaceholder = "#userInput#";
        protected const string RequiredModules = "#requiredModule#";
        protected const string PreevaluationPlaceholder = "#preevaluationCode#";
        protected const string PostevaluationPlaceholder = "#postevaluationCode#";
        protected const string EvaluationPlaceholder = "#evaluationCode#";
        protected const string NodeDisablePlaceholder = "#nodeDisableCode";
        protected const string AdapterFunctionPlaceholder = "#adapterFunctionCode#";

        private const string DefaultAdapterFunctionCode = "(input, code) => code(input);";

        public NodeJsPreprocessExecuteAndCheckExecutionStrategy(
            IProcessExecutorFactory processExecutorFactory,
            string nodeJsExecutablePath,
            string underscoreModulePath,
            int baseTimeUsed,
            int baseMemoryUsed)
            : base(processExecutorFactory, baseTimeUsed, baseMemoryUsed)
        {
            if (!File.Exists(nodeJsExecutablePath))
            {
                throw new ArgumentException(
                    $"NodeJS not found in: {nodeJsExecutablePath}",
                    nameof(nodeJsExecutablePath));
            }

            if (!Directory.Exists(underscoreModulePath))
            {
                throw new ArgumentException(
                    $"Underscore not found in: {underscoreModulePath}",
                    nameof(underscoreModulePath));
            }

            if (baseTimeUsed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(baseTimeUsed));
            }

            if (baseMemoryUsed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(baseMemoryUsed));
            }

            this.NodeJsExecutablePath = nodeJsExecutablePath;
            this.UnderscoreModulePath = FileHelpers.ProcessModulePath(underscoreModulePath);
        }

        protected string NodeJsExecutablePath { get; }

        protected string UnderscoreModulePath { get; }

        protected virtual string JsCodeRequiredModules => $@"
var EOL = require('os').EOL,
_ = require('{this.UnderscoreModulePath}')";

        protected virtual string JsNodeDisableCode => @"
// DataView = undefined;
DTRACE_NET_SERVER_CONNECTION = undefined;
// DTRACE_NET_STREAM_END = undefined;
DTRACE_NET_SOCKET_READ = undefined;
DTRACE_NET_SOCKET_WRITE = undefined;
DTRACE_HTTP_SERVER_REQUEST = undefined;
DTRACE_HTTP_SERVER_RESPONSE = undefined;
DTRACE_HTTP_CLIENT_REQUEST = undefined;
DTRACE_HTTP_CLIENT_RESPONSE = undefined;
COUNTER_NET_SERVER_CONNECTION = undefined;
COUNTER_NET_SERVER_CONNECTION_CLOSE = undefined;
COUNTER_HTTP_SERVER_REQUEST = undefined;
COUNTER_HTTP_SERVER_RESPONSE = undefined;
COUNTER_HTTP_CLIENT_REQUEST = undefined;
COUNTER_HTTP_CLIENT_RESPONSE = undefined;
process.argv = undefined;
process.versions = undefined;
process.env = { NODE_DEBUG: false };
process.addListener = undefined;
process.EventEmitter = undefined;
process.mainModule = undefined;
process.config = undefined;
//process.removeListener = undefined;
// process.on = undefined;
process.openStdin = undefined;
process.chdir = undefined;
process.cwd = undefined;
process.exit = undefined;
process.umask = undefined;
// GLOBAL = undefined;
// root = undefined;
// global = {};
setInterval = undefined;
//setTimeout = undefined;
//clearTimeout = undefined;
clearInterval = undefined;
// setImmediate = undefined;
clearImmediate = undefined;
module = undefined;
require = undefined;
msg = undefined;

// delete DataView;
delete DTRACE_NET_SERVER_CONNECTION;
// delete DTRACE_NET_STREAM_END;
delete DTRACE_NET_SOCKET_READ;
delete DTRACE_NET_SOCKET_WRITE;
delete DTRACE_HTTP_SERVER_REQUEST;
delete DTRACE_HTTP_SERVER_RESPONSE;
delete DTRACE_HTTP_CLIENT_REQUEST;
delete DTRACE_HTTP_CLIENT_RESPONSE;
delete COUNTER_NET_SERVER_CONNECTION;
delete COUNTER_NET_SERVER_CONNECTION_CLOSE;
delete COUNTER_HTTP_SERVER_REQUEST;
delete COUNTER_HTTP_SERVER_RESPONSE;
delete COUNTER_HTTP_CLIENT_REQUEST;
delete COUNTER_HTTP_CLIENT_RESPONSE;
delete process.argv;
delete process.exit;
delete process.versions;
// delete GLOBAL;
// delete root;
delete setInterval;
//delete setTimeout;
//delete clearTimeout;
delete clearInterval;
// delete setImmediate;
delete clearImmediate;
delete module;
delete require;
delete msg;

process.exit = function () {};";

        protected virtual string JsCodePreevaulationCode => @"
let content = '';
let code = {
    run: " + UserInputPlaceholder + @"
};
let adapterFunction = " + AdapterFunctionPlaceholder;

        protected virtual string JsCodeEvaluation => @"
process.stdin.resume();
process.stdin.on('data', function(buf) { content += buf.toString(); });
process.stdin.on('end', function() {
    content = content.replace(new RegExp(EOL + '$'), '');
    let inputData = content.split(EOL);
    let result = adapterFunction(inputData, code.run);
    if (result !== undefined) {
        console.log(result);
    }
});";

        protected virtual string JsCodePostevaulationCode => string.Empty;

        protected virtual string JsCodeTemplate =>
            RequiredModules + @";" +
            NodeDisablePlaceholder +
            PreevaluationPlaceholder +
            EvaluationPlaceholder +
            PostevaluationPlaceholder;

        protected override IExecutionResult<TestResult> ExecuteAgainstTestsInput(
            IExecutionContext<TestsInputModel> executionContext,
            IExecutionResult<TestResult> result)
        {
            var codeSavePath = this.SaveCodeToTempFile(executionContext);

            var executor = this.CreateExecutor(ProcessExecutorType.Restricted);

            var checker = executionContext.Input.GetChecker();

            var testResults = this.ProcessTests(executionContext, executor, checker, codeSavePath);

            result.Results.AddRange(testResults);

            return result;
        }

        protected override IExecutionResult<OutputResult> ExecuteAgainstSimpleInput(
            IExecutionContext<string> executionContext,
            IExecutionResult<OutputResult> result)
        {
            var codeSavePath = this.SaveCodeToTempFile(executionContext);

            var executor = this.CreateExecutor(ProcessExecutorType.Restricted);

            var processExecutionResult = this.ExecuteCode(
                executionContext,
                executor,
                codeSavePath,
                executionContext.Input);

            result.Results.Add(this.GetOutputResult(processExecutionResult));

            return result;
        }

        protected virtual List<TestResult> ProcessTests(
            IExecutionContext<TestsInputModel> executionContext,
            IExecutor executor,
            IChecker checker,
            string codeSavePath)
        {
            var testResults = new List<TestResult>();

            foreach (var test in executionContext.Input.Tests)
            {
                var processExecutionResult = this.ExecuteCode(
                    executionContext,
                    executor,
                    codeSavePath,
                    test.Input);

                var testResult = this.CheckAndGetTestResult(
                    test,
                    processExecutionResult,
                    checker,
                    processExecutionResult.ReceivedOutput);

                testResults.Add(testResult);
            }

            return testResults;
        }

        protected virtual string PreprocessJsSubmission<TInput>(string template, IExecutionContext<TInput> context)
        {
            var problemSkeleton = (context.Input as TestsInputModel)?.TaskSkeletonAsString
                ?? DefaultAdapterFunctionCode;
            var code = context.Code.Trim(';');

            var processedCode = template
                .Replace(RequiredModules, this.JsCodeRequiredModules)
                .Replace(PreevaluationPlaceholder, this.JsCodePreevaulationCode)
                .Replace(EvaluationPlaceholder, this.JsCodeEvaluation)
                .Replace(PostevaluationPlaceholder, this.JsCodePostevaulationCode)
                .Replace(NodeDisablePlaceholder, this.JsNodeDisableCode)
                .Replace(UserInputPlaceholder, code)
                .Replace(AdapterFunctionPlaceholder, problemSkeleton);

            return processedCode;
        }

        protected override string SaveCodeToTempFile<TInput>(IExecutionContext<TInput> executionContext)
        {
            // Preprocess the user submission
            var codeToExecute = this.PreprocessJsSubmission(
                this.JsCodeTemplate,
                executionContext);

            // Save the preprocessed submission which is ready for execution
            return FileHelpers.SaveStringToTempFile(this.WorkingDirectory, codeToExecute);
        }

        private ProcessExecutionResult ExecuteCode<TInput>(
            IExecutionContext<TInput> executionContext,
            IExecutor executor,
            string codeSavePath,
            string input)
            => executor.Execute(
                this.NodeJsExecutablePath,
                input,
                executionContext.TimeLimit,
                executionContext.MemoryLimit,
                new[] { LatestEcmaScriptFeaturesEnabledFlag, codeSavePath });
    }
}
