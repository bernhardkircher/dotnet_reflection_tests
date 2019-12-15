using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflection
{


    /// <summary>
    /// Implementation that uses reflection underneath, but uses cachingfor certain operations.        
    /// </summary>
    class TestClassReflectionWithCaching : ITestClass {

        private Type _type;
        private ConstructorInfo _ctor;

        private PropertyInfo _nameProperty;
        private MethodInfo _getHelloMethod;

        public void Init() {
            _type = typeof(DummyClass);
            _ctor = _type.GetConstructor(new Type[0]);
            _nameProperty = _type.GetProperty("Name");
            _getHelloMethod = _type.GetMethod("GetHello");
        }

        public DummyClass GetInstance() {
             return  _ctor.Invoke(null) as DummyClass;
        }

        public void SetName(DummyClass instance, string name) {           
            _nameProperty.SetValue(instance, name);
        }

        public string GetHello(DummyClass instance) {
            return _getHelloMethod.Invoke(instance, null) as string;
        }
    }
}