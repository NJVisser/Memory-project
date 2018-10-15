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
        private Application application;
        private string markpadLocation;

        [Test]
        public void UITest()
        {
            StartUp();
            NewGame();
            Close();
        }

        void StartUp()
        {
            var directoryName = TestContext.CurrentContext.TestDirectory;
            markpadLocation = Path.Combine(directoryName, @"MemoryProject.exe");
            application = Application.Launch(markpadLocation);
            var StartUpMainWindow = application.GetWindow("Name The Game Main Menu", InitializeOption.NoCache);
            StartUpMainWindow.Get<Button>("Settings").Click();
        }

        void NewGame()
        {
            var mainWindow = application.GetWindow("Name The Game Main Menu", InitializeOption.NoCache);
            var NGbutton = mainWindow.Get<Button>("NewGame");
            NGbutton.Click();
            var newGameWindow = application.GetWindow("New Game", InitializeOption.NoCache);
            var NewGameGridButton = newGameWindow.Get<Button>("7X7Button");
            NewGameGridButton.Click();
            var mainGameWindow = application.GetWindow("Name The Game", InitializeOption.NoCache);
            mainGameWindow.Close();
        }

        void Close()
        {
            var CloseApplication = Application.Launch(markpadLocation);
            var CloseMainWindow = CloseApplication.GetWindow("Name The Game Main Menu", InitializeOption.NoCache);
            var CloseNGbutton = CloseMainWindow.Get<Button>("Exit");
            CloseNGbutton.Click();
        }
    }
}