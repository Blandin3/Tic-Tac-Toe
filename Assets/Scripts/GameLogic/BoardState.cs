/// <summary>
/// Pure game logic class — no Unity dependencies.
/// Tracks board state, turns, win and draw detection.
/// </summary>
public class BoardState
{
    public string[] Cells { get; private set; } = new string[9];
    public string CurrentPlayer { get; private set; } = "X";
    public bool IsGameOver { get; private set; } = false;
    public int MoveCount { get; private set; } = 0;

    static readonly int[][] WinLines = new int[][]
    {
        new int[]{0,1,2}, new int[]{3,4,5}, new int[]{6,7,8},
        new int[]{0,3,6}, new int[]{1,4,7}, new int[]{2,5,8},
        new int[]{0,4,8}, new int[]{2,4,6}
    };

    public BoardState() { Reset(); }

    public void Reset()
    {
        Cells = new string[9] { "","","","","","","","","" };
        CurrentPlayer = "X";
        IsGameOver = false;
        MoveCount = 0;
    }

    /// <summary>Places current player's mark. Returns true if move was valid.</summary>
    public bool MakeMove(int index)
    {
        if (IsGameOver || Cells[index] != "") return false;
        Cells[index] = CurrentPlayer;
        MoveCount++;
        return true;
    }

    /// <summary>Returns winning line index (0-7) or -1 if no winner yet.</summary>
    public int GetWinningLine()
    {
        for (int i = 0; i < WinLines.Length; i++)
        {
            int[] line = WinLines[i];
            if (Cells[line[0]] != "" &&
                Cells[line[0]] == Cells[line[1]] &&
                Cells[line[1]] == Cells[line[2]])
                return i;
        }
        return -1;
    }

    public bool IsDraw() => MoveCount >= 9 && GetWinningLine() == -1;

    public void SwitchPlayer() => CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";

    public void SetGameOver() => IsGameOver = true;
}
