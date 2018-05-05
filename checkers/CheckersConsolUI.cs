using System;
using System.Collections;
using System.Text;

namespace checkers
{
    public class CheckersConsolUI
    {
        // Consol UI settings
        private const char k_Player1Regular = 'O';
        private const char k_Player1King = 'U';
        private const char k_Player2Regular = 'X';
        private const char k_Player2King = 'K';

        private const char k_ColumnBoardHeader = 'A';
        private const char k_RowsBoardHeader = 'a';

        private const char k_HumenSymbol = 'h';
        private const char k_ComputerSymbol = 'c';

        private const string k_Quit = "quit";
        private const string k_QuitShortcut = "q";

        private const string k_Yes = "yes";
        private const string k_YesShortcut = "y";

        private const string k_No = "no";
        private const string k_NoShortcut = "n";

        public void PrintBoard(Piece[,] i_Board)
        {
            int dimension = i_Board.GetLength(0);

            // Create the board header
            StringBuilder headerStringBuilder = new StringBuilder();
            char currentLabel = k_ColumnBoardHeader;

            for (int i = 0; i < dimension; i++)
            {
                headerStringBuilder.Append("   " + currentLabel);
                currentLabel++;
            }

            headerStringBuilder.Append(createLineSeperator(dimension));

            // Print the board
            StringBuilder boardStringBuilder = new StringBuilder();
            currentLabel = k_RowsBoardHeader;

            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    if (j == 0)
                    {
                        boardStringBuilder.Append(currentLabel + "|");
                    }

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
 
            printMessage(headerStringBuilder);
            printMessage(boardStringBuilder);
        }

        private static bool tryParseQuit(string i_UserInput)
        {
            return (i_UserInput == k_Quit) || (i_UserInput == k_QuitShortcut);
        }

        private static string intArrayToString(int[] i_Array)
        {
            StringBuilder arrayString = new StringBuilder();
            if (i_Array == null || i_Array.Length == 0)
            {
                arrayString.Append(string.Empty);
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

        private static char getPlayerSymbol(Player i_Player)
        {
            return i_Player.PlayerPosition == ePlayerPosition.TopPlayer ? k_Player1Regular : k_Player2Regular;
        }

        private char getPieceSymbol(Piece i_Piece)
        {
            char pieceSymbol = ' ';
            switch (i_Piece.PieceSymbol)
            {
                case ePieceSymbol.Player1Regular:
                    pieceSymbol = k_Player1Regular;
                    break;
                case ePieceSymbol.Player1King:
                    pieceSymbol = k_Player1King;
                    break;
                case ePieceSymbol.Player2Regular:
                    pieceSymbol = k_Player2Regular;
                    break;
                case ePieceSymbol.Player2King:
                    pieceSymbol = k_Player2King;
                    break;
            }

            return pieceSymbol;
        }

        private string createLineSeperator(int i_BoardSize)
        {
            StringBuilder lineSeperator = new StringBuilder();
            lineSeperator.Append(Environment.NewLine);

            int lineLength = (i_BoardSize * 4) + 2;

            for (int i = 0; i < lineLength; i++)
            {
                lineSeperator.Append("=");
            }

            lineSeperator.Append(Environment.NewLine);

            return lineSeperator.ToString();
        }

        public string GetUserNameInput(int i_MaxNameSize)
        {
            printMessage(Strings.GetPlayerName);
            string name = getInputFromUser();

            while (name.Length > i_MaxNameSize)
            {
                printMessage(string.Format(Strings.NameTooLong, i_MaxNameSize));
                name = getInputFromUser();
            }

            return name;
        }

        public int GetUserBoardSize(int[] i_AllowedBoardSizes)
        {
            string allowedSizesString = intArrayToString(i_AllowedBoardSizes);
            printMessage(string.Format(Strings.ChooseBoardSize, allowedSizesString));

            int size = 0;
            bool validSize = false;

            while (!validSize)
            {
                string userInput = getInputFromUser();
                if (!int.TryParse(userInput, out size))
                {
                    printMessage(Strings.BoardMustBeInteger);
                } 
                else if (!((IList)i_AllowedBoardSizes).Contains(size))
                {
                    printMessage(string.Format(Strings.BoardSize, allowedSizesString));
                }
                else
                {
                    validSize = true;
                }
            }

            return size;
        }

        public ePlayerType GetPlayerType()
        {
            printMessage(Strings.ChoosePlayer);
            string userInput = getInputFromUser().ToLower();
            bool validPlayerType = false;
            ePlayerType choosenPlayerType = ePlayerType.Human;

            while (!validPlayerType)
            {
                if (userInput == k_ComputerSymbol.ToString())
                {
                    choosenPlayerType = ePlayerType.Computer;
                    validPlayerType = true;
                }
                else if (userInput == k_HumenSymbol.ToString())
                {
                    choosenPlayerType = ePlayerType.Human;
                    validPlayerType = true;
                }
                else
                {
                    printMessage(Strings.InvalidPlayerType);
                    userInput = getInputFromUser();
                }
            }

            return choosenPlayerType;
        }

        public Move GetUserMoveInput(Player i_Player, out bool o_Quit)
        {
            printMessage(string.Format(Strings.Turn, i_Player.Name, getPlayerSymbol(i_Player)));
            Move move = null;
            bool validMove = false, validQuit = false;

            while (!(validMove || validQuit))
            {
                string userInput = getInputFromUser().ToLower();
                validQuit = tryParseQuit(userInput);

                if (!validQuit)
                {
                    validMove = TryParseMove(userInput, out move);
                }

                if (!validMove && !validQuit)
                {
                    printMessage(Strings.MoveSyntaxInvalid);
                }
            }

            if (validMove)
            {
                move.Player = i_Player.PlayerPosition;
            }

            o_Quit = validQuit;

            return move;
        }

        public bool GetUserAnotherGameInput()
        {
            bool validInput = false;
            bool anotherGame = false;

            printMessage(Strings.AnotherGame);
            while (!validInput)
            {
                string userInput = getInputFromUser().ToLower();
                switch (userInput)
                {
                    case k_Yes:
                    case k_YesShortcut:
                        validInput = true;
                        anotherGame = true;
                        break;
                    case k_No:
                    case k_NoShortcut:
                        validInput = true;
                        break;
                    default:
                        printMessage(Strings.InValidInputYN);
                        break;
                }
            }

            return anotherGame;
        }

        public void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public bool TryParseMove(string i_UserInput, out Move o_ParsedMove)
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
                for (int i = 0; i < moveInteger.Length; i++)
                {
                    moveInteger[i] = moveString[i] - 'a';
                }

                Position startPosition = new Position(moveInteger[1], moveInteger[0]);
                Position endPosition = new Position(moveInteger[4], moveInteger[3]);
                o_ParsedMove = new Move(startPosition, endPosition);
            }

            return validSyntax;
        }

