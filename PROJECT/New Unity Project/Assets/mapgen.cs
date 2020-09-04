using System.Collections; using System.Collections.Generic; using UnityEngine;

public class mapgen : MonoBehaviour {
    public GameObject province_template;
    public GameObject state_template;
    private GameObject province_0;
    private provincegen province_0_provincegen;
    private GameObject province_1;
    private provincegen province_1_provincegen;


    private GameObject state_0;
    private stategen state_0_stategen;

    void Start()
    {
        province_0 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_0_provincegen = province_0.GetComponent<provincegen>();
        province_0_provincegen.name = "province_0";
        province_0_provincegen.province_name = "";
        province_0_provincegen.verticles = new Vector3[] {new Vector3(3.0f, 0, -13.375f), new Vector3(11.75f, 0, -6.625f), new Vector3(40.125f, 0, -17.0f), new Vector3(41.125f, 0, -36.75f), new Vector3(19.875f, 0, -33.0f)};
		province_0_provincegen.triangles = new int[] {4, 0, 1, 4, 1, 2, 2, 3, 4};
		province_0_provincegen.capital_coords = new Vector3(18.0f , 0, -27.75f);
        province_0_provincegen.state_color = new Color(165, 103, 151);
        province_0_provincegen.productions = 100;
        province_0_provincegen.education = 100;
        province_0_provincegen.army= 10;
        province_0_provincegen.natural_resources= 100;
        province_0_provincegen.separatism= 50;
        province_0_provincegen.climate = 100;
        province_0_provincegen.sea = 0;
        province_0_provincegen.defensive_ability = 100;
        province_0_provincegen.AddConnection(1);
        province_0_provincegen.Construct();

        province_1 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_1_provincegen = province_1.GetComponent<provincegen>();
        province_1_provincegen.name = "province_1";
        province_1_provincegen.province_name = "";
        province_1_provincegen.verticles = new Vector3[] {new Vector3(-33.875f, 0, -23.625f), new Vector3(-27.875f, 0, -2.75f), new Vector3(-8.375f, 0, -11.75f), new Vector3(-5.625f, 0, -31.125f), new Vector3(-7.625f, 0, -35.375f)};
		province_1_provincegen.triangles = new int[] {4, 0, 1, 4, 1, 2, 2, 3, 4};
		province_1_provincegen.capital_coords = new Vector3(-31.0f , 0, -21.625f);
        province_1_provincegen.state_color = new Color(165, 103, 151);
        province_1_provincegen.productions = 100;
        province_1_provincegen.education = 100;
        province_1_provincegen.army= 10;
        province_1_provincegen.natural_resources= 100;
        province_1_provincegen.separatism= 50;
        province_1_provincegen.climate = 100;
        province_1_provincegen.sea = 0;
        province_1_provincegen.defensive_ability = 100;
        province_1_provincegen.AddConnection(0);
        province_1_provincegen.Construct();





         state_0 = Instantiate(state_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			state_0.name = "state_0";
			state_0_stategen = state_0.GetComponent<stategen>();
			state_0_stategen.state_name = "";
			state_0_stategen.state_color = new Color(165, 103, 151);
         state_0_stategen.political_coords = new Vector2(0f, 0f);
         state_0_stategen.diplomacy_coords = new Vector2(0f, 0f);
         state_0_stategen.AddProvince(0);
         state_0_stategen.AddProvince(1);
        state_0_stategen.capital_province = GameObject.Find("province_0");

    }
}