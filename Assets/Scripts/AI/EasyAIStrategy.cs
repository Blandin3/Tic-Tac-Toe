using UnityEngine;

/// <summary>
/// Easy difficulty: picks a random available cell.
/// </summary>
public class EasyAIStrategy : IAIStrategy
{
    public int GetBestMove(string[] board)
    {
        int[] empty = GetEmptyCells(board);
        return empty[Random.Range(0, empty.Length)];
    }

    int[] GetEmptyCells(string[] board)
    {
        int count = 0;
        foreach (string cell in board) if (cell == "") count++;
        int[] result = new int[count];
        int idx = 0;
        for (int i = 0; i < board.Length; i++)
            if (board[i] == "") result[idx++] = i;
        return result;
    }
}
