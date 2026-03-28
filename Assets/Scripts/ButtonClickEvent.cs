using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonClickEvent : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI buttonText;
    private GameController gameController;
    private GameController1 computerController;
    private int cellIndex;

    public void SetCellIndex(int index)
    {
        cellIndex = index;
    }

    public void SetSpace()
    {
        if (gameController != null)
        {
            gameController.RegisterMove(cellIndex);
            buttonText.text = gameController.GetPlayerSide();
            button.interactable = false;
            gameController.PlayMoveSound();
            gameController.EndTurn();
        }
        else if (computerController != null)
        {
            computerController.PlayerMove(cellIndex);
        }
    }

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public void SetComputerControllerReference(GameController1 controller)
    {
        computerController = controller;
    }

    public void SetComputerSpace()
    {
        // AI move is handled entirely inside GameController1.ComputerMoveWithDelay
        // This method kept for compatibility but logic moved to controller
    }
}
