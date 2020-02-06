using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public float speedMultiplier = 10f;
    public int health = 100;
    public Transform enemyTransform;
    public GameState gameState;

    [Range(0, 1)]
    public float speed = 0.9f;

    public GameObject hpText;

    public UnityEvent ResetEvent;
    public UnityEvent DamageEvent;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        targetPosition = new Vector3(0, 1.55f, 0);
    }

    // Update is called once per frame
    void Update() {
        if(gameState.gameRunning) {
            if (gameState.blocksLifted[(int)transform.position.z / 2, (int)transform.position.x / 2] == "up") {
                targetPosition.y = 2.55f;
            } else {
                targetPosition.y = 1.55f;
            }
            // If we're done moving
            if (Vector3.Distance(transform.position, targetPosition) < 0.005f) {
                Vector3 newPosition = Vector3.zero;
                Vector3 newRotation = new Vector3();
                bool newPositionChosen = false;
                if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    newPosition = transform.position + (Vector3.right * 2);
                    newPosition.x = Mathf.Round(newPosition.x);
                    newRotation.y = 90f;
                    newPositionChosen = true;
                } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    newPosition = transform.position + (Vector3.left * 2);
                    newPosition.x = Mathf.Round(newPosition.x);
                    newRotation.y = 270f;
                    newPositionChosen = true;
                } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                    newPosition = transform.position + (Vector3.forward * 2);
                    newPosition.z = Mathf.Round(newPosition.z);
                    newRotation.y = 0f;
                    newPositionChosen = true;
                } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                    newPosition = transform.position + (Vector3.back * 2);
                    newPosition.z = Mathf.Round(newPosition.z);
                    newRotation.y = 180f;
                    newPositionChosen = true;
                }

                if (newPositionChosen && canMove(newPosition)) {
                    targetPosition = newPosition;
                    transform.localEulerAngles = newRotation;
                }
            }
        }
    }

    private void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMultiplier * Time.fixedDeltaTime);
    }

    public void resetPlayer() {
        rb.velocity = Vector3.zero;
        transform.position = initialPosition;
        targetPosition = new Vector3(0, 1.55f, 0);
        ResetEvent?.Invoke(); // Plays the partical system effects
    }

    // Determines if the player can move in the direction they want
    private bool canMove(Vector3 newPosition) {
        if(newPosition.z > 12.5 || newPosition.z < -0.5 || newPosition.x > 12.5 || newPosition.x < -0.5) {
            return false;
        }
        if(Mathf.Abs(newPosition.x - enemyTransform.position.x) < 0.005f && Mathf.Abs(newPosition.z - enemyTransform.position.z) < 0.005f) {
            return false;
        }

        var thisRaised = gameState.blocksLifted[(int)transform.position.z / 2, (int)transform.position.x / 2];
        var destinationRaised = gameState.blocksLifted[(int)newPosition.z / 2, (int)newPosition.x / 2];

        if(destinationRaised == "up" && thisRaised == "down") {
            return false;
        }

        return true;
    }
}
