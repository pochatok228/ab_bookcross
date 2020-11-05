using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ArmyManager : MonoBehaviour
{
    public intendant intendant;
    public consequentor Consequentor;
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
    }

    // Update is called once per frame
    public void UpdateInfo()
    {
        // Debug.LogError(intendant);
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

    public void ConscriptSoldiers()
    {
        if (intendant.action_points >= 4  && intendant.ProtagonistState.GetComponent<stategen>().Balance >= ConscriptCost)
        {
            intendant.selected_province.GetComponent<provincegen>().army += PeopleToConscript;
            intendant.selected_province.GetComponent <provincegen>().population -= PeopleToConscript;
            intendant.SpendMoney(ConscriptCost);
            intendant.SpendActionPoints(4);
            intendant.UpdateMode();
            UpdateInfo();
        }
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

    public void DemobilizeSoldiers()
    {
        if (intendant.action_points >= 2)
        {
            intendant.selected_province.GetComponent<provincegen>().army -= PeopleToDemobilize;
            intendant.selected_province.GetComponent<provincegen>().population += PeopleToDemobilize;
            intendant.SpendActionPoints(2);
            intendant.UpdateMode(); UpdateInfo();
        }
    }

    public void OnChangeMoveSlider()
    {
        PeopleToMove = (int)(MaxPeopleToMove * MoveSlider.value);
        MoveCost = (int) (PeopleToMove / 10f);
        UpdateMoveCostText(); UpdateMoveQuantityText();
        intendant.Alert("Select the province to move by press right mouse button");
    }

    public void UpdateMoveQuantityText()
    {
        MoveQuantityText.text = String.Format("{0}", PeopleToMove);
    }

    public void UpdateMoveCostText()
    {
        MoveCostText.text = String.Format("{0}MP", MoveCost);
    }

    public void MoveSoldiers()
    {
        // Debug.LogError("Entered MS function");
        try
        {
            bool enough_AP = intendant.action_points >= 1;
            bool enough_MP = intendant.ProtagonistState.GetComponent<stategen>().Balance >= MoveCost;
            bool common_border = intendant.selected_province.GetComponent<provincegen>().connections.Contains(intendant.selected_passive_province.GetComponent<provincegen>().GetId());
            stategen current_stategen = intendant.ProtagonistState.GetComponent<stategen>();
            

            bool self_ally_or_enemy = (intendant.ProtagonistState == intendant.selected_passive_province.GetComponent<provincegen>().state)
                                        ||
                                      (current_stategen.Allies.Contains(intendant.selected_passive_province.GetComponent<provincegen>().state))
                                        ||
                                      (current_stategen.Enemyes.Contains(intendant.selected_passive_province.GetComponent<provincegen>().state));
            // Debug.Log(String.Format("{0}, {1}, {2}, {3}", enough_AP, enough_MP, common_border, self_ally_or_enemy));
            if (enough_AP && enough_MP && self_ally_or_enemy && common_border)
            {
                // Debug.LogError(String.Format(" 1 Moved {0} from {1} to {2}", PeopleToMove, intendant.selected_province, intendant.selected_passive_province));
                Consequentor.MoveSoldiers(intendant.selected_province, intendant.selected_passive_province, PeopleToMove);
                //  Debug.LogError(String.Format(" 2 Moved {0} from {1} to {2}", PeopleToMove, intendant.selected_province, intendant.selected_passive_province));
                intendant.SpendMoney(MoveCost);
                intendant.SpendActionPoints(1);
                intendant.selected_province.GetComponent<provincegen>().army -= PeopleToMove;
                intendant.UpdateMode(); UpdateInfo();
            }
        }
        catch(Exception)
        {

        }
    }
}
