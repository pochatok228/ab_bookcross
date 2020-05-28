using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    public float CameraZoomPerFrame = 0.5f;
    public float LowerCameraBorder = 15;
    public float UpperCameraBorder = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 ZoomVector = new Vector3();
        if (Input.GetKey(KeyCode.Q) && transform.position.y < UpperCameraBorder) ZoomVector += new Vector3(0, CameraZoomPerFrame, -1 * CameraZoomPerFrame);
        if (Input.GetKey(KeyCode.E) && transform.position.y > LowerCameraBorder) ZoomVector += new Vector3(0, -1 * CameraZoomPerFrame, CameraZoomPerFrame);
        transform.Translate(ZoomVector);
    }
}
