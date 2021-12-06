string[] input = File.ReadAllLines("./input.txt");
List<string> inputList = new List<string>();
int inputLineLength = input[0].Length;

foreach (string s in input)
{
    inputList.Add(s);
}
//// SOLVE PART ONE
//Finds Gamma Value
string gammaValueString = string.Empty;
for (int i = 0; i < inputLineLength; i++)
{
    gammaValueString += FindCommonValue(inputList, i, true);
}

float gammaValueDecimal = Convert.ToInt32(gammaValueString, 2);

//Finds Epsilon Value
string epsilonValueString = string.Empty;
for(int i = 0;i < inputLineLength; i++)
{
    epsilonValueString += FindCommonValue(inputList, i, false);
}
float epsilonValueDecimal = Convert.ToInt32(epsilonValueString, 2);

double powerConsumption = epsilonValueDecimal * gammaValueDecimal;

//Outputs part one
Console.WriteLine("PART ONE:");
Console.WriteLine($"Gamma Value Binary: {gammaValueString}");
Console.WriteLine($"Gamma Value Decimal: {gammaValueDecimal}");
Console.WriteLine($"Epsilon Value Binary: {epsilonValueString}");
Console.WriteLine($"Epsilon Value Decimal: {epsilonValueDecimal}");

Console.WriteLine($"Power Usage: {powerConsumption}");
Console.WriteLine("\n\n\n"); //Making some space between part one and two output, makes for easier readability of debug

////SOLVE PART TWO



//Create a working list for oxygen values and CO2-values
List<string> oxygenList = new List<string>();
List<string> co2List = new List<string>();
foreach (string item in inputList)
{
    oxygenList.Add(item);
    co2List.Add(item);
}

//Finds the oxygen value
for(int i = 0; i < inputLineLength; i++)
{
    if(oxygenList.Count <= 1)
    {
        //Console.WriteLine($"Only one left!");
        break;
    }
    //Find most common values for each position
    List<int> mostCommonValues = new List<int>();
    for (int y = 0; y < inputLineLength; y++)
    {
        mostCommonValues.Add(FindCommonValue(oxygenList, y, true));
    }
    List<string> tempList = new List<string>();
    //Console.WriteLine($"Check bit at position {i}. Bitcheck-value is {mostCommonValues[i]}");
    //Console.WriteLine($"Most common values this loop:");
    //foreach (int item in mostCommonValues)
    //{
    //    Console.Write($"{item} ");
    //}
    //Console.WriteLine();
    for (int z = 0; z < oxygenList.Count; z++)
    {
        int bitToCheck = int.Parse(oxygenList[z][i].ToString());
        bool isValid = false;
        if(bitToCheck == mostCommonValues[i])
        {
            isValid = true;
        }
        //Console.WriteLine($"Checking {oxygenList[z]} - Is Valid: {isValid}");
        if (isValid)
        {
            tempList.Add(oxygenList[z]);
        }
    }
    oxygenList.Clear();
    foreach (string item in tempList)
    {
        oxygenList.Add(item);
    }
    tempList.Clear();
}
//Console.WriteLine($"{oxygenList.Count} left in list - {oxygenList[0]}");


//Finds the CO2 value
for (int i = 0; i < inputLineLength; i++)
{
    if (co2List.Count <= 1)
    {
        //Console.WriteLine($"Only one left!");
        break;
    }
    //Find least common values for each position
    List<int> leastCommonValues = new List<int>();
    for (int y = 0; y < inputLineLength; y++)
    {
        leastCommonValues.Add(FindCommonValue(co2List, y, false));
    }
    List<string> tempList = new List<string>();
    //Console.WriteLine($"Check bit at position {i}. Bitcheck-value is {leastCommonValues[i]}");
    //Console.WriteLine($"Least common values this loop:");
    //foreach (int item in leastCommonValues)
    //{
    //    Console.Write($"{item} ");
    //}
    //Console.WriteLine();
    for (int z = 0; z < co2List.Count; z++)
    {
        int bitToCheck = int.Parse(co2List[z][i].ToString());
        bool isValid = false;
        if (bitToCheck == leastCommonValues[i])
        {
            isValid = true;
        }
        //Console.WriteLine($"Checking {co2List[z]} - Is Valid: {isValid}");
        if (isValid)
        {
            tempList.Add(co2List[z]);
        }
    }
    co2List.Clear();
    foreach (string item in tempList)
    {
        co2List.Add(item);
    }
    tempList.Clear();
}
//Console.WriteLine($"{co2List.Count} left in list - {co2List[0]}");

//Calculate decimal rating of oxygen and co2
Console.WriteLine($"PART TWO:");
float oxygenDecimal = Convert.ToInt32(oxygenList[0], 2);
float co2Decimal = Convert.ToInt32(co2List[0], 2);
Console.WriteLine($"Oxygen Decimal: {oxygenDecimal}, CO2 Decimal: {co2Decimal}");
double lifeRating = oxygenDecimal * co2Decimal;
Console.WriteLine($"Life Support Rating: {lifeRating}");

//Function to return most or least common value from list, in fixed position. True for most, false for least common
int FindCommonValue(List<string> inList, int pos, bool returnCommon)
{
    int countZero = 0;
    int countOne = 0;
    foreach (string s in inList)
    {
        switch (s[pos])
        {
            case '0':
                countZero++;
                break;
            case '1':
                countOne++;
                break;
        }
    }

    if(countOne > countZero || countOne == countZero)
    {
        if (returnCommon)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    if (returnCommon)
    {
        return 0;
    }
    return 1;
}