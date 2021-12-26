using Day23;

string[] input = File.ReadAllLines("./test.txt");
//Find width and height of map
int mapWidth = input[0].Length;
int mapHeight = input.Length;

Console.WriteLine($"Height: {mapHeight}, Width: {mapWidth}");
//Build map
for (int y = 0; y < mapHeight; y++)
{
    for (int x = 0; x < mapWidth; x++)
    {
        //if #, wall
        //if letter, Amphipod
        Vector position = new Vector(x, y);
        NodeType type = NodeType.wall;
        if(input[y][x] == '#')
        {
            type = NodeType.wall;
        }else if(input[y][x] == ' ')
        {
            type = NodeType.wall;
        }else if(input[y][x] == '.')
        {
            type = NodeType.hallway;
        }
        else
        {
            type = NodeType.room;
        }
    }
}

public enum NodeType{
    wall,
    hallway,
    room
}