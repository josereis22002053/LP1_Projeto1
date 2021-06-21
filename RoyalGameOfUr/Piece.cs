namespace RoyalGameOfUr
{
    /// <summary>
    /// Defines a piece, containing its current possition and where it can move
    /// into.
    /// </summary>
    public struct Piece
    {
        // Properties

        /// <summary>
        /// Auto-implemented property that holds the value of the current row
        /// where the piece is.
        /// </summary>
        /// <value>Current row where the piece is.</value>
        public int CurrentX { get; }

        /// <summary>
        /// Auto-implemented property that holds the value of the current column
        /// where the piece is.
        /// </summary>
        /// <value>Current column where the piece is.</value>
        public int CurrentY { get; }

        /// <summary>
        /// Auto-implemented property that holds the value of the row which the
        /// piece can move into.
        /// </summary>
        /// <value>Row which the piece can move into.</value>
        public int TargetX { get; }

        /// <summary>
        /// Auto-implemented property that holds the value of the column which
        /// the piece can move into.
        /// </summary>
        /// <value>Column which the piece can move into.</value>
        public int TargetY { get; }

        /// <summary>
        /// The constructor of the Piece struct. Sets all the properties to 
        /// their initial values.
        /// </summary>
        /// <param name="currentX">
        /// Value that the property CurrentX will be set to when a new instance 
        /// of Piece is created.
        /// </param>
        /// <param name="currentY">
        /// Value that the property CurrentY will be set to when a new instance 
        /// of Piece is created.
        /// </param>
        /// <param name="targetX">
        /// Value that the property TargetX will be set to when a new instance 
        /// of Piece is created.
        /// </param>
        /// <param name="targetY">
        /// Value that the property TargetY will be set to when a new instance 
        /// of Piece is created.
        /// </param>
        public Piece(int currentX, int currentY, int targetX, int targetY)
        {
            // Set all the properties' values
            CurrentX = currentX;
            CurrentY = currentY;
            TargetX = targetX;
            TargetY = targetY;
        }
    }
}