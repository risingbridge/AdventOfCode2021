//You can always open a new chunk
//If a chunk opens with (, it must close with )
//If a chunk opens with [, it must close with ]
//If a chunk opens with {, it must close with }
//If a chunk opens with <, it must close with >

string[] inputArray = File.ReadAllLines("./test.txt");
List<string> syntax = new List<string>();
//Set to true to do char by char
bool charByChar = false;
bool timeDelayPartOne = false;
bool timeDelayPartTwo = false;
int timeDelay = 50;

//PART ONE
List<SyntaxLine> syntaxLines = new List<SyntaxLine>();
syntax = inputArray.ToList();
long illegalCharSum = 0;
foreach (string line in syntax)
{
    string syntaxString = string.Empty;
    Stack<char> syntaxStack = new Stack<char>();
    lineState state = lineState.unknown;
    bool foundError = false;
    bool completeLine = true;
    foreach (char newChar in line)
    {
        if(syntaxStack.Count <= 0)
        {
            syntaxStack.Push(newChar);
        }
        else
        {
            if (isOpening(newChar))
            {
                syntaxStack.Push(newChar);
                Console.WriteLine($"Opening new chunk");
            }
            else
            {
                char prev = syntaxStack.Pop();
                if(isValidClose(prev, newChar))
                {
                    //It was valid
                    Console.WriteLine($"Valid close");
                }
                else
                {
                    Console.WriteLine($"Error with data!");
                    foundError = true;
                    illegalCharSum += IllegalCharValue(newChar);
                    break;
                }
            }
            
        }
        Console.WriteLine($"Testing line: {line}");
        Console.WriteLine(GenerateStackString(syntaxStack));
        if (charByChar)
        {
            Console.WriteLine("Enter to continue");
            Console.ReadLine();
            Console.Clear();
        }
        if (timeDelayPartOne)
        {
            Thread.Sleep(timeDelay);
            Console.Clear();
        }
    }
    if (foundError)
    {
        state = lineState.corrupted;
    }
    else if(!foundError && !completeLine)
    {
        state = lineState.incomplete;
    }else if(!foundError && completeLine)
    {
        state = lineState.complete;
    }
    SyntaxLine syntaxLine = new SyntaxLine
    {
        state = state,
        line = line
    };
    syntaxLines.Add(syntaxLine);
}


//PART one output
Console.WriteLine($"PART ONE:");
Console.WriteLine($"Total Syntax Error Score: {illegalCharSum}");

//Debug
//TODO: REMOVE
Console.WriteLine("Clear terminal? Y/N");
char ClearTermAnswer = Console.ReadLine()[0];
if(ClearTermAnswer == 'y' || ClearTermAnswer == 'Y')
{
    Console.Clear();
}

