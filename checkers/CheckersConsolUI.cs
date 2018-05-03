using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;

namespace checkers
{

    class CheckersConsolUI
    {
        public const string PLAYER_1_REGULAR = "O";
        public const string PLAYER_1_KING = "U";
        public const string PLAYER_2_REGULAR = "X";
        public const string PLAYER_2_KING = "K";

        public static void PrintBoard(Piece[,] i_Board)
        {
            int dimension = i_Board.GetLength(0);

            // Create the i_Board header
            StringBuilder headerStringBuilder = new StringBuilder();
            char currentLabel = 'A';

            for (int i = 0; i < dimension; i++)
            {
                headerStringBuilder.Append("   " + currentLabel);
                currentLabel++;
            }

            headerStringBuilder.Append(createLineSeperator(dimension));

            // Print the i_Board
            StringBuilder boardStringBuilder = new StringBuilder();
            currentLabel = 'a';

            for (int i = 0; i < dimension; i++)
            {
                for(int j = 0; j < dimension; j++)
                {
                    if (j == 0) boardStringBuilder.Append(currentLabel + "|");

                    string currentPieceSymbol = " ";

                    if (i_Board[i, j] != null)
                    {
                        currentPieceSymbol = getPieceSymbol(i_Board[i, j]);
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

        public static string GetUserNameInput(int i_MaxNameSize)
        {
            System.Console.Out.WriteLine("Enter the i_Player name:");
            string name = System.Console.In.ReadLine();

            while(name.Length > i_MaxNameSize)
            {
                System.Console.Out.WriteLine("A name should be less then " + i_MaxNameSize + " characters.");
                name = System.Console.In.ReadLine();
            }

            return name;
        }

        public static int GetUserBoardSize(int[] i_AllowedBoardSizes)
        {
            StringBuilder inputRequest = new StringBuilder();
            string allowedSizesString = intArrayToString(i_AllowedBoardSizes);
            inputRequest.AppendFormat("Choose a i_Board size: {0}", allowedSizesString);
            PrintMessage(inputRequest);

            int size = 0;
            bool validSize = false;

            while(!validSize)
            {
                string userInput = System.Console.In.ReadLine();
                if (!Int32.TryParse(userInput, out size))
                {
                    System.Console.WriteLine("Board size can only be an integer!");
                }
                else if ((size != 6) && (size != 8) && (size != 10))
                {
                    System.Console.WriteLine("Board size can be {0} only.", allowedSizesString);
                }
                else
                {
                    validSize = true;
                }
            }

            return size;
        }

        private static string intArrayToString(int[] i_Array)
        {
            StringBuilder arrayString = new StringBuilder();
            if ((i_Array == null) || (i_Array.Length == 0))
            {
                arrayString.Append("");
            }
            else if (i_Array.Length == 1)
            {
                arrayString.Append(i_Array[0]);
            }
            else
            {
                for (int i = 0; i < i_Array.Length - 1; i++)
                {
                    arrayString.AppendFormat("{0}, ", i_Array[i]);
                }

                arrayString.AppendFormat("or {0}", i_Array[i_Array.Length - 1]);
            }

            return arrayString.ToString();
        }

        public static ePlayerType GetPlayerType()
        {
            System.Console.Out.WriteLine("Choose a i_Player - H/C (Human/Computer)");
            string userInput = System.Console.In.ReadLine();
            bool validPlayerType = false;
            ePlayerType choosenPlayerType = ePlayerType.Human;

            while(!validPlayerType)
            {
                if ((userInput == "C") || (userInput == "c"))
                {
                    choosenPlayerType = ePlayerType.Computer;
                    validPlayerType = true;
                }
                else if ((userInput == "H") || (userInput == "h"))
                {
                    choosenPlayerType = ePlayerType.Human;
                    validPlayerType = true;
                }
                else
                {
                    System.Console.Out.WriteLine("Invalid i_Player type. Enter H or C only");
                    userInput = System.Console.In.ReadLine();
                }
            }

            return choosenPlayerType;
        }

        public static Move GetUserMoveInput(Player i_Player)
        {
            // TODO: Allow a user to quit (doesn't return a move, should return something else)
            System.Console.Out.Write("{0}'s turn (SYMBOL): ", i_Player.Name);
            Move move;
            while (!TryParseMove(System.Console.In.ReadLine(), out move))
            {
                System.Console.Out.WriteLine("Move syntax invalid. Enter a new move:");
            }

            move.Player = i_Player.PlayerPosition; 

            return move;
        }    

        public static void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public static bool TryParseMove(string i_UserInput, out Move o_ParsedMove)
        {
            // Gets a user input, if valid returns it as 2 positions
            bool validSyntax = true;
            string moveString = i_UserInput.ToLower();
            int[] moveInteger = new int[5]; 
            o_ParsedMove = null;

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
            }

            // If all checks are valid, create new positions
            if (validSyntax)
            {
                for(int i = 0; i < moveInteger.Length; i++)
                {
                    moveInteger[i] = (int)moveString[i] - 'a';
                }
                Position startPosition = new Position(moveInteger[1], moveInteger[0]);
                Position endPosition = new Position(moveInteger[4], moveInteger[3]);
                o_ParsedMove = new Move(startPosition, endPosition);
            }

            return validSyntax;
        }

        public static void PrintMessage(eListOfMessages i_Message)
        {
            // prints some i_Message to the screen
        }

        public static void PrintMessage(StringBuilder i_Message)
        {
            System.Console.Out.WriteLine(i_Message.ToString());
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
            if (i_Move == null) return;

            StringBuilder moveStringBuilder = new StringBuilder();

            char startRow = (char) ('A' + i_Move.Begin.Row);
            char startCol = (char) ('a' + i_Move.Begin.Col);
            moveStringBuilder.Append(startCol + "" + startRow + ">");

            char endRow = (char) ('A' + i_Move.End.Row);
            char endCol = (char) ('a' + i_Move.End.Col);
            moveStringBuilder.Append(endCol + "" + endRow);

            System.Console.Out.Write(moveStringBuilder.ToString());
        }
    }
}
