using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private GameState gameState;
    private Material matReference;
    public Color playerTint;
    public Color enemyTint;

    private void Start() {
        gameState = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameState>();
        matReference = GetComponent<Renderer>().material;
    }

    private void OnCollisionEnter(Collision collision) {
        var tag = collision.gameObject.tag;
        int xPosition = (int)transform.position.x;
        int zPosition = (int)transform.position.z;
        Debug.Log("Collision entered: " + collision);
        if(tag == "Player") {
            matReference.color = playerTint;
            gameState.UpdateState(new Tuple<int, int, string>(zPosition, xPosition, "player"));
        }
        else if(tag == "Enemy") {
            matReference.color = enemyTint;
            gameState.UpdateState(new Tuple<int, int, string>(zPosition, xPosition, "enemy"));
        }
    }
}
