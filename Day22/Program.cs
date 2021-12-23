using Day22;
using System.Diagnostics;

string[] input = File.ReadAllLines("./input.txt");
int minValue = int.MinValue;
int maxValue = int.MaxValue;

Queue<RebootStep> rebootSteps = new Queue<RebootStep>();
Dictionary<string, bool> cuboids = new Dictionary<string, bool>();
//Load reboot-steps
foreach (string line in input)
{
    //Operation
    string operation = line.Split(' ')[0];
    bool turnOn = false;
    if(operation == "on")
    {
        turnOn = true;
    }else if(operation == "off")
    {
        turnOn = false;
    }
    string temp = line.Replace("on ", "");
    temp = temp.Replace("off ", "");
    string[] tempSplit = temp.Split(',');
    //X
    string xFrom = tempSplit[0].Replace("x=", "").Split("..")[0];
    string xTo = tempSplit[0].Split("..")[1];
    //Y
    string yFrom = tempSplit[1].Replace("y=", "").Split("..")[0];
    string yTo = tempSplit[1].Split("..")[1];
    //Z
    string zFrom = tempSplit[2].Replace("z=", "").Split("..")[0];
    string zTo = tempSplit[2].Split("..")[1];
    //Add to operation
    Vector3 from = new Vector3(int.Parse(xFrom), int.Parse(yFrom), int.Parse(zFrom));
    Vector3 to = new Vector3(int.Parse(xTo), int.Parse(yTo), int.Parse(zTo));
    RebootStep step = new RebootStep(from, to, turnOn);
    if (IsStepWithinValidRange(from, to, minValue, maxValue))
    {
        rebootSteps.Enqueue(step);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"Added to queue: \t");
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"NOT added to queue:\t");
        Console.ResetColor();
    }
    Console.WriteLine($"Operation: {operation}\tFrom: {from.ToString()}\tTo: {to.ToString()}");
    Console.WriteLine();
}

Console.WriteLine($"Number of reboot-steps: {rebootSteps.Count}");

//Run through reboot-queue
int counter = 0;
Stopwatch sw = new Stopwatch();
while(rebootSteps.Count > 0)
{
    Console.Write($"Running step number {counter + 1}");
    sw.Start();
    RebootStep currentStep = rebootSteps.Dequeue();
    for (int x = currentStep.StepFrom.X; x <= currentStep.StepTo.X; x++)
    {
        for (int y = currentStep.StepFrom.Y; y <= currentStep.StepTo.Y; y++)
        {
            for (int z = currentStep.StepFrom.Z; z <= currentStep.StepTo.Z; z++)
            {
                Vector3 currentCube = new Vector3(x, y, z);
                bool currentOperation = currentStep.TurnOn;
                if (cuboids.ContainsKey(currentCube.ToString()))
                {
                    cuboids[currentCube.ToString()] = currentOperation;
                }
                else
                {
                    cuboids.Add(currentCube.ToString(), currentOperation);
                }
            }
        }
    }
    sw.Stop();
    Console.ForegroundColor = ConsoleColor.Green;
    long elapsedTime = sw.ElapsedMilliseconds;
    string timeUnit = "milliseconds";
    if(sw.ElapsedMilliseconds > 2000)
    {
        elapsedTime = (long)sw.ElapsedMilliseconds / 1000;
        timeUnit = "seconds";
    }
    Console.WriteLine($"\t-\tCompleted in {elapsedTime} {timeUnit}");
    Console.ResetColor();
    sw.Reset();
    counter++;
}

Console.WriteLine($"Number of cuboids: {cuboids.Count}");
Console.WriteLine($"Number of cuboids that are turned on: {CountNumberOfOnInDict(cuboids)}");

int CountNumberOfOnInDict(Dictionary<string, bool> dict)
{
    int counter = 0;
    foreach (KeyValuePair<string, bool> item in dict)
    {
        if (item.Value)
        {
            counter++;
        }
    }
    return counter;
}

bool IsStepWithinValidRange(Vector3 from, Vector3 to, int minValue, int maxValue)
{
    if(from.X < minValue && to.X < minValue)
    {
        return false;
    }
    if(from.Y < minValue && to.Y < minValue)
    {
        return false;
    }
    if(from.Z < minValue && to.Z < minValue)
    {
        return false;
    }
    if(from.X > maxValue && to.X > maxValue)
    {
        return false;
    }
    if(from.Y > maxValue && to.Y > maxValue)
    {
        return false;
    }
    if (from.Z > maxValue && to.Z > maxValue)
    {
        return false;
    }
    return true;
}