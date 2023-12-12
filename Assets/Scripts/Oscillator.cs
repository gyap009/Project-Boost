using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor; // [SerializeField] [Range(0,1)] 
    [SerializeField] float period = 2f;
    void Start()
    {
        startingPosition = transform.position;
    }
    void Update()
    {
        // oscillate the object in a sin wave:

        if (period <= Mathf.Epsilon) { return; } // protect against period is zero.
        // also note: when comparing floats, use Mathf.Epsilon (smallest float) instead of decimal 0

        float cycles = Time.time / period; // grows continually from 0
        const float tau = Mathf.PI * 2; // constant value of 1 complete cycle
        float rawSinWave = Mathf.Sin(cycles * tau); // goes from -1 to +1
        movementFactor = (rawSinWave + 1f) / 2f; // recalculated to go from 0 to 1 

        if (movementFactor > 1) { movementFactor = 0; }
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
