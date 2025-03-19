using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilator : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    const float tau = Mathf.PI*2f;//Constant value of 6.28
    [SerializeField] float period = 2f;
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period == Mathf.Epsilon) // Compare to Epsilon, which is the tiniest number in unity, more accurate to zero.
        {
            Debug.Log("Period can't be zero!");
            return;
        }
        /*
        float cycles = Time.time / period;//Growing over time.
        float rawSine = Mathf.Sin(cycles * tau);//Going from -1 to 1
        Debug.Log("Raw Sin wave: " + rawSine);
        movementFactor = (rawSine + 1)/2;//Now going from 0 to 1
        */
        movementFactor = (Mathf.Sin(Time.time / period * tau) + 1.0f) / 2.0f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
 
    }
}
