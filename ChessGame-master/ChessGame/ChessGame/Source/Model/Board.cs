using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Source.Model {

    class Board {

        /// <summary>
        /// Variable showing size of game. Constant.
        /// </summary>
        public const int GameSize = 8;

        /// <summary>
        /// Jagged array of BoardSpace representing the game board.
        /// [0][0] is bottom left --> a1
        /// [0][7] is top left --> a8
        /// [7][0] is bottom right --> h1
        /// [7][7] is top right --> h8
        /// </summary>
        private BoardSpace[][] board;

        /// <summary>
        /// List containing the dead Piece.
        /// A List might be overkill, but I wanted to play with Collections.
        /// </summary>
        private LinkedList<Piece> deadBlacks;
        private LinkedList<Piece> deadWhites;

        public LinkedList<Piece> DeadBlacks {
            get { return this.deadBlacks; }
        }
        public LinkedList<Piece> DeadWhites {
            get { return this.deadWhites; }
        }

        /// <summary>
        /// Board contructor.
        /// </summary>
        public Board() {

            this.Reset();

        }

        /// <summary>
        /// Resets board to initial state.
        /// </summary>
        public void Reset() {
            //initializing the dictionaries (deadpieces)
            this.deadBlacks = new LinkedList<Piece>();
            this.deadWhites = new LinkedList<Piece>();

            //initializing the jagged array
            this.board = new BoardSpace[Board.GameSize][];
            for (int i = 0; i < this.board.Length; i++) {
                this.board[i] = new BoardSpace[Board.GameSize];
            }

            //creating all BoardSpace
            for (int i = 0; i < this.board.Length; i++) {
                for (int j = 0; j < this.board.Length; j++) {

                    this.board[i][j] = new BoardSpace();

                }
            }

            //initializing the rooks
            this.board[0][0].Piece = new Rook(this, 0); //BoardSpace occupied variable is automatically set to true here.
            this.board[7][0].Piece = new Rook(this, 0);
            this.board[0][7].Piece = new Rook(this, 1);
            this.board[7][7].Piece = new Rook(this, 1);

            //initializing the knights
            this.board[1][0].Piece = new Knight(this, 0);
            this.board[6][0].Piece = new Knight(this, 0);
            this.board[1][7].Piece = new Knight(this, 1);
            this.board[6][7].Piece = new Knight(this, 1);

            //initializing the bishops
            this.board[2][0].Piece = new Bishop(this, 0);
            this.board[5][0].Piece = new Bishop(this, 0);
            this.board[2][7].Piece = new Bishop(this, 1);
            this.board[5][7].Piece = new Bishop(this, 1);

            //initializing the queens
            this.board[3][0].Piece = new Queen(this, 0);
            this.board[3][7].Piece = new Queen(this, 1);

            //initializing the kings
            this.board[4][0].Piece = new King(this, 0);
            this.board[4][7].Piece = new King(this, 1);

            //initializing the pawns
            for (int i = 0; i < Board.GameSize; i++) {
                this.board[i][1].Piece = new Pawn(this, 0);
            }

            for (int i = 0; i < Board.GameSize; i++) {
                this.board[i][6].Piece = new Pawn(this, 1);
            }
        }

        public BoardSpace GetBoardSpace(int posX, int posY) {
            return this.board[posX][posY];
        }

        public void KillPiece(BoardSpace space) { 

            Piece tmp = space.Piece;
            int color = tmp.Color;
            tmp.Alive = false;

            //King are added in front to make the search for Kings more efficient
            //as it will happen often.
            if ( tmp.Type.Equals("king") ) {

                if (color == 0) {
                    this.deadWhites.AddFirst(tmp);
                } else {
                    this.deadBlacks.AddFirst(tmp);
                }
            } else {
                if (color == 0) {
                    this.deadWhites.AddLast(tmp);
                } else {
                    this.deadBlacks.AddLast(tmp);
                }
            }
            
            space.Piece = null; //maybe replace by a custom method
            space.Occupied = false; //maybe replace by a custom method
            space.IsPossibleDestination = false; //remove if you take care during view update method

        }

        /// <summary>
        /// String representation of the state of the Board.
        /// </summary>
        /// <returns> String representing the Board. </returns>
        public string ToString() {

            StringBuilder output = new StringBuilder();

            output.Append(this.DeadPiecesToString(this.deadBlacks) + "\n");

            output.Append("    a    b    c    d    e    f    g   h" + "\n");

            output.Append("  ┌────┬────┬────┬────┬────┬────┬────┬────┐" + "\n");

            for (int i = (Board.GameSize * 2) - 1; i > 0; i--) {

                if (i % 2 != 0) {

                    output.Append((i / 2) + 1 + " ");

                    for (int j = 0; j < Board.GameSize; j++) {

                        if (this.board[j][(i / 2)].Occupied && this.board[j][(i / 2)].IsPossibleDestination) {
                            output.Append("│ X  ");
                        } else if (this.board[j][(i / 2)].IsPossibleDestination) {
                            output.Append("│ x  ");
                        } else if (this.board[j][(i / 2)].Occupied) {
                            output.Append("│ " + this.board[j][(i / 2)].Piece.Icon + " ");
                        } else {
                            output.Append("│    ");
                        }

                    }

                    output.Append("│\n");

                } else {
                    output.Append("  ├────┼────┼────┼────┼────┼────┼────┼────┤" + "\n");
                }

            }

            output.Append("  └────┴────┴────┴────┴────┴────┴────┴────┘" + "\n");

            output.Append(this.DeadPiecesToString(this.deadWhites) + "\n");

            return output.ToString();
        }

        /// <summary>
        /// Helper method that returns personalized string of a LinkedList.
        /// Used to get string reprensation of dead pieces.
        /// </summary>
        /// <param name="deadPieces"> The LinkedList to convert to string. </param>
        /// <returns> String representation of the LinkedList in argument. </returns>
        private string DeadPiecesToString(LinkedList<Piece> deadPieces) {

            StringBuilder output = new StringBuilder();

            foreach (Piece piece in deadPieces) {

                output.Append(" " + piece.Icon + " ");

            }

            return output.ToString();

        }

    }

}
