using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Enums;
using Game.Models;
using Action = Game.Enums.Action;

namespace Game
{
    public class Game
    {
        public readonly Cell[,] MyGrid;
        public readonly Cell[,] OtherGrid;

        public readonly Int32 _cellsWide;
        public readonly Int32 _cellsHigh;
        private Int32 _hitNb;

        public readonly List<Boat> _boats;

        private Boolean GameOver { get; set; }
        private Boolean Victory { get; set; }

        public Game(Int32 cellsWide, Int32 cellsHigh)
        {
            _cellsWide = cellsWide;
            _cellsHigh = cellsHigh;
            MyGrid = new Cell[cellsWide, cellsHigh];
            OtherGrid = new Cell[cellsWide, cellsHigh];
            _boats = new List<Boat>();

            CreateBoats();
            PlaceBoats();
            InitOpponentGrid();
        }

        private void InitOpponentGrid()
        {
            for (var i = 0; i < _cellsWide; i++)
            {
                for (var j = 0; j < _cellsHigh; j++)
                {
                    OtherGrid[i, j] = Cell.Untried;
                }
            }
        }

        private void CreateBoats()
        {
            _boats.Add(new Boat { Length = 5, cellId = "1"});
            _boats.Add(new Boat { Length = 4, cellId = "2" });
            _boats.Add(new Boat { Length = 3, cellId = "3" });
            _boats.Add(new Boat { Length = 3, cellId = "4" });
            _boats.Add(new Boat { Length = 2, cellId = "5" });
        }

        private void PlaceBoats()
        {
            var r = new Random();
            foreach (Boat boat in _boats)
            {
                boat.Direction = (Direction)r.Next(0, 3);

                Int32 maxX = _cellsWide - 1, maxY = _cellsHigh - 1, minX = 0, minY = 0;
                switch (boat.Direction)
                {
                    case Direction.Up:
                        minY = boat.Length - 1;
                        break;
                    case Direction.Down:
                        maxY = _cellsHigh - boat.Length - 1;
                        break;
                    case Direction.Left:
                        minX = boat.Length - 1;
                        break;
                    case Direction.Right:
                        maxX = _cellsWide - boat.Length - 1;
                        break;
                    default:
                        Console.WriteLine("forgot this.");
                        break;
                }

                do
                {
                    boat.X = r.Next(minX, maxX);
                    boat.Y = r.Next(minY, maxY);
                } while (!PositionLegal(boat));



                SetBoatCells(boat, Cell.Boat);
            }
        }

        private void SetBoatCells(Boat boat, Cell type)
        {
            var cells = boat.GetCellIndexes();
            for (var i = 0; i < boat.Length; i++)
            {
                MyGrid[cells[i, 0], cells[i, 1]] = type;
            }
        }

        private Boolean PositionLegal(Boat boat)
        {
            try
            {
                var cells = boat.GetCellIndexes();

                for (var i = 0; i < boat.Length; i++)
                {
                    if (MyGrid[cells[i, 0], cells[i, 1]] != Cell.Empty)
                        return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void PrintGrid()
        {
            PrintGrid(MyGrid);
        }

        public void PrintGrid(Cell[,] grid)
        {
            for (var i = 0; i < _cellsWide + 2; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();

            for (var i = 0; i < _cellsWide; i++)
            {
                Console.Write("|");
                for (var j = 0; j < _cellsHigh; j++)
                {
                    Console.Write(grid[i, j].ToString());
                }
                Console.WriteLine("|");
            }

            for (var i = 0; i < _cellsWide + 2; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();

        }

        public void PrintOpponentGrid()
        {
            PrintGrid(OtherGrid);
        }

        public Action Answer(Int32 x, Int32 y)
        {
            switch (MyGrid[x, y])
            {
                case Cell.Empty:
                    MyGrid[x, y] = Cell.Fired;
                    return Action.Eau;
                case Cell.Hit:
                    return Action.Touche;
                case Cell.Sunked:
                    return Action.ToucheCoule;
                case Cell.Fired:
                    return Action.Eau;
                case Cell.Boat:
                    MyGrid[x, y] = Cell.Hit;
                    if (HitBoat(x, y))
                        return Action.ToucheCoule;
                    return Action.Touche;
                default:
                    throw new ArgumentOutOfRangeException("_myGrid[cellsWide, cellsHigh]");
            }

        }

        private Boolean HitBoat(Int32 x, Int32 y)
        {
            var boat = FindBoat(x, y);
            if (boat == null)
                return false;
            return TrySink(boat, x, y);
        }

        private Boolean TrySink(Boat boat, Int32 x, Int32 y)
        {
            var cells = boat.GetCellIndexes();

            var sunked = true;
            for (var i = 0; i < boat.Length; i++)
            {
                if (MyGrid[cells[i, 0], cells[i, 1]] == Cell.Hit ||
                    MyGrid[cells[i, 0], cells[i, 1]] == Cell.Sunked) continue;
                sunked = false;
                break;
            }

            if (sunked)
            {
                SetBoatCells(boat, Cell.Sunked);
                boat.IsSunk = true;
                CheckGameOver();
            }

            return sunked;
        }

        private void CheckGameOver()
        {
            var over = true;
            foreach (var boat in _boats)
            {
                if (!boat.IsSunk)
                    over = false;
            }

            if (over)
                GameOver = true;
        }

        private Boat FindBoat(Int32 x, Int32 y)
        {
            foreach (var boat in _boats)
            {
                var cells = boat.GetCellIndexes();

                for (var i = 0; i < boat.Length; i++)
                {
                    if (cells[i, 0] == x && cells[i, 1] == y)
                        return boat;
                }

            }
            return null;
        }

        public void UpdateEnemyGrid(Int32 x, Int32 y, Cell status)
        {
            OtherGrid[x, y] = status;

            if (status == Cell.Sunked || status == Cell.Hit)
                _hitNb++;

            if (status != Cell.Sunked)
                return;


        }

        public Int32[] Play()
        {
            var r = new Random();
            var pos = new Int32[2];

            var remaining = new List<Tuple<Int32, Int32>>(_cellsWide * _cellsHigh);

            for (var i = 0; i < _cellsWide; i++)
            {
                for (var j = 0; j < _cellsWide; j++)
                {


                    if (OtherGrid[i, j] == Cell.Untried)
                        remaining.Add(new Tuple<Int32, Int32>(i, j));
                }
            }

            if (_hitNb == 17 || remaining.Count == 0)
            {
                Victory = true;
                return new[] { 0, 0 };
            }

            var random = r.Next(remaining.Count);
            pos[0] = remaining[random].Item1;
            pos[1] = remaining[random].Item2;
            return pos;
        }
    }

    

}