using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class intendant : MonoBehaviour
{
    private List<GameObject> list_of_provinces = new List<GameObject>();
    private List<GameObject> list_of_states = new List<GameObject>();

    private int mode = 0;
    public int CHOISE_MODE = 0;
    public int POLITICAL_MODE = 1;

    public GameObject ProtagonistState;

    public GameObject PoliticalCoordsMenuBackground;





    public void Step() // Intendant
    {
        // here we call the determinators

    }


    // Start is called before the first frame update
    void Start()
    {
        UpdateMode();
        PoliticalCoordsMenuBackground = GameObject.Find("PoliticalCoordsMenuBackground");
        PoliticalCoordsMenuBackground.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void CaptureProvince(int province_id, int state_id) // Manager
    {
        try
        {
            provincegen province = GameObject.Find(String.Format("province_{0}", province_id)).GetComponent<provincegen>();
            GameObject state = GameObject.Find(String.Format("state_{0}", state_id));
            state.GetComponent<stategen>().AddProvince(province_id);
            province.SetState(state);
            province.ChangeColor(province.state_color);
        }
        catch (Exception)
        {
            Debug.LogError("Can`t find the state or province");
        }
        

        
    }

    public void UpdateMode() // Manager
    {
        if (mode == CHOISE_MODE)
        {
            AlertDefault();
            GameObject[] provinces = GameObject.FindGameObjectsWithTag("Province");
            foreach (GameObject province in provinces)
            {
                provincegen province_manager = province.GetComponent<provincegen>();
                try
                {
                    String state_name = province_manager.state.GetComponent<stategen>().state_name;
                    province_manager.SetTextFieldValue(state_name);
                }
                catch(Exception)
                {
                    String state_name = province_manager.province_name;
                    province_manager.SetTextFieldValue(state_name);
                }
                
            }
        }
        if (mode == POLITICAL_MODE)
        {
            GameObject[] provinces = GameObject.FindGameObjectsWithTag("Province");
            foreach (GameObject province in provinces){
                province.GetComponent<provincegen>().SetStateColor();
                province.GetComponent<provincegen>().ShowArmyOnTextField();
            }
        }
    }
    public void ChangeMode(int new_mode) { mode = new_mode; UpdateMode(); } // Manager



    public void EnterPoliticalCoords()//Executor
    {
        PoliticalCoordsMenuBackground.SetActive(true);
        string flag_path = ProtagonistState.GetComponent<stategen>().icon_file;
        // Debug.Log(flag_path);
        Sprite flag = Resources.Load<Sprite>(flag_path);
        // Debug.Log(flag);
        GameObject.Find("FlagOnCoords").GetComponent<Image>().sprite = flag;
        String upper_reference_text = String.Format("Confirm your intention to play for {0} by choosing its political course and pressing OK button", ProtagonistState.GetComponent<stategen>().state_name);
        GameObject.Find("UpperReference").GetComponent<TMPro.TextMeshProUGUI>().SetText(upper_reference_text);
        GameObject.Find("StateDescriptionText").GetComponent<TMPro.TextMeshProUGUI>().SetText(ProtagonistState.GetComponent<stategen>().state_description);
    }//E
    public void EnterClickPoliticalCoords()//Executor
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 centerPosition = GameObject.Find("CoordinateField").transform.position;
        GameObject.Find("FlagOnCoords").transform.position = mousePosition;
        Vector2 unformed_political_position = mousePosition - centerPosition;
        
        ProtagonistState.GetComponent<stategen>().political_coords = new Vector2(unformed_political_position.x / 20, unformed_political_position.y / 20);
        Alert(String.Format("{0},           {1}", ProtagonistState.GetComponent<stategen>().political_coords.x, ProtagonistState.GetComponent<stategen>().political_coords.y));
    }
    public void EnterPoliticalCoordsOK()
    {
        PoliticalCoordsMenuBackground.SetActive(false);
        ChangeMode(POLITICAL_MODE);
    }//Executor
    public void EnterPoliticalCoordsCancel()//Executor
    {
        PoliticalCoordsMenuBackground.SetActive(false);
    }
    public void Alert(String text) // Executor
    {
        GameObject InstructionField = GameObject.Find("InstructionField");
        InstructionField.GetComponent<TMPro.TextMeshProUGUI>().SetText(text);
    }

    public void AlertDefault() // Executor
    {
        GameObject InstructionField = GameObject.Find("InstructionField");
        if (mode == CHOISE_MODE) { InstructionField.GetComponent<TMPro.TextMeshProUGUI>().SetText("Choose any province that belongs to state you`re going to play for"); }
    }
    
    public int GetMode() { return mode; } // Executor
    public void AddProvince(GameObject province) { list_of_provinces.Add(province); } // executor
    public void AddState(GameObject state) { list_of_states.Add(state); } // executor
}
