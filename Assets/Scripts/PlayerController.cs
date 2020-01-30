using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speedMultiplier = 10f;
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newMovementVector = Vector3.zero;
        newMovementVector.z = Input.GetAxis("Vertical");
        newMovementVector.x = Input.GetAxis("Horizontal");

        rb.AddForce(newMovementVector * speedMultiplier);
    }


}
