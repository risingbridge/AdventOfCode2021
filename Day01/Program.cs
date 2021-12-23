//Creates a list of ints based in puzzle input
List<int> report = new List<int>();

string[] input = File.ReadAllLines("./input.txt");
foreach (string s in input)
{
    if(int.TryParse(s, out int i)){
        report.Add(i);
    }
}

//Solves the puzzle part 1
int largerCount = 0;
for (int i = 1; i < report.Count; i++)
{
    if(report[i] > report[i - 1])
    {
        largerCount++;
    }
}
Console.WriteLine($"There were {largerCount} larger measurements then previous");

//Solves the puzzle part 2
int lastSum = int.MaxValue;
largerCount = 0;
for(int i = 1; i < report.Count -1; i++)
{
    int a = report[i - 1];
    int b = report[i];
    int c = report[i + 1];
    int newSum = a + b + c;
    if(newSum > lastSum)
    {
        largerCount++;
    }
        lastSum = newSum;
}
Console.WriteLine($"There were {largerCount} larger measurements");