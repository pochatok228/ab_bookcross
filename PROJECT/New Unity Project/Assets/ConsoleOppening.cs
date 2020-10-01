using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleOppening : MonoBehaviour
{
    bool onview = false;
    private void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.BackQuote))
        {
            onview = !onview;
            gameObject.GetComponent<intendant>().ConsoleInput.SetActive(onview);
        }
    }
}
