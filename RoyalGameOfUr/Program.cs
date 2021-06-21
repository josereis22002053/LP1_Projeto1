using System;
using System.Text;

namespace RoyalGameOfUr
{
    /// <summary>
    /// Class that contains the method <see cref="Main()"/>.
    /// The program starts here.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The programs starts here.
        /// </summary>
        /// <param name="args">
        /// Command line options. None are currently supported.
        /// </param>
        static void Main(string[] args)
        {
            // Change the console's output encoding so it can display the
            // symbols that represent safe tiles and the players' pieces
            Console.OutputEncoding = Encoding.UTF8;

            // Create the board
            Board board = new Board();

            // Create the controller
            Controller controller = new Controller(board);

            // Create the view
            ConsoleView view = new ConsoleView(controller, board);

            // Start the game loop
            controller.Run(view);
        }
    }
}
