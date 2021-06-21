namespace RoyalGameOfUr
{
    /// <summary>
    /// The <c>Controller</c> class.
    /// It is responsible for changing the game state, based on the user's 
    /// input. It notifies the <c>ConsoleView</c> when it need to update what is
    /// being shown to the user or when it needs to ask the user for input.
    /// Contains the game loop.
    /// </summary>
    public class Controller
    {
        // Instance variables
        private Board board;

        /// <summary>
        /// The constructor of the <c>Controller</c> class. It creates a new
        /// instance of this class.
        /// </summary>
        /// <param name="board">The game board.</param>

        public Controller(Board board)
        {
            this.board = board;
        }

        /// <summary>
        /// Gets the move chosen by the user and verifies if it is a move that
        /// is on the list of available moves.
        /// If the move is not on the list, tell the view to notify the user
        /// and ask for the move again.
        /// </summary>
        /// <param name="view">An instance of the <c>ConsoleView</c> class.</param>
        /// <returns>The move chosen by the user.</returns>
        
        public void Run(ConsoleView view)
        {
            int chosenMove = new int();

            view.ShowMenu();

            while ((board.Player1Points != 7) && (board.Player2Points != 7))
            {
                view.DrawBoard();

                board.RollDice();

                view.DisplayMessage(MessageCodes.Dice);

                if (board.RolledNum == 0)
                {
                    board.SwapTurn(SwapCodes.RolledZeroSwap);
                    view.DisplayMessage(MessageCodes.PressAny);
                    continue;
                }

                board.GetMoves();

                if (board.GetAvailableMovesCount() == 0)
                {
                    view.ShowMoves(MessageCodes.NoValidMoves);
                    board.SwapTurn(SwapCodes.RolledZeroSwap);
                    view.DisplayMessage(MessageCodes.PressAny);
                    continue;
                }
                else
                {
                    view.ShowMoves(MessageCodes.ValidMoves);
                }

                chosenMove = GetChosenMove(view);

                board.MovePiece(chosenMove);

                if (board.CheckWinner())
                {
                    view.DrawBoard();
                    view.DisplayMessage(MessageCodes.Winner);
                    continue;
                }

                board.SwapTurn(SwapCodes.NormalSwap);
            }
        }

        /// <summary>
        /// Gets the move chosen by the user and verifies if it is a move that
        /// is on the list of available moves.
        /// If the move is not on the list, tell the view to notify the user
        /// and ask for the move again.
        /// </summary>
        /// <param name="view">An instance of the <c>ConsoleView</c> class.</param>
        /// <returns>The move chosen by the user.</returns>
        private int GetChosenMove(ConsoleView view)
        {
            bool gotMove = false;
            int chosenMove = new int();

            while (!gotMove)
            {
                chosenMove = view.AskForMove();

                if ((chosenMove < 1) || (chosenMove > 
                    board.GetAvailableMovesCount()))
                {
                    view.DisplayMessage(MessageCodes.InvalidMove);
                }
                else
                {
                    gotMove = true;
                }
            }

            return chosenMove;
        }
    }
}
