using System;
using System.Collections.Generic;
using System.Text;

namespace checkers
{
    class CheckersConsolUI
    {
        public const int MAX_NAME_SIZE = 20;

        protected static void PrintBoard(Board i_Board)
        {
            // print the board
        }

        protected static String GetUserInput()
        {
            // Get an input from the user
            return null;
        }

        protected static void ClearScreen()
        {
            // call the dll function that clears the screen
        }

        private static bool checkName(String i_Name)
        {
            return (i_Name.Length <= MAX_NAME_SIZE);

        }

        private static bool parseMove(String i_UserInput, Move i_Move)
        {
            // parse the user input into a move type or return false
            return false;
        }

        protected void PrintMessage(listOfMessages i_Message)
        {
            // prints some message to the screen
        }
    }
}
