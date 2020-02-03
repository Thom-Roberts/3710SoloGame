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
    public Transform playerTransform;
    private Vector3 targetPosition;
    private Vector3 previousPosition; // Used for preventing movement to same previous square
    
    private bool gameActive;

    // Start is called before the first frame update
    void Start()
    {   
        gameActive = true;
        targetPosition = new Vector3(12, 1.5f, 12);
        previousPosition = transform.position;
        InvokeRepeating("Move", 1.5f, timeBetweenMoves);
    }

    void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMultiplier * Time.deltaTime);
    }

    private void Move() {
        if(gameActive && transform.position == targetPosition) {
            Vector3 potentialPosition = FindValidPosition();
            if(potentialPosition != Vector3.zero) {
                targetPosition = potentialPosition;
            }
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

        while(true) {
            // No valid positions found
            if(positionsToConsider.Count == 0) {
                return Vector3.zero;
            }

            int numToConsider = Random.Range(0, positionsToConsider.Count);
            Vector3 nextPosition = new Vector3(positionsToConsider[numToConsider].Item2, transform.position.y, positionsToConsider[numToConsider].Item1);
            // Run checks if a valid position
            bool willCollideWithPlayer = (nextPosition.x == playerTransform.position.x) && (nextPosition.z == playerTransform.position.z);
            if(nextPosition == previousPosition || willCollideWithPlayer) { // Don't want the same position twice in a row
                positionsToConsider.RemoveAt(numToConsider);
            }
            else {
                previousPosition = transform.position;
                return nextPosition;
            }
        }
    }
}
