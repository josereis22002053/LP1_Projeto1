using System;
using System.Collections.Generic;

namespace RoyalGameOfUr
{
    /// <summary>
    /// The <c>Board</c> class.
    /// It contains the game's logic and its current state.
    /// </summary>
    public class Board
    {
        // Instance properties

        /// <summary>
        /// Property that contains what player is currently playing.
        /// </summary>
        /// <value>The current player.</value>
        public Players CurrentPlayer { get; private set; }

        /// <summary>
        /// Property that holds the value of how many points Player1 has.
        /// </summary>
        /// <value>Amount of Player1's points.</value>
        public int Player1Points { get; private set; }

        /// <summary>
        /// Property that holds the value of how many points Player2 has.
        /// </summary>
        /// <value>Amount of Player2's points.</value>
        public int Player2Points { get; private set; }

        /// <summary>
        /// Property that holds the value of how many pieces Player1 has left.
        /// </summary>
        /// <value>The amount of pieces Player1 has left.</value>
        public int Player1Pieces { get; private set; }

        /// <summary>
        /// Property that holds the value of how many pieces Player2 has left.
        /// </summary>
        /// <value>The amount of pieces Player2 has left.</value>
        public int Player2Pieces { get; private set; }

        /// <summary>
        /// Property that has the value of how many moves the current player got
        /// by rolling the dice.
        /// </summary>
        /// <value>
        /// Amount of moves the current player got by rolling the dice.
        /// </value>
        public int RolledNum { get; private set; }

        // Instance variables
        private Fields[ , ] tiles;
        private List<Piece> availableMoves;
        private Players nextPlayer;

        /// <summary>
        /// The constructor of the Board class. Sets all the properties to their 
        /// initial values.
        /// </summary>
        public Board()
        {
            // Array that has the Board's content and represents the game state
            tiles = new Fields[3, 8]{
                {Fields.Safe, Fields.Normal, Fields.Normal, Fields.Normal, 
                 Fields.Initial, Fields.End, Fields.Safe, Fields.Normal},
                {Fields.Normal, Fields.Normal, Fields.Normal, Fields.Safe, 
                 Fields.Normal, Fields.Normal, Fields.Normal, Fields.Normal},
                {Fields.Safe, Fields.Normal, Fields.Normal, Fields.Normal, 
                 Fields.Initial, Fields.End, Fields.Safe, Fields.Normal}
                };

            // Set the properties to their initial values
            CurrentPlayer = Players.Player1;
            nextPlayer = Players.Player2;
            availableMoves = new List<Piece>();
            Player1Points = 0;
            Player1Pieces = 7;
            Player2Points = 0;
            Player2Pieces = 7;
            RolledNum = 2;
        }

