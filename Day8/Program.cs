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
//0 = 6 - 7 + (3 andre segmenter)
//1 = 2 - unique
//2 = 5 -
//3 = 5 -
//4 = 4 - unique
//5 = 5 -
//6 = 6 -
//7 = 3 - unique
//8 = 7 - unique
//9 = 6 - 7 + 4 + (et ekstra segment)

string[] inputArray = File.ReadAllLines("./smalltest.txt");
//Split input data into signal pattern and output values
List<string> signalPatterns = new List<string>();
List<string> outputValues = new List<string>();
foreach (string input in inputArray)
{
    signalPatterns.Add(input.Split('|')[0]);
    outputValues.Add(input.Split('|')[1]);
}
Console.WriteLine($"Added {signalPatterns.Count} to signalPatterns and {outputValues.Count} to outputValues");

//Count 1, 4, 7 and 8
int oneCount = 0;
int fourCount = 0;
int sevenCount = 0;
int eightCount = 0;
foreach (string value in outputValues)
{
    Console.WriteLine($"Testing {value}");
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

////PART TWO

//Determine which segment is which code by finding every known number and mapping them to the segments

for(int i = 0; i < signalPatterns.Count; i++)
{
    //Merge both signal and output to single number pile
    string completeString = signalPatterns[i] + outputValues[i];
    List<string> numbers = new List<string>();
    foreach (string item in completeString.Split(" "))
    {
        if(item != string.Empty)
        {
            numbers.Add(item);
        }
    }

    //Figure out which number it is
    foreach (string item in numbers)
    {
        if (IsKnownValue(item))
        {
            Console.WriteLine($"{item} is number {FindKnownValue(item)}");
        }
    }

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