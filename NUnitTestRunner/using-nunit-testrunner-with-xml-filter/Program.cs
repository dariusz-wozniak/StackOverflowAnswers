using System;
using System.Reflection;
using NUnit.Engine;
using NUnit.Framework;

namespace NUnitTestRunner
{
    public class Class1
    {
        [Test] public void Test123() => Assert.Pass();
        [Test] public void Test456() => Assert.Pass();
    }

    /// <summary>
    /// https://stackoverflow.com/questions/48893789/using-nunit-testrunner-with-xml-filter
    /// </summary>
    public class Program
    {
        [TestCase("")]
        [Explicit]
        public static void Main(string[] args)
        {
            var path = Assembly.GetExecutingAssembly().Location;

            var package = new TestPackage(path);
            package.AddSetting("WorkDirectory", Environment.CurrentDirectory);

            using (ITestEngine engine = TestEngineActivator.CreateInstance())
            {
                var filterService = engine.Services.GetService<ITestFilterService>();

                var builder = filterService.GetTestFilterBuilder();
                builder.AddTest("NUnitTestRunner.Class1.Test123");
                builder.AddTest("NUnitTestRunner.Class1.Test456");
                var filter = builder.GetFilter();

                using (ITestRunner runner = engine.GetRunner(package))
                {
                    var result = runner.Run(listener: null, filter: filter);
                }
            }
        }
    }
}
