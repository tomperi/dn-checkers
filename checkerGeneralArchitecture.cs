// add e_ to all enums
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
	Board m_Board;
	Player? m_Player1;
	Player? m_Player2;
	Move?[] m_MoveHistory;
	Player? m_CurrentPlayer;

	public GameManager()
	{
		// default constructor
	}

	public void Start()
	{
		// the function that runs the entire game
	}

	private moveStatus checkMoveLegalality(Move i_Move)
	{
		// check if the move is legal
	} 

	private Move getRandomMove(Player i_Player)
	{
		// returns a move for a specific player, from all possible moves 
	}

	private void changeActivePlayer()
	{
		// changes the active player to the other one
	}

}

protected class Board 
{
	Piece?[][] m_Board;
	boardSize m_Size;

	protected Board() 
	{
		// default constructor
	}

	protected Board(boardSize i_size)
	{
		// create a new board of specific size, init it with pieces
	}

	private void initBoard() 
	{
		// initialized the board with the right amount of pieces
		// init null where there are no objects
	}

	protected void MovePiece(Move i_Move, out moveStatus) // naming convention for out variables? 
	{
		// if the move is legit, move the piece
	}

	protected Move[] GetPossibleMoves(Player i_CurrentPlayer, Move i_LastMove)
	{
		// returns the list of all possible moves for a specific player
		// if the last move was a jump, first check if another jump is possible
	}

	protected Piece[][] GetBoard()
	{
		// returns the board matrix
	}

	protected gameStatus GetGameStatus() 
	{
		// check the current game status, without returning the winner and points
	}

	protected gameStatus GetGameStatus(out Player winner, out int points) 
	{
		// check the current game status
		// if there is a win or a draw, return the winner and the amount of points he got
	}

}

protected class CheckersConsolUI
{

	PUBLIC STATIC FINAL int MAX_NAME_SIZE = 20;

	protected static void PrintBoard(Board i_Board)
	{
		// print the board
	}

	protected static String GetUserInput() 
	{
		 // Get an input from the user
	}

	protected static void ClearScreen()
	{
		// call the dll function that clears the screen
	}

	private static bool checkName(String i_Name)
	{
		return (i_Name.Length <= MAX_NAME_SIZE)
	}

	private static bool parseMove(String i_UserInput, move i_Move)
	{
		// parse the user input into a move type or return false
	}

	protected void PrintMessage(listOfMessages i_Message) 
	{
		// prints some message to the screen
	}

}

protected class Player 
{
	private String? m_Name;
	private playerType m_Type;
	private pieceColor m_Color;
	private Move?[] m_MoveHistory;
	private int m_Points = 0;

	protected Name 
	{
		get
		{
			return m_Name;
		}
		set
		{
			m_Name = value; 
		}
	}

	protected Points 
	{
		get
		{
			return m_Points; 
		}

		set
		{
			m_Points = value;
		}
	}

	protected Type
	{
		get 
		{
			return m_Type;
		}
		set
		{
			m_Type = value;
		}
	}

	protected Color
	{
		get
		{
			return m_Color;
		}
		set
		{
			m_Color = value;
		}

	}

	protected void ClearMoveHistory()
	{
		// clears the move history, in order to start a new game
	}

	protected void AddMove()
	{
		// add a move to the players move history
	}
}


protected struct Move 
{
	private Position m_Begin;
	private Position m_End;
	private Player m_Player;
	private moveType m_Type;

	protected Move(Position i_Begin, Position i_End, Player i_Player)
	{
		// new move constructor 
	}

	// get begin 
	// get end
	// get player
	// get/set move type

}

protected struct Position 
{
	private int m_Row;
	private int m_Col;

	protected Position(int i_Row, int i_Col)
	{
		// new position constructor
	}
}

protected Struct Piece
{
	private pieceColor m_Color;
	private pieceType m_Type;

	protected Piece(pieceColor i_Color, pieceType i_Type) 
	{
		// new piece constructor
	}

	protected void SetKing() 
	{
		// change piece type from regular to king
	}
}

