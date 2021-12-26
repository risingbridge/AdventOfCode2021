using Day25;

string[] input = File.ReadAllLines("./input.txt");
int maxY = input.Length;
int maxX = input[0].Length;
Dictionary<string, Cucumber> cucumbers = new Dictionary<string, Cucumber>();
//Console.ResetColor();
Console.Clear();
ImportCucumbers();
Console.WriteLine("Initial state:");
PrintMap();
int counter = 0;
bool didAnyMove = true;
while (didAnyMove)
{
    counter++;
    Console.WriteLine($"After {counter} step:");
    didAnyMove = false;
    MoveCucumbersEast();
    MoveCucumbersSouth();
    Console.WriteLine();
    PrintMap();
}
Console.WriteLine("\n\nAll stopped moving!\n\n");
Console.WriteLine(counter);

void MoveCucumbersEast()
{
    Dictionary<string, Cucumber> newDict = new Dictionary<string, Cucumber>();
    //Find all moves
    foreach(KeyValuePair<string, Cucumber> c in cucumbers){
        if(c.Value.Type == CucumberType.South)
        {
            continue;
        }
        Cucumber current = c.Value;
        Vector currentPos = current.Pos;
        Vector eastPos = East(currentPos);
        if (!cucumbers.ContainsKey(eastPos.ToString()))
        {
            cucumbers[currentPos.ToString()].CanMove = true;
            didAnyMove = true;
        }
    }
    //Do all moves
    foreach (KeyValuePair<string, Cucumber> c in cucumbers)
    {
        if (!c.Value.CanMove)
        {
            newDict.Add(c.Key, c.Value);
        }
        else
        {
            Cucumber move = c.Value;
            move.CanMove = false;
            move.Pos = East(move.Pos);
            newDict.Add(move.Pos.ToString(), move);
        }
    }
    cucumbers = new Dictionary<string, Cucumber>(newDict);
    newDict.Clear();
}

void MoveCucumbersSouth()
{
    Dictionary<string, Cucumber> newDict = new Dictionary<string, Cucumber>();
    //Find all moves
    foreach (KeyValuePair<string, Cucumber> c in cucumbers)
    {
        if (c.Value.Type == CucumberType.East)
        {
            continue;
        }
        Cucumber current = c.Value;
        Vector currentPos = current.Pos;
        Vector southPos = South(currentPos);
        if (!cucumbers.ContainsKey(southPos.ToString()))
        {
            cucumbers[currentPos.ToString()].CanMove = true;
            didAnyMove = true;
        }
    }
    //Do all moves
    foreach (KeyValuePair<string, Cucumber> c in cucumbers)
    {
        if (!c.Value.CanMove)
        {
            newDict.Add(c.Key, c.Value);
        }
        else
        {
            Cucumber move = c.Value;
            move.CanMove = false;
            move.Pos = South(move.Pos);
            newDict.Add(move.Pos.ToString(), move);
        }
    }
    cucumbers = new Dictionary<string, Cucumber>(newDict);
    newDict.Clear();
}

void ImportCucumbers()
{
    //Import cucumbers
    for (int y = 0; y < maxY; y++)
    {
        for (int x = 0; x < maxX; x++)
        {
            char c = input[y][x];
            Vector pos = new Vector(x, y);
            CucumberType currentType = CucumberType.East;
            if (c == 'v')
            {
                currentType = CucumberType.South;
            }
            Cucumber newCucumber = new Cucumber(pos, currentType, false);
            if (c != '.')
            {
                cucumbers.Add(pos.ToString(), newCucumber);
                Console.WriteLine(newCucumber.ToString());
            }
        }
    }
    Console.WriteLine();
}

void PrintMap()
{
    for (int y = 0; y < maxY; y++)
    {
        for (int x = 0; x < maxX; x++)
        {
            if(cucumbers.ContainsKey(new Vector(x, y).ToString()))
            {
                Cucumber current = cucumbers[new Vector(x, y).ToString()];
                if(current.Type == CucumberType.East)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(">");
                    Console.ResetColor();
                }else if(current.Type == CucumberType.South)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("V");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(".");
            }
        }
        Console.WriteLine();
        Console.ResetColor();
    }
    Console.WriteLine();
}

bool CanMoveEast(Vector pos)
{
    if (cucumbers.ContainsKey(East(pos).ToString()))
    {
        return true;
    }
    return false;
}

bool CanMoveSouth(Vector pos)
{
    if (cucumbers.ContainsKey(South(pos).ToString()))
    {
        return true;
    }
    return false;
}

Vector South(Vector pos)
{
    Vector newPos = new Vector(pos.X, pos.Y);
    newPos.Y++;
    if(newPos.Y >= maxY)
    {
        newPos.Y = 0;
    }
    return newPos;
}

Vector East(Vector pos)
{
    Vector newPos = new Vector(pos.X, pos.Y);
    newPos.X++;
    if(newPos.X >= maxX)
    {
        newPos.X = 0;
    }
    return newPos;
}