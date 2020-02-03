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

    [Range(0, 1)]
    public float speed = 0.9f;

    public GameObject hpText;

    public UnityEvent ResetEvent;
    public UnityEvent DamageEvent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        targetPosition = new Vector3(0, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // If we're done moving
        if(targetPosition == transform.position) {
            Vector3 newPosition = Vector3.zero;
            bool newPositionChosen = false;
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                newPosition = transform.position + (Vector3.right * 2);
                newPositionChosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                newPosition = transform.position + (Vector3.left * 2);
                newPositionChosen = true;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                newPosition = transform.position + (Vector3.forward * 2);
                newPositionChosen = true;
            } 
            else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                newPosition = transform.position + (Vector3.back * 2);
                newPositionChosen = true;
            }

            if(newPositionChosen && canMove(newPosition)) {
                targetPosition = newPosition;
            }
        }
        //Vector3 newMovementVector = Vector3.zero;
        //newMovementVector.z = Input.GetAxis("Vertical");
        //newMovementVector.x = Input.GetAxis("Horizontal");

        //rb.AddForce(newMovementVector * speedMultiplier);
    }

    private void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speedMultiplier * Time.deltaTime);
    }

    private void resetPlayer() {
        ResetEvent?.Invoke();

        rb.velocity = Vector3.zero;
        transform.position = initialPosition;
        health = 100;
        hpText.GetComponent<Text>().text = $"Health {health.ToString()}";
    }

    // Determines if the player can move in the direction they want
    private bool canMove(Vector3 newPosition) {
        if(newPosition.z > 12 || newPosition.z < 0 || newPosition.x > 12 || newPosition.x < 0) {
            return false;
        }
        if(newPosition.x == enemyTransform.position.x && newPosition.z == enemyTransform.position.z) {
            return false;
        }

        return true;
    }
}
