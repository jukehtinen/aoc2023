var input = File.ReadAllText("input.txt");
var length = input.IndexOf("\r\n");
input = input.Replace("\r\n", "");

var dirs = new List<(int x, int y)>() { (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0), (-1, 1) };

var l = 0;
var partSums = 0;
var gears = new Dictionary<int, List<int>>();
while (l < input.Length)
{
    while (l < input.Length && !char.IsDigit(input[l]))
    {
        l++;
    }
    var partNumberStart = l;

    bool isPart = false;
    int gearIndex = -1;
    while (l < input.Length && char.IsDigit(input[l]))
    {
        foreach (var (x, y) in dirs)
        {
            var tx = (l % length) + x;
            var ty = (l / length) + y;
            if (IsSymbol(tx, ty))
            {
                isPart = true;

                if (gearIndex == -1)
                    gearIndex = input[ty * length + tx] == '*' ? (ty * length + tx) : -1;
            }
        }
        l++;
    }

    if (isPart)
        partSums += int.Parse(input[partNumberStart..l]);

    if (gearIndex != -1)
    {
        if (!gears.ContainsKey(gearIndex))
            gears.Add(gearIndex, []);
        gears[gearIndex].Add(int.Parse(input[partNumberStart..l]));
    }
}

Console.WriteLine($"Part 1: {partSums}");

var ratioSum = 0;
foreach (var gear in gears.Where(g => g.Value.Count == 2))
{
    ratioSum += gear.Value[0] * gear.Value[1];
}
Console.WriteLine($"Part 2: {ratioSum}");

bool IsSymbol(int x, int y)
{
    if (x < 0 || x >= length || y < 0 || y >= length)
        return false;
    return !char.IsDigit(input[y * length + x]) && input[y * length + x] != '.';
}
