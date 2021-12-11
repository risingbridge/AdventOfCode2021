//Sets up the map array and load the input data
using Day11;

string[] inputArray = File.ReadAllLines("./input.txt");
int maxColumns = inputArray[0].Length;
int maxRows = inputArray.Length;
int[,] mapArray = new int[maxRows, maxColumns];
for (int y = 0; y < inputArray.Length; y++)
{
    for (int x = 0; x < inputArray[y].Length; x++)
    {
        mapArray[y,x] = int.Parse(inputArray[y][x].ToString());
    }
}

//Settings
int maxSteps = 100;
bool stepThrough = false;

//PART ONE
Console.WriteLine($"Step number 0");
PrintMapArray(mapArray);

int flashCounter = 0;
//Loops through every step
for (int i = 1; i <= maxSteps; i++)
{
    Console.WriteLine($"\nStep number {i}");
    mapArray = Step(mapArray);
    flashCounter += FlashCount(mapArray);
    PrintMapArray(mapArray);
    if (stepThrough)
    {
        Console.ReadLine();
    }
}
Console.WriteLine($"PART ONE:");
Console.WriteLine($"After {maxSteps} steps, there are {flashCounter} flashes\n");

//Part two
//Reload the input
for (int y = 0; y < inputArray.Length; y++)
{
    for (int x = 0; x < inputArray[y].Length; x++)
    {
        mapArray[y, x] = int.Parse(inputArray[y][x].ToString());
    }
}
int numberOfoctopuses = mapArray.GetLength(0) * mapArray.GetLength(1);
bool simultaniousFlash = false;
int stepCounter = 1;
while (!simultaniousFlash)
{
    Console.WriteLine($"\nStep number {stepCounter}");
    mapArray = Step(mapArray);
    PrintMapArray(mapArray);
    if (stepThrough)
    {
        Console.ReadLine();
    }
    if(FlashCount(mapArray) == numberOfoctopuses)
    {
        simultaniousFlash = true;
        break;
    }
    stepCounter++;
}
Console.WriteLine($"PART TWO");
Console.WriteLine($"Simultanious flash occured first at step {stepCounter}");

//////// Step
///Every octopus increases by 1
///any octopus with energy level greater then 9 flashes
///Every neighbour of flashing octopus increases by 1 and flashes if it passed 9
///Every octopus that did flash sets to 0

void PrintMapArray(int[,] printArray)
{
    //Prints the map array for debug
    for (int y = 0; y < printArray.GetLength(0); y++)
    {
        for (int x = 0; x < printArray.GetLength(1); x++)
        {
            if (printArray[y,x] == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.Write(printArray[y, x]);
            Console.ResetColor();
        }
        Console.WriteLine();
    }
}

int FlashCount(int[,] map)
{
    int counter = 0;
    for (int y = 0; y < map.GetLength(0); y++)
    {
        for (int x = 0; x < map.GetLength(1); x++)
        {
            if (map[y, x] == 0) {
                counter++;
            }
        }
    }
    return counter;
}

int[,] Step(int[,] map)
{
    int[,] stepArray = map;
    //Increase all by one
    for (int y = 0; y < stepArray.GetLength(0); y++)
    {
        for (int x = 0; x < stepArray.GetLength(1); x++)
        {
            stepArray[y, x]++;
        }
    }
    //Flash octopusses and increase neighbours - Set flashed octopus to -100
    bool StillNotFlashedAll = true;
    while (StillNotFlashedAll)
    {
        StillNotFlashedAll = false;
        //Check if there still are any with energy higher then 9
        for (int y = 0; y < stepArray.GetLength(0); y++)
        {
            for (int x = 0; x < stepArray.GetLength(1); x++)
            {
                if(stepArray[y,x] > 9)
                {
                    StillNotFlashedAll = true;
                    break;
                }
            }
            if(StillNotFlashedAll)
            { 
                break;
            }
        }
        //Does the flashing
        for (int y = 0; y < stepArray.GetLength(0); y++)
        {
            for (int x = 0; x < stepArray.GetLength(1); x++)
            {
                if (stepArray[y, x] > 9)
                {
                    List<Vector> neighboursToIncrease = PossibleNeighbours(new Vector(x, y), stepArray);
                    foreach (Vector pos in neighboursToIncrease)
                    {
                        if (stepArray[pos.y, pos.x] != -100)
                        {
                            stepArray[pos.y, pos.x]++;
                        }
                    }
                    stepArray[y, x] = -100;
                }
            }
        }
    }
    
    //Sets all that flashed to 0
    for (int y = 0; y < stepArray.GetLength(0); y++)
    {
        for (int x = 0; x < stepArray.GetLength(1); x++)
        {
            if (stepArray[y, x] == -100)
            {
                stepArray[y, x] = 0;
            }
        }
    }

    return stepArray;
}

//////// Positions
///y-1, x-1         y-1, x             y-1, x+1
///y, x-1           y,x                y, x+1
///y+1,x-1          y+1,x              y+1,x+1
///

List<Vector> PossibleNeighbours(Vector currentPos, int[,] map)
{
    List<Vector> possibleNeighbours = new List<Vector>()
    {
        new Vector(currentPos.x - 1, currentPos.y - 1),     //Top left
        new Vector(currentPos.x, currentPos.y - 1),         //Top
        new Vector(currentPos.x + 1, currentPos.y - 1),     //Top right
        new Vector(currentPos.x - 1, currentPos.y),         //Left
        new Vector(currentPos.x + 1, currentPos.y),         //Right
        new Vector(currentPos.x - 1, currentPos.y + 1),     //Down left
        new Vector(currentPos.x, currentPos.y + 1),         //Down
        new Vector(currentPos.x + 1, currentPos.y + 1)     //Down right
    };
    List<Vector> neighbours = new List<Vector>();
    foreach (Vector vector in possibleNeighbours)
    {
        if(IsValidNeighbour(vector, map))
        {
            neighbours.Add(vector);
        }
    }
    return neighbours;
}

bool IsValidNeighbour(Vector currentPos, int[,] map)
{
    if(currentPos.x < 0 || currentPos.x >= map.GetLength(1) || currentPos.y < 0 || currentPos.y >= map.GetLength(0))
    {
        return false;
    }
    return true;
}