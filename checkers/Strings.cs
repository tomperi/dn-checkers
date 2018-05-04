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
        protected internal static string ChoosePlayer = "Choose a player - H/C (Human/Computer)";
        protected internal static string InvalidPlayerType = "Invalid player type. Enter H or C only";
        protected internal static string Turn = "{0}'s turn ({1}): ";
        protected internal static string MoveSyntaxInvalid= "Move syntax invalid. Enter a new move:";
        protected internal static string AnotherGame= "Would you like to play another game? Y/n";
        protected internal static string InValidInputYN= "Invalid input. Write Y/n only";
        protected internal static string Scores= "{0}'s points: {1} -- {2}'s points: {3}";
        protected internal static string Move= "{0}'s move was ({1}): {2}";
        protected internal static string ChooseBoardSize= "Choose a board size: {0}";
        protected internal static string MoveFormat= "{0}{1}>{2}{3}";
        protected internal static string EndGame= "Thank you for playing, hope you enjoyed!";
        protected internal static string Draw = "Game ended in a draw";
        protected internal static string Winning = "{0} has won!";
        

        //Game manager
        protected internal static string ChooseBoardSiz1e = "Choose a board size: {0}";

    }
}
