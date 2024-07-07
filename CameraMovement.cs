using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 10.0f;

    private float up = 0f;

    private float forward = 0f;

    private float right = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        up = 0f;
        forward = 0f;
        right = 0f;

        if (Input.GetKey(KeyCode.W) == true)
        {
            forward = 1.0f;
        }

        if (Input.GetKey(KeyCode.S) == true)
        {
            forward = -1.0f;
        }

        if (Input.GetKey(KeyCode.D) == true)
        {
            right = 1.0f;
        }

        if (Input.GetKey(KeyCode.A) == true)
        {
            right = -1.0f;
        }

        if (Input.GetKey(KeyCode.Space) == true)
        {
            up = 1.0f;
        }

        if (Input.GetKey(KeyCode.LeftShift) == true)
        {
            up = -1.0f;
        }

        transform.position += new Vector3(right, up, forward).normalized * speed * Time.deltaTime;
    }
}