//PART TWO
Console.WriteLine($"PART TWO:");
Console.WriteLine($"Number of lines {inputArray.Length}, number of parsed lines: {syntaxLines.Count}");
List<string> incompleteLines = new List<string>();
foreach (SyntaxLine item in syntaxLines)
{
    if(item.state == lineState.incomplete || item.state == lineState.complete)
    {
        incompleteLines.Add(item.line);
    }
}
Console.WriteLine($"Number of incomplete lines: {incompleteLines.Count}");
List<int> debugScores = new List<int>();
foreach (string line in incompleteLines)
{
    Stack<char> syntaxStack = new Stack<char>();
    for(int i = line.Length -1; i >= 0; i--)
    {
        syntaxStack.Push(line[i]);
    }

    string syntaxString = string.Empty;
    while(syntaxStack.Count > 0)
    {
        char newChar = syntaxStack.Pop();
        if(syntaxString.Length == 0)
        {
            syntaxString += newChar;
        }
        else
        {
            //Try to close
            char prevChar = syntaxString[syntaxString.Length - 1];
            //Console.WriteLine($"Testing {prevChar} vs {newChar}");
            if(isValidClose(prevChar, newChar))
            {
                //Console.WriteLine($"Is valid close: {isValidClose(prevChar, newChar)}");
                syntaxString.Remove(syntaxString.Length - 1, 1);
            }
            else
            {
                syntaxString += newChar;
            }
        }

        //Console.WriteLine(syntaxString);
        if (timeDelayPartTwo)
        {
            Thread.Sleep(timeDelay);
        }
    }
    //Console.WriteLine($"Original line: {line}");
    //Console.WriteLine($"Closed line: {syntaxString}\n");
    //Loop through to close open chunks
    List<char> needToComplete = new List<char>();
    for(int i = syntaxString.Length - 1; i >= 0; i--)
    {
        char lastChar = syntaxString[i];
        //Find closing char and add it to list, so we can calculate sum
        if (isOpening(lastChar))
        {
            char closeChar = FindValidClose(lastChar);
            needToComplete.Add(closeChar);
            syntaxString.Remove(i, 1);
        }
        else
        {
            //Find the closing part in the string
            for (int y = i - 1; y >= 0; y--)
            {
                char testChar = syntaxString[y];
                if(testChar == FindValidOpening(lastChar))
                {
                    syntaxString.Remove(i, 1);
                    syntaxString.Remove(y, 1);
                    i--;
                }
            }
        }
    }
    debugScores.Add(SumPartTwo(needToComplete));
    Console.WriteLine();
    Console.Write($"{line}\tis missing:\t");
    foreach (char item in needToComplete)
    {
        Console.Write(item);
    }
}

foreach (int item in debugScores)
{
    Console.WriteLine(item);
}

int SumPartTwo(List<char> list)
{
    int sum = 0;
    foreach (char c in list)
    {
        switch (c)
        {
            case ')':
                sum *= 5;
                sum += 1;
                break;
            case ']':
                sum *= 5;
                sum += 2;
                break;
            case '}':
                sum *= 5;
                sum += 3;
                break;
            case '>':
                sum *= 5;
                sum += 4;
                break;
        }
    }
    return sum;
}

char FindValidClose(char c)
{
    switch (c)
    {
        case '(':
            return ')';
            break;
        case '[':
            return ']';
            break;
        case '{':
            return '}';
            break;
        case '<':
            return '>';
            break;
        default:
            return 'X';
            break;
    }
}

char FindValidOpening(char c)
{
    switch (c)
    {
        case ')':
            return '(';
            break;
        case ']':
            return '[';
            break;
        case '}':
            return '{';
            break;
        case '>':
            return '<';
            break;
        default:
            return 'X';
            break;
    }
}


int IllegalCharValue(char c)
{
    switch (c)
    {
        case ')':
            return 3;
            break;
        case ']':
            return 57;
            break;
        case '}':
            return 1197;
            break;
        case '>':
            return 25137;
            break;
        default:
            return 0;
            break;
    }
}

bool isValidClose(char prev, char next)
{
    if(prev == '(' && next == ')')
    {
        return true;
    }else if(prev == '[' && next == ']')
    {
        return true;
    }else if(prev == '{' && next == '}')
    {
        return true;
    }else if(prev == '<' && next == '>')
    {
        return true;
    }
    return false;
}

string GenerateStackString(Stack<char> stack)
{
    string returnString = string.Empty;
    Stack<char> newStack = new Stack<char>(stack);
    char[] charArray = new char[newStack.Count];
    for (int i = 0; i < charArray.Length; i++)
    {
        charArray[i] = newStack.Pop();
    }
    foreach (char c in charArray)
    {
        returnString += c;
    }
    return returnString;
}

bool isOpening(char c)
{
    switch (c)
    {
        case '(':
            return true;
            break;
        case '[':
            return true;
            break;
        case '{':
            return true;
            break;
        case '<':
            return true;
            break;
        default:
            return false;
            break;
    }
}

enum lineState
{
    unknown,
    incomplete,
    corrupted,
    complete
}

class SyntaxLine
{
    public lineState state { get; set; }
    public string line { get; set; }
}