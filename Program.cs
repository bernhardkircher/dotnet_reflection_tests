using System;
using System.Diagnostics;
using System.Linq;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome - this is a test application to  test performace of Reflection and other options.");

            // this is totally unscientific, 
            // * we do not use an accurate timer/stopwatch
            // * we do not do a warmup
            // * some tests do not make sence, since e.g. in reflection, we already know the instance and type,.... so we do the same thing 3 times (via reflection).
            // * we run it in debug mode
            // * ...
            
            // we have 10 executions ,per execution we call Init() to check how long it takes to get the reuied cached data, depending on implementation.
            // per iteration we: 
            // 1. create a new instance
            // 2. set a property
            // 3. call a Method (GetHello())

            ExecuteTest("Hardcoded, manual", new TestClassManual());
            ExecuteTest("Compiled expressions", new TestClassCompiledExpressions());
            ExecuteTest("Delegates", new TestClassDelegate());
            ExecuteTest("Dynamic", new TestClassDynamic());
            ExecuteTest("Reflection", new TestClassReflection());
            ExecuteTest("Reflection with caching", new TestClassReflectionWithCaching());

            System.Console.WriteLine("Press <enter> to quit.");
            Console.ReadLine();
        }


        static void ExecuteTest(string testName, ITestClass testClass) {

            int numberOfIterations = 1000000;
            int numberOfRepetitions = 10;     
            var timeResults = new TimeSpan[numberOfRepetitions];    

            System.Console.WriteLine("Starting test for: " + testName + " - nr of iterations per repetition: " + numberOfIterations + " - repetitions: " + numberOfRepetitions);

            for(var j=0; j<numberOfRepetitions; j++) {

                var hellos = new string[numberOfIterations];
                var sw = new Stopwatch();
                sw.Start();

                testClass.Init();

                for(var i = 0; i < numberOfIterations; i++) {
                    var dummy = testClass.GetInstance();
                    testClass.SetName(dummy, "world");
                    var hello = testClass.GetHello(dummy);
                    // we also test adding to the list, but we'll do it for all of the tests...
                    hellos[i] = hello;
                }

                sw.Stop();
                timeResults[j] = sw.Elapsed;

            }            
            
            System.Console.WriteLine("Fastest for " + testName + ":" + timeResults.Min());
            System.Console.WriteLine("Slowest for " + testName + ":" + timeResults.Max());
            System.Console.WriteLine("Average for " + testName + ":" + TimeSpan.FromMilliseconds(timeResults.Select(x => x.TotalMilliseconds).Average()));
            System.Console.WriteLine("**********************************************");
        }

    }
}

    

    