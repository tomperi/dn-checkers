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
        }
    }
}
