string[] input = File.ReadAllLines("./input.txt");
List<string> inputList = new List<string>();
int inputLineLength = input[0].Length;

foreach (string s in input)
{
    inputList.Add(s);
}

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

    if(countOne > countZero)
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