namespace MyTestRunner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Framework;

    public class TestRunner
    {
        private static int totalTests;
        private static int passingTests;
        private static int failedTests;
        private static int testErrors;

        public static ITestReporter TestReporter { get; set; } = new ConsoleReporter();

        public static void ExecuteTests(params Type[] types)
        {
            var assemblies = types.Select(t => t.Assembly).ToArray();

            var testsBySubject = FindTests(assemblies)
                .GroupBy(t => t
                    .GetCustomAttribute<SubjectAttribute>()
                    .Name);

            TestReporter.ReportLine("Running tests...");

            foreach (var tests in testsBySubject)
            {
                var testSubject = tests.Key;
                TestReporter.ReportLine($"--Running tests for '{testSubject}'...");

                foreach (var test in tests)
                {
                    var testComponents = test.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);

                    var givenComponents = GetTestComponents(testComponents, typeof(Given));
                    var becauseComponents = GetTestComponents(testComponents, typeof(Because));
                    var itComponents = GetTestComponents(testComponents, typeof(It));

                    var testInstance = Activator.CreateInstance(test);

                    try
                    {
                        RunGivenComponents(givenComponents, testInstance);
                        RunBecauseComponents(becauseComponents, testInstance);
                        RunItComponents(itComponents, testInstance, test.Name);
                    }
                    catch (Exception exception)
                    {
                        TestReporter.ReportLine();
                        TestReporter.ReportLine("Unhandled exception in test code!");
                        TestReporter.ReportLine(exception.ToString());
                        TestReporter.ReportLine();

                        testErrors++;
                    }
                }
            }

            TestReporter.ReportLine();
            TestReporter.ReportLine(new string('-', 50));
            TestReporter.ReportLine($"Total Tests: {totalTests}");
            TestReporter.ReportLine($"Passing Tests: {passingTests}");
            TestReporter.ReportLine($"Failed Tests: {failedTests}");

            if (testErrors > 0)
            {
                TestReporter.ReportLine($"Tests With Errors: {testErrors}");
            }
        }

        private static IEnumerable<Type> FindTests(params Assembly[] assemblies)
            => assemblies
                .SelectMany(a => a.ExportedTypes)
                .Where(t => t.IsDefined(typeof(SubjectAttribute)))
                .ToList();

        private static List<FieldInfo> GetTestComponents(
            IEnumerable<FieldInfo> fields, 
            Type typeOfComponent)
        {
            var components = fields
                .Where(f => f.FieldType == typeOfComponent)
                .ToList();

            if (!components.Any())
            {
                throw new InvalidOperationException($"Test does not contain {typeOfComponent.Name} specification.");
            }

            return components;
        }

        private static void RunGivenComponents(
            IEnumerable<FieldInfo> testComponents, 
            object test)
        {
            var values = GetComponents<Given>(testComponents, test);

            foreach (var value in values)
            {
                value.Invoke();
            }
        }

        private static void RunBecauseComponents(
            IEnumerable<FieldInfo> testComponents,
            object test)
        {
            var values = GetComponents<Because>(testComponents, test);

            foreach (var value in values)
            {
                value.Invoke();
            }
        }

        private static void RunItComponents(
            IEnumerable<FieldInfo> testComponents,
            object test,
            string testPrefix)
        {
            var its = testComponents
                .Select(tc => new
                {
                    Name = tc.Name,
                    Value = (It)tc.GetValue(test)
                });

            foreach (var it in its)
            {
                totalTests++;

                TestReporter.Report($"----Running {testPrefix}It{it.Name.Capitalize()} - ");

                try
                {
                    it.Value.Invoke();
                    TestReporter.ReportLine("Passing");
                    passingTests++;
                }
                catch (Exception exception)
                {
                    TestReporter.ReportLine("Failed");
                    TestReporter.ReportLine();
                    TestReporter.ReportLine($"Exception Message: {exception.Message}");
                    TestReporter.ReportLine();
                    failedTests++;
                }
            }
        }

        private static IEnumerable<TComponent> GetComponents<TComponent>(
            IEnumerable<FieldInfo> testComponents,
            object test)
            => testComponents
                .Select(tc => tc.GetValue(test))
                .Cast<TComponent>();
    }
}
