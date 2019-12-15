
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflection
{

    /// <summary>
    /// Implementation that uses manual/static code for all of the operations (e.g. hand written)
    /// </summary>
    class TestClassManual : ITestClass {

        public void Init() {
            // NOOP
        }

        public DummyClass GetInstance() {
             return  new DummyClass();
        }

        public void SetName(DummyClass instance, string name) {           
            instance.Name = name;
        }

        public string GetHello(DummyClass instance) {
            return instance.GetHello();
        }
    }

}