        /// <summary>
        /// Gets the available moves for the current player.
        /// It iterates through the game board and when it finds a piece that
        /// belongs to the current player, it calls <c>CalculateMove</c> to
        /// calculate the move for that piece.
        /// </summary>
        public void GetMoves()
        {
            // If Player1 still has pieces left, check if a new piece can be put
            // into play   
            if (CurrentPlayer == Players.Player1)
            {
                if (Player1Pieces != 0)
                {
                    CalculateMove(2, 4, RolledNum);
                }
            }
            // If Player2 still has pieces left, check if a new piece can be put
            // into play
            else
            {
                if (Player2Pieces != 0)
                {
                    CalculateMove(0, 4, RolledNum);
                }
            }
            
            // Check the Board for the current player's pieces
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    // If a piece is found, check if there is any available move
                    //for that piece(Player1)
                    if (CurrentPlayer == Players.Player1)
                    {
                        if ((tiles[row, col] == (Fields.Normal ^ Fields.P1)) || 
                            (tiles[row, col] == (Fields.Safe ^ Fields.P1)))
                        {
                            CalculateMove(row, col, RolledNum);
                        }
                    }
                    // If a piece is found, check if there is any available move 
                    // for that piece(Player2)
                    else
                    {
                        if ((tiles[row, col] == (Fields.Normal ^ Fields.P2)) || 
                            (tiles[row, col] == (Fields.Safe ^ Fields.P2)))
                        {
                            CalculateMove(row, col, RolledNum);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if there is an available move for the piece passed as an
        /// argument to the method.
        /// It goes tile by tile, until it reaches the target tile. Once the
        /// target tile is reached, checks if that tile is considered a valid
        /// move.
        /// The amount of times it advances one tile is determined by the value
        /// passed in the <c>rolledNum</c> argument.
        /// </summary>
        /// <param name="x">The current row where the piece is.</param>
        /// <param name="y">The current column where the piece is.</param>
        /// <param name="rolledNum">
        /// Determines how many times it should advance one tile.
        /// </param>
        public void CalculateMove(int x, int y, int rolledNum)
        {
            int targetX = x;
            int targetY = y;

            Fields targetTile;
            
            while (rolledNum > 0)
            {
                // Verify if the move goes over the limit of the board (if it 
                // goes over the "end" tile into the "initial" or further)
                if ((targetY == 6 && targetX != 1) && rolledNum > 1)
                {
                    break;
                }

                // Calculate moves if the current player is Player1
                if (CurrentPlayer == Players.Player1)
                {
                    // Player1's initial row
                    if ((targetX == 2) && ((targetY <= 4) && (targetY >= 1)))
                    {
                        targetY--;
                        rolledNum--;
                        continue;
                    }
                    // Transition from initial row into the middle one [DANGER ZONE]
                    else if ((targetX == 2) && (targetY == 0))
                    {
                        targetX--;
                        rolledNum--;
                        continue;
                    }
                    // Middle row [DANGER ZONE]
                    else if ((targetX == 1) && ((targetY >= 0) && (targetY <= 6)))
                    {
                        targetY++;
                        rolledNum--;
                        continue;
                    }
                    // Transition from middle row into last row
                    else if ((targetX == 1) && (targetY == 7))
                    {
                        targetX++;
                        rolledNum--;
                        continue;
                    }
                    // Last row
                    else if ((targetX == 2) && (targetY >= 5))
                    {
                        targetY--;
                        rolledNum--;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                // Calculate moves if the current player is Player2
                else
                {
                    // Player2's initial row
                    if ((targetX == 0) && ((targetY <= 4) && (targetY >= 1)))
                    {
                        targetY--;
                        rolledNum--;
                        continue;
                    }
                    // Transition from initial row into the middle one [DANGER ZONE]
                    else if ((targetX == 0) && (targetY == 0))
                    {
                        targetX++;
                        rolledNum--;
                        continue;
                    }
                    // Middle row [DANGER ZONE]
                    else if ((targetX == 1) && ((targetY >= 0) && (targetY <= 6)))
                    {
                        targetY++;
                        rolledNum--;
                        continue;
                    }
                    // Transition from middle row into last row
                    else if ((targetX == 1) && (targetY == 7))
                    {
                        targetX--;
                        rolledNum--;
                        continue;
                    }
                    // Last row
                    else if ((targetX == 0) && (targetY <= 7))
                    {
                        targetY--;
                        rolledNum--;
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            // Save the target tile's content on the variable "targetTile", so 
            // we can check if that's a valid move
            targetTile = tiles[targetX, targetY];

            // Check for Player1
            if (CurrentPlayer == Players.Player1)
            {
                // If the move is available, add it to the list of available moves
                if ((targetTile == Fields.Normal) || (targetTile == Fields.Safe) || 
                    (targetTile == (Fields.Normal ^ Fields.P2)) || targetTile == Fields.End)
                {
                    availableMoves.Add(new Piece(x, y, targetX, targetY));
                }
            }
            // Check for Player2
            else
            {
                // If the move is available, add it to the list of available moves
                if ((targetTile == Fields.Normal) || (targetTile == Fields.Safe) || 
                    (targetTile == (Fields.Normal ^ Fields.P1)) || targetTile == Fields.End)
                {
                    availableMoves.Add(new Piece(x, y, targetX, targetY));
                }
            }
        }
        
        /// <summary>
        /// Performs the move chosen by the current player.
        /// Uses bitwise operations to active the piece on the tile it landed on
        /// and to de-activate it from the tile it moved out from.
        /// </summary>
        /// <param name="availableMovesIndex">
        /// The move chosen by the current player.
        /// </param>
        public void MovePiece(int availableMovesIndex)
        {
            // Variables
            int currentX, currentY, targetX, targetY, index;

            // The value passed as an argument is greater than the actual
            // AvailabaleMoves index that has the move that will be made. We
            // need to decrement it so we don't get an IndexOutOfRangeException
            index = availableMovesIndex - 1;

            // Save the board coordinates correspondant to the current tile and
            // to the tile that the move will be moved into
            currentX = availableMoves[index].CurrentX;
            currentY = availableMoves[index].CurrentY;
            targetX = availableMoves[index].TargetX;
            targetY = availableMoves[index].TargetY;

            // If the player landed on a safe tile he will roll the dice again
            if (tiles[targetX, targetY] == Fields.Safe)
            {
                nextPlayer = CurrentPlayer;
            }
            // If the player didn't land on a safe tile he will pass the turn
            else
            {
                if (CurrentPlayer == Players.Player1)
                {
                    nextPlayer = Players.Player2;
                }
                else
                {
                    nextPlayer = Players.Player1;
                }
            }

            // Moves for Player1
            if (CurrentPlayer == Players.Player1)
            {
                // Player puts a new piece in play
                if (tiles[currentX, currentY] == Fields.Initial)
                {
                    tiles[targetX, targetY] ^= Fields.P1;
                    Player1Pieces--;
                }
                // Player captures an opponent's piece
                else if (tiles[targetX, targetY] == (Fields.Normal ^ Fields.P2))
                {
                    tiles[currentX, currentY] ^= Fields.P1;
                    tiles[targetX, targetY] = (Fields.Normal ^ Fields.P1);
                    Player2Pieces++;
                }
                // Player makes a point
                else if (tiles[targetX, targetY] == Fields.End)
                {
                    tiles[currentX, currentY] ^= Fields.P1;
                    Player1Points++;
                }
                // Player moves to a field that isn't already occupied
                else
                {
                    tiles[currentX, currentY] ^= Fields.P1;
                    tiles[targetX, targetY] ^= Fields.P1;
                }
            }
            // Moves for Player2
            else
            {
                // Player puts a new piece in play
                if (tiles[currentX, currentY] == Fields.Initial)
                {
                    tiles[targetX, targetY] ^= Fields.P2;
                    Player2Pieces--;
                }
                // Player captures an opponent's piece
                else if (tiles[targetX, targetY] == (Fields.Normal ^ Fields.P1))
                {
                    tiles[currentX, currentY] ^= Fields.P2;
                    tiles[targetX, targetY] = (Fields.Normal ^ Fields.P2);
                    Player1Pieces++;
                }
                // Player makes a point
                else if (tiles[targetX, targetY] == Fields.End)
                {
                    tiles[currentX, currentY] ^= Fields.P2;
                    Player2Points++;
                }
                // Player moves to a field that isn't already occupied
                else
                {
                    tiles[currentX, currentY] ^= Fields.P2;
                    tiles[targetX, targetY] ^= Fields.P2;
                }
            }

            // Clear the list of available moves after a move is made 
            // successfully
            availableMoves.Clear();
        }

        /// <summary>
        /// Rolls the dice, changing the value of the property<c>RolledNum<c/>.
        /// </summary>
        public void RollDice()
        {
            // Variables
            RolledNum = 0;

            // Create a Random object
            Random rng = new Random();

            // Start the loop to roll the four dice
            for (int dice = 4; dice > 0; dice--)
            {
                // For each dice get a random value greater or equal to 0 and 
                // less than 1
                double rollRNG = rng.NextDouble();

                // There is a 50% chance to get an extra step for our move
                if (rollRNG <= 0.49)
                {
                    RolledNum++;
                }
            }
        }

        /// <summary>
        /// Checks if someone won the game.
        /// </summary>
        /// <returns>
        /// <c>true</c> if someone reached 7 points.
        /// <c>false</c> if no one has reached 7 points.
        /// </returns>
        public bool CheckWinner()
        {
            if (Player1Points == 7 || Player2Points == 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the value of <c>tiles</c> content at the position specified by
        /// <paramref name="row"/> and <paramref name="col"/>.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>
        /// Value of <c>tiles</c> content at the position specified by
        /// <paramref name="row"/> and <paramref name="col"/>.
        /// </returns>
        public Fields GetTileContent(int row, int col)
        {
            return tiles[row, col];
        }

        /// <summary>
        /// Gets the amount of elements of <c>availableMoves</c>.
        /// </summary>
        /// <returns>Amount of elements of <c>availableMoves</c>.</returns>
        public int GetAvailableMovesCount()
        {
            return availableMoves.Count;
        }

        /// <summary>
        /// Gets a <c>Piece</c> from <c>availableMoves</c>. The piece it gets is
        /// specified by <paramref name="index"/>.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>A <c>Piece</c> from <c>availableMoves</c>.</returns>
        public Piece GetMove(int index)
        {
            return availableMoves[index];
        }

        /// <summary>
        /// Swaps the current player. There are two types of swap, depending on
        /// the value passed by <paramref name="swapCode"/>.
        /// A NormalSwap is made after a player makes a move.
        /// A RolledZeroSwap is made when a player rolls a 0 on the dice.
        /// If this distinction wasn't made, if a player landed on a safe tile
        /// and then rolled a 0, it would cause the current player to always be
        /// the same.
        /// </summary>
        /// <param name="swapCode">The type of swap that will me  made.</param>
        public void SwapTurn(SwapCodes swapCode)
        {
            switch (swapCode)
            {
                case SwapCodes.NormalSwap:
                    CurrentPlayer = nextPlayer;
                    break;
                
                case SwapCodes.RolledZeroSwap:
                    if (CurrentPlayer == Players.Player1)
                    {
                        CurrentPlayer = Players.Player2;
                    }
                    else
                    {
                        CurrentPlayer = Players.Player1;
                    }
                    break;
            }
        }
    }
}