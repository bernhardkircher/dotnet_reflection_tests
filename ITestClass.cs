namespace Reflection
{

    /// <summary>
    /// Interface for all the different Testcase implementations.
    /// </summary>
    interface ITestClass {

        /// <summary>
        /// Allows the implementation to do ionitial preparation, e.g. collecting metadata, caching,.!--.!--.        
        /// </summary>
        void Init();

        DummyClass GetInstance();

        void SetName(DummyClass instance, string name);

        string GetHello(DummyClass instance);
    }
}
