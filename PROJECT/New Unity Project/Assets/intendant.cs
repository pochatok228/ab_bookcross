using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class intendant : MonoBehaviour
{
    public int step = 0;
    public int action_points = 10;


    private List<GameObject> list_of_provinces = new List<GameObject>();
    private List<GameObject> list_of_states = new List<GameObject>();

    public int mode = 0;
    public int CHOISE_MODE = 0;
    public int POLITICAL_MODE = 1;
    public int ECONOMICAL_MODE = 2;
    public int ARMY_MODE = 3;
    public int CONSTRUCION_MODE = 4;

    public GameObject ProtagonistState;

    public GameObject PoliticalCoordsMenuBackground;
    public GameObject PauseMenu;
    public GameObject GameModeMenu;
    public GameObject EconomicMenu;
    public GameObject ConsoleInput;
    public GameObject ConstructionMenu;
    public GameObject ArmyMenu;

    public GameObject ConstructionManager;
    public ArmyManager Armymanager;

    public string scene_name;

    public Color EconomicModeMinColor;
    public Color EconomicModeMaxColor;
    public Color ArmyModeMinColor, ArmyModeMaxColor;
    public Color ConstructionModeMinColor, ConstructionModeMaxColor;

    
    public Slider ftslider;
    public Slider invslider;
    public Slider resslider;
    public Slider armyslider;
    public Slider civslider;
    public Slider prodslider;

    public GameObject selected_province = null;
    public GameObject selected_state = null;

    public Color UITextColor = new Color(244, 255, 0);
    public Color UIErrorTextColor = new Color(255, 68, 0);

    // Start is called before the first frame update
    void Start()
    {
        UpdateMode();
        PoliticalCoordsMenuBackground = GameObject.Find("PoliticalCoordsMenuBackground");
        PoliticalCoordsMenuBackground.SetActive(false);
        PauseMenu = GameObject.Find("PauseMenu");
        PauseMenu.SetActive(false);
        ConsoleInput = GameObject.Find("Console");
        ConsoleInput.SetActive(false);
        GameModeMenu = GameObject.Find("GameModeMenu");
        EconomicMenu = GameObject.Find("Economic Menu");
        EconomicMenu.SetActive(false);
        ConstructionMenu = GameObject.Find("ConstructionMenu");
        ConstructionMenu.SetActive(false);
        ArmyMenu = GameObject.Find("ArmyMenu");
        ArmyMenu.SetActive(false);

        Armymanager = ArmyMenu.GetComponent<ArmyManager>();

        scene_name = SceneManager.GetActiveScene().name;

        EconomicModeMinColor = new Color(95, 20, 198);
        EconomicModeMaxColor = new Color(0, 255, 211);
        ArmyModeMinColor = new Color(22,104,122);
        ArmyModeMaxColor = new Color(83, 98, 20);
        ConstructionModeMinColor = new Color(37, 201, 195);
        ConstructionModeMaxColor = new Color(201, 37, 181);

        UpdateStep(); UpdateBalance(); UpdateActions();
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

    public void SelectProvince(GameObject province)
    {
        try
        {
            selected_province.GetComponent<provincegen>().selected = false;
        }
        catch (Exception)
        {

        }
        selected_province = province;
        Debug.Log(selected_province);
        selected_province.GetComponent<provincegen>().selected = true;
        if (mode == CONSTRUCION_MODE)
        {
            ConstructionManager.GetComponent<ConstructionManager>().UpdateMenuText();
        }
        else if (mode == ARMY_MODE)
        {
            Armymanager.UpdateInfo();
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
        else if (mode == POLITICAL_MODE)
        {
            AlertDefault();
            GameObject[] provinces = GameObject.FindGameObjectsWithTag("Province");
            foreach (GameObject province in provinces){
                province.GetComponent<provincegen>().SetStateColor();
                province.GetComponent<provincegen>().SetTextFieldValue(String.Format("{0}", province.GetComponent<provincegen>().population));
            }
        }
        else if (mode == ECONOMICAL_MODE)
        {

            GameObject[] provinces = GameObject.FindGameObjectsWithTag("Province");
            float minimum_value = 100000000, maximum_value = 0;
            foreach(GameObject province in provinces)
            {
                provincegen current_provincegen = province.GetComponent<provincegen>();
                float GDP = current_provincegen.productions / current_provincegen.population;
                if (GDP < minimum_value) minimum_value = GDP;
                if (GDP > maximum_value) maximum_value = GDP;
            }
            foreach (GameObject province in provinces)
            {
                provincegen current_provincegen = province.GetComponent<provincegen>();
                if (current_provincegen.state != ProtagonistState)
                {
                    current_provincegen.SetStrictColor(Color.white);
                }
                else
                {
                    float GPD = current_provincegen.productions / current_provincegen.population;
                    float percentage = (GPD - minimum_value) / (maximum_value - minimum_value);
                    current_provincegen.ChangeColor(EconomicModeMinColor + (EconomicModeMaxColor - EconomicModeMinColor) * percentage);
                }
            }

        }
        else if (mode == ARMY_MODE)
        {
            int army_min = 0; int army_max = 0;
            for (int i = 0; i < list_of_provinces.Count; i++)
            {
                provincegen current_provincegen = list_of_provinces[i].GetComponent<provincegen>();
                int current_army = current_provincegen.army;
                if (i == 0) { army_min = current_army; army_max = current_army; }
                if (current_army < army_min) { army_min = current_army; }
                if (current_army > army_max) { army_max = current_army; }
            }
            foreach (GameObject province in list_of_provinces)
            {
                provincegen current_provincegen = province.GetComponent<provincegen>();
                int current_army = current_provincegen.army;
                if (current_provincegen.state != ProtagonistState)
                {
                    current_provincegen.SetStrictColor(Color.white);
                    current_provincegen.SetTextFieldValue("");
                }
                else
                {
                    float percentage = (float)(current_army - army_min) / (float)(army_max - army_min);
                    current_provincegen.ChangeColor(ArmyModeMinColor + (ArmyModeMaxColor - ArmyModeMinColor) * percentage);
                    current_provincegen.SetTextFieldValue(Convert.ToString(current_army));
                }
            }
        }
        else if (mode == CONSTRUCION_MODE)
        {
            int construction_min = 0, construction_max = 0;
            for (int i = 0; i < list_of_provinces.Count; i++)
            {
                GameObject province = list_of_provinces[i];
                provincegen current_provincegen = province.GetComponent<provincegen>();
                int current_constructions_quantity = current_provincegen.GetConstructionsQuantity();
                if (i == 0) { construction_min = current_constructions_quantity; construction_max = current_constructions_quantity; }
                if (current_constructions_quantity < construction_min) construction_min = current_constructions_quantity;
                if (current_constructions_quantity > construction_max) construction_max = current_constructions_quantity;
            }
            foreach (GameObject province in list_of_provinces)
            {
                provincegen current_provincegen = province.GetComponent<provincegen>();
                if (current_provincegen.state != ProtagonistState)
                {
                    current_provincegen.SetStrictColor(Color.white); current_provincegen.SetTextFieldValue("");
                }
                else {
                    float percentage;
                    int current_constructions_quantity = current_provincegen.GetConstructionsQuantity();
                    if (construction_max - construction_min == 0) percentage = 0;
                    else { percentage = (float)(current_constructions_quantity - construction_min) / (float)(construction_max - construction_min); }
                    Debug.Log(ConstructionModeMinColor + (ConstructionModeMaxColor - ConstructionModeMinColor) * percentage);
                    current_provincegen.ChangeColor(ConstructionModeMinColor + (ConstructionModeMaxColor - ConstructionModeMinColor) * percentage);
                    current_provincegen.SetTextFieldValue(String.Format("Farms-{0}\nFactory-{1}\nLibrary-{2}\nCastle-{3}", current_provincegen.Farms, current_provincegen.Factories, current_provincegen.Libraries, current_provincegen.Fortresses));
                }
                
                
            }
        }

    }
    public void ChangeMode(int new_mode) { mode = new_mode; UpdateMode(); } // Manager
    public void OpenAndClosePauseMenu() {
        if (PauseMenu.activeSelf) PauseMenu.SetActive(false);
        else
        {
            OpenMenu(PauseMenu);
            
        }
    }
    public void SaveGame()
    {
        string path = EditorUtility.SaveFilePanel("Save game", "Assets/Saves", scene_name + ".es3", "es3");
        ES3AutoSaveMgr.Current.settings.path = path;
        ES3AutoSaveMgr.Current.Save();

    }

    public void OpenFile(string path)
    {
        ES3AutoSaveMgr.Current.settings.path = path;
    }
    public void LoadGame()
    {
        string path = EditorUtility.OpenFilePanel("Save game", "Assets/Saves",  "es3");
        ES3AutoSaveMgr.Current.settings.path = path;
        ES3AutoSaveMgr.Current.Load();

        foreach (GameObject province in GameObject.FindGameObjectsWithTag("Province"))
        {
            province.GetComponent<provincegen>().SetStateColor();
        }
        Debug.Log(mode);
        UpdateMode();
    }
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
        float x = ProtagonistState.GetComponent<stategen>().political_coords.x;
        float y = ProtagonistState.GetComponent<stategen>().political_coords.y;
        string xk, yk;
        if (x >= 0) xk = "R"; else xk = "L";
        if (y >= 0) yk = "A"; else yk = "L";

        Alert(String.Format("{0}{1}. {2}{3}", x, xk, y, yk));
    }
    public void EnterPoliticalCoordsOK()
    {
        PoliticalCoordsMenuBackground.SetActive(false);
        ChangeMode(POLITICAL_MODE);
        UpdateBalance(); UpdateActions(); UpdateStep();
        stategen protagonist_stategen = ProtagonistState.GetComponent<stategen>();
        protagonist_stategen.GetCivilianTax(); protagonist_stategen.GetProductionTax(); protagonist_stategen.GetArmyOutcome();
    }//Executor
    public void EnterPoliticalCoordsCancel()//Executor
    {
        PoliticalCoordsMenuBackground.SetActive(false);
    }

    public void ImportEconomicSlidersDataToProtagonistState() { ProtagonistState.GetComponent<stategen>().RestructBudget(); }
    public void Alert(String text) // Executor
    {
        GameObject InstructionField = GameObject.Find("InstructionField");
        InstructionField.GetComponent<TMPro.TextMeshProUGUI>().SetText(text);
    }
    public void AlertDefault() // Executor
    {
        GameObject InstructionField = GameObject.Find("InstructionField");
        if (mode == CHOISE_MODE) { InstructionField.GetComponent<TMPro.TextMeshProUGUI>().SetText("Choose any province that belongs to state you`re going to play for"); }
        if (mode == ARMY_MODE)
        {
            InstructionField.GetComponent<TMPro.TextMeshProUGUI>().SetText("Choose any province of your state to interact with");
        }
    }
    public void SetMode(int new_mode) { mode = new_mode; UpdateMode(); }
    public int GetMode() { return mode; } // Executor
    public void AddProvince(GameObject province) { list_of_provinces.Add(province); } // executor
    public void AddState(GameObject state) { list_of_states.Add(state); } // executor

    public void OpenMenu(GameObject menu)
    {
        foreach(GameObject i in GameObject.FindGameObjectsWithTag("Menu"))
        {
            i.SetActive(false);
        }
        menu.SetActive(true);
    }

    public void CloseAllMenu() { foreach (GameObject menu in GameObject.FindGameObjectsWithTag("Menu")) menu.SetActive(false); }



    // блок функций для взаимодейтвия с главными параметрами


     public void Step()
    {
        step++;
        UpdateStep();
        action_points = 10; UpdateActions();
        // так бля пора собирать налоги
    }

    public void SpendActionPoints(int points_to_spend)
    {
        action_points -= points_to_spend;
        UpdateActions();
    }

    public void SpendMoney(int money)
    {
        ProtagonistState.GetComponent<stategen>().Balance -= money;
        UpdateBalance();
    }

    public void UpdateStep()
    {
        GameObject.Find("Steps/Text").GetComponent<TMPro.TextMeshProUGUI>().text = String.Format("Step {0}", step);
    }

    public void UpdateBalance()
    {
        try { GameObject.Find("Balance/Text").GetComponent<TMPro.TextMeshProUGUI>().text = String.Format("Balance {0}MP", ProtagonistState.GetComponent<stategen>().Balance); } 
        catch { GameObject.Find("Balance/Text").GetComponent<TMPro.TextMeshProUGUI>().text = "Choose state"; }
    }

    public void UpdateActions()
    {
        GameObject.Find("Actions/Text").GetComponent<TMPro.TextMeshProUGUI>().color = UITextColor;
        GameObject.Find("Actions/Text").GetComponent<TMPro.TextMeshProUGUI>().text = String.Format("Actions {0}", action_points);
    }
}
