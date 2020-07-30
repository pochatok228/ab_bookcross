using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float CameraMovementPerFrame = 0.5f;
    public int CameraBorder = 75;
    public int CameraUpperBorder = 25;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position.Set(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private Vector3 GetVector()
    {
        Vector3 CameraMovementVector = new Vector3();

        if (Input.GetKey(KeyCode.W) && transform.position.z > - 1* CameraBorder) CameraMovementVector += new Vector3(0, CameraMovementPerFrame, CameraMovementPerFrame);
        if (Input.GetKey(KeyCode.S) && transform.position.z < CameraBorder) CameraMovementVector += new Vector3(0, -1 * CameraMovementPerFrame, -1 * CameraMovementPerFrame);
        if (Input.GetKey(KeyCode.A) && transform.position.x < CameraBorder) CameraMovementVector += new Vector3(-1 * CameraMovementPerFrame, 0, 0);
        if (Input.GetKey(KeyCode.D) && transform.position.x >  -1 * CameraBorder) CameraMovementVector += new Vector3(CameraMovementPerFrame, 0, 0);
        return CameraMovementVector;
    }

    private void FixedUpdate()
    {

        Vector3 Movement_Vector = GetVector();
        transform.Translate(Movement_Vector);
    }
}
