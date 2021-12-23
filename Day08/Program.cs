//  0:      1:      2:      3:      4:
// aaaa    ....    aaaa    aaaa    ....
//b    c  .    c  .    c  .    c  b    c
//b    c  .    c  .    c  .    c  b    c
// ....    ....    dddd    dddd    dddd
//e    f  .    f  e    .  .    f  .    f
//e    f  .    f  e    .  .    f  .    f
// gggg    ....    gggg    gggg    ....

//  5:      6:      7:      8:      9:
// aaaa    aaaa    aaaa    aaaa    aaaa
//b    .  b    .  .    c  b    c  b    c
//b    .  b    .  .    c  b    c  b    c
// dddd    dddd    ....    dddd    dddd
//.    f  e    f  .    f  e    f  .    f
//.    f  e    f  .    f  e    f  .    f
// gggg    gggg    ....    gggg    gggg

//Number of segments pr number:
//0 = 6
//1 = 2 - unique
//2 = 5 -
//3 = 5 -
//4 = 4 - unique
//5 = 5 -
//6 = 6 -
//7 = 3 - unique
//8 = 7 - unique
//9 = 6

string[] inputArray = File.ReadAllLines("./input.txt");
//Split input data into signal pattern and output values
List<string> signalPatterns = new List<string>();
List<string> outputValues = new List<string>();
foreach (string input in inputArray)
{
    signalPatterns.Add(input.Split('|')[0]);
    outputValues.Add(input.Split('|')[1]);
}
//Console.WriteLine($"Added {signalPatterns.Count} to signalPatterns and {outputValues.Count} to outputValues");

//Count 1, 4, 7 and 8
int oneCount = 0;
int fourCount = 0;
int sevenCount = 0;
int eightCount = 0;
foreach (string value in outputValues)
{
    //Console.WriteLine($"Testing {value}");
    string[] values = value.Split(" ");
    foreach (string item in values)
    {
        if(item.Length == 2)
        {
            oneCount++;
        }else if(item.Length == 4)
        {
            fourCount++;
        }else if(item.Length == 3)
        {
            sevenCount++;
        }else if(item.Length == 7)
        {
            eightCount++;
        }
    }
}
Console.WriteLine($"PART ONE:");
Console.WriteLine($"Total count is {oneCount + fourCount + sevenCount + eightCount}\n\n");

//  0:      1:      2:      3:      4:
// aaaa    ....    aaaa    aaaa    ....
//b    c  .    c  .    c  .    c  b    c
//b    c  .    c  .    c  .    c  b    c
// ....    ....    dddd    dddd    dddd
//e    f  .    f  e    .  .    f  .    f
//e    f  .    f  e    .  .    f  .    f
// gggg    ....    gggg    gggg    ....

//  5:      6:      7:      8:      9:
// aaaa    aaaa    aaaa    aaaa    aaaa
//b    .  b    .  .    c  b    c  b    c
//b    .  b    .  .    c  b    c  b    c
// dddd    dddd    ....    dddd    dddd
//.    f  e    f  .    f  e    f  .    f
//.    f  e    f  .    f  e    f  .    f
// gggg    gggg    ....    gggg    gggg

//Number of segments pr number:
//0 = 6 - 8 - 1
//1 = 2 - unique
//2 = 5 -
//3 = 5 -
//4 = 4 - unique
//5 = 5 -
//6 = 6 -
//7 = 3 - unique
//8 = 7 - unique
//9 = 6 -

//Vi må finne tallene i rekkefølge
//Finne de kjente
//1 4 7 8
//Hvis den inneholder 4 og 7, er det 9
//Hvis den inneholder 7, og har seks segmenter er det 0
//Andre med seks segmenter er 6
//Hvis den inneholder 1, er det 3
//Den som inneholder 4-1 er 5
//Den andre med fem segmenter er to

////PART TWO

//Determine which segment is which code by finding every known number and mapping them to the segments
long totalSum = 0;
List<int> totalSumList = new List<int>();

