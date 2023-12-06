var input = File.ReadAllLines("input.txt");

// Part 1
var times = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();
var distances = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse).ToArray();

var result = 1;
for (int i = 0; i < times.Length; i++)
{
    var wins = 0;
    for (int hold = 0; hold < times[i]; hold++)
    {
        wins += (times[i] - hold) * hold > distances[i] ? 1 : 0;
    }
    result *= wins;
}
Console.WriteLine(result);

// Part 2
var time = long.Parse(string.Join("", input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)));
var distance = long.Parse(string.Join("", input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)));
var wins2 = 0;
for (long hold = 0; hold < time; hold++)
{
    wins2 += (time - hold) * hold > distance ? 1 : 0;
}
Console.WriteLine(wins2);