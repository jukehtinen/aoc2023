using System.Text;

var input = File.ReadAllLines("input.txt");

// Part 1
var map = new char[input.Length, input[0].Length];
for (var y = 0; y < input.Length; y++)
    for (var x = 0; x < input[y].Length; x++)
        map[x, y] = input[y][x];

while (true)
{
    var somethingMoved = false;
    for (var y = 0; y < input.Length; y++)
    {
        for (var x = 0; x < input[y].Length; x++)
        {
            if (map[x, y] == 'O')
            {
                if (y > 0 && map[x, y - 1] == '.')
                {
                    map[x, y - 1] = 'O';
                    map[x, y] = '.';
                    somethingMoved = true;
                }
            }
        }
    }
    if (!somethingMoved)
        break;
}

Console.WriteLine($"Result {CalculatePoints(map)}");

// Part 2
map = new char[input.Length, input[0].Length];
for (var y = 0; y < input.Length; y++)
    for (var x = 0; x < input[y].Length; x++)
        map[x, y] = input[y][x];

var cache = new Dictionary<string, char[,]>();
var loops = new Dictionary<string, long>();

for (long i = 0; i < 1000000000; i++)
{
    var dir = 0; // 0 - north, 1 - west, 2 - south, 3 - east

    if (cache.ContainsKey(MapToString(map)))
    {
        map = cache[MapToString(map)].Clone() as char[,];
        continue;
    }
    var startState = MapToString(map);

    while (true)
    {
        var somethingMoved = false;
        for (var y = 0; y < input.Length; y++)
        {
            for (var x = 0; x < input[y].Length; x++)
            {
                if (map[x, y] == 'O')
                {
                    if (dir == 0)
                    {
                        if (y > 0 && map[x, y - 1] == '.')
                        {
                            map[x, y - 1] = 'O';
                            map[x, y] = '.';
                            somethingMoved = true;
                        }
                    }
                    else if (dir == 1)
                    {
                        if (x > 0 && map[x - 1, y] == '.')
                        {
                            map[x - 1, y] = 'O';
                            map[x, y] = '.';
                            somethingMoved = true;
                        }
                    }
                    else if (dir == 2)
                    {
                        if (y < input.Length - 1 && map[x, y + 1] == '.')
                        {
                            map[x, y + 1] = 'O';
                            map[x, y] = '.';
                            somethingMoved = true;
                        }
                    }
                    else if (dir == 3)
                    {
                        if (x < input[y].Length - 1 && map[x + 1, y] == '.')
                        {
                            map[x + 1, y] = 'O';
                            map[x, y] = '.';
                            somethingMoved = true;
                        }
                    }
                }
            }
        }
        if (!somethingMoved)
        {
            if (dir < 4)
            {
                dir++;
                continue;
            }

            cache.Add(startState, (char[,])map.Clone());

            if (loops.ContainsKey(MapToString(map)))
            {
                long cycleLength = i - loops[MapToString(map)];
                i += cycleLength * ((1000000000 - i) / cycleLength);
            }

            loops[MapToString(map)] = i;

            break;
        }
    }
}

Console.WriteLine($"Result {CalculatePoints(map)}");

static long CalculatePoints(char[,] map)
{
    long result = 0;
    for (var y = 0; y < map.GetLength(1); y++)
    {
        for (var x = 0; x < map.GetLength(0); x++)
        {
            if (map[x, y] == 'O')
            {
                result += map.GetLength(1) - y;
            }
        }
    }
    return result;
}

static string MapToString(char[,] map)
{
    var sb = new StringBuilder();
    for (var y = 0; y < map.GetLength(1); y++)
    {
        for (var x = 0; x < map.GetLength(0); x++)
        {
            sb.Append(map[x, y]);
        }
    }
    return sb.ToString();
}
