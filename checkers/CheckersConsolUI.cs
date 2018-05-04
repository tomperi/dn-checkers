using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace checkers
{
    public class CheckersConsolUI
    {
        public const char PLAYER_1_REGULAR = 'O';
        public const char PLAYER_1_KING = 'U';
        public const char PLAYER_2_REGULAR = 'X';
        public const char PLAYER_2_KING = 'K';

        public const char COLUMN_BOARD_HEADER = 'A';
        public const char ROWS_BOARD_HEADER = 'a';

        public void PrintBoard(Piece[,] i_Board)
        {
            int dimension = i_Board.GetLength(0);

            // Create the i_Board header
            StringBuilder headerStringBuilder = new StringBuilder();
            char currentLabel = COLUMN_BOARD_HEADER;

            for (int i = 0; i < dimension; i++)
            {
                headerStringBuilder.Append("   " + currentLabel);
                currentLabel++;
            }

            headerStringBuilder.Append(createLineSeperator(dimension));

            // Print the i_Board
            StringBuilder boardStringBuilder = new StringBuilder();
            currentLabel = ROWS_BOARD_HEADER;

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

            // Print the StringBuilders 
            Console.Out.Write(headerStringBuilder.ToString());
            Console.Out.Write(boardStringBuilder.ToString());
        }

        private char getPieceSymbol(Piece i_Piece)
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
            Console.Out.WriteLine(Strings.GetPlayerName);
            string name = Console.In.ReadLine();

            while (name.Length > i_MaxNameSize)
            {
                Console.Out.WriteLine(Strings.NameTooLong, i_MaxNameSize);
                name = Console.In.ReadLine();
            }

            return name;
        }

        public int GetUserBoardSize(int[] i_AllowedBoardSizes)
        {
            string allowedSizesString = intArrayToString(i_AllowedBoardSizes);
            PrintMessage(string.Format(Strings.ChooseBoardSize, allowedSizesString));

            int size = 0;
            bool validSize = false;

            while (!validSize)
            {
                string userInput = Console.In.ReadLine();
                if (!int.TryParse(userInput, out size))
                {
                    Console.WriteLine(Strings.BoardMustBeInteger);
                }
                else if ((size != 6) && (size != 8) && (size != 10))
                {
                    Console.WriteLine(Strings.BoardSize, allowedSizesString);
                }
                else
                {
                    validSize = true;
                }
            }

            return size;
        }

        private string intArrayToString(int[] i_Array)
        {
            StringBuilder arrayString = new StringBuilder();
            if ((i_Array == null) || (i_Array.Length == 0))
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

        public ePlayerType GetPlayerType()
        {
            Console.Out.WriteLine(Strings.ChoosePlayer);
            string userInput = Console.In.ReadLine();
            bool validPlayerType = false;
            ePlayerType choosenPlayerType = ePlayerType.Human;

            while (!validPlayerType)
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
                    Console.Out.WriteLine(Strings.InvalidPlayerType);
                    userInput = Console.In.ReadLine();
                }
            }

            return choosenPlayerType;
        }

        public Move GetUserMoveInput(Player i_Player, out bool o_Quit)
        {
            Console.Out.Write(Strings.Turn, i_Player.Name, getPlayerSymbol(i_Player));
            Move move = null;
            bool validMove = false, validQuit = false;

            while (!(validMove || validQuit))
            {
                string userInput = Console.In.ReadLine().ToLower();
                validQuit = TryParseQuit(userInput);

                if (!validQuit)
                {
                    validMove = TryParseMove(userInput, out move);
                }

                if (!validMove && !validQuit)
                {
                    Console.Out.WriteLine(Strings.MoveSyntaxInvalid);
                }
            }

            if (validMove)
            {
                move.Player = i_Player.PlayerPosition;
            }

            o_Quit = validQuit;

            return move;
        }

        private bool TryParseQuit(string i_UserInput)
        {
            return (i_UserInput == "quit") || (i_UserInput == "q");
        }

        public bool GetUserAnotherGameInput()
        {
            bool validInput = false;
            bool anotherGame = false;

            Console.Out.WriteLine(Strings.AnotherGame);
            while (!validInput)
            {
                string userInput = Console.In.ReadLine().ToLower();
                if (userInput == "y" || userInput == "yes")
                {
                    validInput = true;
                    anotherGame = true;
                }
                else if ((userInput == "n") || (userInput == "no"))
                {
                    validInput = true;
                    anotherGame = false;
                }
                else
                {
                    Console.Out.WriteLine(Strings.InValidInputYN);
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
                    moveInteger[i] = (int)moveString[i] - 'a';
                }

                Position startPosition = new Position(moveInteger[1], moveInteger[0]);
                Position endPosition = new Position(moveInteger[4], moveInteger[3]);
                o_ParsedMove = new Move(startPosition, endPosition);
            }

            return validSyntax;
        }

        public void PrintMessage(string i_Message)
        {
            Console.WriteLine(i_Message);
        }

        public void PrintMessage(StringBuilder i_Message)
        {
            Console.Out.WriteLine(i_Message.ToString());
        }

        public void PrintListOfMoves(List<Move> i_ListofMoves)
        {
            StringBuilder listOfMoveStringBuilder = new StringBuilder();
            foreach (Move move in i_ListofMoves)
            {
                listOfMoveStringBuilder.Append(move.ToString());
                listOfMoveStringBuilder.Append(Environment.NewLine);
            }

            Console.Out.Write(listOfMoveStringBuilder);
        }

        public string PrintMove(Move i_Move)
        {
            // Print a move in the format COLrow>COLrow
            if (i_Move == null)
            {
                return string.Empty;
            }

            StringBuilder moveStringBuilder = new StringBuilder();

            char startCol = (char)(COLUMN_BOARD_HEADER + i_Move.Begin.Col);
            char startRow = (char)(ROWS_BOARD_HEADER + i_Move.Begin.Row);
            char endCol = (char)(COLUMN_BOARD_HEADER + i_Move.End.Col);
            char endRow = (char)(ROWS_BOARD_HEADER + i_Move.End.Row);

            return string.Format(Strings.MoveFormat, startCol, startRow, endCol, endRow);
        }

        public void PrintScoreBoard(string player1Name, int player1Points, string player2Name, int player2Points)
        {
            Console.Out.WriteLine(
                Strings.Scores,
                player1Name,
                player1Points,
                player2Name,
                player2Points);
        }

        public void PrintLastMove(Player i_Player)
        {
            char symbol = getPlayerSymbol(i_Player);
            Move lastMove = i_Player.GetLastMove();
            if (lastMove != null)
            {
                Console.Out.WriteLine(
                    Strings.Move,
                    i_Player.Name,
                    getPlayerSymbol(i_Player),
                    PrintMove(lastMove));
            }
        }

        public char getPlayerSymbol(Player i_Player)
        {
            return (int)i_Player.PlayerPosition == 1 ? PLAYER_1_REGULAR : PLAYER_2_REGULAR;
        }

        public void EndGameMessage()
        {
            PrintMessage(Strings.EndGame);
            Console.In.ReadLine();
        }

        public void Draw()
        {
            PrintMessage(Strings.Draw);
        }

        public void Winning(string i_Winner)
        {
            PrintMessage(string.Format(Strings.Winning, i_Winner));
        }

        public void PlayerForfited(string i_Forfiter)
        {
            PrintMessage(string.Format(Strings.Forfiting, i_Forfiter));
        }

        public void PlayerRecivedPoints(string i_PlayerName, int i_PlayerPoints)
        {
            PrintMessage(string.Format(Strings.PlayerRecivedPoints, i_PlayerName, i_PlayerPoints));
        }

        public void PointStatus(string i_Player1Name, int i_Player1Points, string i_Player2Name, int i_Player2Points)
        {
            PrintMessage(string.Format(Strings.TotalPointsHead));
            PrintMessage(string.Format(Strings.PlayerPoints, i_Player1Name, i_Player1Points));
            PrintMessage(string.Format(Strings.PlayerPoints, i_Player2Name, i_Player2Points));
        }

        public void NotAllowedForfit()
        {
            PrintMessage(Strings.NotAllowdForfit);
        }

        public void InValidMove()
        {
            PrintMessage(Strings.InvalidMove);
        }
    }
}