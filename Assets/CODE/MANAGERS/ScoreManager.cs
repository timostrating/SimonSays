using UnityEngine.UI;
using UnityEngine;


/// <summary>
/// This class is responsible for keeping track of the scores of the game
/// </summary>
public class ScoreManager : MonoBehaviour {

    SimonManager simon;
    public int highScore { get; private set; }
    public int oldScore { get; private set; }
    int score;
    bool firstRound = true;

    [SerializeField] string textString = "Score: ";
    [SerializeField] Text textField;


    void Start() {
        simon = RootManager.instance.simonManager;
        simon.nextRoundCallback += NextRound;
        simon.newGameCallback += NewGame;
    }

    void NextRound() {
        if (firstRound) { firstRound = false; return; }  // skip first round
        score++;
        if (score > highScore)
            highScore = score;
        textField.text = textString + score;
    }

    void NewGame() {
        textField.text = textString + 0;
        oldScore = score;
        score = 0;
        firstRound = true;
    }
}
