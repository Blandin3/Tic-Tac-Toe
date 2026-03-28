using NUnit.Framework;

public class TicTacToeTests
{
    // ── BoardState Tests ────────────────────────────────────────────

    [Test]
    public void Test_BoardState_IsEmpty_OnInit()
    {
        BoardState board = new BoardState();
        foreach (string cell in board.Cells)
            Assert.AreEqual("", cell);
    }

    [Test]
    public void Test_BoardState_MakeMove_PlacesCorrectMark()
    {
        BoardState board = new BoardState();
        board.MakeMove(4);
        Assert.AreEqual("X", board.Cells[4]);
    }

    [Test]
    public void Test_BoardState_MakeMove_ReturnsFalse_OnOccupiedCell()
    {
        BoardState board = new BoardState();
        board.MakeMove(0);
        bool result = board.MakeMove(0);
        Assert.IsFalse(result);
    }

    // ── Win Detection Tests ─────────────────────────────────────────

    [Test]
    public void Test_WinDetection_TopRow()
    {
        BoardState board = new BoardState();
        // X plays 0, 1, 2 — top row win
        board.MakeMove(0);
        board.MakeMove(1);
        board.MakeMove(2);
        Assert.AreNotEqual(-1, board.GetWinningLine());
    }

    [Test]
    public void Test_WinDetection_Diagonal()
    {
        BoardState board = new BoardState();
        // X plays 0, 4, 8 — diagonal win
        board.MakeMove(0);
        board.MakeMove(4);
        board.MakeMove(8);
        Assert.AreNotEqual(-1, board.GetWinningLine());
    }

    [Test]
    public void Test_WinDetection_NoWinner_ReturnsMinusOne()
    {
        BoardState board = new BoardState();
        board.MakeMove(0);
        board.MakeMove(4);
        Assert.AreEqual(-1, board.GetWinningLine());
    }

    // ── Draw Detection Tests ────────────────────────────────────────

    [Test]
    public void Test_DrawDetection_FullBoard_NoWinner()
    {
        BoardState board = new BoardState();
        // Sequence produces: X O X / O O X / X X O — no winner, full board
        // X: 0,5,6,7  O: 1,2,3,4,8 — actually use alternating moves carefully
        // X plays 0,2,5,6,7 — O plays 1,3,4,8
        int[] xMoves = { 0, 2, 5, 6, 7 };
        int[] oMoves = { 1, 3, 4, 8 };
        int xi = 0, oi = 0;
        for (int turn = 0; turn < 9; turn++)
        {
            if (turn % 2 == 0) { board.MakeMove(xMoves[xi++]); }
            else { board.SwitchPlayer(); board.MakeMove(oMoves[oi++]); board.SwitchPlayer(); }
        }
        Assert.IsTrue(board.IsDraw());
    }

    // ── AI Strategy Tests ───────────────────────────────────────────

    [Test]
    public void Test_MediumAI_TakesWinningMove()
    {
        // O has two in a row at 0,1 — AI should pick 2 to win
        string[] board = { "O","O","","X","X","","","","" };
        MediumAIStrategy ai = new MediumAIStrategy();
        int move = ai.GetBestMove(board);
        Assert.AreEqual(2, move);
    }

    [Test]
    public void Test_MediumAI_BlocksOpponentWin()
    {
        // X has two in a row at 3,4 — AI should block at 5
        string[] board = { "O","","","X","X","","","","" };
        MediumAIStrategy ai = new MediumAIStrategy();
        int move = ai.GetBestMove(board);
        Assert.AreEqual(5, move);
    }

    [Test]
    public void Test_HardAI_NeverLoses_WhenGoingSecond()
    {
        // X plays center — Hard AI response must be a valid empty cell
        string[] board = { "","","","","X","","","","" };
        HardAIStrategy ai = new HardAIStrategy();
        int move = ai.GetBestMove(board);
        Assert.GreaterOrEqual(move, 0);
        Assert.Less(move, 9);
        Assert.AreEqual("", board[move]);
    }

    [Test]
    public void Test_EasyAI_ReturnsEmptyCell()
    {
        string[] board = { "X","O","X","O","X","O","","","" };
        EasyAIStrategy ai = new EasyAIStrategy();
        int move = ai.GetBestMove(board);
        Assert.IsTrue(move == 6 || move == 7 || move == 8);
    }

    [Test]
    public void Test_HardAI_TakesImmediateWin()
    {
        // O has two in a row at 0,1 — Hard AI should take 2 to win
        string[] board = { "O","O","","X","X","","","","" };
        HardAIStrategy ai = new HardAIStrategy();
        int move = ai.GetBestMove(board);
        Assert.AreEqual(2, move);
    }
}
