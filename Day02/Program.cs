using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var regex = new Regex(@"Game (\d+): (.*)");
var possibleGameSums = 0;
var possibleGamePowers = 0;
foreach (var line in input)
{
    var isPossible = true;
    var highestRed = 0;
    var highestGreen = 0;
    var highestBlue = 0;
    var match = regex.Match(line);
    var gameId = int.Parse(match.Groups[1].Value);
    foreach (var round in match.Groups[2].Value.Split(';'))
    {
        foreach (var cubes in round.Split(','))
        {
            var colorMatch = Regex.Match(cubes.Trim(), @"(\d+) (\w+)");
            var count = int.Parse(colorMatch.Groups[1].Value);
            var color = colorMatch.Groups[2].Value;
            if ((color == "red" && count > 12) || (color == "green" && count > 13) || (color == "blue" && count > 14))
                isPossible = false;
            if (color == "red" && count > highestRed)
                highestRed = count;
            if (color == "green" && count > highestGreen)
                highestGreen = count;
            if (color == "blue" && count > highestBlue)
                highestBlue = count;
        }
    }
    if (isPossible)
        possibleGameSums += gameId;
    possibleGamePowers += highestBlue * highestGreen * highestRed;
}

Console.WriteLine($"Part 1: {possibleGameSums}");
Console.WriteLine($"Part 2: {possibleGamePowers}");
