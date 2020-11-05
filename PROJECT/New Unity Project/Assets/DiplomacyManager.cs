using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiplomacyManager : MonoBehaviour
{
    intendant intendant;
    string war_declare = "Declare a war";
    string sign_peace = "Sign a peace";
    string alliance_join = "Offer to join the alliance";
    string alliance_kick = "Eject from the alliance";
    bool in_war = false;
    bool in_alliance = false;

    public TMPro.TextMeshProUGUI WarButtonText;
    public TMPro.TextMeshProUGUI AllianceButtonText;
    // Start is called before the first frame update
    void Start()
    {
        intendant = GameObject.Find("Intendant").GetComponent<intendant>();
    }


    public void UpdateInfo()
    {
        in_war = false; in_alliance = false;
        WarButtonText.text = war_declare;
        AllianceButtonText.text = alliance_join;
        if (intendant.ProtagonistState.GetComponent<stategen>().Enemyes.Contains(intendant.selected_state))
        {
            in_war = true;
            WarButtonText.text = sign_peace;
        }
        if (intendant.ProtagonistState.GetComponent<stategen>().Allies.Contains(intendant.selected_state))
        {
            in_alliance = true;
            AllianceButtonText.text = alliance_kick;
        }
    }

    public void ImproveDiplomaticRelations()
    {
        if (intendant.ProtagonistState.GetComponent<stategen>().Balance >= 100 && intendant.action_points >= 2)
        {
            stategen.ImprovePoliticalRelations(intendant.ProtagonistState, intendant.selected_state);
            intendant.SpendMoney(100); intendant.SpendActionPoints(2);
            intendant.UpdateMode();
        }
    }
    public void WorsenDiplomaticRelations()
    {
        if (intendant.ProtagonistState.GetComponent<stategen>().Balance >= 100 && intendant.action_points >= 2)
        {
            stategen.WorsenPoliticalRelations(intendant.ProtagonistState, intendant.selected_state);
            intendant.SpendMoney(100); intendant.SpendActionPoints(2);
            intendant.UpdateMode();
        }
    }

    public void DeclareWar()
    {
        if (intendant.ProtagonistState.GetComponent<stategen>().Enemyes.Contains(intendant.selected_state))
        { 
            if (intendant.action_points >= 4)
            {
                // добавить условия мира
                intendant.ProtagonistState.GetComponent<stategen>().Enemyes.Remove(intendant.selected_state);
                intendant.selected_state.GetComponent<stategen>().Enemyes.Remove(intendant.ProtagonistState);
                intendant.UpdateMode(); intendant.SpendActionPoints(4);
            }
        }

        else {
            if (intendant.action_points >= 4 && stategen.GetDiplomacyDistance(intendant.ProtagonistState, intendant.selected_state) > 5)
            {
                intendant.ProtagonistState.GetComponent<stategen>().Enemyes.Add(intendant.selected_state);
                intendant.selected_state.GetComponent<stategen>().Enemyes.Add(intendant.ProtagonistState);
                intendant.UpdateMode();
                intendant.SpendActionPoints(4);
            }
        }
    }

    public void ChangeAllyStatus()
    {
        if (intendant.ProtagonistState.GetComponent<stategen>().Allies.Contains(intendant.selected_state))
        {
            if (intendant.action_points >= 4)
            {
                intendant.ProtagonistState.GetComponent<stategen>().Allies.Remove(intendant.selected_state);
                intendant.selected_state.GetComponent<stategen>().Allies.Remove(intendant.ProtagonistState);
                intendant.UpdateMode(); intendant.SpendActionPoints(4);
            }
        }
        else
        {
            if (intendant.action_points >= 4 && stategen.GetDiplomacyDistance(intendant.ProtagonistState, intendant.selected_state) < 1)
            {
                intendant.ProtagonistState.GetComponent<stategen>().Allies.Add(intendant.selected_state);
                intendant.selected_state.GetComponent<stategen>().Allies.Add(intendant.ProtagonistState);
                intendant.UpdateMode();
                intendant.SpendActionPoints(4);
            }
        }
    }
}
