using System;
using System.Collections.Generic;
using System.Text;

namespace checkers
{
    public class Strings
    {
        // Console UI
        protected internal static string GetPlayerName = "Enter the player name:";
        protected internal static string NameTooLong = "A name should be less then {0} characters.";
        protected internal static string BoardMustBeInteger = "Board size can only be an integer!";
        protected internal static string BoardSize = "Board size can be {0} only.";
        protected internal static string ChoosePlayer = "Choose a player - h/c (Human/Computer)";
        protected internal static string InvalidPlayerType = "Invalid player type. Enter h or c only";
        protected internal static string Turn = "{0}'s turn ({1}): ";
        protected internal static string MoveSyntaxInvalid = "Move syntax invalid. Enter a new move:";
        protected internal static string AnotherGame = "Would you like to play another game? Y/n";
        protected internal static string InValidInputYN = "Invalid input. Write Y/n only";
        protected internal static string Scores  = "{0}'s points: {1} -- {2}'s points: {3}";
        protected internal static string Move = "{0}'s move was ({1}): {2}";
        protected internal static string ChooseBoardSize = "Choose a board size: {0}";
        protected internal static string MoveFormat = "{0}{1}>{2}{3}";
        protected internal static string EndGame = "Thank you for playing, hope you enjoyed!";
        protected internal static string Draw = "Game ended in a draw";
        protected internal static string Winning = "{0} has won!";
        protected internal static string Forfiting = "{0} has forfited the game :(";
        protected internal static string PlayerRecivedPoints = "{0} recieved {1} points";
        protected internal static string TotalPointsHead = "Total number of points:";
        protected internal static string PlayerPoints = "{0} has {1} points";
        protected internal static string NotAllowdForfit = "You're not allowed to forfit when you're on the lead";
        protected internal static string InvalidMove = "Invalid move, enter a new one:";
        protected internal static string PositionFormat = "({0},{1})";
    }
}
