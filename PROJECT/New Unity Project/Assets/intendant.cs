using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
// using UnityEditor;

public class intendant : MonoBehaviour
{
    public int step = 0;
    public int action_points = 10;
    public int initial_MP = 200;

    public List<GameObject> list_of_provinces = new List<GameObject>();
    public List<GameObject> list_of_states = new List<GameObject>();

    public int mode = 0;
    public int CHOISE_MODE = 0;
    public int POLITICAL_MODE = 1;
    public int ECONOMICAL_MODE = 2;
    public int ARMY_MODE = 3;
    public int CONSTRUCION_MODE = 4;
    public int DIPLOMACY_MODE = 5;
    public int SEPARATISM_MODE = 6;
    public int NATURALRESOURCES_MODE = 7;
    public int EDUCATION_MODE = 8;
    public int CLIMATE_MODE = 9;

    public GameObject ProtagonistState;

    public GameObject PoliticalCoordsMenuBackground;
    public GameObject PauseMenu;
    public GameObject GameModeMenu;
    public GameObject EconomicMenu;
    public GameObject ConsoleInput;
    public GameObject ConstructionMenu;
    public GameObject ArmyMenu;
    public GameObject DiplomacyMenu;
    public GameObject SeparatismMenu;
    public GameObject EndGameMenu;

    public TMPro.TextMeshProUGUI EndGameText;

    public GameObject ConstructionManager;
    public ArmyManager Armymanager;
    public DiplomacyManager diplomacyManager;
    public SeparatismManager separatismManager;

    public string scene_name;

    public Color EconomicModeMinColor;
    public Color EconomicModeMaxColor;
    public Color ArmyModeMinColor, ArmyModeMaxColor;
    public Color ConstructionModeMinColor, ConstructionModeMaxColor;
    public Color DiplomacyModeWorstColor, DiplomacyModeBestColor, DiplomacyModeEnemyColor, DiplomacyModeAllyColor, DiplomacyModeProtagonistColor;
    public Color SeparatismModeMinColor, SeparatismModeMaxColor;
    public Color NatresModeMinColor, NatresModeMaxColor;
    public Color EduModeMinColor, EduModeMaxColor;
    public Color ClimateModeMinColor, ClimateModeMaxColor;
    
    public Slider ftslider;
    public Slider invslider;
    public Slider resslider;
    public Slider armyslider;
    public Slider civslider;
    public Slider prodslider;

    public GameObject selected_province = null;
    public GameObject selected_state = null;
    public GameObject selected_passive_province = null;

    public Color UITextColor = new Color(244, 255, 0);
    public Color UIErrorTextColor = new Color(255, 68, 0);

    public consequentor Consequentor;

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
        DiplomacyMenu = GameObject.Find("DiplomacyMenu");
        DiplomacyMenu.SetActive(false);
        SeparatismMenu = GameObject.Find("SeparatismMenu");
        SeparatismMenu.SetActive(false);
        EndGameMenu = GameObject.Find("EndGameMenu");
        EndGameMenu.SetActive(false);
        

        Armymanager = ArmyMenu.GetComponent<ArmyManager>();
        diplomacyManager = DiplomacyMenu.GetComponent<DiplomacyManager>();

        scene_name = SceneManager.GetActiveScene().name;

        EconomicModeMinColor = new Color(95, 20, 198);
        EconomicModeMaxColor = new Color(0, 255, 211);
        ArmyModeMinColor = new Color(22,104,122);
        ArmyModeMaxColor = new Color(83, 98, 20);
        ConstructionModeMinColor = new Color(37, 201, 195);
        ConstructionModeMaxColor = new Color(201, 37, 181);
        DiplomacyModeWorstColor = new Color (114, 63, 0);
        DiplomacyModeBestColor = new Color(59, 114, 0);
        DiplomacyModeEnemyColor = new Color(114, 0, 0);
        DiplomacyModeAllyColor = new Color(0, 114, 101);
        DiplomacyModeProtagonistColor = new Color(12, 0, 146);
        SeparatismModeMinColor = new Color(0, 0, 208);
        SeparatismModeMaxColor = new Color(208, 0, 0);
        NatresModeMinColor = new Color(185, 255, 113);
        NatresModeMaxColor = new Color(255, 173, 113);
        EduModeMinColor = new Color(251, 243, 146);
        EduModeMaxColor = new Color(127, 162, 255);
        ClimateModeMinColor = new Color(50, 200, 250);
        ClimateModeMaxColor = new Color(50, 250, 63);

