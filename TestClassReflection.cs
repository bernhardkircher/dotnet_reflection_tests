using System;

namespace Reflection
{
    
    /// <summary>
    /// Implementation that uses reflection for all operations underneath.        
    /// </summary>
    class TestClassReflection : ITestClass {

        public void Init() {
            // NOOP - we do no initialization here.
        }

        public DummyClass GetInstance() {
            var instance = Activator.CreateInstance(GetType().Assembly.FullName, "Reflection.DummyClass");
            return instance.Unwrap() as DummyClass;
        }

        public void SetName(DummyClass instance, string name) {
            var property = instance.GetType().GetProperty("Name");
            property.SetValue(instance, name);
        }

        public string GetHello(DummyClass instance) {
            var method = instance.GetType().GetMethod("GetHello");
            return method.Invoke(instance, null) as string;
        }
    }
}


