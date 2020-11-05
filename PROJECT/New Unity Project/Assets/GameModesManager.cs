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
                Debug.Log("opened");
                intendant.selected_state.GetComponent<stategen>().ChangeSelection();
                intendant.selected_state = null;
                Debug.Log("finished");
            }
            catch (Exception) { }
            try
            {
                intendant.selected_state.GetComponent<stategen>().ChangeSelection();
                intendant.selected_state = null;
            }
            catch (Exception)
            { }

        }
    }

    public void SetPoliticalMode() {intendant.SetMode(intendant.POLITICAL_MODE); GameModeMenu.SetActive(false); }
    public void SetEconomicalMode() {
        // intendant.ImportEconomicSlidersDataToProtagonistState();
        
        intendant.SetMode(intendant.ECONOMICAL_MODE);
        intendant.OpenMenu(intendant.EconomicMenu);
        
    }

    public void SetArmyMode()
    {
        intendant.SetMode(intendant.ARMY_MODE);
        intendant.AlertDefault();
        GameModeMenu.SetActive(false);
    }

    public void SetConstructionMode()
    {
        intendant.SetMode(intendant.CONSTRUCION_MODE);
        intendant.AlertDefault();
        GameModeMenu.SetActive(false);
    }

    public void SetDiplomacyMode()
    {
        intendant.SetMode(intendant.DIPLOMACY_MODE);
        intendant.AlertDefault();
        GameModeMenu.SetActive(false);
    }

    public void SetSeparatismMode()
    {
        intendant.SetMode(intendant.SEPARATISM_MODE);
        GameModeMenu.SetActive(false);
        intendant.AlertDefault();
    }

    public void SetNatresMode()
    {
        intendant.SetMode(intendant.NATURALRESOURCES_MODE);
        GameModeMenu.SetActive(false);
    }
    public void SetEducationMode()
    {
        intendant.SetMode(intendant.EDUCATION_MODE);
        GameModeMenu.SetActive(false);
    }

    public void SetClimateMode()
    {
        intendant.SetMode(intendant.CLIMATE_MODE);
        GameModeMenu.SetActive(false);
    }
}
