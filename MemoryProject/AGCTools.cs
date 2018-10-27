using System;
using System.IO;
using System.Windows;

namespace AGC.Tools
{
    public class AGCTools
    {
        private static readonly string _gameFolder =
            $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\AGC\Memory-Game";

        private static readonly string en = Environment.NewLine;

        private static string _logt;

        /// <summary>
        /// Sets up the logger
        /// </summary>
        public static void SetupLogger()
        {
            if (!Directory.Exists(_gameFolder))
                Directory.CreateDirectory(_gameFolder);

            LogNoTimestamp($"Log started at {DateTime.Now}{en}");
        }

        /// <summary>
        /// Write a string to the log with timestamp 
        /// </summary>
        /// <param name="l">String to log</param>
        public static void Log(string l)
        {
            _logt += $"{DateTime.Now}| {l}{en}";
            File.WriteAllText($@"{_gameFolder}\game.log", _logt);
        }

        /// <summary>
        /// Write a exception to the log with timestamp 
        /// </summary>
        /// <param name="e">Exception to log</param>
        public static void LogException(Exception e)
        {
            _logt += $"{DateTime.Now}: ERROR: {e.Message} {en}Source: {e.Source} {en}StackTrace: {e.StackTrace}{en}";
            File.WriteAllText($@"{_gameFolder}\game.log", _logt);
        }

        /// <summary>
        /// Write a string to the log without timestamp 
        /// </summary>
        /// <param name="l">String to log</param>
        public static void LogNoTimestamp(string l)
        {
            _logt += l + en;
            File.WriteAllText($@"{_gameFolder}\game.log", _logt);
        }

        /// <summary>
        /// Returns the game folder
        /// </summary>
        /// <returns></returns>
        public static string GetGameFolder()
        {
            return _gameFolder;
        }
    }
}