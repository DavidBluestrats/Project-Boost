using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ConstantForce>().force = new Vector3(0, Random.Range(-15f,-10f), 0);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
