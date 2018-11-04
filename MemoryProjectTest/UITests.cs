using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using TestStack.White;
using TestStack.White.Factory;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.WindowItems;

namespace MemoryProjectTest
{
    [TestFixture]
    [NonParallelizable]
    public class UITests
    {
        private readonly string _gameFolder =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\AGC\Memory-Game";

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

        private static string[] PlayerNames =
        {
            "Edgar", "AnotherFoxGuy"
        };


        #region Helper functions

        private static Application StartApplication()
        {
            return Application.Launch(Path.Combine(TestContext.CurrentContext.TestDirectory, @"MemoryProject.exe"));
        }

        private static Window CreateMainGameWindow(Application application = null)
        {
            if (application == null)
                application = StartApplication();

            var mainWindow = application.GetWindow("Name The Game Main Menu", InitializeOption.NoCache);
            var nGButton = mainWindow.Get<Button>("NewGame");
            nGButton.Click();
            var newGameWindow = application.GetWindow("New Game", InitializeOption.NoCache);
            newGameWindow.Get<ComboBox>("ThemeSelector").Select(2);
            newGameWindow.Get<TextBox>("Player1NameInput").Text = PlayerNames[0];
            newGameWindow.Get<TextBox>("Player2NameInput").Text = PlayerNames[1];
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

            StringAssert.AreEqualIgnoringCase($"{PlayerNames[0]}: 0", mainGameWindow.Get<Label>("Player1Name").Text);
            StringAssert.AreEqualIgnoringCase($"{PlayerNames[1]}: 0", mainGameWindow.Get<Label>("Player2Name").Text);

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

            var Endscreen = application.GetWindow("Endscreen", InitializeOption.NoCache);
            Endscreen.Close();

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
            var application = StartApplication();
            var mainGameWindow = CreateMainGameWindow(application);

            foreach (var cardName in CardNames)
            {
                mainGameWindow.Get<Image>(cardName).Click();
            }


            var Endscreen = application.GetWindow("Endscreen", InitializeOption.NoCache);
            Endscreen.Close();

            StringAssert.AreEqualIgnoringCase($"{PlayerNames[0]}: 8", mainGameWindow.Get<Label>("Player1Name").Text);
            StringAssert.AreEqualIgnoringCase($"{PlayerNames[1]}: 0", mainGameWindow.Get<Label>("Player2Name").Text);

            mainGameWindow.Close();
        }

        [Test, Order(6)]
        public void FailLoadGame()
        {
            if (Directory.Exists(_gameFolder))
                Directory.Delete(_gameFolder, true);


            var mainGameWindow = CreateMainGameWindow();

            mainGameWindow.Get<Button>("LoadGame").Click();

            var messageBox = mainGameWindow.MessageBox("Game Load");
            messageBox.Get<Button>(SearchCriteria.ByText("OK")).Click();

            mainGameWindow.Close();

            var log = File.ReadAllLines($@"{_gameFolder}\game.log");
            StringAssert.Contains("memory-Quick.sav not found", log[2]);
        }

        [Test, Order(7)]
        public void SaveGame()
        {
            var mainGameWindow = CreateMainGameWindow();

            for (var i = 0; i < CardNames.Length / 2; i++)
            {
                var cardName = CardNames[i];
                mainGameWindow.Get<Image>(cardName).Click();
            }

            mainGameWindow.Get<Button>("SaveGame").Click();

            var messageBox = mainGameWindow.MessageBox("Game Save");
            messageBox.Get<Button>(SearchCriteria.ByText("OK")).Click();

            mainGameWindow.Close();
        }

        [Test, Order(8)]
        public void PlayGameWrong()
        {
            var tmpCardsList = new List<string>();
            tmpCardsList.AddRange(CardNames.Where((c, i) => i % 2 != 0));
            tmpCardsList.AddRange(CardNames.Where((c, i) => i % 2 == 0));

            var mainGameWindow = CreateMainGameWindow();

            foreach (var cardName in tmpCardsList)
            {
                mainGameWindow.Get<Image>(cardName).Click();
            }

            StringAssert.AreEqualIgnoringCase($"{PlayerNames[0]}: 0", mainGameWindow.Get<Label>("Player1Name").Text);
            StringAssert.AreEqualIgnoringCase($"{PlayerNames[1]}: 0", mainGameWindow.Get<Label>("Player2Name").Text);

            mainGameWindow.Close();
        }

        [Test, Order(9)]
        public void LoadGame()
        {
            var application = StartApplication();
            var mainGameWindow = CreateMainGameWindow(application);

            mainGameWindow.Get<Button>("LoadGame").Click();

            var messageLoadBox = mainGameWindow.MessageBox("Game Load");
            messageLoadBox.Get<Button>(SearchCriteria.ByText("OK")).Click();

            StringAssert.AreEqualIgnoringCase($"{PlayerNames[0]}: 4", mainGameWindow.Get<Label>("Player1Name").Text);
            StringAssert.AreEqualIgnoringCase($"{PlayerNames[1]}: 0", mainGameWindow.Get<Label>("Player2Name").Text);

            for (var i = CardNames.Length / 2; i < CardNames.Length; i++)
            {
                var cardName = CardNames[i];
                mainGameWindow.Get<Image>(cardName).Click();
            }

            var Endscreen = application.GetWindow("Endscreen", InitializeOption.NoCache);
            Endscreen.Close();

            StringAssert.AreEqualIgnoringCase($"{PlayerNames[0]}: 8", mainGameWindow.Get<Label>("Player1Name").Text);
            StringAssert.AreEqualIgnoringCase($"{PlayerNames[1]}: 0", mainGameWindow.Get<Label>("Player2Name").Text);

            mainGameWindow.Close();
        }

        #endregion
    }
}