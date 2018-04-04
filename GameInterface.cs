/********************************************************
 *
 *  Project :  A03 Connect Four
 *  File    :  GameInterface.cs
 *  Name    :  Michael Dey Melinda Frandsen
 *  Date    :  5 November 2017
 *
 *  Description : (Narrative desciption, not code)
 *
 *    1) What is the purpose of the code; what problem does the code solve.
 *    		This is the class that interprets the GameEngine information 
 *    		and is in charge of displaying
 *    		to the console.
 *
 *    2) What data-structures are used.
 *          Classes, int[,], int
 *
 *    3) What algorithms, techniques, etc. are used in implementing the data structures.
 *          for loops, foreach loops, switch statements
 *
 *    4) What methods are implemented (optional).
 *          
 *  Changes :  <Description|date of modifications>
 *
 ********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectFour
{
    class GameInterface
    {
        private static int cellSize = 4;
        private static int height = cellSize * 7;
        private static int width = cellSize * 7;

        //set up sound files
        System.Media.SoundPlayer LeftRight = new SoundPlayer("Resources/LeftRight.wav");
        System.Media.SoundPlayer Drop = new SoundPlayer("Resources/Drop.wav");
        System.Media.SoundPlayer GameOver = new SoundPlayer("Resources/GameOver.wav");
        System.Media.SoundPlayer Start = new SoundPlayer("Resources/Start.wav");

        //  2D int array that is the game board
        public int[,] gameBoard = new int[height, width];

        // 2D int array that displays graphic "Connect Four"
        public int[,] title = new int[,] {
                {0, 1, 1, 0, 0, 2, 2, 2, 0, 0, 3, 0, 0, 3, 0, 0, 1, 0, 0, 1, 0, 0, 2, 2, 0, 0, 3, 3, 0, 0, 1, 1, 1, 0, 0, 0, 2, 2, 0, 0, 3, 3, 3, 0, 0, 1, 0, 1, 0, 0, 2, 2, 2},
                {0, 1, 0, 0, 0, 2, 0, 2, 0, 0, 3, 0, 0, 3, 0, 0, 1, 0, 0, 1, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 0, 1, 0, 0, 0, 0, 2, 0, 0, 0, 3, 0, 3, 0, 0, 1, 0, 1, 0, 0, 2, 0, 2},
                {0, 1, 0, 0, 0, 2, 0, 2, 0, 0, 3, 3, 0, 3, 0, 0, 1, 1, 0, 1, 0, 0, 2, 2, 0, 0, 3, 0, 0, 0, 0, 1, 0, 0, 0, 0, 2, 2, 0, 0, 3, 0, 3, 0, 0, 1, 0, 1, 0, 0, 2, 2, 2},
                {0, 1, 0, 0, 0, 2, 0, 2, 0, 0, 3, 0, 3, 3, 0, 0, 1, 0, 1, 1, 0, 0, 2, 0, 0, 0, 3, 0, 0, 0, 0, 1, 0, 0, 0, 0, 2, 0, 0, 0, 3, 0, 3, 0, 0, 1, 0, 1, 0, 0, 2, 2, 0},
                {0, 1, 1, 0, 0, 2, 2, 2, 0, 0, 3, 0, 0, 3, 0, 0, 1, 0, 0, 1, 0, 0, 2, 2, 0, 0, 3, 3, 0, 0, 0, 1, 0, 0, 0, 0, 2, 0, 0, 0, 3, 3, 3, 0, 0, 1, 1, 1, 0, 0, 2, 0, 2}
            };

        // 2D int array that displays graphic "Game Over"
        public int[,] gameOver = new int[,]{
                {0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,2,0,0,0,2,0,0,0,2,0,2,2,2,2},
                {0,0,0,0,0,0,0,0,0,0,2,0,0,0,2,0,0,2,2,2,0,0,2,2,0,2,2,0,2,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,2,0,2,2,0,2,0,2,0,2,0,2,2,2,2},
                {0,0,0,0,0,0,0,0,0,0,2,0,0,2,2,0,2,2,2,2,2,0,2,0,0,0,2,0,2,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,2,0,0,0,2,0,2,0,0,0,2,0,2,0,0,0,2,0,2,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,2,0,0,0,2,0,2,0,0,0,2,0,2,2,2,2},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,2,0,0,0,2,0,2,2,2,2,0,2,2,2,2,0},
                {0,0,0,0,0,0,0,0,0,0,2,2,0,2,2,0,2,0,0,0,2,0,2,0,0,0,0,2,0,0,2,2},
                {0,0,0,0,0,0,0,0,0,0,2,2,0,2,2,0,2,0,0,0,2,0,2,2,2,2,0,2,2,2,2,2},
                {0,0,0,0,0,0,0,0,0,0,2,2,0,2,2,0,2,2,0,2,2,0,2,0,0,0,0,2,2,2,0,0},
                {0,0,0,0,0,0,0,0,0,0,2,2,0,2,2,0,0,2,2,2,0,0,2,0,0,0,0,2,0,0,2,0},
                {0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,2,0,0,0,2,2,2,2,0,2,0,0,0,2}
        };

        // 2D int array that displays graphic "Player 1 wins!!"
        public int[,] playerOne = new int[,]{
            {0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,0,2,0,0,0,0,0,0,2,0,0,0,2,0,0,0,2,0,2,2,2,2,0,2,2,2,2,2},
            {0,0,0,0,0,0,0,0,0,0,2,0,0,0,2,0,2,0,0,0,0,0,2,2,2,0,0,2,2,0,2,2,0,2,0,0,0,0,2,0,0,0,2},
            {0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,0,2,0,0,0,0,2,2,0,2,2,0,0,2,2,2,0,0,2,2,2,0,0,2,2,2,2,2},
            {0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,0,0,0,0,2,2,2,2,2,0,0,0,2,0,0,0,2,0,0,0,0,2,0,2,0,0},
            {0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,0,0,0,0,2,0,0,0,2,0,0,0,2,0,0,0,2,0,0,0,0,2,0,0,2,0},
            {0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,2,2,2,0,2,0,0,0,2,0,0,0,2,0,0,0,2,2,2,2,0,2,0,0,0,2},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,2,0,2,0,2,0,0,0,2,0,2,2,2,2,2,0,0,2,0,2},
            {0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0,0,0,0,2,0,0,0,2,0,2,0,2,2,0,0,2,0,2,0,0,0,0,0,0,2,0,2},
            {0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,0,0,2,0,2,0,2,0,2,0,2,0,2,2,2,2,2,0,0,2,0,2},
            {0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,2,0,2,0,2,0,2,0,2,0,0,2,2,0,0,0,0,0,2,0,0,2,0,2},
            {0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,2,2,0,2,2,0,2,0,2,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,0,2,0,0,0,2,0,2,0,2,0,0,0,2,0,2,2,2,2,2,0,0,2,0,2}
        };

        // 2D int array that displays graphic "Player 2 wins!!"
        public int[,] playerTwo = new int[,]{
            {0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,0,2,0,0,0,0,0,0,2,0,0,0,2,0,0,0,2,0,2,2,2,2,0,2,2,2,2,2},
            {0,0,0,0,0,0,0,0,0,0,2,0,0,0,2,0,2,0,0,0,0,0,2,2,2,0,0,2,2,0,2,2,0,2,0,0,0,0,2,0,0,0,2},
            {0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,0,2,0,0,0,0,2,2,0,2,2,0,0,2,2,2,0,0,2,2,2,0,0,2,2,2,2,2},
            {0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,0,0,0,0,2,2,2,2,2,0,0,0,2,0,0,0,2,0,0,0,0,2,0,2,0,0},
            {0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,0,0,0,0,2,0,0,0,2,0,0,0,2,0,0,0,2,0,0,0,0,2,0,0,2,0},
            {0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,2,2,2,2,0,2,0,0,0,2,0,0,0,2,0,0,0,2,2,2,2,0,2,0,0,0,2},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,2,0,0,0,2,0,2,0,2,0,0,0,2,0,2,2,2,2,2,0,0,2,0,2},
            {0,0,0,0,0,0,0,0,0,0,2,0,0,0,2,0,0,0,0,2,0,0,0,2,0,2,0,2,2,0,0,2,0,2,0,0,0,0,0,0,2,0,2},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,2,0,0,0,2,0,2,0,2,0,2,0,2,0,2,2,2,2,2,0,0,2,0,2},
            {0,0,0,0,0,0,0,0,0,0,0,2,2,2,0,0,0,0,0,2,0,2,0,2,0,2,0,2,0,0,2,2,0,0,0,0,0,2,0,0,2,0,2},
            {0,0,0,0,0,0,0,0,0,0,2,0,0,0,0,0,0,0,0,2,2,0,2,2,0,2,0,2,0,0,0,2,0,0,0,0,0,2,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,2,2,2,2,2,0,0,0,0,2,0,0,0,2,0,2,0,2,0,0,0,2,0,2,2,2,2,2,0,0,2,0,2}
        };

        /// <summary>
        /// Takes in a 2D array and converts indexes of it's cells to a 2D array 4 times the size
        /// </summary>
        /// <param name="small"></param>
        public void ConvertSmallArrayToLargeArray(int[,] small)
        {
            for (int i = 0; i < small.GetLength(0); i++) //loops through all columns
            {
                for (int j = 0; j < small.GetLength(1); j++) //loops through all rows
                {
                    int y = i * 4;              // index of i multiplied by 4
                    int x = j * 4;              // index of j multiplied by 4

                    if (small[i, j] != 0)
                    {
                        GamePiece tempGP = new GamePiece(small[i, j]);
                        tempGP.AddGamePieceToArray(y, x, gameBoard);    // adds values to gameBoard array
                    }
                }
            }
            Framework fw = new Framework();
            fw.PropigateFramework(gameBoard);     // converts information to the gameBoard array  

        }

        /// <summary>
        /// Allows game to play sound upon start of the game, completion of the game
        /// and when user moves or drops the gamepiece
        /// Uses a parameter of int to determine which audio clip to play.
        /// </summary>
        /// <param name="sound"></param>
        public void PlaySound(int sound)
        {
            switch (sound)
            {

                case 1:     // indicates Left and Right arrow
                    LeftRight.Play();
                    break;

                case 2:     //indicates dropping chip
                    Drop.Play();
                    break;

                case 3:     // indicates Game Over
                    GameOver.Play();
                    break;

                case 4:     // indicates game on startup
                    Start.Play();
                    break;
            }
        }

        /// <summary>
        /// Determines which player won and displays an animation 
        /// Takes an integer to determine which player wins
        /// Calls the AnimatGameOver method to animate the results.
        /// </summary>
        /// <param name="player"></param>
        public void DeclareWinner(int player)
        {
            int[,] winner;
            if (player == 1)         // if int is 1 winner is set to playerOne
            {
                winner = playerOne;
            }
            else                    // if int is 2 winner is set to playerTwo
            {
                winner = playerTwo;
            }
            AnimateGameOver(winner, gameOver);  // prints the final animation showing which player won and 'game over'
        }

        /// <summary>
        /// Strings together an animation of the winning player followed by an animation
        /// ofthe game over graphics.
        /// Takes an array of the winning player (1 or 2) and the array of the game over graphics
        /// Calls the Animate method and plays an audio clip indicating the game is over.
        /// </summary>
        /// <param name="win"></param>
        /// <param name="over"></param>
        public void AnimateGameOver(int[,] win, int[,] over)
        {
            PlaySound(3);                       // sound indicates game is over
            Console.Clear();                    // clears console screen
            Console.SetCursorPosition(0, 0);    // sets cursor to upper left position
            Animate(win);                       // animates the winning player graphic

            Console.Clear();                    // clears console screen
            Console.SetCursorPosition(0, 0);    // sets cursor to upper left position
            Animate(over);                      // animates the game over graphic
        }

        /// <summary>
        /// Creates an animation of any array.
        /// Takes an array, makes a copy, then flips the color scheme of the copy using 
        /// the DuplicateFlipArray method. Animates the 2 arrays by alternately
        /// printing one then the other to the console.
        /// </summary>
        /// <param name="arrayToFlip"></param>
        public void Animate(int[,] arrayToFlip)
        {
            int[,] original = arrayToFlip;                      // saves parameter array as 'original'
            int[,] flip = DuplicateFlipArray(arrayToFlip);      // creates duplicate array saves as 'flip'

            for (int i = 0; i < 13; i++)
            {                        // loops 10 times
                Console.SetCursorPosition(0, 0);                // set cursor to upper left
                DrawArray(original);                            // prints original array to console
                Console.SetCursorPosition(0, 0);                // set cursor to upper left
                DrawArray(flip);                                // prints flipped array to console
            }
        }

        /// <summary>
        ///  Creates a duplicate array with a different color scheme.
        ///  Takes an array and makes a duplicate, then changes the colors in each 
        ///  cell by changing the values from 1 to 2, 2 to 3, 3 to 1. 
        ///  Returns the duplicate updated array
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public int[,] DuplicateFlipArray(int[,] original)
        {
            // creates the duplicate array and saves as 'flipped'
            int[,] flipped = new int[original.GetLength(0), original.GetLength(1)];

            for (int i = 0; i < original.GetLength(0); i++)         //loops through all columns
            {
                for (int j = 0; j < original.GetLength(1); j++)     //loops through all rows
                {
                    if (original[i, j] == 1)
                    {
                        flipped[i, j] = 2;              // if value at index is 1, changes to 2
                    }
                    else if (original[i, j] == 2)
                    {
                        flipped[i, j] = 3;              // if value at index is 2, changes to 3
                    }
                    else if (original[i, j] == 3)
                    {
                        flipped[i, j] = 1;              // if value at index is 3, changes to 1
                    }
                    else if (original[i, j] == 0)
                    {
                        flipped[i, j] = 0;              // if value at index is 0, does nothing
                    }
                }
            }
            return flipped;
        }

        /// <summary>
        /// Prints any array to the console.
        /// Takes an array of any size and draws it to the console
        /// Sets array values of 0, 1, 2, 3 to print as black, red, yellow, and blue respectively
        /// </summary>
        /// <param name="drawArray"></param>
        public void DrawArray(int[,] drawArray)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            for (int i = 0; i < drawArray.GetLength(0); i++)        //loops through all columns
            {
                for (int j = 0; j < drawArray.GetLength(1); j++)    //loops through all rows
                {
                    //find out number and set color accordingly
                    switch (drawArray[i, j])
                    {
                        case 0:
                            Console.BackgroundColor = ConsoleColor.Black;       // if 0 print black block
                            Console.Write(" ");
                            break;
                        case 1:
                            Console.BackgroundColor = ConsoleColor.Red;         // if 1 print red block
                            Console.Write(" ");
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.Yellow;      // if 2 print yellow block
                            Console.Write(" ");
                            break;
                        case 3:
                            Console.BackgroundColor = ConsoleColor.Blue;        // if 3 print blue block
                            Console.Write(" ");
                            break;
                        default:
                            break;                                              // if none of the above, do nothing
                    }
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints the gameBoard array to the console.
        /// Loops through a 2D array and assigns colors to each integer value in the array. 
        /// Prints the array to the console as colors.
        /// </summary>
        public void DrawGameBoard()
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++) //loops through all columns
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++) //loops through all rows
                {
                    //find out number and set color accordingly
                    DrawGameBoardToScreen(i, j);
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Black;
            //Thread.Sleep(1000);
        }

        /// <summary>
        /// loops through a 2D array that represents a block of the game board
        /// assigns colors to each integer value in the array
        /// Prints the array to the console as colors.
        /// </summary>
        public void DrawGameBlock(int x, int y)
        {

            int cursorX = x;
            int cursorY = y;


            for (int i = x; i < x + 4; i++)
            {
                for (int j = y; j < y + 4; j++)
                {

                    Console.SetCursorPosition(i * 2, j); //we multiply i * 2 because each square is two spaces "  "

                    //find out number and set color accordingly
                    DrawGameBoardToScreen(j, i);

                    //below erases the trail left behind by the animation
                    //move cursor to space just exited
                    //re-write the console the spaces that were just exited by the gamepiece
                    if (j > 0)//make sure we aren't referencing outside of the gameboard
                    {
                        Console.SetCursorPosition(i * 2, j - 1); //we multiply i * 2 because each square is two spaces "  "
                        DrawGameBoardToScreen(j - 1, i);         //redraw space above the dropping piece
                    }
                    if (i > 0 && j < 5)//make sure we aren't referencing outside of the gameboard
                    {
                        Console.SetCursorPosition(i * 2 - 2, j); //we multiply i * 2 because each square is two spaces "  "
                        DrawGameBoardToScreen(j, i - 1);         //redraw space to the left of the gamepiece
                    }
                    if (i < 27 && j < 5)//make sure we aren't referencing outside of the gameboard
                    {
                        Console.SetCursorPosition(i * 2 + 2, j); //we multiply i * 2 because each square is two spaces "  "
                        DrawGameBoardToScreen(j, i + 1);        //redraw space to the right of the gamepiece
                    }

                }
            }

            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Black;
            //Thread.Sleep(10);
        }

        /// <summary>
        /// This method takes y,x coordinates and searches the gameboard array at those coordinates
        /// it then interperates the number at that coordinate and prints the corresponding color to the screen
        /// </summary>
        /// <param name="y">vertical position of point</param>
        /// <param name="x">horizontal position of point</param>
        private void DrawGameBoardToScreen(int y, int x)
        {
            switch (gameBoard[y, x])
            {
                case 0:
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("  ");
                    break;
                case 1:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write("  ");
                    break;
                case 2:
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.Write("  ");
                    break;
                case 3:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.Write("  ");
                    break;
            }
        }

        /// <summary>
        /// This method takes positional data for a game piece and
        /// loops through from beginning position to end position
        /// and propogates the gameBoard with positional data for the
        /// current game piece at each point in the animation
        /// </summary>
        /// <param name="x1">horizontal position of the starting point</param>
        /// <param name="y1">vertical position of the starting point</param>
        /// <param name="x2">horizontal position of the end point</param>
        /// <param name="y2">vertical position of the end point</param>
        /// <param name="color"></param>
        public void AnimateGamePiece(int x1, int y1, int x2, int y2, int color)
        {
            GamePiece gp = new GamePiece(color);            //create a game piece object that can write to the GameBoard array

            //adjust the gameMaster array to fit the GameInterface array which is 4 times larger
            int StartX = x1 * 4;
            int StartY = y1 * 4;
            int EndX = x2 * 4;
            int EndY = y2 * 4;

            bool finished = false;                                      //control for the loop

            //loop through each iteration of the game piece position
            while (!finished)
            {
                gp.RemoveGamePieceFromArray(StartX, StartY, gameBoard); //erase the current piece

                //iterate x,y coordinates by one step until at the target position
                if (StartX < EndX) StartX++;
                if (StartX > EndX) StartX--;
                if (StartY < EndY) StartY++;
                if (StartY > EndY) StartY--;

                gp.AddGamePieceToArray(StartX, StartY, gameBoard);      //propogate the array with the gamePiece information
                DrawGameBlock(StartY, StartX);                          //draw the block to screen where the game piece has been adjusted
                Thread.Sleep(10);                                       //slow the animation down
                if (StartX == EndX && StartY == EndY)                   //gamePiece is now in the correct position, so end the loop
                {
                    finished = true;
                }
            }
        }

    }

}
