using System;

namespace _GameLogic.Generators
{
    public class RoadGenerator
    {
        private HashFunction _hash;
        private RoadInputs _inputs;

        public Block Generate(RoadInputs inputs)
        {
            _inputs = inputs;

            var block = new Block(_inputs.BlockWidth, _inputs.BlockHeight);
            _hash = new XxHash(_inputs.Seed);

            var currentX = block.Width / 2;
            var currentY = block.Height / 2;

            var roadTileIndex = GetRoadTileIndex(currentX, currentY);
            var roadTile = RoadData.RoadTiles[roadTileIndex];
            block.SetCell(currentX, currentY, roadTile);

            BuildSurroundingCells(currentX, currentY, block);

            return block;
        }

        private void BuildSurroundingCells(int currentX, int currentY, Block block)
        {
            var currentTile = block.GetCell(currentX, currentY);
            if (currentTile == null)
            {
                return;
            }

            var isStraight = currentTile.Id == 1 || currentTile.Id == 2;

            if (currentTile.ExitNorth && currentY > 0 && block.GetCell(currentX, currentY - 1) == null)
            {
                if (!isStraight)
                {
                    BuildStraight(currentX, currentY - 1, block, Direction.South);
                }
                else
                {
                    BuildNewCell(currentX, currentY - 1, block, Direction.South);
                }
            }

            if (currentTile.ExitEast && currentX < block.Width - 1 && block.GetCell(currentX + 1, currentY) == null)
            {
                if (!isStraight)
                {
                    BuildStraight(currentX + 1, currentY, block, Direction.West);
                }
                else
                {
                    BuildNewCell(currentX + 1, currentY, block, Direction.West);
                }
            }

            if (currentTile.ExitSouth && currentY < block.Height - 1 && block.GetCell(currentX, currentY + 1) == null)
            {
                if (!isStraight)
                {
                    BuildStraight(currentX, currentY + 1, block, Direction.North);
                }
                else
                {
                    BuildNewCell(currentX, currentY + 1, block, Direction.North);
                }
            }

            if (currentTile.ExitWest && currentX > 0 && block.GetCell(currentX - 1, currentY) == null)
            {
                if (!isStraight)
                {
                    BuildStraight(currentX - 1, currentY, block, Direction.East);
                }
                else
                {
                    BuildNewCell(currentX - 1, currentY, block, Direction.East);
                }
            }
        }

