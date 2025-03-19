using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fallingObstaclePrefab;
    [SerializeField] float secondsBetweenSpawns;
    float nextSpawnTime;
    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + secondsBetweenSpawns;
            Instantiate(fallingObstaclePrefab, transform.position, Quaternion.identity);
        }
    }
}
