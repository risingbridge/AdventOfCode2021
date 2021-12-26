using Day24;
using System.Diagnostics;

string[] input = File.ReadAllLines("./input.txt");
string inputValue = "25000000000000";
bool dumpMemoryEveryClock = false;
bool stepThrough = false;
bool verboseInstructions = false;
int clockTime = 0;

//////////     INITIALIZE THE ALU
Dictionary<string, int> memory = new Dictionary<string, int>();
memory.Add("w", 0);
memory.Add("x", 0);
memory.Add("y", 0);
memory.Add("z", 0);
int inputCounter = 0;
int programCounter = 0;
List<Instruction> programList = new List<Instruction>();
List<int> inputList = new List<int>();
inputList = LoadInput();

foreach (string item in input)
{
    string opCode = item.Split(" ")[0];
    string modA = string.Empty;
    string modB = string.Empty;
    if(item.Split(" ").Length > 1)
    {
        modA = item.Split(" ")[1];
    }
    if(item.Split(" ").Length > 2)
    {
        modB = item.Split(" ")[2];
    }
    Instruction inst = new Instruction(opCode, modA, modB);
    programList.Add(inst);
}
Console.WriteLine($"Program loaded with {programList.Count} instructions");

//////////     RUN PROGRAM
Console.WriteLine($"RUNNING");
bool isValidModelNumber = false;
long keepAliveCounter = 0;
while (!isValidModelNumber)
{
    if(long.Parse(inputValue) == 0)
    {
        Console.WriteLine("ERROR. Input value is zero!");
        Environment.Exit(0);
    }
    programCounter = 0;
    inputCounter = 0;
    RunProgram(programList, inputList);

    //if (!verboseInstructions || dumpMemoryEveryClock)
    //{
    //    if (keepAliveCounter >= 10000)
    //    {
    //        Console.Write(".");
    //        keepAliveCounter = 0;
    //    }
    //    keepAliveCounter++;
    //}
    //else
    //{
    //    Console.WriteLine($"\n\n===== NEW LOOP =====\n\n");
    //}

    if (memory["z"] == 0)
    {
        isValidModelNumber = true;
        Console.WriteLine($"Valid model number is {inputValue}");
        MemoryDump();
        break;
    }
    else
    {
        Console.WriteLine($"Input: \t{inputValue}");
        Console.WriteLine($"Z Value:\t{memory["z"]}");
        Console.WriteLine();
        long temp = long.Parse(inputValue);
        temp++;
        inputValue = temp.ToString();
        inputList.Clear();
        inputList = LoadInput();
        ClearMemory();
    }
}
Console.WriteLine();
Console.WriteLine($"Program ended. Dumping data:\n" +
    $"W: {memory["w"]}\n" +
    $"X: {memory["x"]}\n" +
    $"Y: {memory["y"]}\n" +
    $"Z: {memory["z"]}");

//////////     RUN PROGRAM FUNCTION
///            Valid instructions: inp a, add a b, mul a b, div a b, mod a b, eql a b
void RunProgram(List<Instruction> instructions, List<int> input)
{
    for (int i = 0; i < instructions.Count; i++)
    {
        Instruction currentInst = instructions[i];
        int currentInput = 0;
        if (inputCounter < input.Count)
        {
            currentInput = input[inputCounter];
        }
        if (verboseInstructions)
        {
            Console.Write($"Instruction:\t{currentInst.OpCode}\t{currentInst.ModA}\t{currentInst.ModB}");
            if (currentInst.OpCode == "inp")
            {
                Console.WriteLine($"{currentInput}");
            }
            else
            {
                Console.WriteLine();
            }
        }
        if (currentInst.OpCode == "inp")
        {
            INP(currentInst.ModA, currentInput);
            inputCounter++;
        }
        else if (currentInst.OpCode == "add")
        {
            ADD(currentInst.ModA, currentInst.ModB);
        }
        else if (currentInst.OpCode == "mul")
        {
            MUL(currentInst.ModA, currentInst.ModB);
        }
        else if (currentInst.OpCode == "div")
        {
            DIV(currentInst.ModA, currentInst.ModB);
        }
        else if (currentInst.OpCode == "mod")
        {
            MOD(currentInst.ModA, currentInst.ModB);
        }
        else if (currentInst.OpCode == "eql")
        {
            EQL(currentInst.ModA, currentInst.ModB);
        }
        if (dumpMemoryEveryClock)
        {
            MemoryDump();
        }
        if (stepThrough)
        {
            Console.ReadLine();
        }

        programCounter++;
        Thread.Sleep(clockTime);
    }
}

//////////     ALU INSTRUCTIONS

//inp a - Read an input value and write it to variable a.
void INP(string mem, int value)
{
    if (memory.ContainsKey(mem))
    {
        memory[mem] = value;
    }
    else
    {
        Console.WriteLine($"Trying to write to a memory address that doesnt exist!");
        Environment.Exit(0);
    }
}

//add a b - Add the value of a to the value of b, then store the result in variable a.
void ADD(string A, string B)
{
    int aVal = memory[A];
    int bVal = 0;
    if (int.TryParse(B, out int bInt))
    {
        bVal = bInt;
    }
    else
    {
        bVal = memory[B];
    }
    bVal += aVal;
    memory[A] = bVal;
}

//mul a b - Multiply the value of a by the value of b, then store the result in variable a.
void MUL(string A, string B)
{
    int aVal = memory[A];
    int bVal = 0;
    if(int.TryParse(B, out int bInt))
    {
        bVal = bInt;
    }
    else
    {
        bVal = memory[B];
    }
    bVal *= aVal;
    memory[A] = bVal;
}

//div a b - Divide the value of a by the value of b, truncate the result to an integer, then store the result in variable a. (Here, "truncate" means to round the value toward zero.)
void DIV(string A, string B)
{
    int aVal = memory[A];
    int bVal = 0;
    if (int.TryParse(B, out int bInt))
    {
        bVal = bInt;
    }
    else
    {
        bVal = memory[B];
    }
    bVal = (int)(aVal / bVal);
    memory[A] = bVal;
}

//mod a b - Divide the value of a by the value of b, then store the remainder in variable a. (This is also called the modulo operation.)
void MOD(string A, string B)
{
    int aVal = memory[A];
    int bVal = 0;
    if (int.TryParse(B, out int bInt))
    {
        bVal = bInt;
    }
    else
    {
        bVal = memory[B];
    }
    int mod = aVal % bVal;
    memory[A] = mod;
}

//eql a b - If the value of a and b are equal, then store the value 1 in variable a. Otherwise, store the value 0 in variable a.
void EQL(string A, string B)
{
    int bVal = 0;
    if (int.TryParse(B, out int bInt))
    {
        bVal = bInt;
    }
    else
    {
        bVal = memory[B];
    }
    if(memory[A] == bVal)
    {
        memory[A] = 1;
    }
    else
    {
        memory[A] = 0;
    }
}

void MemoryDump()
{
    Console.WriteLine($"Program Counter: {programCounter}\tDumping Memory:\n" +
    $"W: {memory["w"]}\n" +
    $"X: {memory["x"]}\n" +
    $"Y: {memory["y"]}\n" +
    $"Z: {memory["z"]}\n");
}

List<int> LoadInput()
{
    List<int> input = new List<int>();
    foreach (char c in inputValue)
    {
        input.Add(int.Parse(c.ToString()));
    }
    return input;
}

void ClearMemory()
{
    memory["w"] = 0;
    memory["x"] = 0;
    memory["y"] = 0;
    memory["z"] = 0;
}