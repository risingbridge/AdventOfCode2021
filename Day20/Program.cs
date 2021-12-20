using Day20;
using System;
using System.Diagnostics;

string[] input = File.ReadAllLines("./input.txt");

string algorithm = input[0];
int NumberOfEnhancements = 50;
int padding = 20;
bool printDebugLines = false;
bool printEveryEnhancement = false;
int ySize = input.Length - 2;
int xSize = input[3].Length;
char[,] imageMap = new char[ySize, xSize];
Dictionary<string, char> imageDict = new Dictionary<string, char>();

for (int y = 0; y < ySize; y++)
{
    for (int x = 0; x < xSize; x++)
    {
        imageMap[y, x] = input[y + 2][x];
        Vector pos = new Vector(x, y);
        imageDict.Add(pos.ToString(), input[y + 2][x]);
    }
}
Console.WriteLine();
Console.WriteLine("Original input:");
PrintImage(imageMap);
Console.WriteLine();
Dictionary<string, char> imgToEnhance = new Dictionary<string, char>(imageDict);
Stopwatch sw = new Stopwatch();
for (int i = 0; i < NumberOfEnhancements; i++)
{
    sw.Start();
    Console.Write($"Enhancement number {i + 1}:");
    imgToEnhance = new Dictionary<string, char>(Enhance(imgToEnhance, padding));
    if((i+1) % 2 == 0)
    {
        imgToEnhance = new Dictionary<string, char>(DePadded(imgToEnhance, padding));
    }
    if (printEveryEnhancement)
    {
        PrintImageDict(imgToEnhance);
        Console.WriteLine();
    }
    sw.Stop();
    Console.WriteLine($"\tComplete, used {sw.ElapsedMilliseconds} ms");
    sw.Reset();
}
PrintImageDict(imgToEnhance);
Console.WriteLine($"Number of lit pixels after {NumberOfEnhancements} enhancements: {CountEveryLitPixel(imgToEnhance)}");

//Console.WriteLine($"Enter minimum-X for count:");
//string inputMinX = Console.ReadLine();
//Console.WriteLine($"Enter maximum-X for count:");
//string inputMaxX = Console.ReadLine();
//Console.WriteLine($"Enter minimum-Y for count:");
//string inputMinY = Console.ReadLine();
//Console.WriteLine($"Enter maximum-Y for count:");
//string inputMaxY = Console.ReadLine();

//int minX = int.MaxValue;
//int maxX = int.MinValue;
//int minY = int.MaxValue;
//int maxY = int.MinValue;
//if(int.TryParse(inputMinX, out minX) && int.TryParse(inputMaxX, out maxX))
//{
//}
//else
//{
//    Console.WriteLine("Something is wrong!");
//    Environment.Exit(0);
//}

//if(int.TryParse(inputMinY, out minY) && int.TryParse(inputMaxY, out maxY))
//{
//}
//else
//{
//    Console.WriteLine("Something is wrong!");
//    Environment.Exit(0);
//}
//Console.WriteLine($"MinX: {minX}, MaxX: {maxX}");
//Console.WriteLine($"MinY: {minY}, MaxY: {maxY}");

//Vector debugTopLeft = new Vector(minX, minY);
//Vector debugBottomRight = new Vector(maxX, maxY);
//Console.WriteLine($"Debug topleft: {debugTopLeft.ToString()}");
//Console.WriteLine($"Debug bottomright: {debugBottomRight.ToString()}");

//Console.WriteLine($"Debug-count: {DebugCountEveryLitPixel(imgToEnhance, debugTopLeft, debugBottomRight)}");


