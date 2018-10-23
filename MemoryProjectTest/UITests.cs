using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
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
        private string[] CardNames =
        {
            "blue", "bluePair",
            "red", "redPair",
            "brown", "brownPair",
            "green", "greenPair",
            "orange", "orangePair",
            "yellow", "yellowPair",
            "purple", "purplePair",
            "pink", "pinkPair"
        };


        #region Helper functions

        private static Application StartApplication()
        {
            return Application.Launch(Path.Combine(TestContext.CurrentContext.TestDirectory, @"MemoryProject.exe"));
        }

        private static Window CreateMainGameWindow()
        {
            return CreateMainGameWindow(StartApplication());
        }

        private static Window CreateMainGameWindow(Application application)
        {
            var mainWindow = application.GetWindow("Name The Game Main Menu", InitializeOption.NoCache);
            var nGButton = mainWindow.Get<Button>("NewGame");
            nGButton.Click();
            var newGameWindow = application.GetWindow("New Game", InitializeOption.NoCache);
            var newGameGridButton = newGameWindow.Get<Button>("4X4Button");
            newGameGridButton.Click();
            return application.GetWindow("Name The Game");
        }

        #endregion


        #region Tests

        [Test, Order(1)]
        public void NewGame()
        {
            var mainGameWindow = CreateMainGameWindow();
            mainGameWindow.Close();
        }

        [Test, Order(2)]
        public void RestartGame()
        {
            var app = StartApplication();
            var mainGameWindow = CreateMainGameWindow(app);
            mainGameWindow.Get<Button>("RestartGame").Click();
            var newWindow = CreateMainGameWindow(app);
            newWindow.Close();
        }

        [Test, Order(3)]
        public void Settings()
        {
            var application = StartApplication();
            var startUpMainWindow = application.GetWindow("Name The Game Main Menu", InitializeOption.NoCache);
            startUpMainWindow.Get<Button>("Settings").Click();
            startUpMainWindow.Close();
        }

        [Test, Order(4)]
        public void Close()
        {
            var application = StartApplication();
            var mainWindow = application.GetWindow("Name The Game Main Menu", InitializeOption.NoCache);
            var button = mainWindow.Get<Button>("Exit");
            button.Click();
        }
        
        [Test, Order(5)]
        public void PlayGame()
        {
            var mainGameWindow = CreateMainGameWindow();

            foreach (var cardName in CardNames)
            {
                mainGameWindow.Get<Image>(cardName).Click();
            }

            StringAssert.AreEqualIgnoringCase("Score: 8", mainGameWindow.Get<Label>("Score1").Text);

            mainGameWindow.Close();
        }

        [Test]
        public void PlayGameWrong()
        {
            var tmpCardsList = new List<string>();
            tmpCardsList.AddRange(CardNames);
            var rnd = new Random();

            var mainGameWindow = CreateMainGameWindow();

            while (tmpCardsList.Count > 0)
            {
                var rCard = tmpCardsList[rnd.Next(tmpCardsList.Count)];
                tmpCardsList.Remove(rCard);
                mainGameWindow.Get<Image>(rCard).Click();
            }

            StringAssert.AreEqualIgnoringCase("Score: 0", mainGameWindow.Get<Label>("Score1").Text);

            mainGameWindow.Close();
        }

        #endregion
    }
}