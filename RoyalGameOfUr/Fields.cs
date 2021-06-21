using System;

namespace RoyalGameOfUr
{
    /// <summary>
    /// Values that each tile of the board can have.
    /// </summary>
    [Flags]
    public enum Fields
    {
        /// <summary>
        /// Represents an initial tile.
        /// </summary>
        Initial = 0b00000001,

        /// <summary>
        /// Represents an end tile. If a player lands on this tile he gets a
        /// point.
        /// </summary>
        End = 0b00000010,

        /// <summary>
        /// Represents a safe tile. If a player lands on this tile, he gets to
        /// play again next turn.
        /// Pieces that are on a safe tile cannot be captured.
        /// </summary>
        Safe = 0b00000100,

        /// <summary>
        /// Represents a normal tile.
        /// </summary>
        Normal = 0b00001000,

        /// <summary>
        /// Used for bitwise operations to tell that a Player1's piece is in a
        /// certain tile.
        /// </summary>
        P1 = 0b00010000,

        /// <summary>
        /// Used for bitwise operations to tell that a Player2's piece is in a
        /// certain tile.
        /// </summary>
        P2 = 0b00100000
    }
}