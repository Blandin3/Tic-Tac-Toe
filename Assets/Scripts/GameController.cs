using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI[] buttonList;
    public TextMeshProUGUI title;
    public GameObject restartButton;
    public GameObject[] strikeThroughs;
    public AudioClip moveSound;
    public AudioClip winSound;

    private BoardState board;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        board = new BoardState();
        SetGameControllerReferenceOnButtons();
        title.text = "Player " + board.CurrentPlayer + " Turn";
        restartButton.SetActive(false);
        DisableAllStrikeThroughs();
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            ButtonClickEvent btn = buttonList[i].GetComponentInParent<ButtonClickEvent>();
            btn.SetGameControllerReference(this);
            btn.SetCellIndex(i);
        }
    }

    public string GetPlayerSide() => board.CurrentPlayer;

    public void RegisterMove(int index) => board.MakeMove(index);

    public void EndTurn()
    {
        int winLine = board.GetWinningLine();
        if (winLine != -1) { GameOver(winLine); return; }
        if (board.IsDraw()) { ShowDraw(); return; }

        board.SwitchPlayer();
        title.text = "Player " + board.CurrentPlayer + " Turn";
    }

    void GameOver(int winLine)
    {
        board.SetGameOver();
        for (int i = 0; i < buttonList.Length; i++)
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        title.text = "Player " + board.CurrentPlayer + " Wins";
        restartButton.SetActive(true);
        PlaySound(winSound);
        ShowStrikeThrough(winLine);
    }

    void ShowDraw()
    {
        board.SetGameOver();
        title.text = "It is a Draw";
        restartButton.SetActive(true);
    }

    public void RestartGame()
    {
        board.Reset();
        restartButton.SetActive(false);
        title.text = "Player " + board.CurrentPlayer + " Turn";
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = true;
            buttonList[i].text = "";
        }
        DisableAllStrikeThroughs();
    }

    public void PlayMoveSound() => PlaySound(moveSound);

    void PlaySound(AudioClip clip)
    {
        if (GameSettings.SoundEnabled && clip != null)
            audioSource.PlayOneShot(clip);
    }

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
