/********************************************************
 *
 *  Project :  A03 Connect Four
 *  File    :  GameEngine.cs
 *  Name    :  Michael Dey Melinda Frandsen
 *  Date    :  5 November 2017
 *
 *  Description : (Narrative desciption, not code)
 *
 *    1) What is the purpose of the code; what problem does the code solve.
 *    		This class drives the logic and function of the Connect Four game
 *
 *    2) What data-structures are used.
 *          Classes, structs, int[,], boolean
 *
 *    3) What algorithms, techniques, etc. are used in implementing the data structures.
 *          There are several for loops and enhanced for loops to access the array in different ways
 *          FindEnd() is a recursive method that searches itself for the end of a line of similar points
 *          
 *    4) What methods are implemented (optional).
 *          
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
    class GameEngine
    {
        private int height; //height of the array
        private int width; //width of the array
        private int[,] gameGrid; //GameGrid is the array that holds all of the checker positions
        private int userPosition; //position of the user controlled gamepiece
        public int currentPlayer;
        private int matchCount; //counts the number of items together in a row (4 in a row wins)
        Boolean win; //keeps track if the game has been won
        public Boolean gameEnd;
        private int winner = 0; //will be converted to a 1 or 2 based on who wins, will stay 0 if a draw

        //will hold animation game piece positional data
        public int animStartX;
        public int animStartY;
        public int animEndX;
        public int animEndY;
        public int animColor = 1;

        //holds sound event information
        // 0 = no sound, 1 = horizontal movement, 2 = drop, 3 = game over
        private int sound = 0;

        private static Point upRight, right, downRight, down, downLeft, left, upLeft; //will hold directional data
        private Point[] directions; //an array to hold all directions

        public GameEngine(int height, int width)
        {
            this.height = height;
            this.width = width;
            gameGrid = new int[height, width];
            userPosition = 0;
            currentPlayer = 1;
            win = false;
            gameEnd = false;
            gameGrid[0, userPosition] = currentPlayer;

            //create points for all directions
            upRight = new Point(-1, 1);
            right = new Point(0, 1);
            downRight = new Point(1, 1);
            down = new Point(1, 0);
            downLeft = new Point(1, -1);
            left = new Point(0, -1);
            upLeft = new Point(-1, -1);
            directions = new Point[] { upRight, right, downRight, down, downLeft, left, upLeft }; //an array of all directional points

        }

        /// <summary>
        /// Gets directional input from the user as to where to place the game piece to make it fall
        /// </summary>
        public void GetUserInput()
        {
            //set current player piece in far left top corner
            gameGrid[0, userPosition] = currentPlayer;
            animStartY = 0;
            animStartX = userPosition;

            //reset the animation end positions
            animEndX = animStartX;
            animEndY = animStartY;
            animColor = currentPlayer;

            ConsoleKeyInfo kb = Console.ReadKey(); //read keyboard input
            if (kb.Key == ConsoleKey.LeftArrow) //shift piece to the left
            {
                if (userPosition > 0)
                {
                    gameGrid[0, userPosition] = 0;
                    userPosition--; //move user position to the left 1 space
                    animEndX--;
                    gameGrid[0, userPosition] = currentPlayer;
                    sound = 1; //horizontal movement 
                }
            }
            if (kb.Key == ConsoleKey.RightArrow) //shift piece to the right
            {
                if (userPosition < width - 1)
                {
                    gameGrid[0, userPosition] = 0;
                    userPosition++; //move user position to the right 1 space
                    animEndX++;
                    gameGrid[0, userPosition] = currentPlayer;
                    sound = 1; //horizontal movement 
                }
            }
            if (kb.Key == ConsoleKey.Enter) //player has selected to place a piece in this slot
            {
                if (!IsPositionFilled(1, userPosition)) //check if it's a valid column to drop a piece into
                {
                    DropGamePiece(); //find bottom most open position and drop game piece into it 
                    sound = 2; //drop movement 
                    gameGrid[0, userPosition] = 0; //reset the grid to not show the old players position
                    SwitchPlayer();
                    userPosition = 0; //reset the user's position to the far left
                    gameGrid[0, userPosition] = currentPlayer; //set current player piece in far left top corner
                }
            }
            if (kb.Key == ConsoleKey.Escape) //exits game
            {
                gameEnd = true;
                Console.Clear();
            }
        }

        /// <summary>
        /// searches through the column to find the first available position to place the gamepiece
        /// When it finds it, it places the piece
        /// Also ends the game if all columns are filled but there is no winner
        /// </summary>
        private void DropGamePiece()
        {
            int pointer = height - 1; //initial value will be bottom of the grid

            while (pointer >= 1)
            {
                if (!IsPositionFilled(pointer, userPosition))
                {
                    gameGrid[pointer, userPosition] = currentPlayer;
                    CountSimilarPieces(pointer, userPosition); //counts similar game pieces in directions to see if win
                    break;
                }
                pointer--;
            }
            animEndY = pointer; //set the vertical position of the animated piece to the final pointer position

            //end the game if the entire board is filled but there is no winner
            if (IsPositionFilled(1, 0) && IsPositionFilled(1, 1) && IsPositionFilled(1, 2)
                && IsPositionFilled(1, 3) && IsPositionFilled(1, 4) && IsPositionFilled(1, 5)
                && IsPositionFilled(1, 6))
            {
                win = false;
                gameEnd = true;
                winner = 0;
            }
        }

        /// <summary>
        /// sends a count of how many game pieces in a given direction to get checked if there is a win
        /// </summary>
        /// <param name="yPos">vertical position of the game piece</param>
        /// <param name="xPos">horizontal position of the game piece</param>
        private void CountSimilarPieces(int yPos, int xPos)
        {
            //loop through an array of Points that represents each direction
            foreach (Point p in directions)
            {
                AdjustWinner(GoToEndAndBack(yPos, xPos, p)); //search from the dropped position point into a given direction and count similar pieces in a line
            }
        }

        /// <summary>
        /// analyzes the count of game piece items in a row, if they are over 4
        /// the game is won
        /// sends the winning information to 
        /// win boolean, gameEnd boolean, and sets winner to current player
        /// </summary>
        /// <param name="similarPieceCount">count of similar pieces in any given direction (from GoToEndAndBack())</param>
        private void AdjustWinner(int similarPieceCount)
        {
            if (similarPieceCount > 3)
            {
                win = true;
                gameEnd = true;
                winner = currentPlayer;
                sound = 3; //game over sound
            }
        }

        /// <summary>
        /// Returns the boolean win which is true if winCount > 4 from checkWin method
        /// </summary>
        /// <returns>win, which means there are 4 similar player items in a row</returns>
        public bool CheckIfWinner()
        {
            return win;
        }

        /// <summary>
        /// returns who is the winner
        /// </summary>
        /// <returns>int of value 1 for player 1 or 2 for player 2</returns>
        public int GetWinner()
        {
            return winner;
        }

        /// <summary>
        /// This method starts at a point and searches in a given direction when it hits the end, it switches direction
        /// and counts all similar values of the array in that direction from the end point back. It returns that count.
        /// </summary>
        /// <param name="yEnd">Starting Y position</param>
        /// <param name="xEnd">Starting X position</param>
        /// <param name="Point P">Point containing vector direction to search in</param>
        /// <returns>Count of all items in a given vector direction</returns>
        private int GoToEndAndBack(int yPos, int xPos, Point p)
        {
            int yEnd = yPos; //create a value that will dynamically change in findEdge()
            int xEnd = xPos; //create a value that will dynamically change in findEdge()

            FindEnd(ref yEnd, ref xEnd, p.Y, p.X); //move pointers to farthest position in scalar direction
            matchCount = 1; //reset match count

            //reverse vector direction and search in new direction to end of the line, also iterates matchCount
            FindEnd(ref yEnd, ref xEnd, (p.Y * -1), (p.X * -1));
            return matchCount;
        }

        /// <summary>
        /// This method spreads out from a given point into a given direction. 
        /// It stops when the next point in that direction is not equal to the given point
        /// Or if it hits the edge of the game board
        /// </summary>
        /// <param name="yEnd">pass by reference y value for the point</param>
        /// <param name="xEnd">pass by reference x value for the point</param>
        /// <param name="yAdd">vertical direction for the point to search</param>
        /// <param name="xAdd">horizontal direction for the point to search</param>
        private void FindEnd(ref int yEnd, ref int xEnd, int yAdd, int xAdd)
        {
            if (((xAdd == 1 && yAdd == 0) && !AtRightEdge(xEnd)) || //go to the right
            ((xAdd == -1 && yAdd == 0) && !AtLeftEdge(xEnd)) || //go to the left
            ((yAdd == 1 && xAdd == 0) && !AtBottom(yEnd)) || //go down
            ((yAdd == -1 && xAdd == 0) && !AtTop(yEnd)) || //go up
            ((xAdd == 1 && yAdd == 1) && (!AtRightEdge(xEnd) && !AtBottom(yEnd))) || //down and to the right
            ((xAdd == -1 && yAdd == 1) && (!AtLeftEdge(xEnd) && !AtBottom(yEnd))) || //down and to the left 
            ((xAdd == 1 && yAdd == -1) && (!AtRightEdge(xEnd) && !AtTop(yEnd))) || //up and to the right
            ((xAdd == -1 && yAdd == -1) && (!AtLeftEdge(xEnd) && !AtTop(yEnd)))) //up and to the left
            {
                if (gameGrid[yEnd, xEnd] == gameGrid[yEnd + yAdd, xEnd + xAdd]) //check neighbor
                {
                    matchCount++;
                    xEnd += xAdd;
                    yEnd += yAdd;
                    FindEnd(ref yEnd, ref xEnd, yAdd, xAdd); //if neighbors match, switch to neighbor and check neighbor
                }
            }
        }


        /// <summary>
        /// Returns if a point is at the edge of the array or not
        /// </summary>
        /// <param name="yPos">Vertical position of the point</param>
        /// <param name="xPos">Horizontal postion of the point</param>
        /// <returns></returns>
        private bool AtEdge(int yPos, int xPos)
        {
            if (xPos >= (width - 1) || xPos <= 0) return true;
            else if (yPos >= (height - 1) || yPos <= 1) return true; //because the user choice is the top row, 1 is the topmost edge
            else return false;
        }

        /// <summary>
        /// Returns true if the point's x position is at the right edge
        /// </summary>
        /// <param name="xPos">x position of a given point</param>
        /// <returns>true if the x position is on an edge</returns>
        private bool AtRightEdge(int xPos)
        {
            if (xPos >= (width - 1)) return true;
            else return false;
        }

        /// <summary>
        /// Returns true if the point's x position is at the left edge
        /// </summary>
        /// <param name="xPos">x position of a given point</param>
        /// <returns>true if the x position is on an edge</returns>
        private bool AtLeftEdge(int xPos)
        {
            if (xPos <= 0) return true;
            else return false;
        }

        /// <summary>
        /// Returns true if the point's y position is at the bottom
        /// </summary>
        /// <param name="yPos">y position of a given point</param>
        /// <returns>true if the y position is at the bottom</returns>
        private bool AtBottom(int yPos)
        {
            if (yPos >= height - 1) return true;
            else return false;
        }

        /// <summary>
        /// Returns true if the point's y position is at the top
        /// </summary>
        /// <param name="yPos">y position of a given point</param>
        /// <returns>true if the y position is at the bottom</returns>
        private bool AtTop(int yPos)
        {
            if (yPos <= 1) return true; //because the user choice is the top row, 1 is the topmost edge
            else return false;
        }

        /// <summary>
        /// Checks if the GameGrid at the given point is filled, if it is, return true, else return false
        /// </summary>
        /// <param name="y">vertical postion in grid</param>
        /// <param name="x">horizontal position in grid</param>
        /// <returns></returns>
        private bool IsPositionFilled(int y, int x)
        {
            return gameGrid[y, x] > 0;
        }

        /// <summary>
        /// This method changes the player to the next player
        /// </summary>
        private void SwitchPlayer()
        {
            if (currentPlayer == 1) currentPlayer = 2;
            else currentPlayer = 1;
        }

        /// <summary>
        /// inserts a value into the array at the given y,x coordinates
        /// </summary>
        /// <param name="y">pointer to the "vertical" position in the array</param>
        /// <param name="x">pointer to the "horizontal" postion in the array</param>
        /// <param name="value">integer to place in the position pointed at</param>
        public void AddToGrid(int y, int x, int value)
        {
            if ((y <= height && y >= 0) && (x <= width && x >= 0))
                gameGrid[y, x] = value;
        }

        /// <summary>
        /// Prints the entire contents of the GameGrid array in order
        /// </summary>
        public void PrintGameGrid()
        {
            Console.Clear();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Console.Write(gameGrid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// this sends the grid to the interface in order to be properly printed to screen
        /// </summary>
        /// <returns>int[,] that holds the information of the game pieces on the grid</returns>
        public int[,] GetGameGrid()
        {
            return gameGrid;
        }

        /// <summary>
        /// returns the sound value for movement noises
        /// </summary>
        /// <returns></returns>
        public int GetSound()
        {
            return sound;
        }

        /// <summary>
        /// Returns the GameEngine width and height in String format
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("GameEngine: width: {0}, height: {1}", width, height);
        }
    }
}