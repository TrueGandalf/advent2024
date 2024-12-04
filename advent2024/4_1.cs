using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace advent2024;

internal class _4_1
{
    public static bool Do()
    {
        var fileName = "4_1";
        //var fileName = "4_1_test";
        //var fileName = "4_1_small_test";
        var input = File.ReadAllText($"C:\\Personal\\advent2024\\advent2024\\{fileName}.txt").ToString();
        var inputText = input;

        string pattern1 = @"XMAS";
        RegexOptions options = RegexOptions.Multiline;

        var a1 = Regex.Matches(input, pattern1, options).Count();

        string pattern2 = @"SAMX";

        var a2 = Regex.Matches(input, pattern2, options).Count();

        //var sum = 0;
        //foreach (Match match in Regex.Matches(input, pattern, options))
        //{
        //    sum += Convert.ToInt32(match.Groups[1].Value)
        //        * Convert.ToInt32(match.Groups[2].Value);
        //}

        var byRows = inputText.Split('\n');

        var rows = new char[byRows.Length - 1][];

        for (var i = 0; i < byRows.Length - 1; i++)
        {
            var row = byRows[i];
            //if (row.Length == 1)
            //    continue;

            rows[i] = new char[row.Length];
            for (var j = 0; j < row.Length; j++)
            {
                rows[i][j] = row[j];
            }
        }

        var flip90 = new char[byRows[0].Length][];
        var input90Builder = new StringBuilder();

        for (int j = 0; j < rows[0].Length; j++)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                flip90[j] = flip90[j] ?? new char[rows.Length];
                flip90[j][i] = rows[i][j];//rows[j][i];// 
                input90Builder.Append(rows[i][j]);//(rows[j][i]);//
            }
            input90Builder.Append("\n");
        }

        var input90 = input90Builder.ToString();


        var b1 = Regex.Matches(input90, pattern1, options).Count();


        var b2 = Regex.Matches(input90, pattern2, options).Count();


        // 45----------

        var flip45List = new List<List<char>>();
        var input45Builder = new StringBuilder();

        var k = 0;
        for (int i = 0; i < rows.Length; i++)
        {
            for (int j = 0; j < rows[i].Length; j++)
            {
                try
                {
                    flip45List[i + j] = flip45List[i + j] ?? new List<char>();
                }
                catch
                {
                    flip45List.Add(new List<char>());
                }
                flip45List[i + j].Add(rows[i][j]);
            }
        }

        foreach (var list in flip45List)
        {

            //var line = string.Concat(list, "").ToString();
            input45Builder.Append(list.ToArray());
            input45Builder.Append("\n");
        }

        var input45 = input45Builder.ToString();

        var c1 = Regex.Matches(input45, pattern1, options).Count();
        var c2 = Regex.Matches(input45, pattern2, options).Count();
        // --------------45

        // 135----------

        var flip135List = new List<List<char>>();
        var input135Builder = new StringBuilder();

        k = 0;
        for (int i = 0; i < rows.Length; i++)

        {
            for (int j = rows[0].Length - 1; j >= 0; j--)
            {
                var antiJ = rows[0].Length - j - 1;
                try
                {
                    flip135List[i + antiJ] = flip135List[i + antiJ] ?? new List<char>();
                }
                catch
                {
                    flip135List.Add(new List<char>());
                }
                flip135List[i + antiJ].Add(rows[i][j]);
            }
        }

        foreach (var list in flip135List)
        {

            //var line = string.Concat(list, "").ToString();
            input135Builder.Append(list.ToArray());
            input135Builder.Append("\n");
        }

        var input135 = input135Builder.ToString();

        var d1 = Regex.Matches(input135, pattern1, options).Count();
        var d2 = Regex.Matches(input135, pattern2, options).Count();
        // --------------135


        var x = new List<int> { a1, a2, b1, b2, c1, c2, d1, d2 };

        var sum = a1 + a2 + b1 + b2 + c1 + c2 + d1 + d2;

        return true;
    }
}

internal class _4_2
{
    public static bool Do()
    {
        var fileName = "4_1";
        //var fileName = "4_2_test";
        //var fileName = "4_1_small_test";
        var input = File.ReadAllText($"C:\\Personal\\advent2024\\advent2024\\{fileName}.txt").ToString();
        var inputText = input;


        // pattern search

        var byRows = inputText.Split('\n');
        var rows = new char[byRows.Length - 1][];

        for (var i = 0; i < byRows.Length - 1; i++)
        {
            var row = byRows[i];

            rows[i] = new char[row.Length];

            for (var j = 0; j < row.Length; j++)
            {
                rows[i][j] = row[j];
            }
        }

        var a = CountA(rows);
        var a_1 = CountB(rows);
        var a_2 = CountC(rows);
        var a_3 = CountD(rows);
        var a_4 = CountE(rows);
        var a_5 = CountF(rows);
        // flip 1
        char[][] flip1 = _4_2.flip90(rows);
        var b = CountA(flip1);
        var b_1 = CountB(flip1);
        // flip 2
        char[][] flip2 = _4_2.flip90(flip1);
        var c = CountA(flip2);
        // flip 3
        char[][] flip3 = _4_2.flip90(flip2);
        var d = CountA(flip3);

        //var res = a + b + c + d;
        //var res = a + a_1 + b + b_1; // 1899
        //var res = a + a_1 + a_2 + a_3; // 1899 too low
        var res = a + a_1 + a_2 + a_3; // 1950 cause -2 not -3
        //var res = a + a_1 + a_2 + a_3 + a_4 + a_5; // 1961 too high

        return true;
    }

