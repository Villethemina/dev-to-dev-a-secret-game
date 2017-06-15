using System;
using System.Collections.Generic;
using Game.Enums;
using Game.Models;

namespace Game.Extensions
{
    public static class Extensions
    {
        public static String ToMyString(this Cell cell)
        {
            switch (cell)
            {
                case Cell.Empty:
                    return " ";
                case Cell.Untried:
                    return " ";
                case Cell.Fired:
                    return "M";
                case Cell.Hit:
                    return "H";
                case Cell.Sunked:
                    return "S";
                case Cell.Boat:
                    return "*";
                default:
                    throw new ArgumentOutOfRangeException(nameof(cell));
            }
        }

        public static String[,] ToArray(this Game game)
        {
            List<Boat> boats = game._boats;


            String[,] ourGrid = new String[game._cellsWide, game._cellsHigh];

            for (var i = 0; i < game._cellsWide; i++)
            {
                for (var j = 0; j < game._cellsHigh; j++)
                {
                    ourGrid[i, j] = game.MyGrid[i, j].ToMyString();
                }
            }

            //foreach (var boat in boats)
            //{
            //    Int32[,] indexs = boat.GetCellIndexes();

            //    for (Int32 i = 0; i < indexs.GetLength(0); i++)
            //    {
            //        for (Int32 j = 0; j < indexs.GetLength(1); j++)
            //        {
            //            Int32 s = indexs[i, j];

            //            if (s == 0) continue;

            //            ourGrid[i, j] = boat.cellId;
            //        }
            //    }
            //}

            return ourGrid;
        }
    }
}
