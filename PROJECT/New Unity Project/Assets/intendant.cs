using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void AddProvince(GameObject province) { list_of_provinces.Add(province); }
    public void AddState(GameObject state) { list_of_states.Add(state); }
}
