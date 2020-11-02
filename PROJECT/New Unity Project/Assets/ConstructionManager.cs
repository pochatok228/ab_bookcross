using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConstructionManager : MonoBehaviour
{
    public intendant intendant;
    public GameObject ConstructionMenu;
    // Start is called before the first frame update
    void Start()
    {
        intendant = GameObject.Find("Intendant").GetComponent<intendant>();
    }
    public void UpdateMenuText()
    {
        provincegen current_provincegen = intendant.selected_province.GetComponent<provincegen>();
        GameObject.Find("FarmBuild/Text").GetComponent<TMPro.TextMeshProUGUI>().text = String.Format("Build farm for {0}", current_provincegen.Farms*100 + 100);
        GameObject.Find("FactoryBuild/Text").GetComponent<TMPro.TextMeshProUGUI>().text = String.Format("Build factory for {0}", current_provincegen.Factories * 100 + 100);
        GameObject.Find("LibraryBuild/Text").GetComponent<TMPro.TextMeshProUGUI>().text = String.Format("Build library for {0}", current_provincegen.Libraries * 100 + 100);
        GameObject.Find("FortressBuild/Text").GetComponent<TMPro.TextMeshProUGUI>().text = String.Format("Build fortress for {0}", current_provincegen.Fortresses * 100 + 100);

    }
    public void BuildFarm()
    {
        int cost = intendant.selected_province.GetComponent<provincegen>().Farms * 100 + 100;
        if (intendant.action_points >= 2 && intendant.ProtagonistState.GetComponent<stategen>().Balance >= cost)
        {
            intendant.selected_province.GetComponent<provincegen>().Farms++;
            UpdateMenuText(); intendant.UpdateMode();
            intendant.selected_province.GetComponent<provincegen>().selected = false;
            intendant.selected_province = null;
            ConstructionMenu.SetActive(false);
            intendant.SpendActionPoints(2);
            intendant.SpendMoney(cost);
        }
 
    }

    public void BuildFactory()
    {
        int cost = intendant.selected_province.GetComponent<provincegen>().Factories * 100 + 100;
        if (intendant.action_points >= 2 && intendant.ProtagonistState.GetComponent<stategen>().Balance >= cost)
        {

            intendant.selected_province.GetComponent<provincegen>().Factories++;
            UpdateMenuText(); intendant.UpdateMode();
            intendant.selected_province.GetComponent<provincegen>().selected = false;
            intendant.selected_province = null;
            ConstructionMenu.SetActive(false);
        }

    }

    public void BuildLibrary()
    {
        int cost = intendant.selected_province.GetComponent<provincegen>().Libraries * 100 + 100;
        if (intendant.action_points >= 2 && intendant.ProtagonistState.GetComponent<stategen>().Balance >= cost)
        {
            intendant.selected_province.GetComponent<provincegen>().Libraries++;
            UpdateMenuText(); intendant.UpdateMode();
            intendant.selected_province.GetComponent<provincegen>().selected = false;
            intendant.selected_province = null;
            ConstructionMenu.SetActive(false);
        }
    }
    
    public void BuildFortress()
    {
        int cost = intendant.selected_province.GetComponent<provincegen>().Fortresses * 100 + 100;
        if (intendant.action_points >= 2 && intendant.ProtagonistState.GetComponent<stategen>().Balance >= cost)
        {
            intendant.selected_province.GetComponent<provincegen>().Fortresses++;
            UpdateMenuText(); intendant.UpdateMode();
            intendant.selected_province.GetComponent<provincegen>().selected = false;
            intendant.selected_province = null;
            ConstructionMenu.SetActive(false);
        }
    }
}
