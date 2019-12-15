
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflection
{
    /// <summary>
    /// Implementation that uses dynamic for property access and method invocation.    
    /// </summary>
    class TestClassDynamic : ITestClass {

        public void Init() {
            // NOOP - nothing to do for us.
        }

        public DummyClass GetInstance() {
             return new DummyClass();
        }

        public void SetName(DummyClass instance, string name) {           
            dynamic d = instance;
            d.Name = name;
        }

        public string GetHello(DummyClass instance) {
            dynamic d = instance;
            return d.GetHello();
        }
    }
}
