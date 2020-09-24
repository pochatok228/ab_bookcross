using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConsoleScript : MonoBehaviour
{
    intendant intendant;
    bool onview = true;

    // Start is called before the first frame update
    void Start() {
        intendant = GameObject.Find("Intendant").GetComponent<intendant>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    void ConsoleManager(string command) // manager
    {
        Debug.Log(command);
        if (command.Contains("capture"))
        {
            string[] args = command.Split('(')[1].Split(')')[0].Split(',');
            intendant.CaptureProvince(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
        }
        else if (command.Contains("changemode"))
        {
            string[] args = command.Split('(')[1].Split(')')[0].Split(',');
            intendant.ChangeMode(Convert.ToInt32(args[0]));
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            GameObject text = GameObject.Find("ConsoleText");
            TMPro.TextMeshProUGUI text_mesh_pro = text.GetComponent<TMPro.TextMeshProUGUI>();
            string text_value = text_mesh_pro.text;
            ConsoleManager(text_value);
            text_mesh_pro.ClearMesh();
        }
    }

    public void ChangeViewStatus() // Executor
    {
        onview = !onview;
        // Debug.Log("Console is opened");
        gameObject.SetActive(onview);
    }
}