        private void BuildStraight(int newX, int newY, Block block, Direction exitDirection)
        {
            var isEastWest = exitDirection == Direction.East || exitDirection == Direction.West;
            var potentialCell = isEastWest
                ? RoadData.RoadTiles[1]
                : RoadData.RoadTiles[0];

            var length = GetInRange(_hash.GetHash(newX, newY), (uint)(_inputs.MaxRoadLength - _inputs.MinRoadLength)) +
                         _inputs.MinRoadLength;

            var lastX = newX;
            var lastY = newY;
            var continueRoad = false;

            for (var i = 0; i < length; i++)
            {
                continueRoad = false;
                if (block.IsValidLocation(newX, newY))
                {
                    if (ValidateCellPlacement(newX, newY, block, potentialCell))
                    {
                        block.SetCell(newX, newY, potentialCell);
                        if (isEastWest)
                        {
                            PlaceEmptyIfPossible(newX, newY, block, Direction.North);
                            PlaceEmptyIfPossible(newX, newY, block, Direction.South);
                        }
                        else
                        {
                            PlaceEmptyIfPossible(newX, newY, block, Direction.West);
                            PlaceEmptyIfPossible(newX, newY, block, Direction.East);
                        }

                        continueRoad = true;
                        lastX = newX;
                        lastY = newY;
                    }
                    else
                    {
                        BuildNewCell(newX, newY, block, exitDirection);
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid Location: {newX}, {newY}");
                }

                switch (exitDirection)
                {
                    case Direction.North:
                        newY += 1;
                        break;
                    case Direction.South:
                        newY -= 1;
                        break;
                    case Direction.East:
                        newX -= 1;
                        break;
                    case Direction.West:
                        newX += 1;
                        break;
                }
            }

            if (continueRoad)
            {
                BuildSurroundingCells(lastX, lastY, block);
            }
        }

        private void PlaceEmptyIfPossible(int newX, int newY, Block block, Direction direction)
        {
            var (x, y) = block.GetLocation(newX, newY, direction);

            if (block.IsValidLocation(x, y) && !block.TryGetCell(x, y, out var _))
            {
                if (ValidateCellPlacement(x, y, block, RoadData.EmptyRoadTile))
                {
                    block.SetCell(x, y, RoadData.EmptyRoadTile);
                }
            }
        }

        private void BuildNewCell(int newX, int newY, Block block, Direction exitDirection)
        {
            var roadTileIndex = GetRoadTileIndexForExit(newX, newY, exitDirection);

            var lastIndexToCheck = roadTileIndex;
            var found = false;
            while (!found)
            {
                var potentialCell = RoadData.RoadTilesByExit[exitDirection][roadTileIndex];
                if (ValidateCellPlacement(newX, newY, block, potentialCell))
                {
                    block.SetCell(newX, newY, potentialCell);
                    found = true;
                }

                roadTileIndex = (roadTileIndex + 1) % RoadData.RoadTilesByExit[exitDirection].Count;
                if (lastIndexToCheck == roadTileIndex)
                {
                    break;
                }
            }

            if (!found)
            {
                Console.WriteLine("Tile Not Found!");
            }

            BuildSurroundingCells(newX, newY, block);
        }

        private bool ValidateCellPlacement(int newX, int newY, Block block, RoadTile potentialCell)
        {
            if (potentialCell.Id == 7 && block.ExistsWithinRadius(newX, newY, _inputs.FourWayRadius, 7))
            {
                return false;
            }

            if (block.TryGetCell(newX, newY, Direction.North, out var northCell))
            {
                if (northCell.ExitSouth && !potentialCell.ExitNorth)
                {
                    return false;
                }

                if (potentialCell.ExitNorth && !northCell.ExitSouth)
                {
                    return false;
                }
            }

            if (block.TryGetCell(newX, newY, Direction.South, out var southCell))
            {
                if (southCell.ExitNorth && !potentialCell.ExitSouth)
                {
                    return false;
                }

                if (potentialCell.ExitSouth && !southCell.ExitNorth)
                {
                    return false;
                }
            }

            if (block.TryGetCell(newX, newY, Direction.East, out var eastCell))
            {
                if (eastCell.ExitWest && !potentialCell.ExitEast)
                {
                    return false;
                }

                if (potentialCell.ExitEast && !eastCell.ExitWest)
                {
                    return false;
                }
            }

            if (block.TryGetCell(newX, newY, Direction.West, out var westCell))
            {
                if (westCell.ExitEast && !potentialCell.ExitWest)
                {
                    return false;
                }

                if (potentialCell.ExitWest && !westCell.ExitEast)
                {
                    return false;
                }
            }

            return true;
        }

        private int GetRoadTileIndex(int x, int y)
        {
            return (int)GetInRange(_hash.GetHash(x, y), (uint)RoadData.RoadTiles.Count);
        }

        private int GetRoadTileIndexForExit(int x, int y, Direction exitDirection)
        {
            return (int)GetInRange(_hash.GetHash(x, y), (uint)RoadData.RoadTilesByExit[exitDirection].Count);
        }

        public uint GetInRange(uint i, uint range)
        {
            return ((i % range) + range) % range;
        }

        private void PrintCells(Block block)
        {
            for (var y = 0; y < block.Height; y++)
            {
                for (var x = 0; x < block.Width; x++)
                {
                    Console.Write(" " + (block.GetCell(x, y)?.Symbol ?? "  "));
                }
                Console.WriteLine();
            }
        }
    }
}
