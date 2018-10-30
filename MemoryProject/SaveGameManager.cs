using System;
using System.Collections.Generic;
using System.IO;
using AGC.Tools;
using MemoryProject.Data;
using MessagePack;

namespace MemoryProject
{
    public class SaveGameManager
    {
        private readonly string _saveGamesPath =
            $@"{AGCTools.GetGameFolder()}\save";


        /// <summary>
        /// Write a savegame
        /// </summary>
        /// <param name="saveName">Name of the save (will be saved as memory-{saveName}.sav)</param>
        /// <returns>True if  successful, false if an error occurred</returns>
        internal bool SaveGame(string saveName)
        {
            try
            {
                if (!Directory.Exists(_saveGamesPath))
                    Directory.CreateDirectory(_saveGamesPath);

                var data = GridManager.Instance.LiveGame;
                var save = MessagePackSerializer.Serialize(data);
                File.WriteAllBytes($@"{_saveGamesPath}\memory-{saveName}.sav", save);
                return true;
            }
            catch (Exception e)
            {
                AGCTools.LogException(e);
                return false;
            }
        }

        /// <summary>
        /// Write a savegame
        /// </summary>
        /// <remarks>
        /// This clears the grid before loading 
        /// </remarks>
        /// <param name="saveName">Name of the save to load</param>
        /// <returns>True if  successful, false if an error occurred</returns>
        internal bool LoadGame(string saveName)
        {
            try
            {
                var saveGame = $@"{_saveGamesPath}\memory-{saveName}.sav";
                if (!File.Exists(saveGame))
                {
                    AGCTools.Log($"memory-{saveName}.sav not found @ {_saveGamesPath}");
                    return false;
                }

                GridManager.Instance.Clear();

                var save = File.ReadAllBytes(saveGame);
                var data = MessagePackSerializer.Deserialize<SingleGame>(save);
                GridManager.Instance.LiveGame = data;
                GridManager.Instance.Player1Name.Content = $"{data.Player1Name}: {data.ScoreP1}";
                GridManager.Instance.Player2Name.Content = $"{data.Player2Name}: {data.ScoreP2}";
                return GridFactory.Instance.RestoreGameGrid(data.Grid);
            }
            catch (Exception e)
            {
                AGCTools.LogException(e);
                return false;
            }
        }
        
        internal List<HighScore> GetHighScoreList()
        {
            try
            {
                var highScores = $@"{AGCTools.GetGameFolder()}\highScores";
                if (!File.Exists(highScores)) return null;
                var hsd = File.ReadAllBytes(highScores);
                return MessagePackSerializer.Deserialize<List<HighScore>>(hsd);
            }
            catch (Exception e)
            {
                AGCTools.LogException(e);
                return null;
            }
        }


        internal void SaveToHighScoreList(HighScore s)
        {
           
            try
            {
                var hsl = GetHighScoreList() ?? new List<HighScore>();
                hsl.Add(s);
                var save = MessagePackSerializer.Serialize(hsl);
                File.WriteAllBytes($@"{AGCTools.GetGameFolder()}\highScores", save);
            }
            catch (Exception e)
            {
                AGCTools.LogException(e);
            }
        }


        #region Singleton

        private static readonly Lazy<SaveGameManager> LazySaveManager =
            new Lazy<SaveGameManager>(() => new SaveGameManager());

        public static SaveGameManager Instance => LazySaveManager.Value;

        private SaveGameManager()
        {
            MessagePackSerializer.SetDefaultResolver(MessagePack.Resolvers.ContractlessStandardResolver.Instance);
        }

        #endregion
    }
}