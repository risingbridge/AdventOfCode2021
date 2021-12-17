//using Day17;

//string input = File.ReadAllLines("./input.txt")[0].Replace("target area: ", "");
//string targetX = input.Split(",")[0];
//string targetY = input.Split(",")[1];

//int startTargetX = int.Parse(targetX.Replace("x=", "").Split("..")[0]);
//int endTargetX = int.Parse(targetX.Replace("x=", "").Split("..")[1]);

//int startTargetY = int.Parse(targetY.Replace("y=", "").Split("..")[0]);
//int endTargetY = int.Parse(targetY.Replace("y=", "").Split("..")[1]);

//List<Vector> targets = new List<Vector>();

//for (int x = startTargetX; x <= endTargetX; x++)
//{
//    for (int y = startTargetY; y <= endTargetY; y++)
//    {
//        Vector targetPos = new Vector(x, y);
//        targets.Add(targetPos);
//    }
//}

//int stepCounter = 0;
//Vector probePos = new Vector(0, 0);
//Vector startVelocity = new Vector(18, 99);
//List<Vector> probeTrajectory = new List<Vector>();
//probeTrajectory.Add(probePos);
//int maxY = 0;
//int bruteForceIterations = 1;
//for (int i = 0; i < bruteForceIterations; i++)
//{
//    probePos = new Vector(0, 0);
//    Vector probeVelocity = new Vector(startVelocity.x, startVelocity.y + i);
//    long heightCounter = long.MinValue;
//    while (!IsProbeAtTarget(probePos))
//    {
//        probePos = new Vector(probePos.x + probeVelocity.x, probePos.y + probeVelocity.y);
//        if (probePos.y > maxY)
//        {
//            maxY = probePos.y;
//        }
//        probeTrajectory.Add(probePos);
//        //Console.WriteLine($"After step {stepCounter}, probe position is X: {probePos.x} Y: {probePos.y}");

//        if (probeVelocity.x > 0)
//        {
//            probeVelocity.x--;
//        }
//        if (probeVelocity.x < 0)
//        {
//            probeVelocity.x++;
//        }
//        probeVelocity.y--;

//        if (probePos.y > heightCounter)
//        {
//            heightCounter = probePos.y;
//        }

//        stepCounter++;
//        if (probePos.y < startTargetY)
//        {
//            //Console.WriteLine("\t-\tMissed the target!");
//            break;
//        }
//    }
//    if (IsProbeAtTarget(probePos))
//    {
//        Console.Write($"Testing Velocity X:{startVelocity.x} Y:{startVelocity.y + i}");
//        Console.Write($"\t-\tHit target position at X: {probePos.x} Y: {probePos.y}");
//        Console.WriteLine($"\t-\tMax height is: {heightCounter}");
//    }
//}


////if (IsProbeAtTarget(probePos))
////{
////    PrintMap();
////}

//bool IsProbeAtTarget(Vector pos)
//{
//    if (pos.x >= startTargetX && pos.x <= endTargetX && pos.y >= startTargetY && pos.y <= endTargetY)
//    {
//        return true;
//    }

//    return false;
//}

//void PrintMap()
//{
//    int maxDepth = startTargetY;
//    int maxWidth = endTargetX;
//    if (probePos.y < maxDepth)
//    {
//        maxDepth = probePos.y;
//    }
//    if (probePos.x > maxWidth)
//    {
//        maxWidth = probePos.x;
//    }

//    for (int y = maxY; y >= maxDepth; y--)
//    {
//        Console.Write($"{y}:\t\t");
//        for (int x = 0; x <= maxWidth; x++)
//        {
//            Vector current = new Vector(x, y);
//            if (x == 0 && y == 0)
//            {
//                Console.ForegroundColor = ConsoleColor.Blue;
//                Console.Write("S");
//                Console.ResetColor();
//            }
//            else if (IsVectorInTrajectory(current, probeTrajectory))
//            {
//                if (IsProbeAtTarget(current))
//                {
//                    Console.ForegroundColor = ConsoleColor.Green;
//                    Console.Write("#");
//                    Console.ResetColor();
//                }
//                else
//                {
//                    Console.Write("#");
//                }
//            }
//            else if (x >= startTargetX && x <= endTargetX && y <= endTargetY && y >= startTargetY)
//            {
//                Console.Write("T");
//            }
//            else
//            {
//                Console.Write(".");
//            }
//        }
//        Console.WriteLine();
//    }
//}

//bool IsVectorInTrajectory(Vector pos, List<Vector> list)
//{
//    foreach (Vector item in list)
//    {
//        if (item.x == pos.x && item.y == pos.y)
//        {
//            return true;
//        }
//    }

//    return false;
//}