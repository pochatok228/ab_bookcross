using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




// Functions in Gamesystem
// can be devided in 5 groups:
//      1. Executor     -> Low-level object function that works with public parameters in objectgens. The last instantion of process
//      2. Manager      -> Middle-level object function that rises step-by-step several executors
//      3. Consequentor -> Gamesystem function that simulate the desicions of player and AI and, depending on result, calls one of managers for each Event
//      4. Determinator -> High-level object function that makes desicions. Replacement of player
//      5. Intendant    -> Gamesystem function that gets result from all alive determinators and player and launches the simulator


    /*              Step Cycle
     *                                       -> Determinator1(Situation) -> Result ->                                  Manager1(); -> Executor1(); Executor2();
     *                                                                                                                                Executor3();
     *  Start Step ->  Intendant(Situation) {--> Player Desicions -> Result        -> } -> Consequentor(Result[]) -> { Manager2(); -> Executor2(); Executor4(); } -> Next Step
     * 
                                             -> Determinator2(Situation) -> Result ->                                  Manager3(); -> Executor0();
     */



public class stategen : MonoBehaviour
{
    public Color state_color;
    public List<GameObject> list_of_provinces;
    public Vector2 political_coords;
    public Vector2 diplomacy_coords;
    public string state_name;
    public string icon_file;
    public Sprite flag;
    private intendant intendant;
    public string state_description;

    private int Balance = 1000;
    private int totalIncomePopulation;
    private int totalIncomeProduction;
    private int totalOutcomeArmy;
    
    public float ForeignTrade = 0.1f;
    public float Investments = 0.1f;
    public float Researches = 0.1f;
    public float Army = 0.1f;
    public double CivilianTax = 0.18f;
    public double ProductionTax = 0.13f;

    public GameObject capital_province;
    // public string flag_file_name;
    // Start is called before the first frame update
    void Start()
    {
        // changing state color to more pastel hue
        intendant = GameObject.Find("Intendant").GetComponent<intendant>();
        float h, s, v;
        Color.RGBToHSV(state_color, out h, out s, out v);
        s = 0.5f;
        v = 1;
        state_color = Color.HSVToRGB(h, s, v);

        intendant.AddState(gameObject);


        //Debug.Log(icon_file);
        flag = Resources.Load(icon_file) as Sprite;
        //Debug.Log(flag);
        UpdateEconomicInfo();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void UpdateEconomicInfo()
    {
        totalIncomePopulation = 0;
        totalIncomeProduction = 0;
        totalOutcomeArmy = 0;
        foreach (GameObject province in list_of_provinces)
        {
            provincegen province_manager = province.GetComponent<provincegen>();
            totalIncomeProduction += province_manager.productions;
            totalIncomePopulation += province_manager.population;
            totalOutcomeArmy += province_manager.army;
            
        }
    }

    public void ImportEconomicSlidersData()
    {
        CivilianTax = intendant.civslider.value;
        ProductionTax = intendant.prodslider.value;
        ForeignTrade = intendant.ftslider.value;
        Investments = intendant.invslider.value;
        Researches = intendant.invslider.value;
        Debug.Log(CivilianTax);

        double income = totalIncomeProduction * CivilianTax + totalIncomePopulation * ProductionTax;
        Army = totalOutcomeArmy / (float)(income);
        intendant.armyslider.GetComponent<Slider>().value = Army;
        double totalOutcomeFT = income * ForeignTrade;
        double totalOutcomeInv = income * Investments;
        double totalOutcomeRes = income * Researches;

        int surplus = Convert.ToInt32(income - totalOutcomeFT - totalOutcomeInv - totalOutcomeRes - totalOutcomeArmy);
        if (surplus > 0)
        {
            string Info = String.Format("Current budget proficit is {0}", surplus);
            intendant.Alert(Info);
        }
        else
        {
            string Info = String.Format("Current budget deficit is {0}", -1 * surplus);
            intendant.Alert(Info);
        }
        
    }
    public void AddProvince(int province_id) // manager
    {
        string province_object_name = "province_" + Convert.ToString(province_id);
        GameObject province = GameObject.Find(province_object_name);
        list_of_provinces.Add(province);
        provincegen current_provincegen = province.GetComponent<provincegen>();
        // Debug.Log(String.Format("stategen {0}, {1}, {2}", state_color.r, state_color.g, state_color.b));
        current_provincegen.ChangeColor(state_color);
        current_provincegen.SetState(gameObject);
    }

    public void CalculateCharacteristics()
    {
        
    }
}
