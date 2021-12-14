using Day13;

string[] inputArray = File.ReadAllLines("./input.txt");

List<string> foldInstruction = new List<string>();
List<Vector> inputVectors = new List<Vector>();
int maxX = int.MinValue;
int maxY = int.MinValue;
foreach (string line in inputArray)
{
    if (string.IsNullOrEmpty(line))
    {

    }else if (line.Contains("fold"))
    {
        foldInstruction.Add(line);
    }
    else
    {
        int x = int.Parse(line.Split(",")[0]);
        int y = int.Parse(line.Split(",")[1]);
        if (x > maxX) maxX = x;
        if (y > maxY) maxY = y;
        inputVectors.Add(new Vector(x, y));
    }
}

Console.WriteLine($"Input contains {inputVectors.Count} vectors, and {foldInstruction.Count} fold-instructions");
Console.WriteLine($"Max X: {maxX} - Max Y: {maxY}");
//Create 2D array
char[,] map = new char[maxY + 1, maxX + 1];
for (int y = 0; y < map.GetLength(0); y++)
{
    for (int x = 0; x < map.GetLength(1); x++)
    {
        Vector v = new Vector(x, y);
        if (Vector.ListContain(inputVectors, v))
        {
            map[y, x] = '#';
        }
        else
        {
            map[y, x] = '.';
        }
    }
}

//Console.WriteLine($"INPUT");
//PrintMap();
//Console.WriteLine();

Console.WriteLine($"Fold-instruction: {foldInstruction[0]}");
map = Fold(map, foldInstruction[0]);
Console.WriteLine($"PART ONE:");
Console.WriteLine($"There are {CountChar('#', map)} visible dots!\n");
//Console.WriteLine($"Enter to continue");
//Console.ReadLine();


//PART TWO
Console.WriteLine($"PART TWO:");
map = new char[maxY + 1, maxX + 1];
//Reload input
for (int y = 0; y < map.GetLength(0); y++)
{
    for (int x = 0; x < map.GetLength(1); x++)
    {
        Vector v = new Vector(x, y);
        if (Vector.ListContain(inputVectors, v))
        {
            map[y, x] = '#';
        }
        else
        {
            map[y, x] = '.';
        }
    }
}
foreach (string line in foldInstruction)
{
    Console.WriteLine($"Fold instruction: {line}");
    //Do the folding, old fold function does not work for some reason
    int foldPos = int.Parse(line.Split("=")[1].ToString());
    if (line.Contains('x'))
    {
        //Flip along vertical axis
        //First, find width of left half and right half
        //left half is total width - split pos
        int halfWidth = map.GetLength(1) - foldPos;
        char[,] leftHalf = new char[map.GetLength(0), halfWidth];
        char[,] rightHalf = new char[map.GetLength(0), halfWidth];
        for(int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if(x == foldPos) //Drop the fold line
                {

                }else if(x < foldPos) //Add to left half
                {
                    leftHalf[y, x] = map[y, x];
                }else if(x > foldPos) //Add to right half
                {
                    rightHalf[y, x - foldPos] = map[y, x];
                }
            }
        }

        //Flipp right array
        char[,] flippedRight = new char[map.GetLength(0), halfWidth];
        for (int y = 0; y < leftHalf.GetLength(0); y++)
        {
            for (int x = 0; x < leftHalf.GetLength(1); x++)
            {
                int flippedX = leftHalf.GetLength(1) - 1 - x;
                flippedRight[y, x] = rightHalf[y, flippedX];
            }
        }

        //Merge arrays
        for (int y = 0; y < leftHalf.GetLength(0); y++)
        {
            for (int x = 0; x < leftHalf.GetLength(1); x++)
            {
                char c = leftHalf[y, x];
                if(c != '#')
                {
                    leftHalf[y,x] = flippedRight[y, x];
                }
            }
        }
        map = leftHalf;
    }
    else
    {
        //Flip along horizontal axis
        //First, find height of top half and bottom half
        //top half height is total height - split pos
        int halfHeight = map.GetLength(0) - foldPos;
        char[,] topHalf = new char[halfHeight, map.GetLength(1)];
        char[,] bottomHalf = new char[halfHeight, map.GetLength(1)];
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (y == foldPos) //Drop the fold line
                {
                }else if(y < foldPos) //Add everything above (lower pos) to top
                {
                    topHalf[y, x] = map[y, x];
                }else if(y > foldPos) //Add rest to below
                {
                    bottomHalf[y - foldPos, x] = map[y, x];
                }
            }
        }

        //Flip bottom array
        char[,] flippedBottom = new char[halfHeight, map.GetLength(1)];
        for (int y = 0; y < bottomHalf.GetLength(0); y++)
        {
            for (int x = 0; x < bottomHalf.GetLength(1); x++)
            {
                int flippedY = bottomHalf.GetLength(0) - 1 - y;
                flippedBottom[flippedY, x] = bottomHalf[y, x];
            }
        }

        //Merge arrays
        for (int y = 0; y < topHalf.GetLength(0); y++)
        {
            for (int x = 0; x < topHalf.GetLength(1); x++)
            {
                char c = topHalf[y, x];
                if(c != '#')
                {
                    topHalf[y, x] = flippedBottom[y, x];
                }
            }
        }
        //Store array into map
        map = topHalf;
    }
}
//Print
Console.WriteLine();
for (int y = 0; y < map.GetLength(0); y++)
{
    for (int x = 0; x < map.GetLength(1); x++)
    {
        Console.Write(map[y, x]);
    }
    Console.WriteLine();
}

