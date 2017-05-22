using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Paused,
    PayersTurn,
    SimonsTurn
}

/// <summary>
/// This class is responsible for all the states the simon game can have.
/// </summary>
public class SimonManager : MonoBehaviour {
    public delegate void MyDelegate();
    public MyDelegate nextRoundCallback;                        // This is our callback if a round has ended
    public MyDelegate newGameCallback;                          // This is our callback if we want to start a new game
    public GameState gameState { get; private set; }            // Here we keep track who's turn it is

    [SerializeField] Commando[] commandos;
    public KeyCode[] keycodes {                                 // This is our public getter to sync all keycodes across all scripts
        get {
            KeyCode[] keys = new KeyCode[commandos.Length + 1];
            for (int i = 0; i < commandos.Length; i++)
                keys[i] = commandos[i].key;
            return keys;
        }
    }

    [SerializeField] float timeBetweenRounds = 1.5f;

    Queue<KeyCode> simonHasSaid = new Queue<KeyCode>();         // This Queue is for the player, there is a backup in the SimonManager



    void Awake() { gameState = GameState.Paused; }
    public void StartGame() {
        NextRound();
    }

    public void Guess(KeyCode key) {
        if (gameState == GameState.PayersTurn && simonHasSaid.Count > 0) {
            if (simonHasSaid.Peek() == key)
                simonHasSaid.Dequeue();
            else
                NewGame();

            HighlightButton(key);
            if (gameState == GameState.PayersTurn && simonHasSaid.Count == 0)
                NextRound();
        }
    }

    public void HighlightButton(KeyCode key) {
        for (int i = 0; i < commandos.Length; i++)
            if (commandos[i].key == key)
                commandos[i].button.Highlight();
    }

    public void SetSimonsCommands(Queue<KeyCode> commands) {
        simonHasSaid = new Queue<KeyCode>(commands);
    }

    void NewGame() {
        // Debug.Log("__You_Lose__");
        gameState = GameState.Paused;
        simonHasSaid = new Queue<KeyCode>();
        newGameCallback();
    }

    void NextRound() { StartCoroutine(IENextRound()); }
    IEnumerator IENextRound() {
        gameState = GameState.SimonsTurn;
        if (nextRoundCallback != null)
            nextRoundCallback();
        yield return new WaitForSeconds(timeBetweenRounds);
        gameState = GameState.PayersTurn;
        yield return null;
    }
}


[System.Serializable]
public class Commando {
    public KeyCode key;
    public ButtonController button;
}