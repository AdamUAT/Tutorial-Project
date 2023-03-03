using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;

    [Tooltip("The time it takes for the platform to oscillate, in seconds.")]
    public float period = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(startPosition, endPosition, Mathf.Sin(Time.time / period * Mathf.PI / 2)* Mathf.Sin(Time.time / period * Mathf.PI / 2));
    }
}
