using System.Collections.Generic;

namespace MemoryProject.Data
{
    public class Card
    {
        public int Column;
        public int Row;
        public string Name;
        public string ID;
        public bool IsClicked;
        public bool IsGone;
    }

    public class SingleGame
    {
        public string ThemeName;
        public bool SinglePlayer;
        public string Player1Name;
        public string Player2Name;
        public int ScoreP1;
        public int ScoreP2;
        public Dictionary<string,Card> Grid;
    }

    public class Theme
    {
        public string ThemeName;
        public List<Card> Cards;
        public string BackImageName;
    }

    public enum Turn
    {
        Player1,
        Player2
    }
}