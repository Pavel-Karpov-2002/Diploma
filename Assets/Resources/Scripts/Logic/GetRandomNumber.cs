using System;
using System.Collections.Generic;
using System.Linq;

public static class GetRandomNumber
{
    public static int GenerateRandomNumberNotUsed(int min, int max, HashSet<int> used)
    {
        if (used == null)
            throw new Exception("HashSet is null");

        var random = new Random();
        var range = Enumerable.Range(min, max - min).Where(i => !used.Contains(i)).ToList();
        var index = random.Next(range.Count);

        if (range.Count != 0)
            return range[index];

        return -1;
    }

    public static IEnumerable<int> GenerateRandomNumbersNotUsed(int min, int max, int countNumbers, HashSet<int> used)
    {
        var random = new System.Random();
        var range = Enumerable.Range(min, max - min).Where(i => !used.Contains(i)).ToList();
        while (countNumbers > 0)
        {
            var index = random.Next(range.Count);
            yield return range[index];
            range.RemoveAt(index);
            countNumbers--;
        }
    }
}
