using System.Text.RegularExpressions;

var input = File.ReadAllLines("input.txt");

var pipes = new Dictionary<string, List<string>>();
var sum = 0;
foreach (var line in input)
{
    var match = Regex.Match(line, @"(?<key>\w+)?{(?<content>.*)}");
    var key = match.Groups["key"].Value;
    var content = match.Groups["content"].Value;

    if (!string.IsNullOrEmpty(key))
    {
        pipes.Add(key, [.. content.Split(',')]);
    }
    else if (!string.IsNullOrEmpty(content))
    {
        var values = content.Split(',');
        var ratings = new Dictionary<string, int>();
        foreach (var value in values)
        {
            var rating = value.Split('=');
            ratings.Add(rating[0], int.Parse(rating[1]));
        }
        if (IsApproved(ratings, pipes, "in"))
        {
            sum += ratings.Sum(r => r.Value);
        }
    }
}
Console.WriteLine($"Approved: {sum}");

static bool IsApproved(Dictionary<string, int> ratings, Dictionary<string, List<string>> pipes, string key)
{
    if (key == "A")
        return true;
    if (key == "R")
        return false;

    var pipe = pipes[key];
    foreach (var p in pipe)
    {
        if (p.Contains('<'))
        {
            var match = Regex.Match(p, @"(\w+)<(\d+):(\w+)");
            if (ratings[match.Groups[1].Value] < int.Parse(match.Groups[2].Value))
            {
                return IsApproved(ratings, pipes, match.Groups[3].Value);
            }
        }
        else if (p.Contains('>'))
        {
            var match = Regex.Match(p, @"(\w+)>(\d+):(\w+)");
            if (ratings[match.Groups[1].Value] > int.Parse(match.Groups[2].Value))
            {
                return IsApproved(ratings, pipes, match.Groups[3].Value);
            }
        }
        else
        {
            return IsApproved(ratings, pipes, p);
        }
    }
    return false;
}
