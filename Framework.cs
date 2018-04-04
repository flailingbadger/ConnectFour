/********************************************************
 *
 *  Project :  A03 Connect Four
 *  File    :  Framework.cs
 *  Name    :  Michael Dey Melinda Frandsen
 *  Date    :  5 November 2017
 *
 *  Description : (Narrative desciption, not code)
 *
 *    1) What is the purpose of the code; what problem does the code solve.
 *    		This class is used to help represent the foreground game board framework (the blue grid)
 *
 *    2) What data-structures are used.
 *          int[,]
 *
 *    3) What algorithms, techniques, etc. are used in implementing the data structures.
 *          for loops
 *          
 *    4) What methods are implemented (optional).
 *           PropigateFramework(), AddFrameWorkToArray()
 *          
 *  Changes :  <Description|date of modifications>
 *
 ********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    class Framework
    {
        int[,] frame;
        public Framework()
        {
            frame = new int[,] {
                {3, 3, 3, 3},
                {3, 0, 0, 3},
                {3, 0, 0, 3},
                {3, 3, 3, 3},
            };

        }
        /// <summary>
        ///     loop through 4x4 chuncks of gameBoard and place frame Pieces inside
        ///     calls addFrameworkToArray to add one piece of frame to x,y values of gameBoard array
        ///     iterates x,y values calling addFrameworkToArray at every x,y interval
        ///     which adds a framework piece to the entire gameBoard array
        /// </summary>
        /// <param name="gameBoard"></param>
        public void PropigateFramework(int[,] gameBoard)
        {
            int x = 4; //set to 4 to skip first row
            int y = 0;

            for (int i = 1; i < 8; i++) //make 1 row 7 pieces wide iterates through 7 columns of gameBoard
            {
                for (int j = 0; j < 7; j++) //make 6 pieces tall iterates through 6 rows of gameBoard
                {
                    y = 4 * j;  //move y value to next row
                    AddFrameWorkToArray(x, y, gameBoard); //insert frame array to gameBoard at x,y
                }
                x = 4 * i;  //move x value to next column
            }


        }

        /// <summary>
        ///     loop through frame array and add 1 version of the frame array to gameBoard array
        ///     place frame array into gameBoard array using x,y as starting point in gameboard array
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="gameBoard"></param>
        private void AddFrameWorkToArray(int x, int y, int[,] gameBoard)
        {
            for (int i = 0; i < frame.GetLength(0); i++)  //loops through all columns of frame
            {
                for (int j = 0; j < frame.GetLength(1); j++)  //loops though all rows of frame
                {
                    if (frame[i, j] != 0)
                        gameBoard[i + x, j + y] = frame[i, j]; //inserts into gameBoard array
                }
            }
        }
    }
}
