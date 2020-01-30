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
    public float speedMultiplier = 10f;
    public int health = 100;

    public GameObject hpText;

    public UnityEvent ResetEvent;
    public UnityEvent DamageEvent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0) {
            resetPlayer();
        }
        else {
            Vector3 newMovementVector = Vector3.zero;
            newMovementVector.z = Input.GetAxis("Vertical");
            newMovementVector.x = Input.GetAxis("Horizontal");

            rb.AddForce(newMovementVector * speedMultiplier);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Enemy")) {
            Debug.Log(health);
            health -= 2;
            hpText.GetComponent<Text>().text = $"Health {health.ToString()}";

            DamageEvent?.Invoke();
        }
    }

    private void resetPlayer() {
        ResetEvent?.Invoke();

        rb.velocity = Vector3.zero;
        transform.position = initialPosition;
        health = 100;
        hpText.GetComponent<Text>().text = $"Health {health.ToString()}";
    }
}