for(int i = 0; i < signalPatterns.Count; i++)
{
    //Create array of known values
    string[] knownValues = new string[10];
    Dictionary<string, int> knownValuesDict = new Dictionary<string, int>();
    List<string> numbers = new List<string>();
    foreach (string item in signalPatterns[i].Split(" "))
    {
        if(item != string.Empty)
        {
            numbers.Add(item);
        }
    }
    //Find the known values (2,4,7,8)
    foreach (string item in numbers)
    {
        if (IsKnownValue(item))
        {
            knownValues[FindKnownValue(item)] = item;
        }
    }

    //Find nine ( contains 4 and 7)
    foreach (string item in numbers)
    {
        if(!IsFoundValue(item, knownValues)){
            if(DoesContainLetter(item, knownValues[7]) && DoesContainLetter(item, knownValues[4]))
            {
                knownValues[9] = item;
            }
        }
    }

    //Find zero (contains 7, six segments)
    foreach (string item in numbers)
    {
        if(!IsFoundValue(item, knownValues))
        {
            if(DoesContainLetter(item, knownValues[7]) && item.Length == 6)
            {
                knownValues[0] = item;
            }
        }
    }

    //Find six (Six segments)
    foreach (string item in numbers)
    {
        if(!IsFoundValue(item, knownValues))
        {
            if(item.Length == 6)
            {
                knownValues[6] = item;
            }
        }
    }

    //Find three (contains one)
    foreach (string item in numbers)
    {
        if(!IsFoundValue(item, knownValues))
        {
            if(DoesContainLetter(item, knownValues[7]) && item.Length == 5)
            {
                knownValues[3] = item;
            }
        }
    }
    //Finds five (Contains part 4, minus one)
    foreach (string item in numbers)
    {
        if(!IsFoundValue(item, knownValues))
        {
            if(IsFive(item, knownValues[1], knownValues[4]))
            {
                knownValues[5] = item;
            }
        }
    }

    //Finds two (Last with five segments)
    foreach (string item in numbers)
    {
        if(!IsFoundValue(item, knownValues))
        {
            if(item.Length == 5)
            {
                knownValues[2] = item;
            }
        }
    }

    //Populates a dictionary with known values for easier lookup
    for (int z = 0; z < knownValues.Length; z++)
    {
        string code = knownValues[z];
        if(code == null)
        {
            code = z.ToString();
        }
        knownValuesDict.Add(code, z);
    }
    //Print the string and the known values
    foreach (string item in numbers)
    {
        int value = 99;
        if (knownValuesDict.ContainsKey(item))
        {
            value = knownValuesDict[item];
        }
        //Console.WriteLine($"{item} is {value}");
    }

    ////Prints the list in order of numbers
    //int counter = 0;
    //foreach (string item in knownValues)
    //{
    //    Console.WriteLine($"Number: {counter} is {item}");
    //    counter++;
    //}

    //Process each number of the value string
    string TotalValueOfLine = String.Empty;
    //Console.WriteLine($"Processing line {signalPatterns[i]} | {outputValues[i]}");
    //Console.WriteLine($"Lookup table:");
    //for (int y = 0; y < knownValues.Length; y++)
    //{
    //    Console.WriteLine($"{y}:\t{knownValues[y]}");
    //}
    //Console.WriteLine($"Produces the value:");
    foreach(string item in outputValues[i].Split(" "))
    {
        if(item != String.Empty)
        {
            int value = LookupNumber(item, knownValues);
            if(value != 999)
            {
                //Console.WriteLine($"{value}:\t{item}");
                TotalValueOfLine += value.ToString();
            }
            else
            {
                //Console.WriteLine($"\n\nERROR\n\n");
            }
        }
    }
    totalSum += int.Parse(TotalValueOfLine);
    totalSumList.Add(int.Parse(TotalValueOfLine));
    //Console.WriteLine($"The total value of this line is {TotalValueOfLine}, total sum is now {totalSum}\n\n");
}
Console.WriteLine($"PART TWO:");
Console.WriteLine($"The total sum is {totalSum}");

int LookupNumber(string item, string[] knownValues)
{
    for(int i = 0; i < knownValues.Length; i++)
    {
        bool containsAll = true;
        foreach (char c in knownValues[i])
        {
            if (!item.Contains(c))
            {
                containsAll = false;
            }
        }
        if (containsAll && knownValues[i].Length == item.Length)
        {
            return i;
        }
    }

    //Returns unknown if it fails
    return 999;
}

//It is five if it contains parts of four, minus one
bool IsFive(string item, string one, string four)
{
    //Find the valid segments
    List<char> validSegments = new List<char>();
    foreach (char c in four)
    {
        if (!one.Contains(c))
        {
            validSegments.Add(c);
        }
    }

    foreach (char c in validSegments)
    {
        if (!item.Contains(c))
        {
            return false;
        }
    }
    return true;
}

bool DoesContainLetter(string item, string contains)
{
    foreach (char c in contains)
    {
        if (!item.Contains(c))
        {
            return false;
        }
    }
    return true;
}

bool IsFoundValue(string value, string[] knownValues)
{
    foreach (string item in knownValues)
    {
        if(value == item)
        {
            return true;
        }
    }
    return false;
}

bool IsKnownValue(string value)
{
    switch (value.Length)
    {
        case 2:
            return true;
            break;
        case 4:
            return true;
            break;
        case 3:
            return true;
            break;
        case 7:
            return true;
            break;
    }
    return false;
}

int FindKnownValue(string value)
{
    switch (value.Length)
    {
        case 2:
            return 1;
            break;
        case 4:
            return 4;
            break;
        case 3:
            return 7;
            break;
        case 7:
            return 8;
            break;
    }
    return 99;
}