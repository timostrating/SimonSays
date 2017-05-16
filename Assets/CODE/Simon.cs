using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState {
    PayersTurn,
    SimonsTurn,
    SimonsTalks
}

public class Simon : MonoBehaviour {
    [SerializeField]
    float timeBetweenCommands = 1f;

    Queue<KeyCode> simonHasSaid = new Queue<KeyCode>();
    Queue<KeyCode> playerHasToSay = new Queue<KeyCode>();
    GameState gameState = GameState.SimonsTalks;

    KeyCode[] keycodes = { KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow };



    void Update() {
        if (gameState == GameState.PayersTurn) {
            for (int i = 0; i < keycodes.Length; i++) {
                if (Input.GetKeyDown(keycodes[i])) {
                    if (playerHasToSay.Peek() == keycodes[i])
                        playerHasToSay.Dequeue();
                    else
                        Debug.LogError("You lose");
                }
            }

            if (playerHasToSay.Count == 0)
                gameState = GameState.SimonsTalks;
        }
        else if(gameState == GameState.SimonsTalks){
            StartCoroutine(NextRound());
        }
    }


    IEnumerator NextRound() {
        gameState = GameState.SimonsTurn;
        KeyCode[] keys = simonHasSaid.ToArray();
        for (int i=0; i<keys.Length; i++) {
            Debug.Log(keys[i]);
            yield return new WaitForSeconds(timeBetweenCommands);
        }

        KeyCode nextCommand = keycodes[Random.Range(0, keycodes.Length)];
        Debug.Log(nextCommand);
        simonHasSaid.Enqueue(nextCommand);
        yield return new WaitForSeconds(timeBetweenCommands);

        for (int i = 0; i < 10; i++)
            Debug.Log("-----");

        playerHasToSay = new Queue<KeyCode>(simonHasSaid);
        gameState = GameState.PayersTurn;
        yield return null;
    }
}
