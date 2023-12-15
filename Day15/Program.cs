var input = File.ReadAllLines("input.txt");

// Part 1
var sum = 0;
foreach (var s in input[0].Split(","))
{
    sum += HASH(s);
}
Console.WriteLine(sum);

// Part 2
var boxes = new Dictionary<int, List<(string label, int focal)>>();
foreach (var s in input[0].Split(","))
{
    if (s.Contains('='))
    {
        var label = s.Split("=")[0];
        var focal = int.Parse(s.Split("=")[1]);
        var hash = HASH(label);
        if (!boxes.TryGetValue(hash, out List<(string label, int focal)>? value))
        {
            boxes[hash] = [(label, focal)];
        }
        else
        {
            var index = value.FindIndex(f => f.label == label);
            if (index != -1)
            {
                value[index] = (label, focal);
            }
            else
            {
                value.Add((label, focal));
            }
        }
    }
    else
    {
        var hash = HASH(s.Split("-")[0]);
        if (boxes.TryGetValue(hash, out List<(string label, int focal)>? value))
        {
            value.RemoveAll(x => x.label == s.Split("-")[0]);
        }
    }
}
sum = 0;
foreach (var (box, lenses) in boxes)
{
    for (int j = 0; j < lenses.Count; j++)
    {
        sum += (box + 1) * (j + 1) * lenses[j].focal;
    }
}
Console.WriteLine(sum);


static int HASH(string s)
{
    var hash = 0;
    foreach (var c in s)
    {
        hash += c;
        hash *= 17;
        hash %= 256;
    }
    return hash;
}
