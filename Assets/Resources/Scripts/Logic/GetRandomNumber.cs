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
        if (range.Count != 0)
            return range[random.Next(range.Count)];
        return -1;
    }
}
