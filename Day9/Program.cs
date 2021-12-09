//Reads input
using Day9;

string[] inputArray = File.ReadAllLines("./input.txt");
//Finds X and Y-max
int xMax = inputArray[0].Length;
int yMax = inputArray.Length;
//Set to true to print tests
bool printTests = true;

//Creates and populates a 2D heightmap array
int[,] heightMap = new int[yMax, xMax];
for (int y = 0; y < inputArray.Length; y++)
{
    for (int x = 0; x < inputArray[y].Length; x++)
    {
        heightMap[y,x] = int.Parse(inputArray[y][x].ToString());
    }
}

//Solve part one
Console.WriteLine("PART ONE:");
List<int> lowestPointValues = new List<int>();
for (int y = 0; y < heightMap.GetLength(0); y++)
{
    for (int x = 0; x < heightMap.GetLength(1); x++)
    {
        if (isLowest(y, x))
        {
            lowestPointValues.Add(heightMap[y, x]);
            if (printTests)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
        }
        if (printTests)
        {
            Console.Write(heightMap[y, x]);
            Console.ResetColor();
        }
    }
    if (printTests)
    {
        Console.WriteLine();
    }
}

int riskLevelSum = 0;
foreach (int riskLevel in lowestPointValues)
{
    riskLevelSum += (riskLevel + 1);
}
Console.WriteLine($"Total risk level: {riskLevelSum}\n");

//Solve part two
List<Vector> lowestPoints = new List<Vector>();
for (int y = 0; y < heightMap.GetLength(0); y++)
{
    for (int x = 0; x < heightMap.GetLength(1); x++)
    {
        if (isLowest(y, x) && heightMap[y,x] != 9)
        {
            Vector currentPos = new Vector { y = y, x = x };
            lowestPoints.Add(currentPos);
        }
    }
}

Dictionary<Vector, List<Vector>> basinDictionary = new Dictionary<Vector, List<Vector>>();
List<List<Vector>> allBasins = new List<List<Vector>>();
for (int i = 0; i < lowestPoints.Count; i++)
{
    Vector basinStart = lowestPoints[i];
    List<Vector> basinPositions = new List<Vector>();
    basinPositions = new List<Vector>(FindBasin(basinStart));
    //Console.WriteLine($"Added basin with {basinPositions.Count} size");
    basinDictionary.Add(basinStart, RemoveDuplicates(basinPositions));
    allBasins.Add(RemoveDuplicates(basinPositions));
}

////Prints all basins for testing
//foreach (KeyValuePair<Vector, List<Vector>> item in basinDictionary)
//{
//    PrintBasin(item.Value, heightMap);
//    Console.WriteLine($"Basin has size {item.Value.Count}");
//    Console.WriteLine();
//}

//Print ALL basins
PrintMultipleBasins(allBasins, heightMap);

//Find the 3 biggest basins
List<List<Vector>> biggestBasins = new List<List<Vector>>();
for (int i = 0; i < 3; i++)
{
    List<Vector> largest = GetLargestBasin(allBasins);
    biggestBasins.Add(largest);
    allBasins.Remove(largest);
}
////Print 3 largest basins for test
//foreach (List<Vector> item in biggestBasins)
//{
//    PrintBasin(item, heightMap);
//    Console.WriteLine($"Basin size: {item.Count}");
//    Console.WriteLine();
//}

////Print 3 biggest basins in one map
//PrintMultipleBasins(biggestBasins, heightMap);

//multiply the 3 largest basins
long basinSum = biggestBasins[0].Count * biggestBasins[1].Count * biggestBasins[2].Count;
Console.WriteLine($"PART TWO:");
Console.WriteLine($"The sum is {basinSum}");

////FUNCTIONS
//Print all basins at one
void PrintMultipleBasins(List<List<Vector>> basins, int[,] heightMap)
{
    List<Vector> basin = new List<Vector>();
    foreach (List<Vector> item in basins)
    {
        foreach (Vector vector in item)
        {
            basin.Add(vector);
        }
    }

    for (int y = 0; y < heightMap.GetLength(0); y++)
    {
        for (int x = 0; x < heightMap.GetLength(1); x++)
        {
            int heightValue = heightMap[y, x];
            Vector currentPos = new Vector { x = x, y = y };
            if (DoesContainVector(currentPos, basin))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(heightValue);
            Console.ResetColor();
        }
        Console.WriteLine();
    }
}

//Find largest basin in all basins
List<Vector> GetLargestBasin(List<List<Vector>> allBasins){
    int largestBasinSize = int.MinValue;
    int largestBasin = 0;
    for (int i = 0; i < allBasins.Count; i++)
    {
        if(allBasins[i].Count > largestBasinSize)
        {
            largestBasinSize = allBasins[i].Count;
            largestBasin = i;
        }
    }
    return allBasins[largestBasin];
}

