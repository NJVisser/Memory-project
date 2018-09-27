using System.IO;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;

namespace MemoryProjectTest
{
    [TestFixture]
    public class Tests
    {
        public Application Application;
        
        [Test]
        public void Test1()
        {
            var directoryName = TestContext.CurrentContext.TestDirectory;
            var markpadLocation = Path.Combine(directoryName, @"MemoryProject.exe");
            Application = Application.Launch(markpadLocation);
            var window = Application.GetWindow("MainWindow", InitializeOption.NoCache);
            var button = window.Get<Button>("TestButton");
            button.Click();
        }
    }
}