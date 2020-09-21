using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleOppening : MonoBehaviour
{
    GameObject console;
    ConsoleScript console_script;

    
    // Start is called before the first frame update
    void Start()
    {
        console = GameObject.Find("Console");
        console_script = console.GetComponent<ConsoleScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.BackQuote))
        {
            
            console_script.ChangeViewStatus();
            
        }
    }
}
