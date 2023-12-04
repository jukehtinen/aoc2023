using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var regex = new Regex(@"Card\s*(\d+): (.*) \| (.*)");
var totalPoints = 0;
foreach (var line in input)
{
    var match = regex.Match(line);
    var winningNumbers = match.Groups[2].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
    var playerNumbers = match.Groups[3].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
    var wins = playerNumbers.Where(winningNumbers.Contains).Count();
    totalPoints += wins == 0 ? 0 : (int)Math.Pow(2, wins - 1);
}
Console.WriteLine($"Part 1: {totalPoints}");

var copies = new Dictionary<int, int>();
var totalCards = 0;
foreach (var line in input)
{
    var match = regex.Match(line);
    var cardId = int.Parse(match.Groups[1].Value);
    var winningNumbers = match.Groups[2].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
    var playerNumbers = match.Groups[3].Value.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse).ToList();
    var wins = playerNumbers.Where(winningNumbers.Contains).Count();

    var copyCount = copies.TryGetValue(cardId, out int value) ? value + 1 : 1;
    for (int i = 0; i < copyCount; i++)
    {
        totalCards++;

        for (int j = 0; j < wins; j++)
        {
            var c = cardId + j + 1;
            if (copies.TryGetValue(c, out int val))
                copies[c] = ++val;
            else
                copies.Add(c, 1);
        }
    }
}
Console.WriteLine($"Part 2: {totalCards}");