using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var directions = input[0];
var map = new Dictionary<string, (string l, string r)>();
for (int i = 2; i < input.Length; i++)
{
    var match = Regex.Match(input[i], @"(\w+) = \((\w+), (\w+)\)");
    map[match.Groups[1].Value] = (match.Groups[2].Value, match.Groups[3].Value);
}

// Part 1
var current = "AAA";
var currentStep = 0;
while (current != "ZZZ")
{
    var dir = directions[currentStep % directions.Length];
    current = dir == 'L' ? map[current].l : map[current].r;
    currentStep++;
}
Console.WriteLine(currentStep);

// Part 2
long result = 1;
foreach (var cur in map.Keys.Where(k => k.EndsWith("A")).ToArray())
{
    current = cur;
    currentStep = 0;
    while (!current.EndsWith("Z"))
    {
        var dir = directions[currentStep % directions.Length];
        current = dir == 'L' ? map[current].l : map[current].r;
        currentStep++;
    }

    var (a, b) = result > currentStep ? (result, (long)currentStep) : ((long)currentStep, result);
    while (b != 0)
    {
        (a, b) = (b, a % b);
    }
    result *= (currentStep / a);
}
Console.WriteLine(result);