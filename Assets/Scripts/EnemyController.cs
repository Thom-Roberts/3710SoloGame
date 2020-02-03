using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public GameState gameState;
    [Range(1, 5)]
    public float timeBetweenMoves = 2.5f;
    public float speedMultiplier = 10f;

    private Vector3 targetPosition;
    // First update should set this to true, after it falls into position
    private bool ableToMove;
    private bool gameActive;

    // Start is called before the first frame update
    void Start()
    {   
        ableToMove = false;
        targetPosition = new Vector3(12, 1.5f, 12);

        InvokeRepeating("Move", 1.5f, timeBetweenMoves);
    }

    void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMultiplier * Time.deltaTime);
    }

    private void Move() {
        if(transform.position == targetPosition) {
            targetPosition = FindValidPosition();
            Debug.Log(targetPosition);
        }
    }



    Vector3 FindValidPosition() {
        // NOTE: Add some check to make sure I don't go back to a position I visited the previous round

        // x is columns, z is rows
        string[,] state = gameState.gameState;

        int xCoordinate = (int)transform.position.x;
        int zCoordinate = (int)transform.position.z;

        List<Tuple<int, int>> positionsToConsider = new List<Tuple<int, int>>();
        // Choose next position randomly
        if(xCoordinate > 0) {
            positionsToConsider.Add(new Tuple<int, int>(zCoordinate, xCoordinate - 2));
        }
        if(xCoordinate < 12) {
            positionsToConsider.Add(new Tuple<int, int>(zCoordinate, xCoordinate + 2));
        }
        if(zCoordinate > 0) {
            positionsToConsider.Add(new Tuple<int, int>(zCoordinate - 2, xCoordinate));
        }
        if(zCoordinate < 12) {
            positionsToConsider.Add(new Tuple<int, int>(zCoordinate + 2, xCoordinate));
        }

        int numToConsider = Random.Range(0, positionsToConsider.Count);
        // Check to see if a valid choice?

        return new Vector3(positionsToConsider[numToConsider].Item2, transform.position.y, positionsToConsider[numToConsider].Item1);
    }
}
