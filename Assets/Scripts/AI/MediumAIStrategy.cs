using UnityEngine;

/// <summary>
/// Medium difficulty: win if possible, block opponent win, otherwise random.
/// </summary>
public class MediumAIStrategy : IAIStrategy
{
    public int GetBestMove(string[] board)
    {
        // Try to win
        int win = FindWinningMove(board, "O");
        if (win != -1) return win;

        // Try to block
        int block = FindWinningMove(board, "X");
        if (block != -1) return block;

        // Fall back to random
        return new EasyAIStrategy().GetBestMove(board);
    }

    /// <summary>Finds a move that completes three in a row for the given player.</summary>
    int FindWinningMove(string[] board, string player)
    {
        int[][] lines = new int[][]
        {
            new int[]{0,1,2}, new int[]{3,4,5}, new int[]{6,7,8},
            new int[]{0,3,6}, new int[]{1,4,7}, new int[]{2,5,8},
            new int[]{0,4,8}, new int[]{2,4,6}
        };

        foreach (int[] line in lines)
        {
            int playerCount = 0, emptyIndex = -1;
            foreach (int i in line)
            {
                if (board[i] == player) playerCount++;
                else if (board[i] == "") emptyIndex = i;
            }
            if (playerCount == 2 && emptyIndex != -1)
                return emptyIndex;
        }
        return -1;
    }
}
