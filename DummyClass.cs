
namespace Reflection
{
    /// <summary>
    /// Class that will be used by the tests.
    /// Name property and GetHello will be called.
    /// </summary>
    public class DummyClass {
        public string Name {get;set;}

        public string GetHello() {
            return "Hello " + Name;
        }
    }
}