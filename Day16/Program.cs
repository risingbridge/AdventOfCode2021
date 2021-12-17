string[] lookup = File.ReadAllLines("./lookup.txt");
string input = File.ReadAllLines("./test.txt")[0];

Dictionary<char, string> lookupTable = new Dictionary<char, string>();
foreach (string line in lookup)
{
    string[] split = line.Split(" = ");
    char hex = split[0][0];
    string binaryValue = split[1];
    lookupTable.Add(hex, binaryValue);
}

foreach (KeyValuePair<char, string> item in lookupTable)
{
    Console.WriteLine($"{item.Key} = {item.Value}");
}

string binaryString = string.Empty;
foreach (char c in input)
{
    binaryString += lookupTable[c];
}

Console.WriteLine(input);
Console.WriteLine(binaryString);

int packetVersion = Convert.ToInt32(binaryString.Substring(0,3), 2);
int typeID = Convert.ToInt32(binaryString.Substring(3, 3), 2);
Console.WriteLine($"Packet version: {packetVersion}, Type ID: {typeID}");