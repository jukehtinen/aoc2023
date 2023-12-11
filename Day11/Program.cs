var input = File.ReadAllLines("input.txt");

var galaxies = new List<(int x, int y)>();
for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input[y].Length; x++)
    {
        if (input[y][x] == '#')
            galaxies.Add((x, y));
    }
}

var emptyHorizontals = new List<int>();
for (int y = 0; y < input.Length; y++)
{
    if (input[y].All(c => c == '.'))
        emptyHorizontals.Add(y);
}

var emptyVerticals = new List<int>();
for (int x = 0; x < input[0].Length; x++)
{
    var empty = true;
    for (int y = 0; y < input.Length; y++)
    {
        if (input[y][x] == '#')
        {
            empty = false;
            break;
        }
    }
    if (empty)
        emptyVerticals.Add(x);
}

long total = 0;
long total2 = 0;
for (int g1 = 0; g1 < galaxies.Count; g1++)
{
    for (int g2 = g1 + 1; g2 < galaxies.Count; g2++)
    {
        total += GetDistance(galaxies[g1], galaxies[g2], 1);
        total2 += GetDistance(galaxies[g1], galaxies[g2], 1000000 - 1);
    }
}
Console.WriteLine($"Total distance: {total}");
Console.WriteLine($"Total distance 2: {total2}");

long GetDistance((int x, int y) g1, (int x, int y) g2, long expansion)
{
    long length = Math.Abs(g1.x - g2.x) + Math.Abs(g1.y - g2.y);
    for (int x = Math.Min(g1.x, g2.x); x <= Math.Max(g1.x, g2.x); x++)
    {
        if (emptyVerticals.Contains(x))
            length += expansion;
    }
    for (int y = Math.Min(g1.y, g2.y); y <= Math.Max(g1.y, g2.y); y++)
    {
        if (emptyHorizontals.Contains(y))
            length += expansion;
    }
    return length;
}