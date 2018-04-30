using System;
using System.Collections.Generic;
using System.Text;

namespace checkers
{

    class CheckersConsolUI
    {
        public const string PLAYER_1_REGULAR = "O";
        public const string PLAYER_1_KING = "U";
        public const string PLAYER_2_REGULAR = "X";
        public const string PLAYER_2_KING = "K";

        public static void PrintBoard(Piece[,] board)
        {
            int dimension = board.GetLength(0);

            // Create the board header
            StringBuilder headerStringBuilder = new StringBuilder();
            char currentLabel = 'A';

            for (int i = 0; i < dimension; i++)
            {
                headerStringBuilder.Append("   " + currentLabel);
                currentLabel++;
            }

            headerStringBuilder.Append(createLineSeperator(dimension));

            // Print the board
            StringBuilder boardStringBuilder = new StringBuilder();
            currentLabel = 'a';

            for (int i = 0; i < dimension; i++)
            {
                for(int j = 0; j < dimension; j++)
                {
                    if (j == 0) boardStringBuilder.Append(currentLabel + "|");

                    string currentPieceSymbol = " ";

                    if (board[i, j] != null)
                    {
                        currentPieceSymbol = getPieceSymbol(board[i, j]);
                    }

                    boardStringBuilder.Append(" " + currentPieceSymbol + " |");
                }

                currentLabel++;
                boardStringBuilder.Append(createLineSeperator(dimension));
            }

            // Print the StringBuilders 
            System.Console.Out.Write(headerStringBuilder.ToString());
            System.Console.Out.Write(boardStringBuilder.ToString());
        }

        private static string getPieceSymbol(Piece i_Piece)
        {
            string pieceSymbol = "";
            switch (i_Piece.PieceSymbol)
            {
                case ePieceSymbol.player1regular:
                    pieceSymbol = PLAYER_1_REGULAR;
                    break;
                case ePieceSymbol.player1king:
                    pieceSymbol = PLAYER_1_KING;
                    break;
                case ePieceSymbol.player2regular:
                    pieceSymbol = PLAYER_2_REGULAR;
                    break;
                case ePieceSymbol.player2king:
                    pieceSymbol = PLAYER_2_KING;
                    break;
                default:
                    break;
            }

            return pieceSymbol;
        }

        private static string createLineSeperator(int i_BoardSize)
        {
            StringBuilder lineSeperator = new StringBuilder();
            lineSeperator.Append(System.Environment.NewLine);

            int lineLength = (i_BoardSize * 4) + 2;

            for (int i = 0; i < lineLength; i++)
            {
                lineSeperator.Append("=");
            }

            lineSeperator.Append(System.Environment.NewLine);

            return lineSeperator.ToString();
        }

        public static string GetUserInput()
        {
            // Get an input from the user
            return null;
        }    

        public static void ClearScreen()
        {
            // call the dll function that clears the screen
        }

        private static bool parseMove(string i_UserInput, Move i_Move)
        {
            // parse the user input into a move type or return false
            return false;
        }

        public static void PrintMessage(eListOfMessages i_Message)
        {
            // prints some message to the screen
        }

        public static void PrintListOfMoves(List<Move> i_ListofMoves)
        {
            StringBuilder listOfMoveStringBuilder = new StringBuilder();
            foreach(Move move in i_ListofMoves)
            {
                listOfMoveStringBuilder.Append(move.ToString());
                listOfMoveStringBuilder.Append(System.Environment.NewLine);
            }

            System.Console.Out.Write(listOfMoveStringBuilder);
        }
    }
}
