/********************************************************
 *
 *  Project :  A03 Connect Four
 *  File    :  Program.cs
 *  Name    :  Michael Dey Melinda Frandsen
 *  Date    :  5 November 2017
 *
 *  Description : (Narrative desciption, not code)
 *
 *    1) What is the purpose of the code; what problem does the code solve.
 *    		This is the main class that runs the Connect Four application
 *
 *    2) What data-structures are used.
 *          Classes
 *
 *    3) What algorithms, techniques, etc. are used in implementing the data structures.
 *
 *    4) What methods are implemented (optional).
 *          PlaySound(), DrawArray, Console.Write, Tread.Sleep, DrawGameBoard(), DeclareWinner(), GetWinner
 *  Changes :  <Description|date of modifications>
 *
 ********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Program
    {
        static void Main(string[] args)
        {
            GameInterface gi = new GameInterface();
            gi.PlaySound(4);
            gi.DrawArray(gi.title);
            Console.WriteLine("\nUse the left and right arrow keys to select the \n" +
            "position of your game piece.\n\n" +
            "Select ENTER to drop the piece into place.");
            Thread.Sleep(3000);

            GameEngine ge = new GameEngine(7, 7);
            gi.ConvertSmallArrayToLargeArray(ge.GetGameGrid());
            Framework fw = new Framework();
            fw.PropigateFramework(gi.gameBoard);

            Console.Clear();
            do
            {
                gi.DrawGameBoard();
                ge.GetUserInput();
                gi.PlaySound(ge.GetSound());
                gi.AnimateGamePiece(ge.animStartY, ge.animStartX, ge.animEndY, ge.animEndX, ge.animColor);
                gi.ConvertSmallArrayToLargeArray(ge.GetGameGrid());
                if (ge.CheckIfWinner())
                {
                    gi.DrawGameBoard();
                    Thread.Sleep(1000);
                    gi.DeclareWinner(ge.GetWinner()); //send winner info to gi
                    break;
                }

            } while (!ge.gameEnd);

        }
    }
}