//Dumb fix because I dont want to find the error in the code
List<Vector> RemoveDuplicates(List<Vector> list)
{
    List<Vector> cleanList = new List<Vector>();
    cleanList.Add(list[0]);
    foreach (Vector vector in list)
    {
        int x = vector.x;
        int y = vector.y;
        bool canUse = true;
        foreach (Vector item in cleanList)
        {
            int iX = item.x;
            int iY = item.y;
            if(iX == x && iY == y)
            {
                canUse = false;
            }
        }
        if (canUse)
        {
            Vector vector1 = new Vector
            {
                x = x,
                y = y,
            };
            cleanList.Add(vector1);
        }
    }

    return cleanList;
}
//Function to return a list with all basin vectors
List<Vector> FindBasin(Vector basinStart)
{
    List<Vector> basinVectors = new List<Vector>();
    Queue<Vector> toCheck = new Queue<Vector>();
    toCheck.Enqueue(basinStart);
    while(toCheck.Count > 0)
    {
        Vector currentCheck = toCheck.Dequeue();
        basinVectors.Add(currentCheck);
        List<Vector> ValidTestPositions = new List<Vector>()
        {
            new Vector{y = currentCheck.y, x = currentCheck.x + 1 }, //Right
            new Vector{y = currentCheck.y, x = currentCheck.x - 1 }, //Left
            new Vector{y = currentCheck.y + 1, x = currentCheck.x }, //Bottom
            new Vector{y = currentCheck.y - 1, x = currentCheck.x }  //Top
        };
        foreach (Vector pos in ValidTestPositions)
        {
            if(isValidPosition(pos.y, pos.x))
            {
                if (heightMap[pos.y, pos.x] < 9)
                {
                    //Console.WriteLine($"Testing from position X:{currentCheck.x} Y:{currentCheck.y}");
                    //Console.WriteLine($"Testing position X: {pos.x} Y: {pos.y}");
                    if(!DoesContainVector(pos, basinVectors))
                    {
                        toCheck.Enqueue(pos);
                        //Console.WriteLine($"Adding x:{pos.x} y:{pos.y}");
                    }
                    else
                    {
                        //Console.WriteLine($"Is already in basin");
                    }
                }
            }
        }
    }
    
    return basinVectors;
}

void PrintBasin(List<Vector> basin, int[,] heightMap)
{
    for (int y = 0; y < heightMap.GetLength(0); y++)
    {
        for (int x = 0; x < heightMap.GetLength(1); x++)
        {
            int heightValue = heightMap[y, x];
            Vector currentPos = new Vector { x = x, y = y };
            if(DoesContainVector(currentPos, basin))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.Write(heightValue);
            Console.ResetColor();
        }
        Console.WriteLine();
    }
}

bool DoesContainVector(Vector check, List<Vector> list)
{
    foreach (Vector item in list)
    {
        int listX = item.x;
        int listY = item.y;
        if(listX == check.x && listY == check.y)
        {
            return true;
        }
    }
    return false;
}

//Function to see if position is lowest of adjacents
bool isLowest(int y, int x)
{
    //List of possible positions around this pos
    //List<Tuple<int, int>> vectors = new List<Tuple<int, int>>()
    //{
    //    new Tuple<int, int>(y, x + 1), //Right
    //    new Tuple<int, int>(y, x - 1), //Left
    //    new Tuple<int, int>(y + 1, x), //Bottom
    //    new Tuple<int, int>(y - 1, x)  //Top
    //};
    List<Vector> ValidTestPositions = new List<Vector>()
    {
        new Vector{y = y, x = x + 1 }, //Right
        new Vector{y = y, x = x - 1 }, //Left
        new Vector{y = y + 1, x = x }, //Bottom
        new Vector{y = y - 1, x = x }  //Top
    };
    int posValue = heightMap[y, x];
    bool isLowest = true;
    //If top line, do not check above
    //If bottom line, do not check below
    //If left, do not check left
    //If right, do not check right

    foreach (Vector pos in ValidTestPositions)
    {
        int checkX = pos.x;
        int checkY = pos.y;
        if(isValidPosition(checkY, checkX))
        {
            if(heightMap[checkY, checkX] <= posValue)
            {
                isLowest = false;
            }
        }
    }
    return isLowest;
}

//Function to return true if it is a valid position in the heightmap
bool isValidPosition(int y, int x)
{
    if(y < 0)
    {
        return false;
    }
    if(y >= yMax)
    {
        return false;
    }
    if(x < 0)
    {
        return false;
    }
    if(x >= xMax)
    {
        return false;
    }
    return true;
}