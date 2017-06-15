using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Enums;

namespace Game.Models
{
    public class Boat
    {
        public String cellId;

        public Int32 X;

        public Int32 Y;

        public Int32 Length;

        public Direction Direction;

        public Boolean IsSunk = false;

        public Int32[,] GetCellIndexes()
        {
            var cells = new Int32[Length, 2];
            switch (Direction)
            {
                case Direction.Up:
                    for (var i = 0; i < Length; i++)
                    {
                        cells[i, 1] = Y - i;
                        cells[i, 0] = X;
                    }
                    break;
                case Direction.Down:
                    for (var i = 0; i < Length; i++)
                    {
                        cells[i, 1] = Y + i;
                        cells[i, 0] = X;
                    }
                    break;
                case Direction.Left:
                    for (var i = 0; i < Length; i++)
                    {
                        cells[i, 1] = Y;
                        cells[i, 0] = X - i;
                    }
                    break;
                case Direction.Right:
                    for (var i = 0; i < Length; i++)
                    {
                        cells[i, 1] = Y;
                        cells[i, 0] = X + i;
                    }
                    break;
            }
            return cells;
        }
    }
}
