var input = File.ReadAllLines("input.txt");

// Part 1
long sum = 0;
foreach (var line in input)
{
    var numbers = line.Split(" ").Select(x => long.Parse(x)).ToList();
    sum += Handle(numbers, 0);
}
Console.WriteLine(sum);

// Part 2
sum = 0;
foreach (var line in input)
{
    var numbers = line.Split(" ").Select(x => long.Parse(x)).Reverse().ToList();
    sum += Handle(numbers, 0);
}
Console.WriteLine(sum);

static long Handle(List<long> numbers, long prevLastNumber)
{
    if (numbers.All(n => n == 0))
        return prevLastNumber;

    var newNumbers = new List<long>();
    for (int i = 0; i < numbers.Count - 1; i++)
        newNumbers.Add(numbers[i + 1] - numbers[i]);

    return Handle(newNumbers, numbers.Last()) + prevLastNumber;
}