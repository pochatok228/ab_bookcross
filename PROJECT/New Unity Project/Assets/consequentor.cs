using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Action
{
    int type;
    GameObject action_object;
    GameObject action_subject;
    public static int move = 0;
    int quantity;
    public Action(int type, GameObject ActionSubject, GameObject ActionObject, int quantity)
    {
        this.type = type;
        this.action_subject = ActionSubject;
        this.action_object = ActionObject;
        this.quantity = quantity;
    }

    public int GetActionType(){ return this.type; }
    public GameObject GetActionSubject() { return this.action_subject; }
    public GameObject GetActionObject() { return this.action_object; }

    public int GetQuantity()
    {
        if (type == move)
        {
            return quantity;
        }

        return 0;

    }

    public static int MoveType() { return 0; }
}

public class consequentor : MonoBehaviour
{
    List<Action> actions = new List<Action>();
    intendant intendant;
    // Start is called before the first frame update
    void Start()
    {
        intendant = GameObject.Find("Intendant").GetComponent<intendant>();
    }

    // Update is called once per frame
    public void MoveSoldiers(GameObject province_subject, GameObject province_object, int quantity)
    {
        // Debug.Log(actions);
        Action moveEvent = new Action(Action.move, province_subject, province_object, quantity);
        actions.Add(moveEvent);
    }

    public void Simulate()
    {
        // Debug.Log(actions);
        foreach (Action action in actions)
        {
            if (action.GetActionType() == Action.move)
            {
                if (action.GetActionSubject().GetComponent<provincegen>().state == action.GetActionObject().GetComponent<provincegen>().state)
                {
                    action.GetActionObject().GetComponent<provincegen>().army += action.GetQuantity();
                }
                else
                {

                    provincegen oc = action.GetActionSubject().GetComponent<provincegen>();
                    provincegen def = action.GetActionObject().GetComponent<provincegen>();
                    // Debug.LogError(string.Format("equality of "));
                    int ocArm = action.GetQuantity();
                    int defArm = def.army;
                    int ocTech = oc.state.GetComponent<stategen>().GetCurrentTechLevel();
                    int defTech = def.state.GetComponent<stategen>().GetCurrentTechLevel();
                    // Debug.Log(string.Format("defTech = {0}", defTech));
                    int ocLoyality = (150 - oc.separatism);
                    int defLoyality = (150 - def.separatism);
                    int ocRes = ocArm * ocTech * ocLoyality;
                    int defRes = (int)(defArm * defTech * defLoyality * (1 + (float)def.defensive_ability / 100f));
                    if (oc.state == intendant.ProtagonistState)
                    { // Debug.LogError("contact"); Debug.LogError(string.Format("{0}, {1}", ocRes, defRes)); }
                        if (ocRes > defRes)
                        {
                            Debug.Log("captured");
                            // Debug.Log(string.Format("{0}, {1}, {2}, {3}, {4}", defRes, defArm, defTech, defLoyality, (1 + (float)def.defensive_ability / 100f)));
                            // Debug.Log(string.Format("{0}, {1}", def.GetId(), oc.state.GetComponent<stategen>().GetId()));
                            intendant.CaptureProvince(def.gameObject, oc.state);
                            def.army = ocArm - defArm;
                        }
                        else
                        {
                            def.army = defArm - ocArm;
                        }

                    }
                }
            }
        }
    }
}
