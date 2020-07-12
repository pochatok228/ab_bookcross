using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapgen : MonoBehaviour {
    public GameObject province_template;
    private GameObject province_0;
    private provincegen province_0_provincegen;

    void Start()
    {

        province_0 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_0.name = "province_0";
        province_0_provincegen = province_0.GetComponent<provincegen>();
        province_0_provincegen.name = "province_0";
        province_0_provincegen.dots = new List<Vector3> { new Vector3(0, 0, 0), new Vector3(1, 1, 0), new Vector3(2, 0, 0), new Vector3(1, -1, 0)};

    }
}
