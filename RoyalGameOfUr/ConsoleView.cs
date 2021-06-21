using System;

namespace RoyalGameOfUr
{
    /// <summary>
    /// The <c>ConsoleView</c> class.
    /// It is represents the UI of the Board and is responsible by notifying 
    /// the Controller that something was done by the user.
    /// </summary>
    public class ConsoleView
    {
        // Instance variables
        private Controller controller;
        private Board board;
        private const char flower = '\u03A8';
        private const char p1Piece = '\u03A6'; 
        private const char p2Piece = '\u0394';

        // Constructor
        /// <summary>
        /// The constructor of the <c>ConsoleView</c> class.
        /// It creates a new instance of <c>ConsoleView</c>.
        /// </summary>
        /// <param name="controller">An instance of <c>Controller</c>.</param>
        /// <param name="board">An instance of <c>Board</c>.</param>
        public ConsoleView(Controller controller, Board board)
        {
            this.controller = controller;
            this.board = board;
        }

        /// <summary>
        /// Displays the game menu to the players.
        /// </summary>
        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Royal Game Of Ur!");
            Console.WriteLine("--------------------------------------------"
                + "--------------------------------");
            Console.WriteLine("How to play:");
            Console.WriteLine();
            Console.Write("At the start of each turn the player will roll "
                + "the dice.");
            Console.WriteLine(" The number\nyou get is how many squares you can"
                + " advance.");
            Console.WriteLine("A list with the available moves for that turn "
                + "will be shown to you.");
            Console.WriteLine();
            Console.WriteLine("Here's an example of what it will look like:");
            Console.WriteLine("1: (3, 5) -> (3, 2)");
            Console.WriteLine("2: (3, 1) -> (2, 3)");
            Console.WriteLine("The first pair of coordinates represents where "
                + "a piece is.");
            Console.WriteLine("The second pair of coordinates represents where "
                + "a piece can move to.");
            Console.WriteLine();
            Console.Write("You will then be prompted to choose your move. "
                + "You can do that by \n");
            Console.WriteLine("inserting the number that is to the left of the "
                + "move you want to choose.");
            Console.WriteLine("You can only make on move per turn... choose "
                + "wisely!");
            Console.Write("You can capture your opponent's piece if you "
                + "land on it, making it \n");
            Console.WriteLine("get out of the board.");
            Console.WriteLine($"{p1Piece} represents the Player1's pieces");
            Console.WriteLine($"{p2Piece} represents the Player2's pieces");
            Console.WriteLine("--------------------------------------------"
                + "--------------------------------");
            Console.WriteLine("Rules:");
            Console.WriteLine("1) Each player starts with 7 pieces.");
            Console.WriteLine("2) The first player to reach 7 points wins.");
            Console.WriteLine("3) You get 1 point for each piece you manage to "
                + "get to the end of the board.");
            Console.WriteLine("4) You can only move the exact number of squares"
                + " you rolled on the dice.");
            Console.Write($"5) {flower} represents Safe Squares. If you land"
                + " on one of these you get to play \n");
            Console.WriteLine("   again next turn and that piece cannot be "
                + "captured.");
                Console.WriteLine("6) If you roll a 0, the turn is passed to "
                + "the next player.");
            Console.WriteLine();
               
            DisplayMessage(MessageCodes.PressAny);
        }

        /// <summary>
        /// Draws the board, displaying the current state of the game.
        /// </summary>
        public void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("  1 2 3 4 5 6 7 8");
            Console.WriteLine("  ________   _____");

            for (int row = 0; row < 3; row++)
            {
                Console.Write($"{row + 1}|");
                for (int col = 0; col < 8; col++)
                {
                    Fields currentTile = board.GetTileContent(row, col);
                    if (currentTile == Fields.Safe)
                    {
                        Console.Write($"{flower} ");
                    }
                    else if ((currentTile == Fields.Initial) || 
                        (currentTile == Fields.End))
                    {
                        Console.Write("| ");
                    }
                    else if ((currentTile == (Fields.Normal ^ Fields.P1)) || 
                        (currentTile == (Fields.Safe ^ Fields.P1)))
                    {
                        Console.Write($"{p1Piece} ");
                    }
                    else if ((currentTile == (Fields.Normal ^ Fields.P2)) || 
                        (currentTile == (Fields.Safe ^ Fields.P2)))
                    {
                        Console.Write($"{p2Piece} ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }

                    if (col == 7)
                    {
                        Console.Write("|");
                    }

                    if (row == 0 && col == 7)
                    {
                        Console.Write($" Player2 -> Pieces left: "
                            + $"{board.Player2Pieces} | Points: {board.Player2Points}");
                    }
                    else if (row == 2 && col == 7)
                    {
                        Console.Write($" Player1 -> Pieces left: "
                            + $"{board.Player1Pieces} | Points: {board.Player1Points}");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  ________   _____");
            Console.WriteLine();
            Console.WriteLine($"{board.CurrentPlayer}'s turn");
        }

        /// <summary>
        /// Shows the user his current available moves, if there are any.
        /// The displayed message depends on the message code passed as an
        /// argument to the method.
        /// </summary>
        /// <param name="showCode">A code from <c>MesageCodes</c>.</param>
        
        public void ShowMoves(MessageCodes showCode)
        {
            // If showCode = MessageCodes.ValidMoves, it means there are moves 
            // to be shown 
            if (showCode == MessageCodes.ValidMoves)
            {
                int counter = 1;

                Console.WriteLine();
                Console.WriteLine("Valid moves:");

                // Print the available moves for the current turn
                for (int i = 0; i < board.GetAvailableMovesCount(); i++)
                {
                    Piece move = board.GetMove(i);

                    Console.WriteLine($"{counter}: ({move.CurrentX + 1}, {move.CurrentY + 1}) -> "
                        + $"({move.TargetX + 1}, {move.TargetY + 1})");

                    counter++;
                }
            }
            // If showCode = MessageCodes.NoValidMoves, it means there are no 
            // moves to be shown 
            else
            {
                Console.WriteLine("No valid moves!");
                DisplayMessage(MessageCodes.PressAny);
            }
        }

        /// <summary>
        /// Asks the user to choose his next move.
        /// In case a FormatException is thrown, displays an error message and
        /// asks the user to choose a move again.
        /// </summary>
        /// <returns>The move chosen by the user</returns>
        public int AskForMove()
        {
            // Variables
            int chosenMove = new int();

            // Ask user for his move and save it on the variable "chosenMove"
            Console.WriteLine();
            Console.Write("Choose your move: ");

            try
            {
                chosenMove = Int32.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine();
                Console.WriteLine("ERROR: Invalid input!");
            }

            return chosenMove;
        }

        /// <summary>
        /// Displays a message to the user. The message to be displayed is
        /// determined by a message code from <c>MessageCodes</c> passed as an
        /// argument to the method.
        /// </summary>
        /// <param name="messageCode">A code from <c>MessageCodes</c>.</param>
        public void DisplayMessage(MessageCodes messageCode)
        {
            switch (messageCode)
            {
                case MessageCodes.PressAny:
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    break;

                case MessageCodes.InvalidMove:
                    Console.WriteLine($"Please choose a move between 1 and "
                        + $"{board.GetAvailableMovesCount()}");
                    break;
                
                case MessageCodes.Dice:
                    Console.WriteLine($"{board.CurrentPlayer} rolled a "
                        + $"{board.RolledNum}");
                    break;

                case MessageCodes.Winner:
                    Console.WriteLine($"{board.CurrentPlayer} wins!");
                    Console.WriteLine();
                    Console.WriteLine("Press Enter to close...");
                    break;
            }
        }
    }
}