////FUNCTIONS

long CountChar(char c, char[,] inputMap)
{
    long counter = 0;
    for(int y = 0; y < inputMap.GetLength(0); y++)
    {
        for (int x = 0; x < inputMap.GetLength(1); x++)
        {
            if(inputMap[y,x] == c)
            {
                counter++;
            }
        }
    }
    return counter;
}

char[,] Fold(char[,] inputMap, string foldInstruction)
{
    int foldPos = int.Parse(foldInstruction.Split("=")[1].ToString());
    if (foldInstruction.Contains('x'))
    {
        //FOLD Vertical (|)
        int xLength = (inputMap.GetLength(1) - 1) - foldPos;
        char[,] leftHalf = new char[inputMap.GetLength(0), xLength];
        char[,] rightHalf = new char[inputMap.GetLength(0), inputMap.GetLength(1) - xLength];
        for (int y = 0; y < inputMap.GetLength(0); y++)
        {
            for (int x = 0; x < inputMap.GetLength(1); x++)
            {
                if(x < foldPos)
                {
                    //Put into left map
                    leftHalf[y,x] = inputMap[y,x];
                }else if(x > foldPos)
                {
                    //Put into right map
                    rightHalf[y, x - foldPos] = inputMap[y, x];
                }
            }
        }
        //Merge
        for (int y = 0; y < leftHalf.GetLength(0); y++)
        {
            for (int x = 0; x < leftHalf.GetLength(1); x++)
            {
                int rightXPos = rightHalf.GetLength(1) - 1 - x;
                char c = leftHalf[y,x];
                if(c != '#')
                {
                    leftHalf[y, x] = rightHalf[y, rightXPos];
                }
            }
        }
        return leftHalf;
    }
    else
    {
        //FOLD Horizontal (---------)
        int yLength = (inputMap.GetLength(0)-1) - foldPos;
        //Split the arrays into top and bottom half
        char[,] topHalf = new char[yLength, inputMap.GetLength(1)];
        char[,] bottomHalf = new char[(inputMap.GetLength(0)) - yLength, inputMap.GetLength(1)];
        for (int y = 0;y < inputMap.GetLength(0); y++)
        {
            if(y < foldPos)
            {
                for(int x = 0;x < inputMap.GetLength(1); x++)
                {
                    topHalf[y,x] = inputMap[y, x];
                }
            }else if(y > foldPos)
            {
                for (int x = 0; x < inputMap.GetLength(1); x++)
                {
                    bottomHalf[y - foldPos, x] = inputMap[y, x];
                }
            }
        }
        //Merge
        for (int y = 0; y < topHalf.GetLength(0); y++)
        {
            for (int x = 0; x < topHalf.GetLength(1); x++)
            {
                int bottomYPos = bottomHalf.GetLength(0) - 1 - y;
                char c = topHalf[y, x];
                if(c != '#')
                {
                    topHalf[y, x] = bottomHalf[bottomYPos, x];
                }
            }
        }
        return topHalf;
    }
    return inputMap;
}

void PrintMap()
{
    for (int y = 0; y < map.GetLength(0); y++)
    {
        for (int x = 0; x < map.GetLength(1); x++)
        {
            Vector v = new Vector(x, y);
            if (Vector.ListContain(inputVectors, v))
            {
                Console.Write(map[y,x]);
            }
            else
            {
                Console.Write(map[y, x]);
            }
        }
        Console.WriteLine();
    }
}