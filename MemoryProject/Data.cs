using System.Collections.Generic;
using System.Windows.Controls.Primitives;

namespace MemoryProject.Data
{
	public class Card
	{
		public int Column = 0;
		public int Row = 0;
		public string Name = "";
		public bool IsClicked = false;
		public bool IsGone = false;
	}

	public class GameGrid
	{
		public Dictionary<string,Card> cards = new Dictionary<string, Card>();
	}

	public class Theme
	{
		public List<Card> cards = new List<Card>();
		public string BackImageName = "";
	}
}
