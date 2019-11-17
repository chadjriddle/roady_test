using System;
using System.Collections.Generic;
using System.Linq;

namespace _GameLogic.Generators
{
    public static class RoadData
    {
        public static RoadTile EmptyRoadTile = new RoadTile
        {
            Id = 0, Name = "Empty", Symbol = "@@", ExitNorth = false, ExitEast = false, ExitSouth = false, ExitWest = false
        };

        public static List<RoadTile> RoadTiles = new List<RoadTile>
        {
            new RoadTile {Id = 1, Name = "NorthSouth", Symbol = "NS", ExitNorth = true, ExitEast = false, ExitSouth = true, ExitWest = false },
            new RoadTile {Id = 2, Name = "EastWest", Symbol = "EW", ExitNorth = false, ExitEast = true, ExitSouth = false, ExitWest = true },
            new RoadTile {Id = 3, Name = "NorthEast", Symbol = "NE", ExitNorth = true, ExitEast = true, ExitSouth = false, ExitWest = false },
            new RoadTile {Id = 4, Name = "NorthWest", Symbol = "NW", ExitNorth = true, ExitEast = false, ExitSouth = false, ExitWest = true },
            new RoadTile {Id = 5, Name = "SouthEast", Symbol = "SE", ExitNorth = false, ExitEast = true, ExitSouth = true, ExitWest = false },
            new RoadTile {Id = 6, Name = "SouthWest", Symbol = "SW", ExitNorth = false, ExitEast = false, ExitSouth = true, ExitWest = true },
            new RoadTile {Id = 7, Name = "FourWay", Symbol = "FW", ExitNorth = true, ExitEast = true, ExitSouth = true, ExitWest = true },
            new RoadTile {Id = 8, Name = "T-East", Symbol = "TE", ExitNorth = true, ExitEast = true, ExitSouth = true, ExitWest = false },
            new RoadTile {Id = 9, Name = "T-West", Symbol = "TW", ExitNorth = true, ExitEast = false, ExitSouth = true, ExitWest = true },
            new RoadTile {Id = 10, Name = "T-North", Symbol = "TN", ExitNorth = true, ExitEast = true, ExitSouth = false, ExitWest = true },
            new RoadTile {Id = 11, Name = "T-South", Symbol = "TS", ExitNorth = false, ExitEast = true, ExitSouth = true, ExitWest = true },
            new RoadTile {Id = 12, Name = "NorthEnd", Symbol = "-N", ExitNorth = true, ExitEast = false, ExitSouth = false, ExitWest = false },
            new RoadTile {Id = 13, Name = "EastEnd", Symbol = "-E", ExitNorth = false, ExitEast = true, ExitSouth = false, ExitWest = false },
            new RoadTile {Id = 14, Name = "SouthEnd", Symbol = "-S", ExitNorth = false, ExitEast = false, ExitSouth = true, ExitWest = false },
            new RoadTile {Id = 15, Name = "WestEnd", Symbol = "-W", ExitNorth = false, ExitEast = false, ExitSouth = false, ExitWest = true },
        };

        public static Dictionary<Direction, List<RoadTile>> RoadTilesByExit;

        public static Block TestBlock;

        static RoadData()
        {
            BuildRoadTilesByExit();
            BuildTestBlock();
        }

        private static void BuildRoadTilesByExit()
        {
            RoadTilesByExit = new Dictionary<Direction, List<RoadTile>>
            {
                {Direction.North, GetTilesWithExit(Direction.North).ToList()},
                {Direction.East, GetTilesWithExit(Direction.East).ToList()},
                {Direction.South, GetTilesWithExit(Direction.South).ToList()},
                {Direction.West, GetTilesWithExit(Direction.West).ToList()}
            };
        }

        public static IEnumerable<RoadTile> GetTilesWithExit(Direction exitDirection)
        {
            switch (exitDirection)
            {
                case Direction.North: return RoadTiles.Where(t => t.ExitNorth);
                case Direction.South: return RoadTiles.Where(t => t.ExitSouth);
                case Direction.East: return RoadTiles.Where(t => t.ExitEast);
                case Direction.West: return RoadTiles.Where(t => t.ExitWest);
            }

            return Enumerable.Empty<RoadTile>();
        }

        private static void BuildTestBlock()
        {
            var width = (int)Math.Ceiling(Math.Sqrt(RoadTiles.Count));

            TestBlock = new Block(width, width);
            var currentX = 0;
            var currentY = 0;
            foreach (var roadTile in RoadTiles)
            {
                TestBlock.SetCell(currentX, currentY, roadTile);
                currentX = (currentX + 1) % width;
                if (currentX == 0)
                {
                    currentY += 1;
                }
            }
        }
    }
}