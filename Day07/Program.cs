var input = File.ReadAllLines("input.txt");

var hands = new List<(string hand, int bid)>();
foreach (var line in input)
    hands.Add(new(line.Split(' ')[0], int.Parse(line.Split(' ')[1])));

// Part 1
hands.Sort(delegate ((string h, int b) a, (string h, int b) b)
{
    if (GetHandType(a.h) == GetHandType(b.h))
    {
        for (int i = 0; i < 5; i++)
        {
            if (GetCardValue(a.h[i].ToString()) == GetCardValue(b.h[i].ToString()))
                continue;
            return GetCardValue(a.h[i].ToString()) > GetCardValue(b.h[i].ToString()) ? 1 : -1;
        }
        return 0;
    }
    return GetHandType(a.h) > GetHandType(b.h) ? 1 : -1;
});

var sum = 0;
for (int i = 0; i < hands.Count; i++)
    sum += hands[i].bid * (i + 1);

Console.WriteLine(sum);

// Part 2
var cache = new Dictionary<string, string>();
hands.Sort(delegate ((string h, int b) a, (string h, int b) b)
{
    var bestA = SolveJokers(a.h);
    var bestB = SolveJokers(b.h);
    if (GetHandType(bestA) == GetHandType(bestB))
    {
        for (int i = 0; i < 5; i++)
        {
            if (GetCardValue(a.h[i].ToString(), true) == GetCardValue(b.h[i].ToString(), true))
                continue;
            return GetCardValue(a.h[i].ToString(), true) > GetCardValue(b.h[i].ToString(), true) ? 1 : -1;
        }
        return 0;
    }
    return GetHandType(bestA) > GetHandType(bestB) ? 1 : -1;
});

sum = 0;
for (int i = 0; i < hands.Count; i++)
    sum += hands[i].bid * (i + 1);

Console.WriteLine(sum);

int GetHandType(string hand, bool part2 = false)
{
    var cards = hand.ToCharArray().Select(c => c.ToString()).Select(c => GetCardValue(c, part2)).OrderBy(r => r).ToArray();
    if (cards.GroupBy(r => r).Any(g => g.Count() == 5)) return 6;
    if (cards.GroupBy(r => r).Any(g => g.Count() == 4)) return 5;
    if (cards.GroupBy(r => r).Any(g => g.Count() == 3) && cards.GroupBy(r => r).Any(g => g.Count() == 2)) return 4;
    if (cards.GroupBy(r => r).Any(g => g.Count() == 3)) return 3;
    if (cards.GroupBy(r => r).Count(g => g.Count() == 2) == 2) return 2;
    if (cards.GroupBy(r => r).Any(g => g.Count() == 2)) return 1;
    return 0;
}

int GetCardValue(string card, bool part2 = false)
{
    return card switch
    {
        "T" => 10,
        "J" => part2 ? 1 : 11,
        "Q" => 12,
        "K" => 13,
        "A" => 14,
        _ => int.Parse(card)
    };
}

string GetValueLetter(int value)
{
    return value switch
    {
        10 => "T",
        11 => "J",
        12 => "Q",
        13 => "K",
        14 => "A",
        _ => value.ToString()
    };
}

string SolveJokers(string hand)
{
    if (cache.TryGetValue(hand, out string? value))
        return value;

    if (!hand.Contains('J'))
        return hand;

    var newHand = ModifyHand(hand, 0);
    cache[hand] = newHand;
    return newHand;
}

string ModifyHand(string hand, int index)
{
    var bestHand = hand;
    int bestType = GetHandType(bestHand, true);
    for (int i = index; i < 5; i++)
    {
        if (hand[i] == 'J')
        {
            for (int t = 2; t <= 14; t++)
            {
                if (t == 11)
                    continue;

                var newHand = ModifyHand(hand[..i] + GetValueLetter(t) + hand[(i + 1)..], i + 1);
                if (GetHandType(newHand, true) > bestType)
                {
                    bestHand = newHand;
                    bestType = GetHandType(newHand, true);
                }
            }
        }
    }
    return bestHand;
}