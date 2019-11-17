using System;

namespace _GameLogic.Generators
{
    public class Block
    {
        private readonly RoadTile[,] tiles;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Block(int width, int height)
        {
            Width = width;
            Height = height;

            tiles = new RoadTile[height, width];
        }

        public bool IsValidLocation(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        public void SetCell(int x, int y, RoadTile tile)
        {
            tiles[y, x] = tile;
        }

        public RoadTile GetCell(int x, int y)
        {
            return tiles[y, x];
        }

        public bool TryGetCell(int x, int y, out RoadTile roadTile)
        {
            if (IsValidLocation(x, y))
            {
                roadTile = GetCell(x, y);
                return roadTile != null;
            }

            roadTile = null;
            return false;
        }

        public bool TryGetCell(int x, int y, Direction direction, out RoadTile roadTile)
        {
            switch (direction)
            {
                case Direction.North : return TryGetCell(x, y - 1, out roadTile);
                case Direction.South: return TryGetCell(x, y + 1, out roadTile);
                case Direction.East: return TryGetCell(x + 1, y, out roadTile);
                case Direction.West: return TryGetCell(x - 1, y, out roadTile);
            }

            roadTile = null;
            return false;
        }

        public (int x, int y) GetLocation(int x, int y, Direction direction)
        {
            switch (direction)
            {
                case Direction.North : return (x, y - 1);
                case Direction.South: return (x, y + 1);
                case Direction.East: return (x + 1, y);
                case Direction.West: return (x - 1, y);
            }

            throw new Exception("Invalid Direction: " + direction);
        }

        public bool ExistsWithinRadius(int centerX, int centerY, int radius, int tileId)
        {
            for(int y = centerY - radius;y < centerY + radius;y++)
            {
                for (int x = centerX - radius; x < centerX + radius; x++)
                {
                    if (TryGetCell(x, y, out var roadTile))
                    {
                        if (roadTile.Id == tileId)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}