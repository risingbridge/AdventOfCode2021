using Day15;

string[] input = File.ReadAllLines("./input.txt");
int[,] map = new int[input.Length, input[0].Length];
for (int y = 0; y < input.Length; y++)
{
    for (int x = 0; x < input[0].Length; x++)
    {
        map[y, x] = int.Parse(input[y][x].ToString());
    }
}

Console.WriteLine($"Todays map: ");
Dictionary<string, Node> nodeMap = new Dictionary<string, Node>();
Dictionary<string, Node> openSet = new Dictionary<string, Node>();
Dictionary<string, Node> closedSet = new Dictionary<string, Node>();

for (int y = 0; y < map.GetLength(0); y++)
{
    for (int x = 0; x < map.GetLength(1); x++)
    {
        Console.Write(map[y, x]);
        Vector nodePos = new Vector(x, y);
        List<Vector> neighbours = GetNeigbours(nodePos, map);
        Dictionary<string, int> neighbourDict = new Dictionary<string, int>();
        foreach (Vector v in neighbours)
        {
            int cost = map[v.y, v.x];
            neighbourDict.Add(v.ToString(), cost);
        }
        Node newNode = new Node { Neighbours = neighbourDict, Position = nodePos, Cost = int.MaxValue};
        nodeMap.Add(nodePos.ToString(), newNode);

    }
    Console.WriteLine();
}

Vector startPos = new Vector(0, 0);
Vector goal = new Vector(map.GetLength(1) - 1, map.GetLength(0) - 1);
//Add all nodes to openset
foreach (KeyValuePair<string, Node> node in nodeMap)
{
    openSet.Add(node.Key, node.Value);
}
//Remote start from openset and add to closed
closedSet.Add(startPos.ToString(), openSet[startPos.ToString()]);
openSet.Remove(startPos.ToString());

Vector currentPos = startPos;
Node current = closedSet[currentPos.ToString()];
while (openSet.Count > 0)
{
    
}

List<Vector> GetNeigbours(Vector pos, int[,] map)
{
    List<Vector> neighbours = new List<Vector>();
    Vector up = new Vector(pos.x, pos.y - 1); //Up
    Vector down = new Vector(pos.x, pos.y + 1); //Down
    Vector left = new Vector(pos.x - 1, pos.y); //Left
    Vector right = new Vector(pos.x + 1, pos.y); //Right

    if(up.y >= 0)
    {
        neighbours.Add(up);
    }
    if(down.y < map.GetLength(0))
    {
        neighbours.Add(down);
    }
    if(left.x >= 0)
    {
        neighbours.Add(left);
    }
    if (right.x < map.GetLength(1))
    {
        neighbours.Add(right);
    }

    return neighbours;
}