using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ArmyManager : MonoBehaviour
{
    intendant intendant;
    int MaxPeopleToConscript;
    int MaxPeopleToDemobilize;
    int MaxPeopleToMove;
    int PeopleToConscript, PeopleToDemobilize, PeopleToMove;
    int ConscriptCost, MoveCost;

    public Slider ConscriptSlider, DemobilizeSlider, MoveSlider;
    public TMPro.TextMeshProUGUI ConscriptQuantityText, DemobilizeQuantityText, MoveQuantityText;
    public TMPro.TextMeshProUGUI ConscriptCostText, MoveCostText;
    // Start is called before the first frame update
    void Start()
    {
        intendant = GameObject.Find("Intendant").GetComponent<intendant>();
    }

    // Update is called once per frame
    public void UpdateInfo()
    {
        provincegen current_provincegen = intendant.selected_province.GetComponent<provincegen>();
        stategen current_stategen = intendant.ProtagonistState.GetComponent<stategen>();

        MaxPeopleToConscript = (int)((float)current_provincegen.population / 2);
        MaxPeopleToDemobilize = current_provincegen.army;
        MaxPeopleToMove = current_provincegen.army;

        PeopleToConscript = 0;
        PeopleToDemobilize = 0;
        PeopleToMove = 0;

        ConscriptSlider.value = 0; DemobilizeSlider.value = 0; MoveSlider.value = 0;
    }

    public void OnChangeConstrictSlider()
    {
        PeopleToConscript = (int) (MaxPeopleToConscript * ConscriptSlider.value);
        // intendant.Alert("Changed");
        int markup = (int)(PeopleToConscript * (intendant.ProtagonistState.GetComponent<stategen>().political_coords.y - 10) / -20);
        ConscriptCost = PeopleToConscript + markup;
        UpdateConscriptQuantityText(); UpdateConscriptQuantityCost();

    }

    void UpdateConscriptQuantityText()
    {
        ConscriptQuantityText.text = (String.Format("{0}", PeopleToConscript));
    }

    void UpdateConscriptQuantityCost()
    {
        ConscriptCostText.text = (String.Format("{0}MP", ConscriptCost));
    }

    public void OnChangeDemobilizeSlider()
    {
        PeopleToDemobilize = (int)(MaxPeopleToDemobilize * DemobilizeSlider.value);
        UpdateDemobilizeQuantityText();
    }

    public void UpdateDemobilizeQuantityText()
    {
        DemobilizeQuantityText.text = String.Format("{0}", PeopleToDemobilize);
    }

    public void OnChangeMoveSlider()
    {
        PeopleToMove = (int)(MaxPeopleToMove * MoveSlider.value);
        MoveCost = (int) (PeopleToMove / 10f);
        UpdateMoveCostText(); UpdateMoveQuantityText();
    }

    public void UpdateMoveQuantityText()
    {
        MoveQuantityText.text = String.Format("{0}", PeopleToMove);
    }

    public void UpdateMoveCostText()
    {
        MoveCostText.text = String.Format("{0}MP", MoveCost);
    }

}