        Consequentor = GameObject.Find("Consequentor").GetComponent<consequentor>();

        UpdateStep(); UpdateBalance(); UpdateActions();
        foreach (GameObject province in list_of_provinces)
        {
            province.GetComponent<Renderer>().material.SetColor("_EmissionColor", province.GetComponent<provincegen>().state.GetComponent<stategen>().state_color);
            province.GetComponent<Renderer>().material.EnableKeyword("__EMISSION");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }



    public void CaptureProvince(GameObject province, GameObject state) // Manager
    {
        try
        {
            
            provincegen pr = province.GetComponent<provincegen>();
            pr.state.GetComponent<stategen>().list_of_provinces.Remove(province);
            Debug.Log(String.Format("current state{0} ", pr.state));

            state.GetComponent<stategen>().AddProvince(pr.GetId());
            pr.separatism = 100;
            Debug.LogError(String.Format("setting state{0} except state{1}", state, pr.state));
            pr.SetState(state);
            pr.ChangeColor(pr.state_color);
        }
        catch (Exception)
        {
            Debug.LogError("Can`t find the state or province");
        }
        UpdateMode();
    }

    public void SelectProvince(GameObject province)
    {
        // Debug.LogError(selected_province);
        try
        {
            selected_province.GetComponent<provincegen>().selected = false;
            // Debug.LogError(selected_province.GetComponent<provincegen>().selected);
        }
        catch (Exception)
        {

        }
        selected_province = province;
        // Debug.Log(selected_province);
        selected_province.GetComponent<provincegen>().selected = true;
        if (mode == CONSTRUCION_MODE)
        {
            ConstructionManager.GetComponent<ConstructionManager>().UpdateMenuText();
        }
        else if (mode == ARMY_MODE)
        {
            OpenMenu(ArmyMenu);
            Armymanager.UpdateInfo();
        }
        else if (mode == SEPARATISM_MODE)
        {
            OpenMenu(SeparatismMenu);
            separatismManager.UpdateInfo();
        }
    }

    public void SelectState(GameObject state)
    {
        try
        {
            selected_state.GetComponent<stategen>().ChangeSelection();
        }
        catch (Exception)
        {

        }
        stategen current_stategen = state.GetComponent<stategen>();
        selected_state = state;
        current_stategen.ChangeSelection();
        if (mode == DIPLOMACY_MODE && state != ProtagonistState)
        {
            OpenMenu(DiplomacyMenu);
            diplomacyManager.UpdateInfo();
        }
    }


