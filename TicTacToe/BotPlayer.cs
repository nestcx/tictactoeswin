using System;
namespace tictactoe
{
	public class BotPlayer
	{
		public BotPlayer()
		{
		}

		public static int get_move(string[] cells)
		{
			// return -1; causes broken loop!
			Random rng = new Random(); // Should this be created each time? Hmm...
			return rng.Next(9); // From 0 up to (but not including) 9
		}
	}
}