        private void printMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        private void printMessage(StringBuilder i_Message)
        {
            Console.Write(i_Message.ToString());
        }

        private string getInputFromUser()
        {
            return Console.ReadLine();
        }

        public string PrintMove(Move i_Move)
        {
            string move;

            if (i_Move == null)
            {
                move = string.Empty;
            }
            else
            {
                char startCol = (char)(k_ColumnBoardHeader + i_Move.Begin.Col);
                char startRow = (char)(k_RowsBoardHeader + i_Move.Begin.Row);
                char endCol = (char)(k_ColumnBoardHeader + i_Move.End.Col);
                char endRow = (char)(k_RowsBoardHeader + i_Move.End.Row);

                move = string.Format(Strings.MoveFormat, startCol, startRow, endCol, endRow);
            }

            return move;
        }

        public void PrintScoreBoard(string i_Player1Name, int i_Player1Points, string i_Player2Name, int i_Player2Points)
        {
            printMessage(string.Format(
                Strings.Scores,
                i_Player1Name,
                i_Player1Points,
                i_Player2Name,
                i_Player2Points));
        }

        public void PrintLastMove(Player i_Player)
        { 
            char symbol = getPlayerSymbol(i_Player);
            Move lastMove = i_Player.GetLastMove();
            if (lastMove != null)
            {
                printMessage(string.Format(
                    Strings.Move,
                    i_Player.Name,
                    symbol,
                    PrintMove(lastMove)));
            }
        }

        public void EndGameMessage()
        {
            printMessage(Strings.EndGame);
            Console.In.ReadLine();
        }

        public void Draw()
        {
            printMessage(Strings.Draw);
        }

        public void Winning(string i_Winner)
        {
            printMessage(string.Format(Strings.Winning, i_Winner));
        }

        public void PlayerForfited(string i_Forfiter)
        {
            printMessage(string.Format(Strings.Forfiting, i_Forfiter));
        }

        public void PlayerRecivedPoints(string i_PlayerName, int i_PlayerPoints)
        {
            printMessage(string.Format(Strings.PlayerRecivedPoints, i_PlayerName, i_PlayerPoints));
        }

        public void PointStatus(string i_Player1Name, int i_Player1Points, string i_Player2Name, int i_Player2Points)
        {
            printMessage(string.Format(Strings.TotalPointsHead));
            printMessage(string.Format(Strings.PlayerPoints, i_Player1Name, i_Player1Points));
            printMessage(string.Format(Strings.PlayerPoints, i_Player2Name, i_Player2Points));
        }

        public void NotAllowedForfit()
        {
            printMessage(Strings.NotAllowdForfit);
        }

        public void InValidMove()
        {
            printMessage(Strings.InvalidMove);
        }
    }
}