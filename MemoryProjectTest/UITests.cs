using System.IO;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace MemoryProjectTest
{
    [TestFixture]
    [NonParallelizable]
    public class UITests
    {
        [Test]
        public void UITest()
        {
            #region StartUp
            var directoryName = TestContext.CurrentContext.TestDirectory;
            var markpadLocation = Path.Combine(directoryName, @"MemoryProject.exe");
            var application = Application.Launch(markpadLocation);
            var mainWindow = application.GetWindow("Name The Game", InitializeOption.NoCache);
            #endregion

            #region NewGame
            var NGbutton = mainWindow.Get<Button>("NewGameButton");
            NGbutton.Click();
            var newGameWindow = application.GetWindow("New Game", InitializeOption.NoCache);
            var NewGameGridButton = newGameWindow.Get<Button>("7X7Button");
            NewGameGridButton.Click();
            #endregion
          
            #region Close
            mainWindow.Close();
            #endregion
        }
    }
}