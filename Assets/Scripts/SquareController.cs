using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareController : MonoBehaviour
{
    public Vector3 destinationPosition;
    public float speedMultiplier = 10f;
    // Start is called before the first frame update
    void Start()
    {
        // Initially don't move
        destinationPosition = transform.position;
    }

    void FixedUpdate() {
        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, speedMultiplier * Time.fixedDeltaTime);    
    }
}
