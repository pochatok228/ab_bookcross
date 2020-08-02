using System.Collections; using System.Collections.Generic; using UnityEngine;

public class mapgen : MonoBehaviour {
    public GameObject province_template;
    public GameObject state_template;
    private GameObject province_0;
    private provincegen province_0_provincegen;
    private GameObject province_1;
    private provincegen province_1_provincegen;
    private GameObject province_2;
    private provincegen province_2_provincegen;


    private GameObject state_0;
    private stategen state_0_stategen;
    private GameObject state_1;
    private stategen state_1_stategen;

    void Start()
    {
        province_0 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_0_provincegen = province_0.GetComponent<provincegen>();
        province_0_provincegen.name = "province_0";
        province_0_provincegen.province_name = "";
        province_0_provincegen.verticles = new Vector3[] {new Vector3(55.0f, 0, -16.125f), new Vector3(49.125f, 0, -17.125f), new Vector3(37.0f, 0, -13.0f), new Vector3(30.375f, 0, -9.5f), new Vector3(26.625f, 0, -3.625f), new Vector3(26.625f, 0, 8.875f), new Vector3(35.25f, 0, 14.75f), new Vector3(40.25f, 0, 10.375f), new Vector3(42.25f, 0, 6.125f), new Vector3(42.75f, 0, 4.125f), new Vector3(44.875f, 0, 1.875f), new Vector3(46.75f, 0, 2.375f), new Vector3(49.25f, 0, 9.875f), new Vector3(48.75f, 0, 17.125f), new Vector3(50.875f, 0, 19.875f), new Vector3(52.625f, 0, 21.0f), new Vector3(54.375f, 0, 20.375f), new Vector3(55.875f, 0, 16.625f), new Vector3(58.25f, 0, 9.125f)};
		province_0_provincegen.triangles = new int[] {18, 0, 1, 18, 1, 2, 18, 2, 3, 3, 4, 5, 3, 5, 6, 3, 6, 7, 3, 7, 8, 3, 8, 9, 3, 9, 10, 3, 10, 11, 18, 3, 11, 18, 11, 12, 18, 12, 13, 18, 13, 14, 18, 14, 15, 18, 15, 16, 16, 17, 18};
		province_0_provincegen.capital_coords = new Vector3(-34.875f , 0, -8.75f);
        province_0_provincegen.state_color = new Color(85, 188, 129);
        province_0_provincegen.productions = 100;
        province_0_provincegen.education = 100;
        province_0_provincegen.army= 10;
        province_0_provincegen.natural_resources= 100;
        province_0_provincegen.separatism= 50;
        province_0_provincegen.climate = 100;
        province_0_provincegen.sea = 0;
        province_0_provincegen.defensive_ability = 100;
province_0_provincegen.Construct();

        province_1 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_1_provincegen = province_1.GetComponent<provincegen>();
        province_1_provincegen.name = "province_1";
        province_1_provincegen.province_name = "";
        province_1_provincegen.verticles = new Vector3[] {new Vector3(23.125f, 0, -43.625f), new Vector3(7.75f, 0, -27.875f), new Vector3(14.625f, 0, -14.5f), new Vector3(13.5f, 0, -1.0f), new Vector3(-5.5f, 0, 12.75f), new Vector3(-7.25f, 0, 4.25f), new Vector3(6.875f, 0, 13.5f), new Vector3(14.125f, 0, 7.375f), new Vector3(21.0f, 0, -1.375f), new Vector3(22.25f, 0, -7.875f), new Vector3(22.75f, 0, -20.5f), new Vector3(26.75f, 0, -31.125f), new Vector3(36.125f, 0, -33.625f)};
		province_1_provincegen.triangles = new int[] {12, 0, 1, 5, 6, 7, 5, 7, 8, 8, 9, 10, 11, 12, 1, 11, 1, 2, 11, 2, 3, 10, 11, 3, 8, 10, 3, 8, 3, 4, 4, 5, 8};
		province_1_provincegen.capital_coords = new Vector3(-8.0f , 0, -30.125f);
        province_1_provincegen.state_color = new Color(85, 188, 129);
        province_1_provincegen.productions = 100;
        province_1_provincegen.education = 100;
        province_1_provincegen.army= 10;
        province_1_provincegen.natural_resources= 100;
        province_1_provincegen.separatism= 50;
        province_1_provincegen.climate = 100;
        province_1_provincegen.sea = 0;
        province_1_provincegen.defensive_ability = 100;
province_1_provincegen.Construct();

        province_2 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_2_provincegen = province_2.GetComponent<provincegen>();
        province_2_provincegen.name = "province_2";
        province_2_provincegen.province_name = "";
        province_2_provincegen.verticles = new Vector3[] {new Vector3(-39.0f, 0, -29.875f), new Vector3(-36.75f, 0, -22.875f), new Vector3(-22.375f, 0, -9.75f), new Vector3(-6.25f, 0, -9.875f), new Vector3(-3.625f, 0, -18.875f), new Vector3(-6.375f, 0, -35.75f), new Vector3(-16.625f, 0, -39.875f)};
		province_2_provincegen.triangles = new int[] {6, 0, 1, 6, 1, 2, 6, 2, 3, 6, 3, 4, 4, 5, 6};
		province_2_provincegen.capital_coords = new Vector3(33.5f , 0, -23.625f);
        province_2_provincegen.state_color = new Color(201, 126, 222);
        province_2_provincegen.productions = 100;
        province_2_provincegen.education = 100;
        province_2_provincegen.army= 10;
        province_2_provincegen.natural_resources= 100;
        province_2_provincegen.separatism= 50;
        province_2_provincegen.climate = 100;
        province_2_provincegen.sea = 0;
        province_2_provincegen.defensive_ability = 100;
province_2_provincegen.Construct();





        state_0 = Instantiate(state_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			state_0.name = "state_0";
			state_0_stategen = state_0.GetComponent<stategen>();
			state_0_stategen.state_name = "fds";
			state_0_stategen.state_color = new Color(85, 188, 129);
         state_0_stategen.political_coords = new Vector2(0, 0);
         state_0_stategen.diplomacy_coords = new Vector2(0, 0);
        state_0_stategen.AddProvince(0);
        state_0_stategen.AddProvince(1);

        state_1 = Instantiate(state_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
			state_1.name = "state_1";
			state_1_stategen = state_1.GetComponent<stategen>();
			state_1_stategen.state_name = "fds";
			state_1_stategen.state_color = new Color(201, 126, 222);
         state_1_stategen.political_coords = new Vector2(0, 0);
         state_1_stategen.diplomacy_coords = new Vector2(0, 0);
        state_1_stategen.AddProvince(2);

    }
}