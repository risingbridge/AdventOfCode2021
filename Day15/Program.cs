using Day15;

string[] input = File.ReadAllLines("./input.txt");

int xSize = input[0].Length;
int ySize = input.Length;

int[,] map = new int[ySize, xSize];
Vector start = new Vector(0, 0);
Vector end = new Vector(ySize - 1, xSize - 1);
Dictionary<Tuple<int, int>, Node> nodes = new Dictionary<Tuple<int,int>, Node>();
for(int y = 0; y < ySize; y++)
{
    for(int x = 0; x < xSize; x++)
    {
        map[y, x] = int.Parse(input[y][x].ToString());
    }
}
//Generate nodes and print
for (int y = 0; y < ySize; y++)
{
    for (int x = 0; x < xSize; x++)
    {
        Vector pos = new Vector(x, y);
        Tuple<int,int> intPos = new Tuple<int, int>(x, y);
        Node node = new Node()
        {
            X = x,
            Y = y,
            Neighbours = FindNeighbours(pos)
        };
        nodes.Add(intPos, node);
        if(x == start.x && y == start.y)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }else if(x == end.x && y == end.y)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        Console.Write(map[y, x]);
        Console.ResetColor();
    }
    Console.WriteLine();
}

//Pathfind
Vector current = start;
List<Vector> path = new List<Vector>();
while(current != end)
{
    path.Add(current);
    Vector best = current;
    int bestCost = int.MaxValue;
    Node thisNode = nodes[new Tuple<int, int>(current.x, current.y)];
    foreach (KeyValuePair<Vector, int> n in thisNode.Neighbours)
    {
        if(n.Value < bestCost)
        {
            bestCost = n.Value;
            best = n.Key;
        }
    }
    current = best;
}

//Functions

Dictionary<Vector, int> FindNeighbours(Vector pos)
{
    Dictionary<Vector, int> neighbours = new Dictionary<Vector, int>();
    //up y-1, x
    //down y+1, x
    //left y, x-1
    //right y, x+1
    Vector up = new Vector(pos.x, pos.y - 1);
    Vector down = new Vector(pos.x, pos.y + 1);
    Vector left = new Vector(pos.x - 1, pos.y);
    Vector right = new Vector(pos.x + 1, pos.y);
    if(up.y >= 0)
    {
        neighbours.Add(up, map[up.y, up.x]);
    }
    if(down.y < ySize)
    {
        neighbours.Add(down, map[down.y, down.x]);
    }
    if(left.x >= 0)
    {
        neighbours.Add(left, map[left.y, left.x]);
    }
    if(right.x < xSize)
    {
        neighbours.Add(right, map[right.y, right.x]);
    }

    return neighbours;
}
