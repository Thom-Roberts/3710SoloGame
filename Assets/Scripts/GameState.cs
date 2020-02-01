using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public int maxGameBoardSize = 7;
    private string[,] gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = new string[maxGameBoardSize, maxGameBoardSize];
        for(var i = 0; i < 7; ++i) {
            for(var j = 0; j < 7; ++j) {
                gameState[i, j] = "none";
            }
        }
    }

    // Update is called once per frame
    void Update() {
        CheckForWin();
    }

    void CheckForWin() {
        int playerCount = 0;
        int opponentCount = 0;

        for(var i = 0; i < maxGameBoardSize; ++i) {
            for(var j = 0; j < maxGameBoardSize; ++j) {
                if(gameState[i, j] == "player") {
                    playerCount++;
                }
                else if(gameState[i, j] == "enemy") {
                    opponentCount++;
                }
            }
        }

        // Must be more than half of the squares. Doing this casts to make sure I round up
        // See here: https://code-examples.net/en/q/e0e5c
        int winSqaureNumber = (int)Math.Ceiling((double)maxGameBoardSize * maxGameBoardSize / 2);
        if(playerCount > winSqaureNumber) {
            // Player wins!
        }
        else if(opponentCount > winSqaureNumber) {
            // Enemy wins!
        }
    }

    // Comes in with [x, y, player/opponent]
    // Reason it's an array is in the case that someone doesn't move while the other person does
    public void UpdateState(Tuple<int, int, string> change) {
        // For now these divisions are hard set because the elements are scaled by 2 in the x & z directions
        gameState[change.Item1 / 2, change.Item2 / 2] = change.Item3;
    }
}
