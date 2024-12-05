using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace advent2024;

internal class _5_1
{
    public static bool Do()
    {
        //var fileName = "5_1_test";
        var fileName = "5_1";
        var input = File.ReadAllText($"C:\\Personal\\advent2024\\advent2024\\{fileName}.txt").ToString();


        // pattern search

        var twoParts = input.Split("\n\n");
        var rulesStrings = twoParts[0].Trim().Split("\n");
        var rowsStrings = twoParts[1].Trim().Split("\n");

        var reverseRules = new Dictionary<int, List<int>>();
        foreach (var rule in rulesStrings)
        {
            var ruleInts = rule.Split("|");

            var nextNumbers = new List<int>();
            if (!reverseRules.TryGetValue(Convert.ToInt32(ruleInts[0]), out nextNumbers))
            {
                reverseRules.Add(Convert.ToInt32(ruleInts[0]), new List<int>());
            }
            reverseRules[Convert.ToInt32(ruleInts[0])].Add(Convert.ToInt32(ruleInts[1]));
        }

        var rows = new List<List<int>>();
        foreach (var row in rowsStrings)
        {
            var rowInts = row.Split(",");
            rows.Add(rowInts.Select(x => Convert.ToInt32(x)).ToList());
        }


        var goodRows = new List<List<int>>();

        foreach (var row in rows)
        {
            var rowIsGood = true;

            foreach (var mainElement in row)
            {
                if (!rowIsGood)
                {
                    break;
                }
                var nextNumbers = new List<int>();
                if (reverseRules.TryGetValue(Convert.ToInt32(mainElement), out nextNumbers))
                {
                    var mainElementIndex = row.FindIndex(x => x == mainElement);

                    foreach (var nextNumber in nextNumbers)
                    {
                        var nextNumberIndex = row.FindIndex(x => x == nextNumber);
                        if ((nextNumberIndex >= 0) && (nextNumberIndex < mainElementIndex))
                        {
                            rowIsGood = false;
                            break;
                        }
                    }
                }
            }

            if (rowIsGood)
            {
                goodRows.Add(row);
                Console.WriteLine("goodRow:");
                Console.WriteLine(JsonSerializer.Serialize(row));
            }
            else
            {
                Console.WriteLine("badRow:");
                Console.WriteLine(JsonSerializer.Serialize(row));
            }
        }


        var sum = 0;
        foreach (var goodRow in goodRows)
        {
            sum += goodRow[(goodRow.Count() - 1) / 2];
        }

        //var res = a + a_1 + a_2 + a_3;

        return true;
    }
}

