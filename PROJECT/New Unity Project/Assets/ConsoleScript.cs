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
        Debug.LogError(command);
        if (command.Contains("capture"))
        {
            string[] args = command.Split('(')[1].Split(')')[0].Split(',');
            // intendant.CaptureProvince(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
        }
        else if (command.Contains("changemode"))
        {
            string[] args = command.Split('(')[1].Split(')')[0].Split(',');
            intendant.ChangeMode(Convert.ToInt32(args[0]));
        }
        else if (command.Contains("ecinfo"))
        {
            try
            {
                stategen state = intendant.ProtagonistState.GetComponent<stategen>();
                Debug.LogError("Economic info");
                Debug.LogError(String.Format("Foreign Trade is {0}", state.ForeignTrade));
                Debug.LogError(String.Format("Investments is {0}", state.Investments));
                Debug.LogError(String.Format("Foreign Trade is {0}", state.Researches));
                Debug.LogError(String.Format("Army is {0}", state.Army));
                Debug.LogError(String.Format("CivTax is {0}", state.CivilianTax));
                Debug.LogError(String.Format("ProdTax is {0}", state.ProductionTax));
            }
            catch(Exception)
            {
                Debug.LogError("Error occured");
            }
        }

        else if (command.Contains("givemoney"))
        {
            intendant.ProtagonistState.GetComponent<stategen>().Balance += 1000;
            intendant.UpdateBalance();
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
