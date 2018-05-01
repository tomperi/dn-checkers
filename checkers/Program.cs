using System.Collections.Generic;
using System.Threading;

namespace checkers
{
    public class Program
    {
        static void Main()
        {
            GameManager gameManager = new GameManager();
            gameManager.Start();

//            Move move = CheckersConsolUI.GetUserMoveInput();
//            
//            CheckersConsolUI.PrintMove(move);
//
//            CheckersConsolUI.TryParseMove("AA>BB", out Move? parsedMove);
//            if (parsedMove != null) 
//                CheckersConsolUI.PrintMove(parsedMove.Value);
//
//            System.Console.Out.WriteLine();
//
//            CheckersConsolUI.TryParseMove("YX>zU", out parsedMove);
//            if (parsedMove != null)
//                CheckersConsolUI.PrintMove(parsedMove.Value);
//
//            System.Console.Out.Write(CheckersConsolUI.GetUserNameInput());
//
//            System.Console.Out.WriteLine();
//
//            // Create a new instance of GameManager and run the game
//            Board board = new Board();
//            board.m_Board[3, 4] = new Piece(ePlayer.BottomPlayer);
//            board.m_Board[3, 4].SetKing();
//
//
////            board.m_Board[4, 1] = new Piece(ePlayer.TopPlayer);
////            board.m_Board[4, 1].SetKing();
//
//            CheckersConsolUI.PrintBoard(board.GetBoard());
//
//            List<Move> possibleMoves = board.GetPossibleMoves(ePlayer.TopPlayer, null);
//
//            System.Console.Out.WriteLine("Possible moves for top player");
//            CheckersConsolUI.PrintListOfMoves(possibleMoves);
//
//            possibleMoves = board.GetPossibleMoves(ePlayer.BottomPlayer, null);
//
//            System.Console.Out.WriteLine("Possible moves for bottom player");
//            CheckersConsolUI.PrintListOfMoves(possibleMoves);
//
//            // MOVE A "X" PIECE
//            Position startPosition = new Position(5,6);
//            Position endposiPosition = new Position(4,7);
//            Move newMove = new Move(startPosition, endposiPosition, ePlayer.BottomPlayer);
//
//            board.MovePiece(newMove, null, out eMoveStatus moveStatus);
//            System.Console.Out.WriteLine(moveStatus);
//
//            // REPRINT BOARD AND CALCULATE MOVES
//            CheckersConsolUI.PrintBoard(board.GetBoard());
//
//            possibleMoves = board.GetPossibleMoves(ePlayer.TopPlayer, null);
//
//            System.Console.Out.WriteLine("Possible moves for top player");
//            CheckersConsolUI.PrintListOfMoves(possibleMoves);
//
//            possibleMoves = board.GetPossibleMoves(ePlayer.BottomPlayer, null);
//
//            System.Console.Out.WriteLine("Possible moves for bottom player");
//            CheckersConsolUI.PrintListOfMoves(possibleMoves);

            System.Console.In.Read();
        }
    }
}
