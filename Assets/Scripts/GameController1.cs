using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController1 : MonoBehaviour
{
    public TextMeshProUGUI[] buttonList;
    public TextMeshProUGUI title;
    public GameObject restartButton;
    public GameObject[] strikeThroughs;
    public AudioClip moveSound;
    public AudioClip winSound;

    private string[] cells = new string[9];
    private string currentPlayer = "X";
    private bool gameOver = false;
    private IAIStrategy aiStrategy;
    private AudioSource audioSource;

    static readonly int[][] WinLines = new int[][]
    {
        new int[]{0,1,2}, new int[]{3,4,5}, new int[]{6,7,8},
        new int[]{0,3,6}, new int[]{1,4,7}, new int[]{2,5,8},
        new int[]{0,4,8}, new int[]{2,4,6}
    };

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        aiStrategy = GetStrategy(GameSettings.AIDifficulty);
        ResetCells();
        SetGameControllerReferenceOnButtons();
        title.text = "Player X Turn";
        restartButton.SetActive(false);
        DisableAllStrikeThroughs();
    }

    IAIStrategy GetStrategy(int difficulty)
    {
        switch (difficulty)
        {
            case 1: return new EasyAIStrategy();
            case 2: return new MediumAIStrategy();
            default: return new HardAIStrategy();
        }
    }

    void ResetCells()
    {
        for (int i = 0; i < 9; i++) cells[i] = "";
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            ButtonClickEvent btn = buttonList[i].GetComponentInParent<ButtonClickEvent>();
            btn.SetComputerControllerReference(this);
            btn.SetCellIndex(i);
        }
    }

    public string GetPlayerSide() => currentPlayer;

    /// <summary>Called by ButtonClickEvent when player clicks a cell.</summary>
    public void PlayerMove(int index)
    {
        if (gameOver || cells[index] != "") return;

        // Register player move
        cells[index] = "X";
        buttonList[index].text = "X";
        buttonList[index].GetComponentInParent<Button>().interactable = false;
        PlaySound(moveSound);

        int winLine = GetWinningLine();
        if (winLine != -1) { GameOver(winLine); return; }
        if (IsDraw()) { ShowDraw(); return; }

        currentPlayer = "O";
        title.text = "Player O Turn";
        StartCoroutine(ComputerMoveWithDelay());
    }

    IEnumerator ComputerMoveWithDelay()
    {
        SetAllButtonsInteractable(false);
        yield return new WaitForSeconds(1f);

        string[] snapshot = (string[])cells.Clone();
        int bestMove = aiStrategy.GetBestMove(snapshot);

        if (bestMove < 0 || bestMove > 8 || cells[bestMove] != "")
        {
            for (int i = 0; i < 9; i++)
            {
                if (cells[i] == "") { bestMove = i; break; }
            }
        }

        cells[bestMove] = "O";
        buttonList[bestMove].text = "O";
        buttonList[bestMove].GetComponentInParent<Button>().interactable = false;
        PlaySound(moveSound);

        int winLine = GetWinningLine();
        if (winLine != -1)
        {
            GameOver(winLine);
            yield break;
        }

        if (IsDraw())
        {
            ShowDraw();
            yield break;
        }

        currentPlayer = "X";
        title.text = "Player X Turn";

        for (int i = 0; i < 9; i++)
            if (cells[i] == "") buttonList[i].GetComponentInParent<Button>().interactable = true;
    }

    int GetWinningLine()
    {
        foreach (int[] line in WinLines)
            if (cells[line[0]] != "" && cells[line[0]] == cells[line[1]] && cells[line[1]] == cells[line[2]])
                return System.Array.IndexOf(WinLines, line);
        return -1;
    }

    bool IsDraw()
    {
        foreach (string c in cells) if (c == "") return false;
        return true;
    }

    void GameOver(int winLine)
    {
        gameOver = true;
        SetAllButtonsInteractable(false);
        title.text = "Player " + currentPlayer + " Wins!";
        restartButton.SetActive(true);
        PlaySound(winSound);
        ShowStrikeThrough(winLine);
    }

    void ShowDraw()
    {
        gameOver = true;
        title.text = "It is a Draw";
        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        gameOver = false;
        currentPlayer = "X";
        aiStrategy = GetStrategy(GameSettings.AIDifficulty);
        ResetCells();
        restartButton.SetActive(false);
        title.text = "Player X Turn";
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
            buttonList[i].GetComponentInParent<Button>().interactable = true;
        }
        DisableAllStrikeThroughs();
    }

    void SetAllButtonsInteractable(bool state)
    {
        for (int i = 0; i < buttonList.Length; i++)
            buttonList[i].GetComponentInParent<Button>().interactable = state;
    }

    void PlaySound(AudioClip clip)
    {
        if (GameSettings.SoundEnabled && clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void PlayMoveSound() => PlaySound(moveSound);

    void ShowStrikeThrough(int index)
    {
        if (strikeThroughs.Length > index && strikeThroughs[index] != null)
            strikeThroughs[index].SetActive(true);
    }

    void DisableAllStrikeThroughs()
    {
        foreach (GameObject s in strikeThroughs)
            if (s != null) s.SetActive(false);
    }

    public void GoBack() => SceneManager.LoadScene("MainMenu");
}
