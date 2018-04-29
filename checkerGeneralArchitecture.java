public enum boardSize {small, medium, large}
public enum gameStatus {playing, win, draw}
public enum playerType {human, computer}
public enum pieceColor {black, white}
public enum pieceType {regular, king}
public enum moveType {regular, jump}
public enum moveStatus {legal, logicError} // syntax error should be checked in the UI part
public enum listOfMessages {} // all possible ui messages 

public class GameManager 
{
	Board board;
	Player player1;
	Player player2;
	Move[] moveHistory;
	Player currentPlayer;

	public GameManager()
	{
		// default constructor
	}

	public void Start()
	{
		// the function that runs the entire game
	}

}

public class Board 
{
	Piece[][] board;
	boardSize size;

	public Board() 
	{
		// default constructor
	}

	public Board(boardSize size)
	{
		// create a new board of specific size, init it with pieces
	}

	private void initBoard() 
	{
		// initialized the board with the right amount of pieces
	}

	public void MovePiece(Move move, out moveStatus)
	{
		// if the move is legit, move the piece
	}

	private moveStatus checkMoveLegalality(Move move)
	{
		// check if the move is legal
	} 

	public Move[] GetPossibleMoves(Player currentPlayer, Move lastMove)
	{
		// returns the list of all possible moves for a specific player
		// if the last move was a jump, first check if another jump is possible
	}

	public Piece[][] GetBoard()
	{
		// returns the board matrix
	}

	public gameStatus GetGameStatus() 
	{
		// check the current game status, without returning the winner and points
	}

	public gameStatus GetGameStatus(out Player winner, out int points) 
	{
		// check the current game status
		// if there is a win or a draw, return the winner and the amount of points he got
	}

}

public class CheckersConsolUI
{

	PUBLIC STATIC FINAL int MAX_NAME_SIZE = 20;
	public static void printBoard(Board board)
	{
		// print the board
	}

	public static String getUserInput() 
	{
		 // Get an input from the user
	}

	public static void clearScreen()
	{
		// call the dll function that clears the screen
	}

	private static boolean checkNameSize(String name)
	{
		return (name.length() <= MAX_NAME_SIZE)
	}

	private static boolean parseMove(String userInput, move move)
	{
		// parse the user input into a move type or return false
	}

	public void printMessage(listOfMessages) 
	{
		// prints some message to the screen
	}

}

public interface Player 
{
	playerType type;
	pieceColor color;
	Move[] moveHistory;
	int points;

	public abstract move GetMove() 

	public void ChangePoints(int amount)
	{
		points += decrease;
		// When losing a piece, decrease by 1
		// When losing a king, decrease by 4
		// When getting a king, increase by 3 (make sure this is true)
	} 

	public int GetPoints() 
	{
		return points;
	}

	public void ClearMoveHistory()
	{
		// clears the move history, in order to start a new game
	}
}

public class Human extends Player 
{
	String name;

	public Human(pieceColor color) 
	{
		type = playerType.human;
		this.color = color;
	}


	public move GetMove() 
	{
		// ask the UI for a move
	}

	public void SetName() 
	{
		// sets the players name
	}

}

public class Computer extends Player 
{
	public Computer(pieceColor color) 
	{
		type = playerType.computer;
		this.color = color;
	}

	public move GetMove()
	{
		// ask the board for a list of moves, return a random one
	}

}

public class Move 
{
	Position begin;
	Position End;
	int player;
	moveType type;

}

public class Position 
{
	int row;
	int col;
}

public class Piece
{
	pieceColor color;
	pieceType type;
}