Dictionary<string, char> DePadded(Dictionary<string,char> img, int padding)
{
    Vector topLeft = new Vector(int.MaxValue, int.MaxValue);
    Vector bottomRight = new Vector(int.MinValue, int.MinValue);
    foreach (KeyValuePair<string, char> pixel in img)
    {
        int x = int.Parse(pixel.Key.Split(",")[0].Replace("X:", ""));
        int y = int.Parse(pixel.Key.Split(",")[1].Replace("Y:", ""));
        if (x < topLeft.X)
        {
            topLeft.X = x;
        }
        if (y < topLeft.Y)
        {
            topLeft.Y = y;
        }
        if (x > bottomRight.X)
        {
            bottomRight.X = x;
        }
        if (y > bottomRight.Y)
        {
            bottomRight.Y = y;
        }
    }
    topLeft.X += padding + 1;
    topLeft.Y += padding + 1;
    bottomRight.X -= padding + 1;
    bottomRight.Y -= padding + 1;
    Dictionary<string, char> dePadded = new Dictionary<string, char>();
    for (int y = topLeft.Y; y < bottomRight.Y; y++)
    {
        for (int x = topLeft.X; x < bottomRight.X; x++)
        {
            Vector current = new Vector(x, y);
            if (img.ContainsKey(current.ToString()))
            {
                dePadded.Add(current.ToString(), img[current.ToString()]);
            }
        }
    }
    foreach (KeyValuePair<string, char> pixel in dePadded)
    {
        int x = int.Parse(pixel.Key.Split(",")[0].Replace("X:", ""));
        int y = int.Parse(pixel.Key.Split(",")[1].Replace("Y:", ""));
        if (x < topLeft.X)
        {
            topLeft.X = x;
        }
        if (y < topLeft.Y)
        {
            topLeft.Y = y;
        }
        if (x > bottomRight.X)
        {
            bottomRight.X = x;
        }
        if (y > bottomRight.Y)
        {
            bottomRight.Y = y;
        }
    }
    //Console.WriteLine($"DePadded:");
    //PrintImageDict(dePadded);
    //Console.WriteLine($"dePadded min: {topLeft.ToString()}, dePadded max: {bottomRight.ToString()}");
    Vector actualTopLeft = new Vector(int.MaxValue, int.MaxValue);
    Vector actualBottomRight = new Vector(int.MinValue, int.MinValue);
    for (int y = topLeft.Y - 1; y < bottomRight.Y + 1; y++)
    {
        for (int x = topLeft.X - 1; x < bottomRight.X + 1; x++)
        {
            Vector current = new Vector(x, y);
            if (dePadded.ContainsKey(current.ToString()))
            {
                if (dePadded[current.ToString()] == '#')
                {
                    if(current.X < actualTopLeft.X)
                    {
                        actualTopLeft.X = current.X;
                    }
                    if(current.X > actualBottomRight.X)
                    {
                        actualBottomRight.X = current.X;
                    }
                    if(current.Y < actualTopLeft.Y)
                    {
                        actualTopLeft.Y = current.Y;
                    }
                    if(current.Y > actualBottomRight.Y)
                    {
                        actualBottomRight.Y = current.Y;
                    }
                }
            }
        }
    }
    Dictionary<string, char> newDePadded = new Dictionary<string, char>();
    for (int y = actualTopLeft.Y; y < actualBottomRight.Y + 2; y++)
    {
        for (int x = actualTopLeft.X; x < actualBottomRight.X + 2; x++)
        {
            Vector current = new Vector(x, y);
            if (img.ContainsKey(current.ToString()))
            {
                newDePadded.Add(current.ToString(), img[current.ToString()]);
            }
        }
    }
    //Console.WriteLine("newDePadded:");
    //PrintImageDict(newDePadded);
    return newDePadded;
}

Dictionary<string, char> Enhance(Dictionary<string, char> image, int padding)
{
    Dictionary<string, char> output = new Dictionary<string, char>();
    //Finn utkant av bilde og legg på en liten padding
    //Hvis top-left er 0,0, gjør top-left til -5,-5 og samme med bottom-right
    Vector topLeft = new Vector(int.MaxValue, int.MaxValue);
    Vector bottomRight = new Vector(int.MinValue, int.MinValue);
    foreach (KeyValuePair<string, char> pixel in image)
    {
        int x = int.Parse(pixel.Key.Split(",")[0].Replace("X:", ""));
        int y = int.Parse(pixel.Key.Split(",")[1].Replace("Y:", ""));
        if(x < topLeft.X)
        {
            topLeft.X = x;
        }
        if(y < topLeft.Y)
        {
            topLeft.Y = y;
        }
        if(x > bottomRight.X)
        {
            bottomRight.X = x;
        }
        if(y > bottomRight.Y)
        {
            bottomRight.Y = y;
        }
    }
    bottomRight.X += padding;
    bottomRight.Y += padding;
    topLeft.X -= padding;
    topLeft.Y -= padding;
    //Do the algorithm
    for (int y = topLeft.Y; y < bottomRight.Y; y++)
    {
        for (int x = topLeft.X; x < bottomRight.X; x++)
        {
            Vector current = new Vector(x, y);
            char pixel = '.';
            if (image.ContainsKey(current.ToString())){
                pixel = image[current.ToString()];
            }
            string binaryString = string.Empty;
            foreach (string n in PixelNeighbours(current))
            {
                if (image.ContainsKey(n))
                {
                    binaryString += image[n];
                }
                else
                {
                    binaryString += ".";
                }
            }
            if (printDebugLines)
            {
                Console.WriteLine($"Working on pixel X:{x}, Y:{y}\tValue is {pixel}\t" +
                    $"Binary-string: {binaryString}\t" +
                    $"Binary: {BinaryStringToValueString(binaryString)}\t" +
                    $"Decimal: {BinaryStringToDecimal(BinaryStringToValueString(binaryString))}\t" +
                    $"New Pixel Value:{GetEnhancedPixel(binaryString, algorithm)}");
            }
            output.Add(current.ToString(), GetEnhancedPixel(binaryString, algorithm));
        }
    }

    return output;
}

