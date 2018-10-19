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
        private static Application StartApplication()
        {
            return Application.Launch(Path.Combine(TestContext.CurrentContext.TestDirectory, @"MemoryProject.exe"));
        }

        private static Window CreateMainGameWindow()
        {
            var application = StartApplication();
            var mainWindow = application.GetWindow("Name The Game Main Menu", InitializeOption.NoCache);
            var NGbutton = mainWindow.Get<Button>("NewGame");
            NGbutton.Click();
            var newGameWindow = application.GetWindow("New Game", InitializeOption.NoCache);
            var NewGameGridButton = newGameWindow.Get<Button>("4X4Button");
            NewGameGridButton.Click();
            return application.GetWindow("Name The Game");
        } 



        [Test, Order(1)]
        public void NewGame()
        {
            var mainGameWindow = CreateMainGameWindow();
            mainGameWindow.Close();
        }

        [Test, Order(2)]
        public void Settings()
        {
            var application = StartApplication();
            var startUpMainWindow = application.GetWindow("Name The Game Main Menu", InitializeOption.NoCache);
            startUpMainWindow.Get<Button>("Settings").Click();
            startUpMainWindow.Close();
        }
        
        [Test, Order(3)]
        public void Close()
        {
            var application = StartApplication();
            var mainWindow = application.GetWindow("Name The Game Main Menu", InitializeOption.NoCache);
            var button = mainWindow.Get<Button>("Exit");
            button.Click();
        }

        [Test]
        public void PlayGame()
        {
            var mainGameWindow = CreateMainGameWindow();
            
            for (var row = 0; row < 4; row++)
            {
                for (var column = 0; column < 4; column++)
                {
                    mainGameWindow.Get<Image>($"I{row}X{column}").Click();
                }
            }
            mainGameWindow.Close();
        }
        
        
    }
}