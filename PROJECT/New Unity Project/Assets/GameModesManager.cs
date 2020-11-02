using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameModesManager : MonoBehaviour
{
    GameObject GameModeMenu;
    intendant intendant;
    // Start is called before the first frame update
    void Start()
    {
        GameModeMenu = GameObject.Find("GameModeMenu");
        GameModeMenu.SetActive(false);
        intendant = GameObject.Find("Intendant").GetComponent<intendant>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenGameModeMenu()
    {
        if (intendant.GetMode() != intendant.CHOISE_MODE)
        {
            bool onview = GameModeMenu.activeSelf;
            if (onview) GameModeMenu.SetActive(false);
            else intendant.OpenMenu(GameModeMenu);
            try
            {
                intendant.selected_province.GetComponent<provincegen>().selected = false;
                intendant.selected_province = null;
            }
            catch (Exception) { }
        }
    }

    public void SetPoliticalMode() {intendant.SetMode(intendant.POLITICAL_MODE); GameModeMenu.SetActive(false); }
    public void SetEconomicalMode() {
        // intendant.ImportEconomicSlidersDataToProtagonistState();
        
        intendant.SetMode(intendant.ECONOMICAL_MODE);
        intendant.OpenMenu(intendant.EconomicMenu);
        foreach (GameObject province in GameObject.FindGameObjectsWithTag("Province"))
        {
            provincegen prg = province.GetComponent<provincegen>();
            if (prg.state == intendant.ProtagonistState)
            {
                
                double GDP = prg.productions / prg.population;
                prg.SetTextFieldValue(String.Format("{0:0.000}", GDP));
            }
            else
            {
                prg.SetTextFieldValue("???");
            }
        }
    }

    public void SetArmyMode()
    {
        intendant.SetMode(intendant.ARMY_MODE);
        intendant.AlertDefault();
    }

    public void SetConstructionMode()
    {
        intendant.SetMode(intendant.CONSTRUCION_MODE);
        intendant.AlertDefault();
        GameModeMenu.SetActive(false);
    }
}
