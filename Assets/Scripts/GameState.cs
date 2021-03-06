﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

using Random = UnityEngine.Random;

public class GameState : MonoBehaviour
{
    public int maxGameBoardSize = 7;
    public string[,] gameState;
    public string[,] blocksLifted;
    [Range(0, 10)]
    public int numBlocksToShift = 5;
    public bool gameRunning = true;

    public GameObject playerScoreText;
    public GameObject enemyScoreText;
    public GameObject victoryText;
    public GameObject countText;
    public GameObject whiteBox;
    public GameObject resetButton;

    public UnityEvent ResetEvent;
    public UnityEvent VictoryEvent;
    public UnityEvent DefeatEvent;


    // Start is called before the first frame update
    void Start()
    {
        InitializeState();
        InvokeRepeating("MoveBlocks", 1.5f, 1.5f);
    }

    void Update() {
        if(!gameRunning) {
            CancelInvoke("MoveBlocks");
        }
        if(Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    void MoveBlocks() {
        HashSet<Tuple<int, int>> blocksToMove = new HashSet<Tuple<int, int>>();
        
        for(var i = 0; i < numBlocksToShift; ++i) {
            var row = Random.Range(0, maxGameBoardSize - 1);
            var column = Random.Range(0, maxGameBoardSize - 1);
            blocksToMove.Add(new Tuple<int, int>(row, column));
        }

        foreach(Tuple<int,int> block in blocksToMove) {
            Transform rowInScene = whiteBox.transform.Find($"Row {block.Item1}");
            var childTransforms = rowInScene.GetComponentsInChildren<Transform>();

            foreach (var childTransform in childTransforms) {
                // Get components in children also gets the parent component, so we need to ignore that
                if(childTransform.name.StartsWith("Row")) {
                    continue;
                }
                if (childTransform.position.x == block.Item2 * 2) {
                    // Move up or down?
                    if (blocksLifted[block.Item1, block.Item2] == "up") {
                        blocksLifted[block.Item1, block.Item2] = "down";
                        var controller = childTransform.GetComponent<SquareController>();
                        controller.destinationPosition = 
                            new Vector3(childTransform.position.x, childTransform.position.y - 1, childTransform.position.z);
                    } 
                    else {
                        blocksLifted[block.Item1, block.Item2] = "up";
                        var controller = childTransform.GetComponent<SquareController>();
                        
                        controller.destinationPosition = 
                            new Vector3(childTransform.position.x, childTransform.position.y + 1, childTransform.position.z);
                    }

                    break;
                }
            }
        }
    }

    void CheckForWin() {
        int playerCount = 0;
        int enemyCount = 0;

        for(var i = 0; i < maxGameBoardSize; ++i) {
            for(var j = 0; j < maxGameBoardSize; ++j) {
                if(gameState[i, j] == "player") {
                    playerCount++;
                }
                else if(gameState[i, j] == "enemy") {
                    enemyCount++;
                }
            }
        }

        UpdateUI(playerCount, enemyCount);

        // Must be more than half of the squares. Doing this casts to make sure I round up
        // See here: https://code-examples.net/en/q/e0e5c
        int winSqaureNumber = (int)Math.Ceiling((double)maxGameBoardSize * maxGameBoardSize / 2);
        if(playerCount >= winSqaureNumber) {
            // Player wins!
            victoryText.GetComponent<TextMeshProUGUI>().text = "You Win!";
            victoryText.SetActive(true);
            VictoryEvent?.Invoke();
            gameRunning = false;
            resetButton.SetActive(true);

        }
        else if(enemyCount >= winSqaureNumber) {
            // Enemy wins!
            victoryText.GetComponent<TextMeshProUGUI>().text = "You lose...";
            victoryText.SetActive(true);
            DefeatEvent?.Invoke();
            gameRunning = false;
            resetButton.SetActive(true);
        }
    }

    // Comes in with [x, y, player/opponent]
    // Reason it's an array is in the case that someone doesn't move while the other person does
    public void UpdateState(Tuple<int, int, string> change) {
        // For now these divisions are hard set because the elements are scaled by 2 in the x & z directions
        gameState[change.Item1 / 2, change.Item2 / 2] = change.Item3;
        
        CheckForWin();
    }

    private void UpdateUI(int playerCount, int enemyCount) {
        playerScoreText.GetComponent<TextMeshProUGUI>().text = $"{playerCount}";
        enemyScoreText.GetComponent<TextMeshProUGUI>().text = $"{enemyCount}";
    }

    public void Reset() {
        InitializeState();

        ResetEvent?.Invoke();
        InvokeRepeating("MoveBlocks", 1.5f, 1.5f);
    }

    private void InitializeState() {
        gameState = new string[maxGameBoardSize, maxGameBoardSize];
        blocksLifted = new string[maxGameBoardSize, maxGameBoardSize];

        for (var i = 0; i < maxGameBoardSize; ++i) {
            for (var j = 0; j < maxGameBoardSize; ++j) {
                gameState[i, j] = "none";
                blocksLifted[i, j] = "down";
            }
        }

        // Reset block height as well
        var transforms = whiteBox.GetComponentsInChildren<Transform>();
        foreach(Transform transform in transforms) {
            if(transform.name.StartsWith("Square")) {
                transform.GetComponent<SquareController>().ResetPosition();
                transform.GetComponent<ColorChange>().ResetColor();
            }
        }

        // I want to calculate the winning num at runtime, so set the text after that is done
        countText.GetComponent<TextMeshProUGUI>().text = $"To win: {(int)Math.Ceiling((double)maxGameBoardSize * maxGameBoardSize / 2)}";
        countText.SetActive(true);
        victoryText.SetActive(false);
        resetButton.SetActive(false);
        gameRunning = true;
        
    }
}
