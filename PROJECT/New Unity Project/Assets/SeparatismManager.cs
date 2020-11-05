using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SeparatismManager : MonoBehaviour
{
    int max_cost;
    int cost, change;

    intendant intendant;
    public Slider AssimilateSlider;
    public TMPro.TextMeshProUGUI CostText;
    // Start is called before the first frame update
    void Start()
    {
        intendant = GameObject.Find("Intendant").GetComponent<intendant>();
    }

    // Update is called once per frame
    public void UpdateInfo()
    {
        provincegen pr = intendant.selected_province.GetComponent<provincegen>();
        max_cost = (int)(pr.population * pr.separatism / 100f);
        AssimilateSlider.value = 0;
        CostText.text = "0MP";
    }

    public void OnChangeSlider()
    {
        cost = (int) (max_cost * AssimilateSlider.value);
        CostText.text = String.Format("{0}MP", cost);
    }

    public void OnPressAssimilateButton()
    {
        if (intendant.GetBalance() >= cost && intendant.action_points >= 1)
        {
            provincegen pr = intendant.selected_province.GetComponent<provincegen>();
            pr.separatism = (int)(pr.separatism * (1 - AssimilateSlider.value));
            intendant.SpendMoney(cost); intendant.SpendActionPoints(1);
            intendant.UpdateMode(); UpdateInfo();
        }
        
    }
}
