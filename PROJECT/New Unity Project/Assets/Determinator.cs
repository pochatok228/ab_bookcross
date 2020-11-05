using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Determinator : MonoBehaviour
{
    int action_points;
    stategen state;
    intendant intendant;
    consequentor Consequentor;
    // Start is called before the first frame update
    void Start()
    {
        state = gameObject.GetComponent<stategen>();
        intendant = GameObject.Find("Intendant").GetComponent<intendant>();
        Consequentor = GameObject.Find("Consequentor").GetComponent<consequentor>();
    }


    public void Determinate()
    {
        action_points = 10;
        CheckExistance();
        if (state.Enemyes.Count > 0)
        {
            ArmyDeterminate(10);
        }
        else
        {
            ConstructionDeterminate(4);
            DiplomacyDeterminate(6);
        }
    }


    public void CheckExistance()
    {
        if (state.list_of_provinces.Count > 0) { }
        else
        {
            foreach (GameObject current_state in state.Allies)
            {
                current_state.GetComponent<stategen>().Allies.Remove(gameObject);
            }
            foreach (GameObject current_state in state.Enemyes)
            {
                current_state.GetComponent<stategen>().Enemyes.Remove(gameObject);
            }
            intendant.list_of_states.Remove(gameObject);
            Destroy(gameObject);
        }
    }
    void ConstructionDeterminate(int action_points)
    {
        int province_quantity = gameObject.GetComponent<stategen>().list_of_provinces.Count;
        int id1 = Random.Range(0, province_quantity);
        int id2 = Random.Range(0, province_quantity);
        int obj1 = Random.Range(0, 4);
        int obj2 = Random.Range(0, 4);
        if (obj1 == 0) gameObject.GetComponent<stategen>().list_of_provinces[id1].GetComponent<provincegen>().Farms++;
        if (obj1 == 1) gameObject.GetComponent<stategen>().list_of_provinces[id1].GetComponent<provincegen>().Factories++;
        if (obj1 == 2) gameObject.GetComponent<stategen>().list_of_provinces[id1].GetComponent<provincegen>().Libraries++;
        if (obj1 == 3) gameObject.GetComponent<stategen>().list_of_provinces[id1].GetComponent<provincegen>().Fortresses++;
        if (obj2 == 0) gameObject.GetComponent<stategen>().list_of_provinces[id2].GetComponent<provincegen>().Farms++;
        if (obj2 == 1) gameObject.GetComponent<stategen>().list_of_provinces[id2].GetComponent<provincegen>().Factories++;
        if (obj2 == 2) gameObject.GetComponent<stategen>().list_of_provinces[id2].GetComponent<provincegen>().Libraries++;
        if (obj2 == 3) gameObject.GetComponent<stategen>().list_of_provinces[id2].GetComponent<provincegen>().Fortresses++;
    }

    void DiplomacyDeterminate(int action_points)
    {
        int state_quantity = intendant.list_of_states.Count;
        // Debug.Log("vG");
        
        while (true)
        {
            if (action_points <= 0)
            { break; }
            int id = Random.Range(0, state_quantity);
            int event_id = Random.Range(0, 4);
            if (event_id == 0)
            {
                stategen.ImprovePoliticalRelations(gameObject, intendant.list_of_states[id]);
                action_points -= 2;
            }
            else if (event_id == 1)
            {
                stategen.WorsenPoliticalRelations(gameObject, intendant.list_of_states[id]);
                action_points -= 2;
            }
            else if (event_id == 2 && stategen.GetDiplomacyDistance(gameObject, intendant.list_of_states[id]) > 5)
            {
                gameObject.GetComponent<stategen>().Enemyes.Add(intendant.list_of_states[id]);
                intendant.list_of_states[id].GetComponent<stategen>().Enemyes.Add(gameObject);
                action_points -= 4;
            }
            else if (event_id == 3 && stategen.GetDiplomacyDistance(gameObject, intendant.list_of_states[id]) < 1)
            {
                gameObject.GetComponent<stategen>().Allies.Add(intendant.list_of_states[id]);
                intendant.list_of_states[id].GetComponent<stategen>().Allies.Add(gameObject);
                action_points -= 4;
            }
        }
    }

    void ArmyDeterminate(int action_points)
    {
        List<provincegen> border_provinces = new List<provincegen>();
        foreach (GameObject province in state.list_of_provinces)
        {
            provincegen pr = province.GetComponent<provincegen>();
            foreach (int connection in pr.connections)
            {
                if (state.Enemyes.Contains(provincegen.GetProvinceById(connection).GetComponent<provincegen>().state))
                {
                    border_provinces.Add(pr);
                }
            }
        }
        while (true)
        {
            if (action_points < 0)
            {
                break;
            }
            int action = Random.Range(0, 2);
            if (action == 0)
            {
                int province_id = Random.Range(0, border_provinces.Count);
                int quantity = Random.Range(0, border_provinces[province_id].population / 2);
                border_provinces[province_id].population -= quantity;
                border_provinces[province_id].army += quantity;
                action_points -= 4;
            }
            if (action == 1)
            {
                int province_id = Random.Range(0, border_provinces.Count);
                List<provincegen> enemy_provinces = new List<provincegen>();
                foreach (int conenction in border_provinces[province_id].connections)
                {
                    provincegen pr = provincegen.GetProvinceById(conenction).GetComponent<provincegen>();
                    if (state.Enemyes.Contains(pr.state))
                    {
                        enemy_provinces.Add(pr);
                    }
                }
                int province_subject_id = Random.Range(0, enemy_provinces.Count);
                int quantity = Random.Range(0, border_provinces[province_id].army + 1);
                Consequentor.MoveSoldiers(border_provinces[province_id].gameObject, enemy_provinces[province_subject_id].gameObject, quantity);
                border_provinces[province_id].army -= quantity;
            }
        }
    }
}
