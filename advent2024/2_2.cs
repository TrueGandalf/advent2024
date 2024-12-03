using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace advent2024;

internal class _2_2
{
    public static bool Do()
    {
        var data = Data.GetRowsOfElements("2_1");
        //var data = Data.GetRowsOfElements("2_1_test");

        int result = 0;


        List<int> results = new();
        int resultNumber = 0;

        foreach (var record in data)
        {

            Console.WriteLine(resultNumber);
            Console.WriteLine("org: " + JsonSerializer.Serialize(record));

            var countOfProblems = CountProblems(record);

            if (countOfProblems < 1)
            {
                result++;
                resultNumber++;
                results.Add(1);

                Console.WriteLine("success");
            }
            else
            {
                var skip = false;

                for (int i = 0; i < record.Count(); i++)
                {
                    if (skip)
                        continue;
                    var changed = record.Take(i)
                        .Concat(
                            record.Skip(i + 1)).ToArray();

                    countOfProblems = CountProblems(changed);
                    if (countOfProblems < 1)
                    {
                        result++;
                        results.Add(1);
                        resultNumber++;
                        skip = true;
                        //continue;

                        Console.WriteLine("changed: " + JsonSerializer.Serialize(changed));
                        Console.WriteLine("success");
                    }
                }
                if (!skip)
                {
                    results.Add(0);
                    resultNumber++;
                }

            }
        }

        return true;
    }

    private static int CountProblems(int[] record)
    {
        int countOfProblems = 0;

        if (record.Length == 1)
        {
            return 0;
        }

        bool desc = false;

        if (record[0] > record[1])
        {
            desc = true;
        }

        for (int i = 1; i < record.Length; i++)
        {
            var diff = record[i] - record[i - 1];
            var absDiff = Math.Abs(diff);

            if (!(absDiff <= 3) || !(absDiff >= 1))
            {
                countOfProblems++;
            }

            bool currentDesc = record[i - 1] > record[i];

            if (currentDesc != desc)
            {
                countOfProblems++;
            }
        }

        return countOfProblems;
    }
}