    private static int CountA(char[][] rows)
    {
        var a = 0;

        for (var i = 0; i < rows.Length - 2; i++)
        {
            for (var j = 0; j < rows[0].Length - 2; j++)
            {
                if (
                    rows[i + 0][j + 0] == 'M' /*&& rows[i + 0][j + 1] == 'M'*/ && rows[i + 0][j + 2] == 'S' &&
                    /*rows[i + 1][j + 0] == 'M' &&*/ rows[i + 1][j + 1] == 'A' && /*rows[i + 0][j + 2] == 'M' &&*/
                    rows[i + 2][j + 0] == 'M' /*&& rows[i + 2][j + 1] == 'M'*/ && rows[i + 2][j + 2] == 'S')

                    a++;
            }
        }

        return a;
    }

    private static int CountB(char[][] rows)
    {
        var a = 0;

        for (var i = 0; i < rows.Length - 2; i++)
        {
            for (var j = 0; j < rows[0].Length - 2; j++)
            {
                if (
                    rows[i + 0][j + 0] == 'S' /*&& rows[i + 0][j + 1] == 'M'*/ && rows[i + 0][j + 2] == 'M' &&
                    /*rows[i + 1][j + 0] == 'M' &&*/ rows[i + 1][j + 1] == 'A' && /*rows[i + 0][j + 2] == 'M' &&*/
                    rows[i + 2][j + 0] == 'S' /*&& rows[i + 2][j + 1] == 'M'*/ && rows[i + 2][j + 2] == 'M')

                    a++;
            }
        }

        return a;
    }

    private static int CountC(char[][] rows)
    {
        var a = 0;

        for (var i = 0; i < rows.Length - 2; i++)
        {
            for (var j = 0; j < rows[0].Length - 2; j++)
            {
                if (
                    rows[i + 0][j + 0] == 'S' /*&& rows[i + 0][j + 1] == 'M'*/ && rows[i + 0][j + 2] == 'S' &&
                    /*rows[i + 1][j + 0] == 'M' &&*/ rows[i + 1][j + 1] == 'A' && /*rows[i + 0][j + 2] == 'M' &&*/
                    rows[i + 2][j + 0] == 'M' /*&& rows[i + 2][j + 1] == 'M'*/ && rows[i + 2][j + 2] == 'M')

                    a++;
            }
        }

        return a;
    }

    private static int CountD(char[][] rows)
    {
        var a = 0;

        for (var i = 0; i < rows.Length - 2; i++)
        {
            for (var j = 0; j < rows[0].Length - 2; j++)
            {
                if (
                    rows[i + 0][j + 0] == 'M' /*&& rows[i + 0][j + 1] == 'M'*/ && rows[i + 0][j + 2] == 'M' &&
                    /*rows[i + 1][j + 0] == 'M' &&*/ rows[i + 1][j + 1] == 'A' && /*rows[i + 0][j + 2] == 'M' &&*/
                    rows[i + 2][j + 0] == 'S' /*&& rows[i + 2][j + 1] == 'M'*/ && rows[i + 2][j + 2] == 'S')

                    a++;
            }
        }

        return a;
    }

    private static int CountE(char[][] rows)
    {
        var a = 0;

        for (var i = 0; i < rows.Length - 3; i++)
        {
            for (var j = 0; j < rows[0].Length - 3; j++)
            {
                if (
                    rows[i + 0][j + 0] == 'S' && rows[i + 0][j + 1] == 'M' && rows[i + 0][j + 2] == 'M' &&
                    rows[i + 1][j + 0] == 'M' && rows[i + 1][j + 1] == 'A' && rows[i + 0][j + 2] == 'M' &&
                    rows[i + 2][j + 0] == 'M' /*&& rows[i + 2][j + 1] == 'M'*/ && rows[i + 2][j + 2] == 'S')

                    a++;
            }
        }

        return a;
    }

    private static int CountF(char[][] rows)
    {
        var a = 0;

        for (var i = 0; i < rows.Length - 3; i++)
        {
            for (var j = 0; j < rows[0].Length - 3; j++)
            {
                if (
                    rows[i + 0][j + 0] == 'M' /*&& rows[i + 0][j + 1] == 'M'*/ && rows[i + 0][j + 2] == 'S' &&
                    /*rows[i + 1][j + 0] == 'M' &&*/ rows[i + 1][j + 1] == 'A' && /*rows[i + 0][j + 2] == 'M' &&*/
                    rows[i + 2][j + 0] == 'S' /*&& rows[i + 2][j + 1] == 'M'*/ && rows[i + 2][j + 2] == 'M')

                    a++;
            }
        }

        return a;
    }

    private static char[][] flip90(char[][] rows)
    {
        var flip90 = new char[rows[0].Length][];

        for (int j = 0; j < rows[0].Length; j++)
        {
            for (int i = 0; i < rows.Length; i++)
            {
                flip90[j] = flip90[j] ?? new char[rows.Length];
                flip90[j][i] = rows[i][j];
            }
        }

        return flip90;
    }
}

