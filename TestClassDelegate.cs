using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Reflection
{

    /// <summary>
    /// Implementation that uses Delegates underneath.        
    /// </summary>
    class TestClassDelegate : ITestClass {

        private Func<DummyClass,string> _getHelloMethodDelegate;

        private Func<DummyClass> _ctorFunc;

        private Action<DummyClass,string> _setName;

        public void Init() {
             var type = typeof(DummyClass);
            var ctor = type.GetConstructor(new Type[0]);
            var nameProperty = type.GetProperty("Name");
            var getHelloMethod = type.GetMethod("GetHello");

             _setName = (Action<DummyClass,string>)
                        Delegate.CreateDelegate(typeof(Action<DummyClass, string>), null,
                            nameProperty.GetSetMethod());

            _getHelloMethodDelegate = (Func<DummyClass,string>)Delegate.CreateDelegate(typeof(Func<DummyClass, string>), getHelloMethod);

            // use expressions, i cannot get the example with a delegate.. might be the same as
            // for property...            
             var lambda = Expression.Lambda<Func<DummyClass>>(Expression.New(ctor));
            _ctorFunc = lambda.Compile();            
        }

        public DummyClass GetInstance() {            
            return _ctorFunc();
        }

        public void SetName(DummyClass instance, string name) {           
            _setName(instance, name);
        }

         public string GetHello(DummyClass instance) {
            return _getHelloMethodDelegate(instance);
        }
    }
}