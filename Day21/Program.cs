string[] input = File.ReadAllLines("./test.txt");

int playerOneStartPos = int.Parse(input[0].Split(": ")[1]);
int playerTwoStartPos = int.Parse(input[1].Split(": ")[1]);

Console.WriteLine($"Player One Starts at {playerOneStartPos}\n" +
    $"Player Two starts at {playerTwoStartPos}");

int playerOneScore = 0;
int playerTwoScore = 0;
int playerOnePosition = playerOneStartPos;
int playerTwoPosition = playerTwoStartPos;
int previousDice = 0;
int dieCounter = 0;
int endScore = 1000;
int sleepTime = 0;
bool sleepInsteadOfEnter = true;
bool printEveryTurn = false;


int highestScore = 0;


while(highestScore < endScore)
{
    //Player One Plays
    int totalDie = 0;
    if (printEveryTurn)
    {
        Console.Write($"Player 1 rolls ");
    }
    for (int i = 0; i < 3; i++)
    {
        previousDice = RollDeterministicDie(previousDice);
        if (printEveryTurn)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{previousDice}");
            Console.ResetColor();
            if (i < 2)
            {
                Console.Write("+");
            }
        }
        totalDie += previousDice;
    }
    playerOnePosition = CalculateMovePos(playerOnePosition, totalDie);
    playerOneScore += playerOnePosition;
    if (printEveryTurn)
    {
        Console.Write($" and moves to space ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(playerOnePosition);
        Console.ResetColor();
        Console.Write(" for a total score of ");
        Console.ForegroundColor= ConsoleColor.Green;
        Console.Write(playerOneScore);
        Console.ResetColor();
    }
    if (playerOneScore > highestScore)
    {
        highestScore = playerOneScore;
    }
    if(playerOneScore >= endScore)
    {
        break;
    }
    if (sleepInsteadOfEnter)
    {
        Thread.Sleep(sleepTime);
    }
    else
    {
        Console.WriteLine("Enter to continue");
        Console.ReadLine();
    }
    if (printEveryTurn)
    {
        Console.WriteLine(".");
    }
    //Player Two Plays
    totalDie = 0;
    if (printEveryTurn)
    {
        Console.Write($"Player 2 rolls ");
    }
    for (int i = 0; i < 3; i++)
    {
        previousDice = RollDeterministicDie(previousDice);
        if (printEveryTurn)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{previousDice}");
            Console.ResetColor();
            if (i < 2)
            {
                Console.Write("+");
            }
        }
        totalDie += previousDice;
    }
    playerTwoPosition = CalculateMovePos(playerTwoPosition, totalDie);
    playerTwoScore += playerTwoPosition;
    if (printEveryTurn)
    {
        Console.Write($" and moves to space ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(playerTwoPosition);
        Console.ResetColor();
        Console.Write(" for a total score of ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(playerTwoScore);
        Console.ResetColor();
    }
    if (playerTwoScore > highestScore)
    {
        highestScore = playerTwoScore;
    }
    if (playerTwoScore >= endScore)
    {
        break;
    }
    if (sleepInsteadOfEnter)
    {
        Thread.Sleep(sleepTime);
    }
    else
    {
        Console.WriteLine("Enter to continue");
        Console.ReadLine();
    }
    if (printEveryTurn)
    {
        Console.WriteLine(".");
    }
}

Console.WriteLine("\n");
int losingScore = 0;
Console.ForegroundColor = ConsoleColor.Red;
if(playerOneScore > playerTwoScore)
{
    Console.WriteLine($"Player one won!");
    losingScore = playerTwoScore;
}
else
{
    Console.WriteLine($"Player two won!");
    losingScore = playerOneScore;
}
Console.WriteLine();
Console.ResetColor();
Console.WriteLine($"Player One Score: {playerOneScore}\n" +
    $"Player Two Score: {playerTwoScore}\n" +
    $"Total number of time the die has been rolled: {dieCounter}\n" +
    $"Losing player times dieroll: {(long)((long)losingScore * (long)dieCounter)}");

int CalculateMovePos(int current, int die)
{
    int moveToPos = 0;
    if(current + die > 10)
    {
        moveToPos = (current + die) % 10;
        if(moveToPos == 0)
        {
            moveToPos = 10;
        }
    }
    else
    {
        moveToPos = current + die;
    }
    if(moveToPos == 0)
    {
        moveToPos = 1;
    }
    return moveToPos;
}

int RollDeterministicDie(int prev)
{
    dieCounter++;
    int dieToss = 0;
    if(prev < 100)
    {
        dieToss = prev + 1;
    }
    else
    {
        dieToss = 1;
    }
    return dieToss;
}