namespace advent2024;

public class Data
{
    public static int[][] GetRowsOfElements(string fileName)
    {
        var inputText = File.ReadAllText($"C:\\Personal\\advent2024\\advent2024\\{fileName}.txt").ToString();
        var byRows = inputText.Split('\n');

        var rows = new int[byRows.Length - 1][];

        for (var i = 0; i < byRows.Length - 1; i++)
        {
            var row = byRows[i].Split(" ");
            //if (row.Length == 1)
            //    continue;

            rows[i] = new int[row.Length];
            for (var j = 0; j < row.Length; j++)
            {
                rows[i][j] = Convert.ToInt32(row[j]);
            }
        }

        return rows;
    }
}
