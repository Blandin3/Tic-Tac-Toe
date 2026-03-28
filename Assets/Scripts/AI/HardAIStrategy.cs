using UnityEngine;

/// <summary>
/// Hard difficulty: unbeatable Minimax algorithm.
/// </summary>
public class HardAIStrategy : IAIStrategy
{
    public int GetBestMove(string[] board)
    {
        int bestVal = -1000;
        int bestMove = -1;
        string[] b = (string[])board.Clone();

        for (int i = 0; i < b.Length; i++)
        {
            if (b[i] == "")
            {
                b[i] = "O";
                int moveVal = Minimax(b, 0, false);
                b[i] = "";
                if (moveVal > bestVal) { bestVal = moveVal; bestMove = i; }
            }
        }
        return bestMove;
    }

    int Minimax(string[] board, int depth, bool isMaximizing)
    {
        int score = Evaluate(board);
        if (score == 10) return score - depth;
        if (score == -10) return score + depth;
        if (!HasMovesLeft(board)) return 0;

        if (isMaximizing)
        {
            int best = -1000;
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == "")
                {
                    board[i] = "O";
                    best = Mathf.Max(best, Minimax(board, depth + 1, false));
                    board[i] = "";
                }
            }
            return best;
        }
        else
        {
            int best = 1000;
            for (int i = 0; i < board.Length; i++)
            {
                if (board[i] == "")
                {
                    board[i] = "X";
                    best = Mathf.Min(best, Minimax(board, depth + 1, true));
                    board[i] = "";
                }
            }
            return best;
        }
    }

    int Evaluate(string[] b)
    {
        int[][] lines = new int[][]
        {
            new int[]{0,1,2}, new int[]{3,4,5}, new int[]{6,7,8},
            new int[]{0,3,6}, new int[]{1,4,7}, new int[]{2,5,8},
            new int[]{0,4,8}, new int[]{2,4,6}
        };
        foreach (int[] line in lines)
        {
            if (b[line[0]] == b[line[1]] && b[line[1]] == b[line[2]] && b[line[0]] != "")
                return b[line[0]] == "O" ? 10 : -10;
        }
        return 0;
    }

    bool HasMovesLeft(string[] board)
    {
        foreach (string cell in board) if (cell == "") return true;
        return false;
    }
}
