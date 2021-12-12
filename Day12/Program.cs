using Day12;

string[] inputArray = File.ReadAllLines("./input.txt");
Dictionary<string, Cave> caves = new Dictionary<string, Cave>();
foreach (string line in inputArray)
{
    string[] inputNodes = line.Split('-');
    for (int i = 0; i < inputNodes.Length; i++)
    {
        string nodeName = inputNodes[i];
        string from = string.Empty;
        string to = string.Empty;
        bool isBig = false;
        if (Char.IsUpper(nodeName[0]))
        {
            isBig = true;
        }
        if(i > 0)
        {
            from = inputNodes[i - 1];
        }
        if(i < inputNodes.Length - 1)
        {
            to = inputNodes[i + 1];
        }
        Cave? newCave = null;
        if (caves.ContainsKey(nodeName))
        {
            newCave = caves[nodeName];
            if (!newCave.PathTo.Contains(from))
            {
                if(!string.IsNullOrEmpty(from))
                {
                    newCave.PathTo.Add(from);
                }
            }
            if (!newCave.PathTo.Contains(to))
            {
                if(!string.IsNullOrWhiteSpace(to))
                {
                    newCave.PathTo.Add(to);
                }
            }
        }
        else
        {
            List<string> neighbours = new List<string>();
            if (!string.IsNullOrWhiteSpace(from))
            {
                neighbours.Add(from);
            }
            if (!string.IsNullOrWhiteSpace(to))
            {
                neighbours.Add(to);
            }
            newCave = new Cave(nodeName, isBig, neighbours);
            caves.Add(nodeName, newCave);
        }
    }
}


//Debug to display all nodes
foreach (KeyValuePair<string, Cave> cave in caves)
{
    Console.WriteLine($"Cave {cave.Key}:");
    Console.WriteLine($"Big cave? {cave.Value.isBig}");
    Console.Write($"Paths: ");
    foreach (string path in cave.Value.PathTo)
    {
        Console.Write($"{path}, ");
    }
    Console.WriteLine("\n");
}

//Pathfinding
Cave startCave = caves["start"];
Console.WriteLine($"Starting at {startCave.Name}");