    public void SelectPassiveProvince(GameObject province)
    {
        selected_passive_province = province;
        Alert("Province selected");
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
                catch (Exception)
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
            foreach (GameObject province in provinces)
            {
                province.GetComponent<provincegen>().SetStateColor();
                province.GetComponent<provincegen>().SetTextFieldValue(String.Format("{0}", province.GetComponent<provincegen>().population));
            }
        }
        else if (mode == ECONOMICAL_MODE)
        {

            GameObject[] provinces = GameObject.FindGameObjectsWithTag("Province");
            float minimum_value = 100000000, maximum_value = 0;
            foreach (GameObject province in provinces)
            {
                float GDP;
                provincegen current_provincegen = province.GetComponent<provincegen>();
                if (current_provincegen.population != 0)
                { GDP = current_provincegen.productions / current_provincegen.population; }
                else
                {
                    GDP = 0;
                }
                if (GDP < minimum_value) minimum_value = GDP;
                if (GDP > maximum_value) maximum_value = GDP;
            }
            foreach (GameObject province in provinces)
            {
                provincegen current_provincegen = province.GetComponent<provincegen>();
                if (current_provincegen.state != ProtagonistState)
                {
                    current_provincegen.SetStrictColor(Color.white);
                    current_provincegen.SetTextFieldValue("");
                }
                else
                {
                    float GPD = (float)current_provincegen.productions / (float)current_provincegen.population;
                    float percentage = (GPD - minimum_value) / (maximum_value - minimum_value);
                    current_provincegen.ChangeColor(EconomicModeMinColor + (EconomicModeMaxColor - EconomicModeMinColor) * percentage);
                    string sgpd = Convert.ToString(GPD);
                    Debug.LogError(sgpd);
                    current_provincegen.SetTextFieldValue(sgpd.Substring(0, sgpd.IndexOf(',') + 2));

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
                    if (ProtagonistState.GetComponent<stategen>().Enemyes.Contains(current_provincegen.state))
                    {
                        current_provincegen.SetStrictColor(DiplomacyModeEnemyColor);
                    }
                    if (ProtagonistState.GetComponent<stategen>().Allies.Contains(current_provincegen.state))
                    {
                        current_provincegen.SetStrictColor(DiplomacyModeAllyColor);
                    }
                    else { current_provincegen.SetStrictColor(Color.white); }
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
                else
                {
                    float percentage;
                    int current_constructions_quantity = current_provincegen.GetConstructionsQuantity();
                    if (construction_max - construction_min == 0) percentage = 0;
                    else { percentage = (float)(current_constructions_quantity - construction_min) / (float)(construction_max - construction_min); }
                    // Debug.Log(ConstructionModeMinColor + (ConstructionModeMaxColor - ConstructionModeMinColor) * percentage);
                    current_provincegen.ChangeColor(ConstructionModeMinColor + (ConstructionModeMaxColor - ConstructionModeMinColor) * percentage);
                    current_provincegen.SetTextFieldValue(String.Format("Farms-{0}\nFactory-{1}\nLibrary-{2}\nCastle-{3}", current_provincegen.Farms, current_provincegen.Factories, current_provincegen.Libraries, current_provincegen.Fortresses));
                }


            }
        }
        else if (mode == DIPLOMACY_MODE)
        {
            float DiplomacyDistanceMin = 0f;
            float DiplomacyDistanceMax = (float)Math.Pow(200f, 0.5f);
            foreach (GameObject province in list_of_provinces)
            {
                provincegen current_provincegen = province.GetComponent<provincegen>();
                // Debug.Log(current_provincegen);
                if (current_provincegen.state == ProtagonistState)
                {
                    current_provincegen.ChangeColor(DiplomacyModeProtagonistColor);
                    current_provincegen.SetTextFieldValue("");
                }
                else if (ProtagonistState.GetComponent<stategen>().Enemyes.Contains(current_provincegen.state))
                {
                    current_provincegen.ChangeColor(DiplomacyModeEnemyColor);
                    current_provincegen.SetTextFieldValue("In war");
                }
                else if (ProtagonistState.GetComponent<stategen>().Allies.Contains(current_provincegen.state))
                {
                    current_provincegen.ChangeColor(DiplomacyModeAllyColor);
                    current_provincegen.SetTextFieldValue("In alliance");
                }
                else
                {
                    try
                    {
                        float diplomacyDistance = stategen.GetDiplomacyDistance(ProtagonistState, current_provincegen.state);
                        float percentage = diplomacyDistance / DiplomacyDistanceMax;
                        current_provincegen.ChangeColor(DiplomacyModeBestColor + (DiplomacyModeWorstColor - DiplomacyModeBestColor) * percentage);
                        current_provincegen.SetTextFieldValue(String.Format("{0}", diplomacyDistance));
                    }
                    catch (Exception)
                    {
                        current_provincegen.ChangeColor(Color.white);
                    }
                }
            }
        }
        else if (mode == SEPARATISM_MODE)
        {
            int separatism_min = 0, separatism_max = 0;
            for (int i = 0; i < list_of_provinces.Count; i++)
            {
                GameObject province = list_of_provinces[i];
                provincegen current_provincegen = province.GetComponent<provincegen>();
                int current_constructions_quantity = current_provincegen.separatism;
                if (i == 0) { separatism_min = current_constructions_quantity; separatism_max = current_constructions_quantity; }
                if (current_constructions_quantity < separatism_min) separatism_min = current_constructions_quantity;
                if (current_constructions_quantity > separatism_max) separatism_max = current_constructions_quantity;
            }
            foreach (GameObject province in list_of_provinces)
            {
                provincegen current_provincegen = province.GetComponent<provincegen>();
                if (current_provincegen.state != ProtagonistState)
                {
                    current_provincegen.SetStrictColor(Color.white); current_provincegen.SetTextFieldValue("");
                }
                else
                {
                    float percentage;
                    int current_separatism = current_provincegen.separatism;
                    if (separatism_max - separatism_min == 0) percentage = 0;
                    else { percentage = (float)(current_separatism - separatism_min) / (float)(separatism_max - separatism_min); }
                    // Debug.Log(ConstructionModeMinColor + (ConstructionModeMaxColor - ConstructionModeMinColor) * percentage);

                    current_provincegen.ChangeColor(SeparatismModeMinColor + (SeparatismModeMaxColor - SeparatismModeMinColor) * percentage);
                    current_provincegen.SetTextFieldValue(String.Format("{0}", current_separatism));
                }


            }
        }
        else if (mode == NATURALRESOURCES_MODE || mode == EDUCATION_MODE || mode == CLIMATE_MODE)
        {
            int min_value = 0, max_value = 0;
            for (int i = 0; i < list_of_provinces.Count; i++)
            {
                provincegen pr = list_of_provinces[i].GetComponent<provincegen>();
                if (i == 0)
                {
                    if (mode == NATURALRESOURCES_MODE)
                    {
                        min_value = pr.natural_resources; max_value = pr.natural_resources;
                    }
                    if (mode == EDUCATION_MODE)
                    {
                        min_value = pr.education; max_value = pr.education;
                    }
                    if (mode == CLIMATE_MODE)
                    {
                        min_value = pr.climate; max_value = pr.climate;
                    }
                }
                else
                {
                    int current_value = 0;
                    if (mode == NATURALRESOURCES_MODE) current_value = pr.natural_resources; 
                    if (mode == EDUCATION_MODE) current_value = pr.education;
                    if (mode == CLIMATE_MODE) current_value = pr.climate;

                    if (current_value < min_value) min_value = current_value;
                    if (current_value > max_value) max_value = current_value;

                }
            }
            foreach (GameObject province in list_of_provinces)
            {
                provincegen pr = province.GetComponent<provincegen>();
                int current_value = 0;
                if (mode == NATURALRESOURCES_MODE) current_value = pr.natural_resources;
                if (mode == EDUCATION_MODE) current_value = pr.education;
                if (mode == CLIMATE_MODE) current_value = pr.climate;

                float percentage = 0;
                if (max_value - min_value != 0) percentage = (float)(current_value - min_value) / (float)(max_value - min_value);
                else percentage = 0;

                Color color_min = Color.white, color_max = Color.white;
                if (mode == NATURALRESOURCES_MODE) color_min = NatresModeMinColor; color_max = NatresModeMaxColor;
                if (mode == EDUCATION_MODE) color_min = EduModeMinColor; color_max = EduModeMaxColor;
                if (mode == CLIMATE_MODE) color_min = ClimateModeMinColor; color_max = ClimateModeMaxColor;

                pr.ChangeColor(color_min + (color_max - color_min) * percentage);
                pr.SetTextFieldValue(String.Format("{0}", current_value));

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
        string path = "Assets/Saves" + scene_name + ".es3";
        ES3AutoSaveMgr.Current.settings.path = path;
        ES3AutoSaveMgr.Current.Save();

    }

    public void OpenFile(string path)
    {
        ES3AutoSaveMgr.Current.settings.path = path;
    }
    public void LoadGame()
    {
        string path = "Assets/Saves" + scene_name + ".es3";
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
        protagonist_stategen.Balance = initial_MP; UpdateBalance();
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
        if (mode == ARMY_MODE || mode == CONSTRUCION_MODE || mode == SEPARATISM_MODE)
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
        // так бля пора обрабатывать детерминаторы
        foreach (GameObject state in list_of_states)
        {
            stategen st = state.GetComponent<stategen>();
            for (int i = 0; i < st.list_of_provinces.Count; i++)
            {
                if (st.list_of_provinces[i].GetComponent<provincegen>().state != state)
                {
                    Debug.LogError(st);
                }
            }
            if (state != ProtagonistState)
            {
                state.GetComponent<Determinator>().Determinate();
            }
        }
        // так бля пока запускать консеквентор
        Consequentor.Simulate();

        // так бля пора собирать налоги

        foreach (GameObject state in list_of_states)
        {
            stategen st = state.GetComponent<stategen>();
            int people_growth = 0, prod_growth = 0, edu_growth = 0;
            if (state == ProtagonistState)
            {
                people_growth = (int)((st.GetCivilianTax() + st.GetProductionTax()) * st.ForeignTrade);

                prod_growth = (int)((st.GetCivilianTax() + st.GetProductionTax()) * st.Investments);

                edu_growth = (int)((st.GetCivilianTax() + st.GetProductionTax()) * st.Researches);

            }
            else
            {
                people_growth = (int)((float)(st.political_coords.x + 10) / 200f * st.GetPopulation());
                prod_growth = (int)((float)(st.political_coords.x - 10) / -200f * st.GetProduction());
                edu_growth = (int)((float)(st.political_coords.y - 10) / -200f * st.GetEducation());

            }
            for (int i = 0; i < edu_growth; i++)
            {
                st.list_of_provinces[UnityEngine.Random.Range(0, st.list_of_provinces.Count)].GetComponent<provincegen>().education++;
            }
            for (int i = 0; i < prod_growth; i++)
            {
                st.list_of_provinces[UnityEngine.Random.Range(0, st.list_of_provinces.Count)].GetComponent<provincegen>().productions++;
            }
            for (int i = 0; i < people_growth; i++)
            {
                st.list_of_provinces[UnityEngine.Random.Range(0, st.list_of_provinces.Count)].GetComponent<provincegen>().population++;
            }
        }
        stategen protst = ProtagonistState.GetComponent<stategen>();
        SpendMoney(-1 * protst.proficite);
        UpdateMode(); protst.RestructBudget();

        Debug.Log(CheckDiplomacyWin());
        if (CheckSquareWin() || CheckEconomicWin() || CheckDiplomacyWin() || CheckSeparatismWin())
        {
            EndGameMenu.SetActive(true);
        }
        if (ProtagonistState.GetComponent<stategen>().list_of_provinces.Count == 0 || step > 100)
        {
            EndGameMenu.SetActive(false);
            EndGameText.text = "You lose";
        }
    }

    public void quitgame()
    {
        Application.Quit();
    }

    public void HideEndGameMenu()
    {
        EndGameMenu.SetActive(false);
    }
    public bool CheckSquareWin()
    {
        int provinces_no_seas = 0;
        int provinces_prot = 0;
        foreach (GameObject province in list_of_provinces)
        {
            provincegen pr = province.GetComponent<provincegen>();
            if (pr.sea == 0)
            {
                provinces_no_seas++;
            }
            if (pr.state == ProtagonistState)
            {
                provinces_prot++;
            }
        }
        return (float)provinces_prot / (float)provinces_no_seas >= 0.9f;
    }

    public bool CheckEconomicWin()
    {
        int provinces_economic= 0;
        int provinces_prot = 0;
        foreach (GameObject province in list_of_provinces)
        {
            provincegen pr = province.GetComponent<provincegen>();
            provinces_economic += pr.productions;
            if (pr.state == ProtagonistState)
            {
                provinces_prot += pr.productions;
            }
        }
        return (float)provinces_prot / (float)provinces_economic >= 0.9f;
    }

    public bool CheckDiplomacyWin()
    {
        return ProtagonistState.GetComponent<stategen>().Allies.Count == list_of_states.Count - 1;
    }

    public bool CheckSeparatismWin()
    {
        foreach (GameObject province in ProtagonistState.GetComponent<stategen>().list_of_provinces)
        {
            if (province.GetComponent<provincegen>().separatism > 0)
                {
                return false;
            }
        }
        return true;
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

    public int GetBalance() { return ProtagonistState.GetComponent<stategen>().Balance; }
}