internal class _5_2
{
    public static bool Do()
    {
        //var fileName = "5_1_test";
        var fileName = "5_1";
        var input = File.ReadAllText($"C:\\Personal\\advent2024\\advent2024\\{fileName}.txt").ToString();


        // pattern search

        var twoParts = input.Split("\n\n");
        var rulesStrings = twoParts[0].Trim().Split("\n");
        var rowsStrings = twoParts[1].Trim().Split("\n");

        var originalRules = new Dictionary<int, List<int>>();
        foreach (var rule in rulesStrings)
        {
            var ruleInts = rule.Split("|");

            var nextNumbers = new List<int>();
            if (!originalRules.TryGetValue(Convert.ToInt32(ruleInts[0]), out nextNumbers))
            {
                originalRules.Add(Convert.ToInt32(ruleInts[0]), new List<int>());
            }
            originalRules[Convert.ToInt32(ruleInts[0])].Add(Convert.ToInt32(ruleInts[1]));
        }

        var rows = new List<List<int>>();
        foreach (var row in rowsStrings)
        {
            var rowInts = row.Split(",");
            rows.Add(rowInts.Select(x => Convert.ToInt32(x)).ToList());
        }


        var goodRows = new List<List<int>>();
        var badRows = new List<List<int>>();

        foreach (var row in rows)
        {
            var rowIsGood = true;

            foreach (var mainElement in row)
            {
                if (!rowIsGood)
                {
                    break;
                }
                var nextNumbers = new List<int>();
                if (originalRules.TryGetValue(Convert.ToInt32(mainElement), out nextNumbers))
                {
                    var mainElementIndex = row.FindIndex(x => x == mainElement);

                    foreach (var nextNumber in nextNumbers)
                    {
                        var nextNumberIndex = row.FindIndex(x => x == nextNumber);
                        if ((nextNumberIndex >= 0) && (nextNumberIndex < mainElementIndex))
                        {
                            rowIsGood = false;
                            break;
                        }
                    }
                }
            }

            if (rowIsGood)
            {
                goodRows.Add(row);
                //Console.WriteLine("goodRow:");
                //Console.WriteLine(JsonSerializer.Serialize(row));
            }
            else
            {
                badRows.Add(row);
                //Console.WriteLine("badRow:");
                //Console.WriteLine(JsonSerializer.Serialize(row));
            }
        }
        ///////////////////////////////////////////////
        ///

        var globalRules = originalRules.Keys.ToList();
        var newGlobalRules = originalRules.Keys.ToList();
        
        foreach (var originalRule in originalRules)
        {
            var left = originalRule.Key;
            foreach (var right in originalRule.Value)
            {
                newGlobalRules.Sort((a, b) =>
                    a == left
                        ? b == right
                            ? -1//1
                            : 0
                        : b == left
                            ? a == right
                                ? 1//-1
                                : 0
                            : 0);
            }
        }

        newGlobalRules.Reverse();

        // Create a copy of originalRules.Keys to avoid modifying during iteration
        var originalRulesKeys = originalRules.Keys.ToList();

        //order inner rules
        for (int i = 0; i < 100; i++)
        {
            foreach (var originalRuleKey in originalRulesKeys)
            {
                foreach (var globalRule in newGlobalRules)
                {
                    var rules = originalRules[globalRule];
                    var left = globalRule;
                    
                    // Create a copy of rules to iterate over
                    var rulesCopy = rules.ToList();

                    foreach (var right in rulesCopy) //error //System.InvalidOperationException: 'Collection was modified; enumeration operation may not execute.'
                    {
                        originalRules[originalRuleKey].Sort((a, b) =>
                            a == left
                                ? b == right
                                    ? 1//-1//1
                                    : 0
                                : b == left
                                    ? a == right
                                        ? -1//1//-1
                                        : 0
                                    : 0);
                    }
                }
            }
        }


        for (int i = 0; i < 10000; i++)
        {
            foreach (var row in badRows)
            {
                foreach (var globalRule in newGlobalRules)
                {
                    var rules = originalRules[globalRule];
                    var left = globalRule;
                    foreach (var right in rules)
                    {
                        row.Sort((a, b) =>
                            a == left
                                ? b == right
                                    ? -1//1
                                    : 0
                                : b == left
                                    ? a == right
                                        ? 1//-1
                                        : 0
                                    : 0);
                    }
                }
            }
        }
        

        //var rowIsGood = true;

        //foreach (var mainElement in row)
        //{
        //    //if (!rowIsGood)
        //    //{
        //    //    break;
        //    //}
        //    var nextNumbers = new List<int>();
        //    if (originalRules.TryGetValue(Convert.ToInt32(mainElement), out nextNumbers))
        //    {
        //        var mainElementIndex = row.FindIndex(x => x == mainElement);

        //        foreach (var nextNumber in nextNumbers)
        //        {
        //            var nextNumberIndex = row.FindIndex(x => x == nextNumber);
        //            if ((nextNumberIndex >= 0) && (nextNumberIndex < mainElementIndex))
        //            {
        //                rowIsGood = false;
        //                break;
        //            }
        //        }
        //    }
        //}

        //if (rowIsGood)
        //{
        //    goodRows.Add(row);
        //    Console.WriteLine("goodRow:");
        //    Console.WriteLine(JsonSerializer.Serialize(row));
        //}
        //else
        //{
        //    Console.WriteLine("badRow:");
        //    Console.WriteLine(JsonSerializer.Serialize(row));
        //}
        //}
        //////////////////////////////////////////////


        var sum = 0;
        foreach (var goodRow in badRows)
        {
            sum += goodRow[(goodRow.Count() - 1) / 2];
        }

        // 6431 too high
        //var res = a + a_1 + a_2 + a_3;

        return true;
    }
}