char GetEnhancedPixel(string s, string key)
{
    int decimalValue = BinaryStringToDecimal(BinaryStringToValueString(s));
    char output = key[decimalValue];
    return output;
}

string BinaryStringToValueString(string s)
{
    string outString = string.Empty;
    foreach (char c in s)
    {
        if(c == '.')
        {
            outString += "0";
        }else if(c == '#')
        {
            outString += "1";
        }
    }
    return outString;
}

int BinaryStringToDecimal(string s)
{
    int number = Convert.ToInt32(s, 2);
    return number;

}

List<string> PixelNeighbours(Vector pos)
{
    int x = pos.X;
    int y = pos.Y;
    List<string> neighbours = new List<string>();
    Vector topLeft = new Vector(x - 1, y - 1);
    Vector top = new Vector(x, y - 1);
    Vector topRight = new Vector(x + 1, y - 1);
    Vector left = new Vector(x - 1, y);
    Vector middle = pos;
    Vector right = new Vector(x + 1, y);
    Vector bottomLeft = new Vector(x - 1, y + 1);
    Vector bottom = new Vector(x, y + 1);
    Vector bottomRight = new Vector(x + 1, y + 1);

    neighbours.Add(topLeft.ToString());
    neighbours.Add(top.ToString());
    neighbours.Add(topRight.ToString());
    neighbours.Add(left.ToString());
    neighbours.Add(middle.ToString());
    neighbours.Add(right.ToString());
    neighbours.Add(bottomLeft.ToString());
    neighbours.Add(bottom.ToString());
    neighbours.Add(bottomRight.ToString());
    return neighbours;
}

void PrintImageDict(Dictionary<string, char> dict)
{
    Vector topLeft = new Vector(int.MaxValue, int.MaxValue);
    Vector bottomRight = new Vector(int.MinValue, int.MinValue);
    foreach (KeyValuePair<string, char> pixel in dict)
    {
        int x = int.Parse(pixel.Key.Split(",")[0].Replace("X:", ""));
        int y = int.Parse(pixel.Key.Split(",")[1].Replace("Y:", ""));
        if (x < topLeft.X)
        {
            topLeft.X = x;
        }
        if (y < topLeft.Y)
        {
            topLeft.Y = y;
        }
        if (x > bottomRight.X)
        {
            bottomRight.X = x;
        }
        if (y > bottomRight.Y)
        {
            bottomRight.Y = y;
        }
    }
    int xCounter = 0;
    
    for (int y = topLeft.Y - 1; y < bottomRight.Y; y++)
    {
        if(y == topLeft.Y - 1)
        {
            Console.Write("X: \t");
            for (int i = topLeft.X; i < bottomRight.X; i++)
            {
                if(xCounter > 9)
                {
                    xCounter = 0;
                }
                if(xCounter == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                Console.Write(xCounter);
                if (xCounter == 0)
                {
                    Console.ResetColor();
                }
                xCounter++;
            }
            Console.WriteLine();
            continue;
        }
        Console.Write($"{y}\t");
        for (int x = topLeft.X; x < bottomRight.X; x++)
        {
            Vector current = new Vector(x, y);
            char thisChar = '.';
            if (dict.ContainsKey(current.ToString()))
            {
                thisChar = dict[current.ToString()];
            }
            Console.Write(thisChar);
        }
        Console.WriteLine();
    }
}

void PrintImage(char[,] image)
{
    for (int y = 0; y < image.GetLength(0); y++)
    {
        for (int x = 0; x < image.GetLength(1); x++)
        {
            Console.Write(image[y, x]);
        }
        Console.WriteLine();
    }
}

long CountEveryLitPixel(Dictionary<string, char> img)
{
    long counter = 0;
    foreach (KeyValuePair<string,char> pixel in img)
    {
        if(pixel.Value == '#')
        {
            counter++;
        }
    }
    return counter;
}

long DebugCountEveryLitPixel(Dictionary<string, char> img, Vector topLeft, Vector bottomRight)
{
    long counter = 0;
    for (int y = topLeft.Y; y < bottomRight.Y; y++)
    {
        for (int x = topLeft.X; x < bottomRight.X; x++)
        {
            Vector current = new Vector(x, y);
            if (img.ContainsKey(current.ToString()))
            {
                if(img[current.ToString()] == '#')
                {
                    counter++;
                }
            }
        }
    }
    return counter;
}