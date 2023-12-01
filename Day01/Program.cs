using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

// Part 1
var sum = 0;
foreach (var line in input)
{
    var numbers = Regex.Matches(line, @"\d").Select(m => int.Parse(m.Value)).ToArray();
    sum += int.Parse($"{numbers.First()}{numbers.Last()}");
}
Console.WriteLine(sum);

// Part 2
var digits = new List<(string, int)>() { ("one", 1), ("two", 2), ("three", 3), ("four", 4), ("five", 5), ("six", 6), ("seven", 7), ("eight", 8), ("nine", 9), ("0", 0), ("1", 1), ("2", 2), ("3", 3), ("4", 4), ("5", 5), ("6", 6), ("7", 7), ("8", 8), ("9", 9) };
sum = 0;
foreach (var line in input)
{
    int firstIndex = int.MaxValue;
    int lastIndex = 0;
    int firstValue = 0;
    int lastValue = 0;

    foreach (var digit in digits)
    {
        var i = line.LastIndexOf(digit.Item1);
        if (i != -1)
        {
            if (i >= lastIndex)
            {
                lastIndex = i;
                lastValue = digit.Item2;
            }
        }

        i = line.IndexOf(digit.Item1);
        if (i != -1)
        {
            if (i <= firstIndex)
            {
                firstIndex = i;
                firstValue = digit.Item2;
            }
        }
    }
    sum += int.Parse($"{firstValue}{lastValue}");
}
Console.WriteLine(sum);