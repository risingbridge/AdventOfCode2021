string[] input = File.ReadAllLines("./input.txt");
string drawOrder = input[0];
int boardWidth = 5;
int boardHeight = 5;

int lineCounter = 0;
int columnCounter = 0;
//Set to false if you want to use one key to skip through every draw
bool autoRun = true;
//Set to true if you want to display every board for every draw, false if you only want to display winning board
bool showEveryBoard = false;
//Set to true to display each win and wait for keypress
bool displayWin = false;
List<BingoNode> bingoNodes = new List<BingoNode>();
List<List<BingoNode>> bingoBoards = new List<List<BingoNode>>();
List<int> winningOrder = new List<int>();

//Add all boards
for(int i = 1; i < input.Length; i++)
{
    if(input[i] == "")
    {
        lineCounter = 0;
        columnCounter = 0;
        bingoNodes.Clear();
    }
    else
    {
        string inputString = input[i];
        string[] explodedString = inputString.Split(" ");
        foreach(string exploded in explodedString)
        {
            if(exploded != "")
            {
                //Console.Write($"{exploded},");
                bool isUsed = false;
                //Debug color test
                //if(lineCounter == 3 && columnCounter == 3)
                //{
                //    isUsed = true;
                //}
                BingoNode node = new BingoNode()
                {
                    number = int.Parse(exploded),
                    posX = lineCounter,
                    posY = columnCounter,
                    used = isUsed
                };
                bingoNodes.Add(node);
                columnCounter++;
            }
        }
        //Console.WriteLine();
        lineCounter++;
        columnCounter = 0;
    }

    if(lineCounter == 5)
    {
        bingoBoards.Add(new List<BingoNode>(bingoNodes));
        bingoNodes.Clear();
        //Console.WriteLine($"New board!");
    }
}
Console.WriteLine($"{bingoBoards.Count} boards loaded");

//Load the draw-string into a list of ints

string[] drawStrings = drawOrder.Split(",");
List<int> drawList = new List<int>();
foreach (string item in drawStrings)
{
    if(int.TryParse(item, out int i)){
        drawList.Add(i);
    }
}


//Runs through the bingo
foreach (int number in drawList)
{
    Console.WriteLine($"The drawn number is {number}");
    UseNumber(number);
    if (showEveryBoard)
    {
        DisplayAllBoards();
    }
    //Check if there are any winners
    for(int i = 0; i < bingoBoards.Count; i++)
    {
        if (CheckWin(i))
        {
            if (!showEveryBoard)
            {
                DisplayBoard(i);
            }
            Console.WriteLine($"Bingo Board ID {i} won!");
            float sumOfWinner = SumOfWinningBoard(i);
            float finalScore = sumOfWinner * number;
            Console.WriteLine($"Sum of winning board is {sumOfWinner}, and the total score is {finalScore}");
            //Adds winning board to winning-order to solve part two
            if (!winningOrder.Contains(i))
            {
                winningOrder.Add(i);
            }
            if (displayWin)
            {
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }
    }
    if (!autoRun)
    {
        Console.WriteLine("Press any key to draw next");
        Console.ReadKey();
    }
    if(winningOrder.Count >= bingoBoards.Count)
    {
        //Solves part two
        int lastWinningID = winningOrder[winningOrder.Count - 1];
        float sumOfLast = SumOfWinningBoard(lastWinningID);
        float lastScore = sumOfLast * number;
        Console.WriteLine($"The last board to win is {lastWinningID}");
        Console.WriteLine($"It will have a sum of {sumOfLast} and total score of {lastScore}");
        break;
    }
}

 


//Function to calculate part-one sum of winning board
float SumOfWinningBoard(int id)
{
    float sum = 0;
    foreach(BingoNode node in bingoBoards[id])
    {
        if (!node.used)
        {
            sum += node.number;
        }
    }
    return sum;
}

//Function to check if a board has a winning row
bool CheckWin(int id)
{
    bool hasWon = false;
    if (bingoBoards.Count <= id)
    {
        Console.WriteLine($"Board with ID {id} does not exist!");
        return false;
    }
    //Check every vertical first
    for (int y = 0; y < boardWidth; y++)
    {
        int winVert = 0;
        for (int x = 0; x < boardHeight; x++)
        {
            foreach (BingoNode node in bingoBoards[id])
            {
                if(node.posX == x && node.posY == y)
                {
                    if (node.used)
                    {
                        winVert++;
                    }
                }
            }
        }
        if(winVert == 5)
        {
            hasWon = true;
            Console.WriteLine($"Won vertical");
            break;
        }
    }

    //Check horizontal
    for (int x = 0; x < boardHeight; x++)
    {
        int winHor = 0;
        for (int y = 0;y < boardWidth; y++)
        {
            foreach (BingoNode node in bingoBoards[id])
            {
                if (node.posX == x && node.posY == y)
                {
                    if (node.used)
                    {
                        winHor++;
                    }
                }
            }
        }
        if(winHor == 5)
        {
            hasWon = true;
            Console.WriteLine($"Won horizontal");
            break;
        }
    }

    return hasWon;
}

//Function to loop through every board and mark drawn number as used
void UseNumber(int number)
{
    foreach (List<BingoNode> board in bingoBoards)
    {
        for(int i = 0; i < board.Count; i++)
        {
            if(board[i].number == number)
            {
                board[i].used = true;
            }
        }
    }
}

//Function to display all boards
void DisplayAllBoards()
{
    for (int i = 0; i < bingoBoards.Count; i++)
    {
        DisplayBoard(i);
        Console.WriteLine("\n");
    }
}

//Function to display a selected bingo board
void DisplayBoard(int id)
{
    if(bingoBoards.Count <= id)
    {
        Console.WriteLine($"Board with ID {id} does not exist!");
        return;
    }

    int nodePosCounter = 0;
    for (int x = 0; x < boardHeight; x++)
    {
        for (int y = 0; y < boardWidth; y++)
        {
            if (bingoBoards[id][nodePosCounter].used)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ResetColor();
            }
            Console.Write($"{bingoBoards[id][nodePosCounter].number}");
            Console.ResetColor();
            Console.Write(",");
            nodePosCounter++;
        }
        Console.WriteLine("");
    }
}

internal class BingoNode
{
    public int number { get; set; }
    public int posX { get; set; }
    public int posY { get; set; }
    public bool used { get; set; }

}