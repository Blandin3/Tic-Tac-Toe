# Tic-Tac-Toe

A Unity Tic-Tac-Toe game supporting Player vs Player and Player vs AI modes.

## Project Setup

1. Open Unity Hub and click **Open Project**
2. Navigate to this folder and open it
3. Unity version: 6000.x
4. Open `Assets/Scenes/HomeScreen.unity` as the starting scene
5. Press Play to run in the Editor

## Scene Flow

```
HomeScreen → MainMenu → TicTacToe (PvP)
                      → Human vs Computer (PvAI)
HomeScreen → SettingScene
```

## Class Responsibilities

| Class | Responsibility |
|---|---|
| `HomeScreen` | First scene navigation — Start, Settings, Quit |
| `MainMenu` | Game mode selection — PvP or PvAI |
| `Settings` | Saves AI difficulty, brightness and sound to GameSettings |
| `GameSettings` | Static data holder — persists settings across scenes |
| `BoardState` | Pure game logic — board cells, move validation, win/draw detection |
| `GameController` | Unity UI controller for PvP scene |
| `GameController1` | Unity UI controller for PvAI scene — delegates AI moves to IAIStrategy |
| `ButtonClickEvent` | Handles individual cell button clicks |
| `IAIStrategy` | Interface for AI move strategies (Strategy Pattern) |
| `EasyAIStrategy` | Random move selection |
| `MediumAIStrategy` | Rule-based: win → block → random |
| `HardAIStrategy` | Unbeatable Minimax algorithm |

## AI Algorithm

The AI uses the **Strategy Pattern** via `IAIStrategy`. Difficulty is selected in Settings (1–3):

- **Easy** — picks a random empty cell
- **Medium** — rule-based logic:
  1. If AI can win this turn, take it
  2. If opponent can win next turn, block it
  3. Otherwise pick randomly
- **Hard** — Minimax algorithm that evaluates all possible future game states and picks the move with the highest score. It is unbeatable.

## Design Patterns Used

- **Strategy Pattern** — `IAIStrategy` with `EasyAIStrategy`, `MediumAIStrategy`, `HardAIStrategy`
- **Separation of Concerns** — `BoardState` contains zero Unity code; controllers only handle UI

## Folder Structure

```
Assets/
  Scripts/
    AI/          — IAIStrategy, EasyAIStrategy, MediumAIStrategy, HardAIStrategy
    GameLogic/   — BoardState
    UI/          — HomeScreen, MainMenu, Settings
  Scenes/        — HomeScreen, MainMenu, SettingScene, TicTacToe, Human vs Computer
Tests/           — TicTacToeTests.cs, TicTacToe.Tests.asmdef
```

## Running Tests

1. In Unity go to **Window → General → Test Runner**
2. Select **Edit Mode**
3. Click **Run All**
