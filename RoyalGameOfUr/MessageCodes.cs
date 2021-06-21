namespace RoyalGameOfUr
{
    /// <summary>
    /// Codes that define which type of message is displayed to the user.
    /// </summary>
    public enum MessageCodes
    {
        /// <summary>
        /// Code to display the "Press Enter to continue..." message.
        /// </summary>
        PressAny,

        /// <summary>
        /// Code to display when an invalid move is chosen by the user.
        /// </summary>
        InvalidMove,

        /// <summary>
        /// Code to display how many moves the player got by rolling the dice.
        /// </summary>
        Dice,

        /// <summary>
        /// Code to display the winner of the game.
        /// </summary>
        Winner,

        /// <summary>
        /// Code to display the valid moves for the current player.
        /// </summary>
        ValidMoves,

        /// <summary>
        /// Code to display the message "No valid moves!".
        /// </summary>
        NoValidMoves
    }
}
