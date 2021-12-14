string[] input = File.ReadAllLines("./input.txt");

int iterations = 40;
string polymerString = string.Empty;
Dictionary<string, string> lookupDict = new Dictionary<string, string>();
foreach (string line in input)
{
    if (!string.IsNullOrEmpty(line) && !line.Contains("->"))
    {
        polymerString = line;
    }
    else if (line.Contains("->"))
    {
        string key = line.Split(" -> ")[0];
        string value = line.Split(" -> ")[1];
        lookupDict.Add(key, value);
    }
}
List<char> polymerList = new List<char>();
for (int i = 0; i < polymerString.Length; i++)
{
    polymerList.Add(polymerString[i]);
}

//Load into polymerDict
Dictionary<string, long> polymerDict = new Dictionary<string, long>();
for (int i = 0; i < polymerString.Length - 1; i++)
{
    char a = polymerString[i];
    char b = polymerString[i + 1];
    string subPol = a.ToString() + b.ToString();
    if (!polymerDict.ContainsKey(subPol))
    {
        polymerDict.Add(subPol, 1);
    }
    else
    {
        polymerDict[subPol]++;
    }
}

Dictionary<string, long> charCount = new Dictionary<string, long>();
foreach (char c in polymerString)
{
    if (!charCount.ContainsKey(c.ToString()))
    {
        charCount.Add(c.ToString(), 1);
    }
    else
    {
        charCount[c.ToString()]++;
    }
}
Console.WriteLine("Load:");
Console.WriteLine("Pair:");
foreach (KeyValuePair<string, long> item in polymerDict)
{
    Console.WriteLine($"{item.Key}: {item.Value}");
}

Console.WriteLine("Signles:");
foreach (KeyValuePair<string, long> item in charCount)
{
    Console.WriteLine($"{item.Key}: {item.Value}");
}

Console.WriteLine("\n\nDEBUG:\n");
for (int i = 1; i <= iterations; i++)
{
    Console.WriteLine($"Iteration {i}");
    Dictionary<string, long> tempDict = new Dictionary<string, long>(polymerDict);
    foreach (KeyValuePair<string, long> item in polymerDict)
    {
        //Hvis pair-count er 0, drit i det
        if (item.Value <= 0)
        {
            //Drit i det
        }
        else
        {
            //Remove the pair that is split
            tempDict[item.Key] -= item.Value;
            //Da må de to bokstavene også fjernes fra char-lista
            string a = item.Key[0].ToString();
            string b = item.Key[1].ToString();
            string newChar = lookupDict[a + b];
            Console.WriteLine($"{a}{b} -> {newChar}");
            if (charCount.ContainsKey(newChar))
            {
                charCount[newChar] += item.Value;
            }
            else
            {
                charCount.Add(newChar, item.Value);
            }
            string newA = a + newChar;
            string newB = newChar + b;
            Console.WriteLine($"This creates new pairs {newA} and {newB}");
            if (tempDict.ContainsKey(newA))
            {
                tempDict[newA] += item.Value;
            }
            else
            {
                tempDict.Add(newA, item.Value);
            }
            if (tempDict.ContainsKey(newB))
            {
                tempDict[newB] += item.Value;
            }
            else
            {
                tempDict.Add(newB, item.Value);
            }
        }
    }
    polymerDict = new Dictionary<string, long>(tempDict);
    Console.WriteLine("Pair:");
    foreach (KeyValuePair<string, long> item in polymerDict)
    {
        Console.WriteLine($"{item.Key}: {item.Value}");
    }

    Console.WriteLine("Signles:");
    foreach (KeyValuePair<string, long> item in charCount)
    {
        Console.WriteLine($"{item.Key}: {item.Value}");
    }
    Console.WriteLine();
}

long totalCharCount = 0;
foreach (KeyValuePair<string, long> item in charCount)
{
    totalCharCount += item.Value;
}

//Find least and most used char
long minVal = long.MaxValue;
long maxVal = long.MinValue;
string minChar = string.Empty;
string maxChar = string.Empty;
foreach (KeyValuePair<string, long> item in charCount)
{
    if(item.Value > maxVal)
    {
        maxVal = item.Value;
        maxChar = item.Key;
    }else if(item.Value < minVal)
    {
        minVal = item.Value;
        minChar = item.Key;
    }
}


Console.WriteLine($"Total length: {totalCharCount}");
Console.WriteLine($"Least used: {minChar} with {minVal} uses");
Console.WriteLine($"Most used: {maxChar} with {maxVal} uses");
Console.WriteLine($"Puzzle Answer with {iterations} iterations: {maxVal - minVal}");