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

    public int Balance = 1000;
    private int totalIncomePopulation;
    private int totalIncomeProduction;
    private int totalOutcomeArmy;

    public float ForeignTrade = 0.1f;
    public float Investments = 0.1f;
    public float Researches = 0.1f;
    public float Army = 0.1f;
    public float CivilianTax = 0.18f;
    public float ProductionTax = 0.13f;


    public int population;
    public int productions;

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

    }

    public void ExportEconomicSlidersData()
    {
        // Изменяем позиции слайдеров, вызываем при открытии эк мода
        intendant.civslider.value = CivilianTax;
        intendant.prodslider.value = ProductionTax;
        intendant.ftslider.value = ForeignTrade;
        intendant.invslider.value = Investments;
        intendant.resslider.value = Researches;

    }
    public void ImportEconomicSlidersData()
    {
        CivilianTax = intendant.civslider.value;
        ProductionTax = intendant.prodslider.value;
        ForeignTrade = intendant.ftslider.value;
        Investments = intendant.invslider.value;
        Researches = intendant.resslider.value;
        Debug.Log(CivilianTax);
    }
    public void RestructBudget()
    {
        ImportEconomicSlidersData();
        int income = GetCivilianTax() + GetProductionTax();
        int army_outcome = GetArmyOutcome();
        float ArmyOutcomePercentage = (float)army_outcome / (float)income;
        intendant.armyslider.value = ArmyOutcomePercentage;
    }
    public int GetCivilianTax()
    {
        int civilian_tax = 0;
        foreach (GameObject province in list_of_provinces)
        {
            civilian_tax += province.GetComponent<provincegen>().GetCivilianTax();
        }
        totalIncomePopulation = civilian_tax;
        return civilian_tax;
    }
    public int GetProductionTax()
    {
        int production_tax = 0;
        foreach (GameObject province in list_of_provinces)
        {
            production_tax += province.GetComponent<provincegen>().GetProductionTax();
        }
        totalIncomeProduction = production_tax;
        return production_tax;
    }

    public int GetArmyOutcome()
    {
        int army_outcome = 0;
        foreach (GameObject province in list_of_provinces)
        {
            army_outcome += province.GetComponent<provincegen>().GetProductionTax();
        }
        totalOutcomeArmy = army_outcome;
        return army_outcome;
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
        population = 0; productions = 0;
        foreach (GameObject province in list_of_provinces)
        {
            provincegen current_provincegen = province.GetComponent<provincegen>();
            population += current_provincegen.population;
            productions += current_provincegen.productions;
        }
    }
    
}
