using System;
using System.Linq.Expressions;

namespace Reflection
{    

    /// <summary>
    /// Implementation that uses compiled expressions underneath.      
    /// IMPORTANT: not all operations use compiled expressions, since I ran out of time to test the implementation/make it work...   
    /// </summary>
    class TestClassCompiledExpressions : ITestClass {

        private Func<DummyClass,string> _getHelloMethodDelegate;

        private Func<DummyClass> _ctorFunc;

        private Action<object,object> _setName;

        public void Init() {
            var type = typeof(DummyClass);
            var ctor = type.GetConstructor(new Type[0]);
            var nameProperty = type.GetProperty("Name");
            var getHelloMethod = type.GetMethod("GetHello");
                
            var wrappedObjectParameter = Expression.Parameter(typeof(object));
            var valueParameter = Expression.Parameter(typeof(object));

            var setExpression = Expression.Lambda<Action<object, object>>(
                Expression.Assign(
                    Expression.Property(
                        Expression.Convert(wrappedObjectParameter, nameProperty.DeclaringType), nameProperty),
                    Expression.Convert(valueParameter, nameProperty.PropertyType)),
                wrappedObjectParameter, valueParameter);

            _setName =  setExpression.Compile();

            _getHelloMethodDelegate = getHelloMethod.CreateDelegate(typeof(Func<DummyClass, string>)) as Func<DummyClass, string>; 

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