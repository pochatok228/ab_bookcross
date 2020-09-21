using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBackgroundHeightUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UpdateHeight(int length)
    {
        Vector3 old_scale = gameObject.transform.localScale;
        int rows = length / 9 + 1;
        gameObject.transform.localScale = new Vector3(old_scale.x, old_scale.y, 0.5f * rows);
    }
}
