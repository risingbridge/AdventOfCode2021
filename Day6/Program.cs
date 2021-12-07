using System.Diagnostics;

string[] input = File.ReadAllLines("./input.txt");
//Set number of days to simulate
int SimDaysPartOne = 80;
int SimDaysPartTwo = 256;
//Set to true if you want output of list each day
bool displayListEachDay = false;
//Set to true to print each days its simulated
bool displaySimulatedDay = false;

//Creates the initial fishes
List<Lanternfish> lanternfishList = new List<Lanternfish>();
string[] inputs = input[0].Split(",");
foreach (string item in inputs)
{
    Lanternfish lanternfish = new Lanternfish
    {
        Age = 0,
        InternalTimer = int.Parse(item)
    };
    lanternfishList.Add(lanternfish);
}
Console.WriteLine($"Added {lanternfishList.Count} fishes");
//Prints initial state:
string stateString = string.Empty;
foreach (Lanternfish fish in lanternfishList)
{
    stateString += fish.InternalTimer.ToString();
    stateString += ",";
}
//Console.WriteLine($"Initial state: \t\t {stateString}");

//Runs simulation for part one
List<Lanternfish> newFishes = new List<Lanternfish>();
Stopwatch sw = new Stopwatch();
for (int day = 1; day <= SimDaysPartOne; day++)
{
    sw.Start();
    for(int i = 0; i < lanternfishList.Count; i++)
    {
        if(lanternfishList[i].InternalTimer <= 0)
        {
            lanternfishList[i].InternalTimer = 6;
            //SpawnNewFish
            Lanternfish newFish = new Lanternfish
            {
                Age = 0,
                InternalTimer = 8
            };
            newFishes.Add(newFish);
        }
        else
        {
            lanternfishList[i].InternalTimer--;
        }
        lanternfishList[i].Age++;
    }
    if (newFishes.Count > 0)
    {
        foreach (Lanternfish lanternfish in newFishes)
        {
            lanternfishList.Add(lanternfish);
        }
    }
    newFishes.Clear();

    //Prints state of day
    if (displayListEachDay)
    {
        stateString = string.Empty;
        foreach (Lanternfish lanternfish in lanternfishList)
        {
            stateString += lanternfish.InternalTimer.ToString() + ",";
        }
        Console.WriteLine($"After \t {day} days: \t {stateString}");
    }
    sw.Stop();
    //Prints the completed day
    if (displaySimulatedDay)
    {
        Console.WriteLine($"Completed day {day}! Used {sw.ElapsedMilliseconds} milliseconds");
    }
    sw.Reset();
}

Console.WriteLine($"PART ONE:");
Console.WriteLine($"After {SimDaysPartOne} days, the total number of fish is: {lanternfishList.Count}");

//Solve part two
lanternfishList.Clear();
foreach (string item in inputs)
{
    Lanternfish lanternfish = new Lanternfish
    {
        Age = 0,
        InternalTimer = int.Parse(item)
    };
    lanternfishList.Add(lanternfish);
}
Console.WriteLine($"Added {lanternfishList.Count} fishes");
//Sort fish based on internal timer
long[] fishArray = new long[9];
for (int i = 0; i < lanternfishList.Count; i++)
{
    fishArray[lanternfishList[i].InternalTimer]++;
}
//Simulates
for (int day = 1; day <= SimDaysPartTwo; day++)
{
    long newFish = fishArray[0];
    for (int i = 1; i < fishArray.Length; i++)
    {
        fishArray[i - 1] = fishArray[i];
    }
    fishArray[8] = newFish;
    fishArray[6] += newFish;
}
long totalFishCount = fishArray.Sum();
Console.WriteLine($"PART TWO:");
Console.WriteLine($"After {SimDaysPartTwo} days, the number of fish is: {totalFishCount}");

internal class Lanternfish
{
    public int Age { get; set; }
    public int InternalTimer { get; set; }

}