using Day17;

string input = File.ReadAllLines("./input.txt")[0].Replace("target area: ", "");
string targetX = input.Split(",")[0];
string targetY = input.Split(",")[1];

int startTargetX = int.Parse(targetX.Replace("x=", "").Split("..")[0]);
int endTargetX = int.Parse(targetX.Replace("x=", "").Split("..")[1]);

int startTargetY = int.Parse(targetY.Replace("y=", "").Split("..")[0]);
int endTargetY = int.Parse(targetY.Replace("y=", "").Split("..")[1]);

List<Vector> targets = new List<Vector>();

for (int x = startTargetX; x <= endTargetX; x++)
{
    for (int y = startTargetY; y <= endTargetY; y++)
    {
        Vector targetPos = new Vector(x, y);
        targets.Add(targetPos);
    }
}

int stepCounter = 0;
Vector probePos = new Vector(0, 0);
Vector startVelocity = new Vector(0,0);
List<Vector> probeTrajectory = new List<Vector>();
probeTrajectory.Add(probePos);
int maxY = 0;
int testMax = 200;
List<Vector> validVelocities = new List<Vector>();
long HighestY = long.MinValue;
for (int x = -testMax; x < testMax; x++)
{
    for (int y = -testMax; y < testMax; y++)
    {
        probePos = new Vector(0, 0);
        Vector probeVelocity = new Vector(startVelocity.x + x, startVelocity.y + y);
        long heightCounter = 0;
        probeTrajectory.Clear();
        while (!IsProbeAtTarget(probePos))
        {
            probePos = new Vector(probePos.x + probeVelocity.x, probePos.y + probeVelocity.y);
            if (probePos.y > maxY)
            {
                maxY = probePos.y;
            }
            probeTrajectory.Add(probePos);
            //Console.WriteLine($"After step {stepCounter}, probe position is X: {probePos.x} Y: {probePos.y}");

            if (probeVelocity.x > 0)
            {
                probeVelocity.x--;
            }
            if (probeVelocity.x < 0)
            {
                probeVelocity.x++;
            }
            probeVelocity.y--;

            if (probePos.y > heightCounter)
            {
                heightCounter = probePos.y;
            }

            stepCounter++;
            if (probePos.y < startTargetY)
            {
                //Console.WriteLine("\t-\tMissed the target!");
                break;
            }
        }
        if (IsProbeAtTarget(probePos))
        {
            //Console.Write($"Testing Velocity X:{startVelocity.x + x} Y:{startVelocity.y + y}");
            //Console.Write($"\t-\tHit target position at X: {probePos.x} Y: {probePos.y}");
            //Console.WriteLine($"\t-\tMax height is: {heightCounter}");
            if(heightCounter > HighestY)
            {
                HighestY = heightCounter;
            }
            validVelocities.Add(new Vector(startVelocity.x + x, startVelocity.y + y));
        }
    }
}
Console.WriteLine($"Part one: {HighestY}");
Console.WriteLine($"Part Two: {validVelocities.Count}");

bool IsProbeAtTarget(Vector pos)
{
    if(pos.x >= startTargetX && pos.x <= endTargetX && pos.y >= startTargetY && pos.y <= endTargetY)
    {
        return true;
    }

    return false;
}