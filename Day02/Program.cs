//Creates a list of puzzle input
List<InputCommand> inputCommands = new List<InputCommand>();

string[] input = File.ReadAllLines("./input.txt");
foreach (string s in input)
{
    string[] split = s.Split(" ");
    InputCommand cmd = new InputCommand();
    cmd.Command = split[0];
    if(int.TryParse(split[1], out int i)){
        if(split[0] == "up")
        {
            i = i * -1;
        }
        cmd.Distance = i;
    }
    else
    {
        cmd.Distance = 0;
    }
    inputCommands.Add(cmd);
}

//Solves part one
int horizontalPos = 0;
int verticalPos = 0;

foreach (InputCommand command in inputCommands)
{
    if(command.Command == "forward")
    {
        horizontalPos += command.Distance;
    }
    else
    {
        verticalPos += command.Distance;
    }
}

double multipliedPos = horizontalPos * verticalPos;
Console.WriteLine($"PART ONE:\n" +
    $"Horizontal position: {horizontalPos}\n" +
    $"Vertical position: {verticalPos}\n" +
    $"Multiplied position: {multipliedPos}\n");


//Solves part two
horizontalPos = 0;
verticalPos = 0;
int aim = 0;
foreach (InputCommand c in inputCommands)
{
    if(c.Command == "forward")
    {
        horizontalPos += c.Distance;
        verticalPos += (aim * c.Distance);
    }
    else
    {
        aim += c.Distance;
    }
}
multipliedPos = horizontalPos * verticalPos;
Console.WriteLine($"PART TWO:\n" +
    $"Horizontal position: {horizontalPos}\n" +
    $"Vertical position: {verticalPos}\n" +
    $"Multiplied position: {multipliedPos}");


//Classes
public class InputCommand
{
    public string? Command;
    public int Distance;
}