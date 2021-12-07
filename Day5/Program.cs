string[] input = File.ReadAllLines("./input.txt");
List<Line> lines = new List<Line>();
//True to print array between every added line
bool displayArray = false;
//True to also wait for keypress to add new line
bool displayAndWait = false;
foreach (string s in input)
{
    int xStart = 0;
    int yStart = 0;
    int xEnd = 0;
    int yEnd = 0;
    string[] split = s.Split(" -> ");
    xStart = int.Parse(split[0].Split(",")[0]);
    yStart = int.Parse(split[0].Split(",")[1]);
    xEnd = int.Parse(split[1].Split(",")[0]);
    yEnd = int.Parse(split[1].Split(",")[1]);

    //Console.WriteLine($"Line from X:{xStart}, Y:{yStart} to X:{xEnd}, Y{yEnd}");
    Vector start = new Vector
    {
        x = xStart,
        y = yStart
    };
    Vector end = new Vector
    {
        x = xEnd,
        y = yEnd
    };

    Line line = new Line()
    {
        start = start,
        end = end
    };
    lines.Add(setLowest(line));
    //Console.WriteLine($"Is the line straight? {isStraight(line)}\n");
}
//Console.WriteLine($"Largest X in dataset: {LargestValues().x}, Largest Y in dataset: {LargestValues().y}");

////Solve part one
//Create array to store lines
int[,] lineArray = new int[LargestValues().y + 1, LargestValues().x + 1];
//Populates array with zeroes
for (int y = 0; y < lineArray.GetLength(0); y++)
{
    for (int x = 0; x < lineArray.GetLength(1); x++)
    {
        lineArray[y,x] = 0;
    }
}


//Loop through lines to add them into the array
foreach (Line line in lines)
{
    //Only concider straight lines
    if (isStraight(line))
    {
        for (int y = line.start.y; y <= line.end.y; y++)
        {
            for (int x = line.start.x; x <= line.end.x; x++)
            {
                lineArray[y, x]++;
            }
        }
        if (displayArray)
        {
            Console.WriteLine($"Printing line from X:{line.start.x} Y:{line.start.y} to X:{line.end.x} Y:{line.end.y}");
            PrintArray();
            if (displayAndWait)
            {
                Console.ReadLine();
            }
        }
    }
}

//Finds number of crossings (where value is bigger then 1)
int crossingsCount = 0;
for(int y = 0; y < lineArray.GetLength(0); y++)
{
    for (int x = 0; x < lineArray.GetLength(1); x++)
    {
        if(lineArray[y,x] > 1)
        {
            crossingsCount++;
        }
    }
}
Console.WriteLine($"PART ONE: ");
Console.WriteLine($"Number of straight line-crossings: {crossingsCount}");

////SOLVE PART TWO

//Reset array with zeros
for (int y = 0; y < lineArray.GetLength(0); y++)
{
    for (int x = 0; x < lineArray.GetLength(1); x++)
    {
        lineArray[y, x] = 0;
    }
}

foreach (Line line in lines)
{
    foreach (Vector point in returnLineList(line))
    {
        lineArray[point.y, point.x]++;
    }
    if (displayArray)
    {
        Console.WriteLine($"Printing line from X:{line.start.x} Y:{line.start.y} to X:{line.end.x} Y:{line.end.y}");
        PrintArray();
        if (displayAndWait)
        {
            Console.ReadLine();
        }
    }
}

//Finds number of crossings (where value is bigger then 1)
crossingsCount = 0;
for (int y = 0; y < lineArray.GetLength(0); y++)
{
    for (int x = 0; x < lineArray.GetLength(1); x++)
    {
        if (lineArray[y, x] > 1)
        {
            crossingsCount++;
        }
    }
}

Console.WriteLine($"PART TWO: ");
Console.WriteLine($"Number of line-crossings: {crossingsCount}");

//// FUNCTIONS

//Function to return a list of vectors with all points in a line
List<Vector> returnLineList(Line line)
{
    List<Vector> lineList = new List<Vector>();
    Vector start = line.start;
    Vector end = line.end;
    if(start.y > end.y)
    {
        Vector temp = end;
        end = start;
        start = temp;
    }
    //If horizontal line
    if(start.y == end.y)
    {
        if(start.x > end.x)
        {
            Vector temp = end;
            end = start;
            start = temp;
        }
        for (int x = start.x; x <= end.x; x++)
        {
            Vector point = new Vector
            {
                x = x,
                y = start.y
            };
            lineList.Add(point);
        }
        return lineList;
    }
    //If vertical line
    if(start.x == end.x && start.y != end.y)
    {
        for(int y = start.y; y <= end.y; y++)
        {
            Vector point = new Vector
            {
                x = start.x,
                y = y
            };
            lineList.Add(point);
        }
        return lineList;
    }
    //If angle
    //Check if X increases or decreases
    if(end.x < start.x)
    {
        //Line goes to the left
        int x = start.x;
        for(int y = start.y; y <= end.y; y++)
        {
            Vector point = new Vector
            {
                x = x,
                y = y
            };
            lineList.Add(point);
            x--;
        }
        return lineList;
    }
    else
    {
        //Line goes to the right
        int x = start.x;
        for(int y = start.y; y <= end.y; y++)
        {
            Vector point = new Vector
            {
                x = x,
                y = y
            };
            lineList.Add(point);
            x++;
        }
        return lineList;
    }
}

//Function to print array
void PrintArray()
{
    for (int y = 0; y < lineArray.GetLength(1); y++)
    {
        for (int x = 0; x < lineArray.GetLength(0); x++)
        {
            if (lineArray[y, x] != 0)
            {
                Console.Write(lineArray[y, x]);
            }
            else
            {
                Console.Write(".");
            }
        }
        Console.WriteLine();
    }
}

//Function to set lowest value first, taking into concideration if its vertical or horizontal
Line setLowest(Line line)
{
    bool isVert = isVertical(line);
    if (isVert)
    {
        if(line.start.y > line.end.y)
        {
            Vector temp = line.start;
            line.start = line.end;
            line.end = temp;
        }
    }
    else
    {
        if(line.start.x > line.end.x)
        {
            Vector temp = line.start;
            line.start = line.end;
            line.end = temp;
        }
    }

    return line;
}

//Function to see if vertical or horizontal
bool isVertical(Line line)
{
    if(line.start.y != line.end.y)
    {
        return true;
    }
    else
    {
        return false;
    }
}

//Function to check if line is vertical or horizontal
bool isStraight(Line line)
{
    if(line.start.x == line.end.x || line.start.y == line.end.y)
    {
        return true;
    }
    else
    {
        return false;
    }
}

//Function to find the largest X and Y-values
(int x,int y) LargestValues()
{
    int largestX = 0;
    int largestY = 0;
    foreach (Line line in lines)
    {
        if(line.start.x > largestX)
        {
            largestX = line.start.x;
        }
        if(line.end.x > largestX)
        {
            largestX = line.end.x;
        }
        if(line.start.y > largestY)
        {
            largestY = line.start.y;
        }
        if(line.end.y > largestY)
        {
            largestY= line.end.y;
        }
    }
    return (largestX, largestY);
}


internal class Line
{
    public Vector start { get; set; }
    public Vector end { get; set; }
}

internal class Vector
{
    public int x { get; set; }
    public int y { get; set; }
}