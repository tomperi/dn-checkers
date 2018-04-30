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
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public static bool ParseMove(string i_UserInput, out Move? parsedMove)
        {
            // Gets a user input, if valid returns it as 2 positions
            bool validSyntax = true;
            string moveString = i_UserInput.ToLower();
            int[] moveInteger = new int[5]; 
            parsedMove = null;

            // A move should have 5 letters only, with '>' in the middle
            if ((moveString.Length != 5) || (moveString[2] != '>'))
            {
                validSyntax = false;
            }

            for (int i = 0; i < moveString.Length; i++)
            {
                if (!char.IsLetter(moveString[i]) && (moveString[i] != '>'))
                {
                    validSyntax = false;
                }
                else
                {
                    moveInteger[i] = (int) moveString[i] - 'a';
                }
            }

            // If all checks are valid, create new positions
            if (validSyntax)
            {
                Position startPosition = new Position(moveInteger[0], moveInteger[1]);
                Position endPosition = new Position(moveInteger[3], moveInteger[4]);
                parsedMove = new Move(startPosition, endPosition);
            }

            return validSyntax;
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

        public static void PrintMove(Move i_Move)
        {
            // Print a move in the format ROWcol>ROWCOL
            StringBuilder moveStringBuilder = new StringBuilder();

            char startRow = (char) ('A' + i_Move.Begin.Row);
            char startCol = (char) ('a' + i_Move.Begin.Col);
            moveStringBuilder.Append(startRow + "" + startCol + ">");

            char endRow = (char) ('A' + i_Move.End.Row);
            char endCol = (char) ('a' + i_Move.End.Col);
            moveStringBuilder.Append(endRow + "" + endCol);

            System.Console.Out.Write(moveStringBuilder.ToString());
        }
    }
}
