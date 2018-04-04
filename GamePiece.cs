/********************************************************
 *
 *  Project :  A03 Connect Four
 *  File    :  GamePiece.cs
 *  Name    :  Michael Dey Melinda Frandsen
 *  Date    :  5 November 2017
 *
 *  Description : (Narrative desciption, not code)
 *
 *    1) What is the purpose of the code; what problem does the code solve.
 *    		This class is used to help represent a game piece to be drawn to the console
 *    		it is used by the GameInterface class
 *
 *    2) What data-structures are used.
 *          int[,]
 *
 *    3) What algorithms, techniques, etc. are used in implementing the data structures.
 *          for loops
 *          
 *    4) What methods are implemented (optional).
 *          CreateGamePiece(), AddGamePieceToArray(),   RemoveGamePieceFromArray()
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
    class GamePiece
    {
        // get x and y value from gameMaster
        int[,] gamePiece;

        public GamePiece(int color)
        {
            CreateGamePiece(color);
        }

        public void CreateGamePiece(int color)
        {
            gamePiece = new int[,] {
                {0, color, color, 0},
                {color, color, color, color},
                {color, color, color, color},
                {0, color, color, 0}
            };
        }

        /// <summary>
               ///     loop through gamePiece array and add 1 version of the gamePiece array to gameBoard array
               ///     place gamePiece array into gameBoard array using x,y as starting point in gameboard array
               /// </summary>
               /// <param name="x"></param>
               /// <param name="y"></param>
               /// <param name="gameBoard"></param>
        public void AddGamePieceToArray(int y, int x, int[,] gameBoard)
        {
            for (int i = 0; i < gamePiece.GetLength(0); i++)  //loops through all columns of gamePiece
            {
                for (int j = 0; j < gamePiece.GetLength(1); j++)  //loops though all rows of gamePiece
                {

                    if (gamePiece[i, j] != 0)
                    {
                        if (gameBoard[i + y, j + x] != 3)//this prevents overwriting the blue frame
                        {
                            gameBoard[i + y, j + x] = gamePiece[i, j]; //inserts into gameBoard array
                        }
                    }
                }
            }
        }


        /// <summary>
               ///     loop through gamePiece array and remove the gamePiece array from gameBoard array
               ///     place gamePiece remove array into gameBoard array using x,y as starting point in gameboard array
               /// </summary>
               /// <param name="x"></param>
               /// <param name="y"></param>
               /// <param name="gameBoard"></param>
        public void RemoveGamePieceFromArray(int y, int x, int[,] gameBoard)
        {
            for (int i = 0; i < gamePiece.GetLength(0); i++)  //loops through all columns of gamePiece
            {
                for (int j = 0; j < gamePiece.GetLength(1); j++)  //loops though all rows of gamePiece
                {
                    if (gamePiece[i, j] != 0)
                    {
                        if (gameBoard[i + y, j + x] != 3) //this prevents overwriting the blue frame
                        {
                            gameBoard[i + y, j + x] = 0; //inserts into gameBoard array
                        }
                    }
                }
            }
        }
    }
}
