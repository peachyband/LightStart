using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    public float freq = 3.0f;
    public float phase = 0.0f;
    public float amplitude = 0.05f;
    private float elapsedTime = 0.0f;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;
        //transform.position.x += Mathf.Cos(Time.deltaTime) * speed;
        transform.position = startPos + new Vector3(0, 
            Mathf.Cos(elapsedTime* freq +phase) * amplitude, 
            0);
    }
}
