var input = File.ReadAllLines("input.txt");

List<long> seeds = [];
List<Map> maps = [];
for (int i = 0; i < input.Length; i++)
{
    var line = input[i];

    if (line.Contains("seeds"))
        seeds = line.Split(": ")[1].Split(" ").Select(long.Parse).ToList();
    if (line.Contains("-to-"))
    {
        var src = line.Split("-to-")[0].Split(" ").Last();
        var dst = line.Split("-to-")[1].Split(" ").First();
        var values = new List<(long dst, long src, long length)>();
        line = input[++i];
        while (!string.IsNullOrWhiteSpace(line))
        {
            values.Add((long.Parse(line.Split(" ")[0]), long.Parse(line.Split(" ")[1]), long.Parse(line.Split(" ")[2])));
            if (i + 1 >= input.Length)
                break;

            line = input[++i];
        }
        maps.Add(new Map { From = src, To = dst, Values = values });
    }
}

// Part 1
var smallest = long.MaxValue;
foreach (var seed in seeds)
{
    var location = FindLocation(seed);
    if (location < smallest)
        smallest = location;
}
Console.WriteLine($"Part 1: {smallest}");

// Part 2
smallest = long.MaxValue;
for (long i = 0; i < long.MaxValue; i++)
{
    long seedIndex = FindSeed(i);

    for (int x = 0; x < seeds.Count; x += 2)
    {
        if (seedIndex >= seeds[x] && seedIndex < seeds[x] + seeds[x + 1])
        {
            if (i < smallest)
            {
                smallest = i;
                // TODO, got lucky with input, smallest was near the start...
                Console.WriteLine($"Smallest: {smallest}");
            }
        }
    }
}
Console.WriteLine($"Part 1: {smallest}");

long FindLocation(long seed)
{
    var from = "seed";
    var source = seed;
    while (from != "location")
    {
        var mapping = maps.First(m => m.From == from);
        var values = mapping.Values.Where(v => v.src <= source && (v.src + v.length) > source).ToList();

        long mapped = 0;
        if (values.Count == 0)
            mapped = source;
        else
            mapped = MapRanges(source, values[0].src, values[0].src + values[0].length, values[0].dst, values[0].dst + values[0].length);

        from = mapping.To;
        source = mapped;
    }
    return source;
}

long FindSeed(long location)
{
    var to = "location";
    var source = location;
    while (to != "seed")
    {
        var mapping = maps.First(m => m.To == to);
        var values = mapping.Values.Where(v => v.dst <= source && (v.dst + v.length) > source).ToList();

        long mapped = 0;
        if (values.Count == 0)
            mapped = source;
        else
            mapped = MapRanges(source, values[0].dst, values[0].dst + values[0].length, values[0].src, values[0].src + values[0].length);

        to = mapping.From;
        source = mapped;
    }
    return source;
}

long MapRanges(long x, long in_min, long in_max, long out_min, long out_max)
{
    return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
}

record Map
{
    public required string From { get; set; }
    public required string To { get; set; }
    public required List<(long dst, long src, long length)> Values { get; set; }
}
