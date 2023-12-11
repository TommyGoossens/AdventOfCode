using System.Diagnostics;
using AdventOfCodeShared;
using AdventOfCodeShared.Models;

namespace AdventOfCode2023;


internal enum Direction
{
    North,
    East,
    South,
    West
}

internal enum MetalIslandTile
{
    Start,
    NorthAndSouth,
    EastAndWest,
    NorthAndEast,
    NorthAndWest,
    SouthAndWest,
    SouthAndEast,
    Ground
}

internal static class MetalIslandTileExtensions
{
    internal static IEnumerable<MetalIslandTile> GetValidTransitions(this MetalIslandTile tile, Direction direction)
    {
        return direction switch
        {
            Direction.North => tile switch
            {
                MetalIslandTile.NorthAndSouth or MetalIslandTile.NorthAndWest or MetalIslandTile.NorthAndEast or MetalIslandTile.Start => CreateEnumList(
                    MetalIslandTile.NorthAndSouth,
                    MetalIslandTile.SouthAndEast,
                    MetalIslandTile.SouthAndWest,
                    MetalIslandTile.Start),
                _ => CreateEnumList()
            },
            Direction.East => tile switch
            {
                MetalIslandTile.EastAndWest or MetalIslandTile.NorthAndEast or MetalIslandTile.SouthAndEast or MetalIslandTile.Start => CreateEnumList(
                    MetalIslandTile.EastAndWest,
                    MetalIslandTile.NorthAndWest,
                    MetalIslandTile.SouthAndWest,
                    MetalIslandTile.Start),
                _ => CreateEnumList()
            },
            Direction.South => tile switch
            {
                MetalIslandTile.NorthAndSouth or MetalIslandTile.SouthAndWest or MetalIslandTile.SouthAndEast or MetalIslandTile.Start => CreateEnumList(
                    MetalIslandTile.NorthAndSouth,
                    MetalIslandTile.NorthAndEast,
                    MetalIslandTile.NorthAndWest,
                    MetalIslandTile.Start
                ),
                _ => CreateEnumList()
            },
            Direction.West => tile switch
            {
                MetalIslandTile.EastAndWest or MetalIslandTile.NorthAndWest or MetalIslandTile.SouthAndWest or MetalIslandTile.Start => CreateEnumList(
                    MetalIslandTile.EastAndWest,
                    MetalIslandTile.NorthAndEast,
                    MetalIslandTile.SouthAndEast,
                    MetalIslandTile.Start),
                _ => CreateEnumList()
            },
            _ => throw new Exception($"Somehow there are no valid transitions for tile type: {tile} to direction {direction}")
        };

        // | is a vertical pipe connecting north and south. North And South
        // - is a horizontal pipe connecting east and west.
        // L is a 90-degree bend connecting north and east.
        // J is a 90-degree bend connecting north and west. je mag naar het westen en noorden
        // 7 is a 90-degree bend connecting south and west. je mag naar het zuiden of westen
        // F is a 90-degree bend connecting south and east. Je mag naar oosten of zuiden
        // . is ground; there is no pipe in this tile.
    }

    private static IEnumerable<MetalIslandTile> CreateEnumList(params MetalIslandTile[] tilesToAdd)
    {
        var list = new List<MetalIslandTile>();
        list.AddRange(tilesToAdd);
        return list;
    }
}

[DebuggerDisplay("Location: {Location} - Tile: {Type}")]
internal record TileLocation(Point Location, MetalIslandTile Type);

internal record Animal
{

    public Animal(TileLocation currentLocation, IEnumerable<IEnumerable<TileLocation>> map)
    {
        CurrentLocation = currentLocation;
        PreviousLocation = currentLocation;
        this.map = map;
        StepsTaken = [currentLocation];
        UpdatePossibleDirections();
    }

    private readonly IEnumerable<IEnumerable<TileLocation>> map;
    internal List<TileLocation> StepsTaken { get; private set; }
    internal TileLocation? NorthTile { get; private set; }
    internal TileLocation? EeastTile { get; private set; }
    internal TileLocation? SouthTile { get; private set; }
    internal TileLocation? WestTile { get; private set; }
    internal TileLocation CurrentLocation { get; private set; }
    internal TileLocation PreviousLocation { get; private set; }

    private TileLocation? SetLocationIfValid(TileLocation nextLocation, Direction direction)
    {
        var validTransitions = CurrentLocation.Type.GetValidTransitions(direction);
        if (validTransitions.Any(t => nextLocation.Type == t)) return nextLocation;
        return null;
    }

    /**
01234          
..F7.0
.FJ|.1
SJ.L72
|F--J3
LJ...4
    **/
    internal void TakeStep()
    {
        IEnumerable<TileLocation> options = new List<TileLocation?>() { NorthTile, EeastTile, SouthTile, WestTile }.Where(l => l != null).Select(l => l!);
        if (!options.Any()) throw new Exception($"There are no ways to move for position {CurrentLocation}");
        if (options.Select(o => o.Type).Contains(MetalIslandTile.Start) && StepsTaken.Count > 2)
        {
            CurrentLocation = options.First(o => o.Type == MetalIslandTile.Start);
        }
        else
        {
            var nextStep = options.Except(StepsTaken).First();
            PreviousLocation = CurrentLocation;
            CurrentLocation = nextStep!;
            StepsTaken.Add(nextStep!);
            UpdatePossibleDirections();
        }

    }

    private void UpdatePossibleDirections()
    {
        var (point, _) = CurrentLocation;
        var (x, y) = point;

        if (x > 0) WestTile = SetLocationIfValid(map.ElementAt(y).ElementAt(x - 1), Direction.West);
        if (x < map.ElementAt(y).Count() - 1) EeastTile = SetLocationIfValid(map.ElementAt(y).ElementAt(x + 1), Direction.East);
        if (y > 0) NorthTile = SetLocationIfValid(map.ElementAt(y - 1).ElementAt(x), Direction.North);
        if (y < map.Count() - 1) SouthTile = SetLocationIfValid(map.ElementAt(y + 1).ElementAt(x), Direction.South);
    }
}

public class Day10(string? text = null) : AdventOfCodeProgram<int>(text)
{
    private static MetalIslandTile ParseTile(char tile)
    {
        return tile switch
        {
            '|' => MetalIslandTile.NorthAndSouth,
            '-' => MetalIslandTile.EastAndWest,
            'L' => MetalIslandTile.NorthAndEast,
            'J' => MetalIslandTile.NorthAndWest,
            '7' => MetalIslandTile.SouthAndWest,
            'F' => MetalIslandTile.SouthAndEast,
            '.' => MetalIslandTile.Ground,
            'S' => MetalIslandTile.Start,
            _ => throw new Exception($"Unknown tile type: {tile}")
        };
    }

    private IEnumerable<IEnumerable<TileLocation>> ParseMap()
    {
        return Lines
            .Select((line, row) => (line.Select(c => c), row))
            .Select(l => l.Item1
                .Select((c, col) => new TileLocation(new Point(col, l.row), ParseTile(c))));
    }

    public override int RunPartOne()
    {
        var map = ParseMap();
        var startingPoint = map.First(r => r.Any(c => c.Type == MetalIslandTile.Start)).First(c => c.Type == MetalIslandTile.Start);

        var animal = new Animal(startingPoint, map);
        animal.TakeStep();
        while (animal.CurrentLocation != startingPoint) animal.TakeStep();

        return animal.StepsTaken.Count / 2;
    }

    public override int RunPartTwo()
    {
        return 4;
    }
}
