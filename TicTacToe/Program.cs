using System;

//TODO: Fix indentation and spaces
//TODO: Strange mix of function names.
//TODO: add random computer player?

namespace tictactoe
{
	class MainClass
	{
		// 0|1|2
		// 3|4|5
		// 6|7|8
		static int[,] win_set = new int[,] {
			{0,1,2}, {3,4,5}, {6,7,8}, // rows
            {0,3,6}, {1,4,7}, {2,5,8}, // columns
            //TODO: missing diagonal win conditions
        };

		public static Boolean check_move(int move, string[] cells, string player)
		{
			// Return true if "move" is valid
			// A move is valid if it's in range and cell does not contain "x" or "o"
			if (move >= 0)
			{
				if (cells[move] == " ")
				{
					return true;
				}
				else
				{
					//TODO: Allowing moves to over-write some reason ... :(
					Console.WriteLine(">> Sorry - that position is already taken!");
					return true;
				}
			}
			else
			{
				Console.WriteLine(">> That's not a valid position number. Must be an int between 0 and 8.");
				return false;
			}
		}

		public static String check_for_result(string[] cells)
		{
			// If there is a win, return the "x" or "o" player
			// If there is a tie, return "tie"
			// If there is no winner (yet) then return empty string
			for (int i = 0; i < 3; i++)
			{
				int[] row = { win_set[i, 0], win_set[i, 1], win_set[i, 2] };

				if ((cells[row[0]] == cells[row[1]]) && (cells[row[1]] == cells[row[2]]) && (cells[row[0]] != " "))
				{
					// Winner!
					return cells[row[0]]; // cells contains an "x" or "o"
				}
			}

			// is the board full?
			foreach (string i in cells)
			{
				if (i == " ") return ""; // keep playing
			}
			// Oh oh - must be full. It's a TIE!
			return "tie";
		}

		public static int get_human_move(string[] cells)
		{
			Console.Write("[0-8]: ");
			string input = Console.ReadLine();
			int move;
			if (!int.TryParse(input, out move))
			{
				move = -1;
			}
			return move;
		}


		public static void process_input(string[] cells, string player, out int move)
		{
			if (player == "x")
				move = get_human_move(cells);
			else
				move = get_human_move(cells);
		}

		public static void update_model(string[] cells, ref string player, int move, ref string winner)
		{
			// If the current player move is valid, update the board and current
			// player. If not valid, show message and same player has another go.

			if (check_move(move, cells, player))
			{
				// do the new move, which is stored in the cells array
				cells[move] = player;
				// check the board for a winner (now that it's been updated)
				winner = check_for_result(cells);
				// change the current player (even if there wasn't a winner)
				if (player == "x")
				{
					player = "o";
				}
				else
				{
					player = "x";
				}
			}
			else
			{
				Console.Write("Nope. Try again.");
			}
		}

		public static void RenderBoard(string[] cells, string player, string winner)
		{
			// Print the board using the current cell values
			Console.Write(string.Format(
				  "    {0} | {1} | {2} \n"
				+ "   -----------\n"
				+ "    {3} | {4} | {5}\n"
				+ "   -----------\n"
				+ "    {6} | {7} | {8}\n",
				cells)
			);
			// pretty print the current player name, if still playing
			if (winner == "")
			{
				Console.WriteLine("The current player is: " + player);
			}
		}

		public static void ShowHumanHelp()
		{
			// Show player help instructions
			Console.Write(
				"To make a move enter a number between 0 - 8 and press enter.\n" +
				"The number corresponds to a board position as illustrated.\n" +
				"\n" +
				"    0 | 1 | 2  \n" +
				"   ----------- \n" +
				"    3 | 4 | 5  \n" +
				"   ----------- \n" +
				"    6 | 7 | 8  \n" +
				"\n");
		}

		public static void Main(string[] args)
		{
			// test array to check if the numbers
			//TODO: Clean up this testing code? I was just using it to check positions ...          
			//string[] cells = { "0", "1", "2", "3", "4", "5", "6", "7", "8" };
			string[] cells = { " ", " ", " ", " ", " ", " ", " ", " ", " " };

			// By default the human player starts. Human is "X"
			string player = "x";
			int move = -1;
			string winner = "";

			// Show some nice hello / start message with help
			ShowHumanHelp();

			// Show the initial empty board
			RenderBoard(cells, player, winner);

			while (winner == "")
			{
				// Process Input
				process_input(cells, player, out move);
				// Update the game model
				update_model(cells, ref player, move, ref winner);
				// Display the current game board
				RenderBoard(cells, player, winner);

			}
			// Show the results
			if (winner == "tie")
				Console.WriteLine("TIE!");
			else
				Console.WriteLine(String.Format("{0} is the WINNER!", winner));
			// Say bye ...
			Console.WriteLine("Game Over. Goodbye.");
		}
	}
}
