/// <summary>
/// Strategy interface for AI move selection.
/// Swap implementations to change AI difficulty.
/// </summary>
public interface IAIStrategy
{
    int GetBestMove(string[] board);
}
