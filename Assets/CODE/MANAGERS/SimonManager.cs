using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    PayersTurn,
    SimonsTurn
}

public class SimonManager : MonoBehaviour {
    public delegate void MyDelegate();
    public MyDelegate nextRoundCallback;                        // This is our callback if a round has ended

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
    
    GameState gameState = GameState.SimonsTurn;                 // Here we keep track who's turn it is.
    Queue<KeyCode> simonHasSaid = new Queue<KeyCode>();         // This Queue is for the player, there is a backup in the SimonManager



    void Start() {
        NextRound();
    }

    public void Guess(KeyCode key) {
        if (gameState == GameState.PayersTurn && simonHasSaid.Count > 0) {
            if (simonHasSaid.Peek() == key)
                simonHasSaid.Dequeue();
            else
                Debug.LogError("You lose");

            Highlight(key);
            if (simonHasSaid.Count == 0)
                NextRound();
        }
    }

    public void Highlight(KeyCode key) {
        for (int i = 0; i < commandos.Length; i++)
            if (commandos[i].key == key)
                commandos[i].button.Highlight();
    }

    void NextRound() { StartCoroutine(IENextRound()); }
    IEnumerator IENextRound() {
        gameState = GameState.SimonsTurn;
        yield return new WaitForSeconds(timeBetweenRounds);
        if (nextRoundCallback != null)
            nextRoundCallback();
        gameState = GameState.PayersTurn;
        yield return null;
    }


    public void SetSimonsCommands(Queue<KeyCode> commands) {
        simonHasSaid = new Queue<KeyCode>(commands);
    }
}


[System.Serializable]
public class Commando {
    public KeyCode key;
    public ButtonController button;
}