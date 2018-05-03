using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;

namespace checkers
{

    class CheckersConsolUI
    {
        public const char PLAYER_1_REGULAR = 'O';
        public const char PLAYER_1_KING = 'U';
        public const char PLAYER_2_REGULAR = 'X';
        public const char PLAYER_2_KING = 'K';

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

                    char currentPieceSymbol = ' ';

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

        private static char getPieceSymbol(Piece i_Piece)
        {
            char pieceSymbol = ' ';
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
            System.Console.Out.WriteLine("Enter the player name:");
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
            inputRequest.AppendFormat("Choose a board size: {0}", allowedSizesString);
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
            System.Console.Out.WriteLine("Choose a player - H/C (Human/Computer)");
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

        public static Move GetUserMoveInput(Player i_Player, out bool o_Quit)
        {
            System.Console.Out.Write("{0}'s turn ({1}): ", i_Player.Name, getPlayerSymbol(i_Player));
            Move move = null;
            bool validMove = false, validQuit = false;

            while (!(validMove || validQuit))
            {
                string userInput = System.Console.In.ReadLine().ToLower();
                validQuit = TryParseQuit(userInput);
     
                if (!validQuit)
                {
                    validMove = TryParseMove(userInput, out move);
                }

                if (!validMove && !validQuit)
                {
                    System.Console.Out.WriteLine("Move syntax invalid. Enter a new move:");
                }
            }

            if (validMove)
            {
                move.Player = i_Player.PlayerPosition;
            }

            o_Quit = validQuit;

            return move;
        }

        private static bool TryParseQuit(string i_UserInput)
        {
            return (i_UserInput == "quit") || (i_UserInput == "q");
        }

        public static bool GetUserAnotherGameInput()
        {
            bool validInput = false;
            bool anotherGame = false;

            System.Console.Out.WriteLine("Would you like to play another game? Y/n");
            while (!validInput)
            {
                string userInput = System.Console.In.ReadLine().ToLower();
                if (userInput == "y" || userInput == "yes")
                {
                    validInput = true;
                    anotherGame = true;
                } else if ((userInput == "n") || (userInput == "no"))
                {
                    validInput = true;
                    anotherGame = false;
                }
                else
                {
                    System.Console.Out.WriteLine("Invalid input. Write Y/n only");
                }
            }

            return anotherGame;
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

        public static string PrintMove(Move i_Move)
        {
            // Print a move in the format COLrow>COLrow
            if (i_Move == null) return "";

            StringBuilder moveStringBuilder = new StringBuilder();

            char startCol = (char)('A' + i_Move.Begin.Col);
            char startRow = (char) ('a' + i_Move.Begin.Row);
            moveStringBuilder.Append(startCol + "" + startRow + ">");

            char endCol = (char)('A' + i_Move.End.Col);
            char endRow = (char) ('a' + i_Move.End.Row);
            moveStringBuilder.Append(endCol + "" + endRow);

            return (moveStringBuilder.ToString());
        }

        public static void PrintScoreBoard(string player1Name, int player1Points, string player2Name, int player2Points)
        {

            System.Console.Out.WriteLine("{0}'s points: {1} -- {2}'s points: {3}", 
                player1Name, player1Points, player2Name, player2Points);
        }

        public static void PrintLastMove(Player i_Player)
        {
            char symbol = getPlayerSymbol(i_Player);
            Move lastMove = i_Player.GetLastMove();
            if (lastMove != null)
                System.Console.Out.WriteLine("{0}'s move was ({1}): {2}", 
                    i_Player.Name, getPlayerSymbol(i_Player), PrintMove(lastMove));
        }

        public static char getPlayerSymbol(Player i_Player)
        {
            return (int) i_Player.PlayerPosition == 1 ? PLAYER_1_REGULAR : PLAYER_2_REGULAR;
        }
    }
}
