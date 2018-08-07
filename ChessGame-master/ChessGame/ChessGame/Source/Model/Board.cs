using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Source.Model
{

    class Board
    {

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

        public LinkedList<Piece> DeadBlacks
        {
            get { return this.deadBlacks; }
        }
        public LinkedList<Piece> DeadWhites
        {
            get { return this.deadWhites; }
        }

        /// <summary>
        /// Board contructor.
        /// </summary>
        public Board(string gameMode)
        {

            this.Reset(gameMode);

        }

        /// <summary>
        /// Resets board to initial state.
        /// </summary>
        public void Reset(string gameMode)
        {
            //initializing the dictionaries (deadpieces)
            this.deadBlacks = new LinkedList<Piece>();
            this.deadWhites = new LinkedList<Piece>();

            //initializing the jagged array
            this.board = new BoardSpace[Board.GameSize][];
            for (int i = 0; i < this.board.Length; i++)
            {
                this.board[i] = new BoardSpace[Board.GameSize];
            }

            //creating all BoardSpace
            for (int i = 0; i < this.board.Length; i++)
            {
                for (int j = 0; j < this.board.Length; j++)
                {

                    this.board[i][j] = new BoardSpace();

                }
            }

            //initializing the rooks
            if (gameMode.Equals("Chess"))
            {
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
            }
            else
            {
                Random r = new Random();
                int rook1 = 0;
                int rook2 = 0;
                int king = 0;
                int bishop1 = 0;
                int bishop2 = 0;
                List<int> spaces = new List<int>();
                spaces.AddRange(new int[]
                {
                        0, 1, 2, 3, 4, 5, 6, 7
                });
                List<int> evens = new List<int>();
                evens.AddRange(new int[]
                {
                        0, 2, 4, 6
                });
                List<int> odds = new List<int>();
                odds.AddRange(new int[]
                {
                        1, 3, 5, 7
                });
                List<Piece> whitepieces = new List<Piece>();
                whitepieces.AddRange(new List<Piece>() {
                        new Rook(this, 0),
                        new King(this, 0),
                        new Rook(this, 0),
                        new Bishop(this, 0),
                        new Bishop(this, 0),
                        new Knight(this, 0),
                        new Knight(this, 0),
                        new Queen(this, 0)
                    });
                List<Piece> blackpieces = new List<Piece>();
                blackpieces.AddRange(new List<Piece>() {
                        new Rook(this, 1),
                        new King(this, 1),
                        new Rook(this, 1),
                        new Bishop(this, 1),
                        new Bishop(this, 1),
                        new Knight(this, 1),
                        new Knight(this, 1),
                        new Queen(this, 1)
                    });
                for (int j = 0; j < 8;)
                {
                    int index = r.Next(8);
                    if (spaces.Contains(index))
                    {
                        if (j == 0)
                        {
                            rook1 = index;
                            this.board[index][0].Piece = whitepieces[0];
                            this.board[index][7].Piece = blackpieces[0];
                            spaces.Remove(index);
                            whitepieces.Remove(whitepieces[0]);
                            blackpieces.Remove(blackpieces[0]);
                            j++;
                        }
                        else if (j == 1)
                        {
                            king = index;
                            if (king == 0 || king == 7)
                            {
                                bool found = false;
                                while (!found)
                                {
                                    index = r.Next(8);
                                    king = index;
                                    if (spaces.Contains(index))
                                    {
                                        if (king != 0 && king != 7)
                                        {
                                            this.board[index][0].Piece = whitepieces[0];
                                            this.board[index][7].Piece = blackpieces[0];
                                            spaces.Remove(index);
                                            whitepieces.Remove(whitepieces[0]);
                                            blackpieces.Remove(blackpieces[0]);
                                            found = true;
                                            j++;
                                        }
                                    }
                                }
                                
                            }
                            else
                            {
                                this.board[index][0].Piece = whitepieces[0];
                                this.board[index][7].Piece = blackpieces[0];
                                spaces.Remove(index);
                                whitepieces.Remove(whitepieces[0]);
                                blackpieces.Remove(blackpieces[0]);
                                j++;
                            }
                        }
                        else if (j == 2)
                        {
                            rook2 = index;
                            if (((rook2 < king) && (king < rook1)) || ((rook1 < king) && (king < rook2)))
                            {
                                this.board[index][0].Piece = whitepieces[0];
                                this.board[index][7].Piece = blackpieces[0];
                                spaces.Remove(index);
                                whitepieces.Remove(whitepieces[0]);
                                blackpieces.Remove(blackpieces[0]);
                                j++;
                            }
                            else
                            {
                                bool found = false;
                                while (!found)
                                {
                                    index = r.Next(8);
                                    rook2 = index;
                                    if (spaces.Contains(index))
                                    {                                  
                                        if (((rook2 < king) && (king < rook1)) || ((rook1 < king) && (king < rook2)))
                                        {
                                            this.board[index][0].Piece = whitepieces[0];
                                            this.board[index][7].Piece = blackpieces[0];
                                            spaces.Remove(index);
                                            whitepieces.Remove(whitepieces[0]);
                                            blackpieces.Remove(blackpieces[0]);
                                            found = true;
                                            j++;
                                        }
                                    }
                                }
                            }
                        }
                        else if (j == 3)
                        {
                            bishop1 = index;
                            this.board[index][0].Piece = whitepieces[0];
                            this.board[index][7].Piece = blackpieces[0];
                            spaces.Remove(index);
                            whitepieces.Remove(whitepieces[0]);
                            blackpieces.Remove(blackpieces[0]);
                            j++;
                        }
                        else if (j == 4)
                        {
                            bishop2 = index;
                            if ((evens.Contains(bishop1) && odds.Contains(bishop2)) || (evens.Contains(bishop2) && odds.Contains(bishop1)))
                            {
                                this.board[index][0].Piece = whitepieces[0];
                                this.board[index][7].Piece = blackpieces[0];
                                spaces.Remove(index);
                                whitepieces.Remove(whitepieces[0]);
                                blackpieces.Remove(blackpieces[0]);
                                j++;
                            }
                            else
                            {
                                bool found = false;
                                while (!found)
                                {
                                    index = r.Next(8);
                                    bishop2 = index;
                                    if (spaces.Contains(index))
                                    {
                                        if ((evens.Contains(bishop1) && odds.Contains(bishop2)) || (evens.Contains(bishop2) && odds.Contains(bishop1)))
                                        {
                                            this.board[index][0].Piece = whitepieces[0];
                                            this.board[index][7].Piece = blackpieces[0];
                                            spaces.Remove(index);
                                            whitepieces.Remove(whitepieces[0]);
                                            blackpieces.Remove(blackpieces[0]);
                                            j++;
                                            found = true;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            this.board[index][0].Piece = whitepieces[0];
                            this.board[index][7].Piece = blackpieces[0];
                            spaces.Remove(index);
                            whitepieces.Remove(whitepieces[0]);
                            blackpieces.Remove(blackpieces[0]);
                            j++;
                        }
                    }
                }

            }
            //initializing the pawns
            for (int i = 0; i < Board.GameSize; i++)
            {
                this.board[i][1].Piece = new Pawn(this, 0);
            }

            for (int i = 0; i < Board.GameSize; i++)
            {
                this.board[i][6].Piece = new Pawn(this, 1);
            }
        }

        public BoardSpace GetBoardSpace(int posX, int posY)
        {
            return this.board[posX][posY];
        }

        public void KillPiece(BoardSpace space)
        {

            Piece tmp = space.Piece;
            int color = tmp.Color;
            tmp.Alive = false;

            //King are added in front to make the search for Kings more efficient
            //as it will happen often.
            if (tmp.Type.Equals("king"))
            {

                if (color == 0)
                {
                    this.deadWhites.AddFirst(tmp);
                }
                else
                {
                    this.deadBlacks.AddFirst(tmp);
                }
            }
            else
            {
                if (color == 0)
                {
                    this.deadWhites.AddLast(tmp);
                }
                else
                {
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
        public string ToString()
        {

            StringBuilder output = new StringBuilder();

            output.Append(this.DeadPiecesToString(this.deadBlacks) + "\n");

            output.Append("    a    b    c    d    e    f    g   h" + "\n");

            output.Append("  ┌────┬────┬────┬────┬────┬────┬────┬────┐" + "\n");

            for (int i = (Board.GameSize * 2) - 1; i > 0; i--)
            {

                if (i % 2 != 0)
                {

                    output.Append((i / 2) + 1 + " ");

                    for (int j = 0; j < Board.GameSize; j++)
                    {

                        if (this.board[j][(i / 2)].Occupied && this.board[j][(i / 2)].IsPossibleDestination)
                        {
                            output.Append("│ X  ");
                        }
                        else if (this.board[j][(i / 2)].IsPossibleDestination)
                        {
                            output.Append("│ x  ");
                        }
                        else if (this.board[j][(i / 2)].Occupied)
                        {
                            output.Append("│ " + this.board[j][(i / 2)].Piece.Icon + " ");
                        }
                        else
                        {
                            output.Append("│    ");
                        }

                    }

                    output.Append("│\n");

                }
                else
                {
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
        private string DeadPiecesToString(LinkedList<Piece> deadPieces)
        {

            StringBuilder output = new StringBuilder();

            foreach (Piece piece in deadPieces)
            {

                output.Append(" " + piece.Icon + " ");

            }

            return output.ToString();

        }

    }

}
