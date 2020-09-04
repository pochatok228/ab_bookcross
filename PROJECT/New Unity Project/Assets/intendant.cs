using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class intendant : MonoBehaviour
{
    private List<GameObject> list_of_provinces = new List<GameObject>();
    private List<GameObject> list_of_states = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void CaptureProvince(int province_id, int state_id)
    {
        try
        {
            provincegen province = GameObject.Find(String.Format("province_{0}", province_id)).GetComponent<provincegen>();
            GameObject state = GameObject.Find(String.Format("state_{0}", state_id));
            state.GetComponent<stategen>().AddProvince(province_id);
            province.SetState(state);
            province.ChangeColor(province.state_color);
        }
        catch (Exception)
        {
            Debug.LogError("Can`t find the state or province");
        }
        

        
    }

    public void AddProvince(GameObject province) { list_of_provinces.Add(province); } // executor
    public void AddState(GameObject state) { list_of_states.Add(state); } // executor
}
