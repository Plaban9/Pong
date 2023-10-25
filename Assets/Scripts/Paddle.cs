    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private Vector3 paddlePosition;

    [SerializeField]
    private float magnitude;

    [SerializeField]
    KeyCode leftMovementKeyCode;
    
    [SerializeField]
    KeyCode rightMovementKeyCode;

    // Start is called before the first frame update
    void Start()
    {
        paddlePosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(leftMovementKeyCode))
        {
            paddlePosition.Set(paddlePosition.x, paddlePosition.y + magnitude * Time.deltaTime, paddlePosition.z);
        }
        else if ((Input.GetKey(rightMovementKeyCode)))
        {
            paddlePosition.Set(paddlePosition.x, paddlePosition.y - magnitude * Time.deltaTime, paddlePosition.z);
        }

        this.transform.position = paddlePosition;
    }
}
