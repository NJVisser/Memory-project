using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MemoryProject.Data
{
    public struct Card
    {
        public int Column;
        public int Row;
        public string Name;
        public string ID;
        public bool IsClicked;
        public bool IsGone;
    }

    public struct GameGrid
    {
        public Dictionary<string,Card> cards;
    }

    public struct Theme
    {
        public List<Card> cards;
        public string BackImageName;
    }
}