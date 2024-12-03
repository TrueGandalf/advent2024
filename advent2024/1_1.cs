using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace advent2024;

public class _1_1
{
    public static bool Do()
    {
        var inputText = File.ReadAllText("C:\\Personal\\advent2024\\advent2024\\1_1.txt").ToString();
        var byRows = inputText.Split('\n');

        List<int> firstList = [];
        List<int> secondList = [];

        foreach (var row in byRows)
        {
            var pair = row.Split("  ");
            if (pair.Length == 1)
                continue;
            firstList.Add(int.Parse(pair[0]));
            secondList.Add(int.Parse(pair[1]));
        }
        firstList.Sort();
        secondList.Sort();

        var sum = 0;

        for (var i = 0; i < firstList.Count; i++)
        {
            sum += Math.Abs(firstList[i] - secondList[i]);
        }

        // 1_2
        var _1_2_answer = firstList.Select(f1 => f1 * secondList.Where(f2 => f2 == f1).Count()).Sum();

        return true;
    }
}
