using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace advent2024;

internal class _3_1
{
    public static bool Do()
    {
        var fileName = "3_1";
        var input = File.ReadAllText($"C:\\Personal\\advent2024\\advent2024\\{fileName}.txt").ToString();


        string pattern = @"mul[(](?'a'\d{1,3}),(?'b'\d{1,3})[)]";
        RegexOptions options = RegexOptions.Multiline;

        var sum = 0;
        foreach (Match match in Regex.Matches(input, pattern, options))
        {
            sum += Convert.ToInt32(match.Groups[1].Value)
                * Convert.ToInt32(match.Groups[2].Value);
        }


        return true;
    }
}

internal class _3_2
{
    public static bool Do()
    {
        var fileName = "3_1";
        var input = File.ReadAllText($"C:\\Personal\\advent2024\\advent2024\\{fileName}.txt").ToString();


        //string pattern1 = @"(don't\(\)).*(do\(\)){0,1}";
        string pattern1 = @"don't\(\)(.*?)(do\(\)|$)";
        string substitution1 = @"";
        RegexOptions options = RegexOptions.Singleline;

        var regex = new Regex(pattern1, options);
        string input2 = regex.Replace(input, substitution1);


        string pattern2 = @"mul[(](?'a'\d{1,3}),(?'b'\d{1,3})[)]";

        var sum = 0;
        foreach (Match match in Regex.Matches(input2, pattern2, options))
        {
            sum += Convert.ToInt32(match.Groups[1].Value)
                * Convert.ToInt32(match.Groups[2].Value);
        }

        return true;
    }
}
