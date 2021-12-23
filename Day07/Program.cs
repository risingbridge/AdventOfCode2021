string[] input = File.ReadAllLines("./input.txt");
List<int> crabPositions = new List<int>();
foreach (string line in input[0].Split(","))
{
    if(int.TryParse(line, out int crabPosition))
    {
        crabPositions.Add(crabPosition);
    }
    else
    {
        Console.WriteLine($"Error with crab position {line}");
    }
}

//Find min and max-positions
int minPos = int.MaxValue;
int maxPos = int.MinValue;
foreach (int crabPosition in crabPositions)
{
    if(crabPosition < minPos)
    {
        minPos = crabPosition;
    }
    if(crabPosition > maxPos)
    {
        maxPos = crabPosition;
    }
}

////Solve part one
//Loop through possible crab positions to calculate fuel expendature
//Console.WriteLine($"Min Pos: {minPos}, Max Pos: {maxPos}");
int bestPos = 0;
int bestFuel = int.MaxValue;
for(int testPos = minPos; testPos <= maxPos; testPos++)
{
    int totalFuelCost = 0;
    foreach (int pos in crabPositions)
    {
        if (pos < testPos)
        {
            int fuelUse = testPos - pos;
            totalFuelCost += fuelUse;
        }
        else if(pos > testPos)
        {
            int fuelUse = pos - testPos;
            totalFuelCost += fuelUse;
        }
    }
    if(totalFuelCost < bestFuel)
    {
        bestFuel = totalFuelCost;
        bestPos = testPos;
    }
    //Console.WriteLine($"Tested position {testPos}, total fuel use is {totalFuelCost}");
}
Console.WriteLine($"PART ONE:");
Console.WriteLine($"Best position is: {bestPos}");
Console.WriteLine($"Total fuel used is: {bestFuel}");


////Solve part two
//Loop through possible crab positions to calculate fuel expendature
//Console.WriteLine($"Min Pos: {minPos}, Max Pos: {maxPos}");
bestPos = 0;
bestFuel = int.MaxValue;
for (int testPos = minPos; testPos <= maxPos; testPos++)
{
    int totalFuelCost = 0;
    foreach (int pos in crabPositions)
    {
        if (pos < testPos)
        {
            int moves = testPos - pos;
            int fuelUse = 0;
            for (int i = 1; i <= moves; i++)
            {
                fuelUse += i;
            }

            totalFuelCost += fuelUse;
        }
        else if (pos > testPos)
        {
            int moves = pos - testPos;
            int fuelUse = 0;
            for (int i = 1; i <= moves; i++)
            {
                fuelUse += i;
            }

            totalFuelCost += fuelUse;
        }
    }
    if (totalFuelCost < bestFuel)
    {
        bestFuel = totalFuelCost;
        bestPos = testPos;
    }
    //Console.WriteLine($"Tested position {testPos}, total fuel use is {totalFuelCost}");
}
Console.WriteLine($"\n\nPART TWO:");
Console.WriteLine($"Best position is: {bestPos}");
Console.WriteLine($"Total fuel used is: {bestFuel}");