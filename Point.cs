/********************************************************
 *
 *  Project :  A03 Connect Four
 *  File    :  Point.cs
 *  Name    :  Michael Dey Melinda Frandsen
 *  Date    :  5 November 2017
 *
 *  Description : (Narrative desciption, not code)
 *
 *    1) What is the purpose of the code; what problem does the code solve.
 *    		This Struct is used to hold positional data that represent various vectors
 *
 *    2) What data-structures are used.
 *          int
 *
 *    3) What algorithms, techniques, etc. are used in implementing the data structures.
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
using System.Threading.Tasks;

namespace ConnectFour
{
    public struct Point
    {

        public Point(int y, int x) : this()
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }

        public int Y { get; set; }

    }
}
