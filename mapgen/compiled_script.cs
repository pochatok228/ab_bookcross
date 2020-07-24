using System.Collections; using System.Collections.Generic; using UnityEngine;

public class mapgen : MonoBehaviour {
    GameObject province_template;
    private GameObject province_0;
    private provincegen province_0_provincegen;
    private GameObject province_1;
    private provincegen province_1_provincegen;
    private GameObject province_2;
    private provincegen province_2_provincegen;
    private GameObject province_3;
    private provincegen province_3_provincegen;

    void Start()
    {
        province_0 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_0.name = "province_0";
        province_0_provincegen = province_0.GetComponent<provincegen>();
        province_0_provincegen.name = "asd";
        province_0_provincegen.dots = new List<Vector3> {new Vector3(9.375f, 0, -22.5f), new Vector3(-2.5f, 0, -19.125f), new Vector3(-1.75f, 0, -12.25f), new Vector3(11.625f, 0, -9.625f)};
        province_0_provincegen.state_color = new Color(108, 113, 75);
        province_0_provincegen.productions = 100;
        province_0_provincegen.education = 100;
        province_0_provincegen.army= 10;
        province_0_provincegen.natural_resources= 100;
        province_0_provincegen.separatism= 50;
        province_0_provincegen.climate = 100;
        province_0_provincegen.sea = 0;
        province_0_provincegen.defensive_ability = 100;

        province_1 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_1.name = "province_1";
        province_1_provincegen = province_1.GetComponent<provincegen>();
        province_1_provincegen.name = "vcx";
        province_1_provincegen.dots = new List<Vector3> {new Vector3(31.5f, 0, -22.5f), new Vector3(27.75f, 0, -23.375f), new Vector3(23.0f, 0, -22.125f), new Vector3(21.0f, 0, -21.875f), new Vector3(20.0f, 0, -20.0f), new Vector3(22.25f, 0, -14.875f), new Vector3(23.625f, 0, -13.0f), new Vector3(24.25f, 0, -11.375f), new Vector3(25.25f, 0, -10.375f), new Vector3(27.5f, 0, -8.375f), new Vector3(28.0f, 0, -7.5f), new Vector3(29.5f, 0, -7.75f), new Vector3(29.5f, 0, -9.75f), new Vector3(28.375f, 0, -11.75f), new Vector3(28.375f, 0, -13.0f), new Vector3(30.625f, 0, -11.25f), new Vector3(32.125f, 0, -9.625f), new Vector3(34.0f, 0, -8.125f), new Vector3(35.0f, 0, -9.625f), new Vector3(34.5f, 0, -11.75f), new Vector3(33.75f, 0, -13.375f), new Vector3(32.625f, 0, -15.25f), new Vector3(32.875f, 0, -16.5f), new Vector3(35.125f, 0, -16.125f), new Vector3(36.25f, 0, -16.375f), new Vector3(36.0f, 0, -20.25f), new Vector3(35.25f, 0, -22.875f), new Vector3(33.0f, 0, -24.75f), new Vector3(31.125f, 0, -25.125f)};
        province_1_provincegen.state_color = new Color(254, 210, 35);
        province_1_provincegen.productions = 100;
        province_1_provincegen.education = 100;
        province_1_provincegen.army= 10;
        province_1_provincegen.natural_resources= 100;
        province_1_provincegen.separatism= 50;
        province_1_provincegen.climate = 100;
        province_1_provincegen.sea = 0;
        province_1_provincegen.defensive_ability = 100;

        province_2 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_2.name = "province_2";
        province_2_provincegen = province_2.GetComponent<provincegen>();
        province_2_provincegen.name = "gfd";
        province_2_provincegen.dots = new List<Vector3> {new Vector3(6.625f, 0, 1.625f), new Vector3(-0.375f, 0, 1.125f), new Vector3(-8.375f, 0, 3.875f), new Vector3(-9.75f, 0, 12.5f), new Vector3(-8.25f, 0, 16.375f), new Vector3(-4.375f, 0, 22.0f), new Vector3(1.875f, 0, 24.375f), new Vector3(8.5f, 0, 24.5f), new Vector3(16.625f, 0, 21.5f), new Vector3(22.5f, 0, 18.5f), new Vector3(22.0f, 0, 11.625f), new Vector3(20.25f, 0, 5.625f), new Vector3(18.375f, 0, 1.5f)};
        province_2_provincegen.state_color = new Color(108, 113, 75);
        province_2_provincegen.productions = 100;
        province_2_provincegen.education = 100;
        province_2_provincegen.army= 10;
        province_2_provincegen.natural_resources= 100;
        province_2_provincegen.separatism= 50;
        province_2_provincegen.climate = 100;
        province_2_provincegen.sea = 0;
        province_2_provincegen.defensive_ability = 100;

        province_3 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_3.name = "province_3";
        province_3_provincegen = province_3.GetComponent<provincegen>();
        province_3_provincegen.name = "hgf";
        province_3_provincegen.dots = new List<Vector3> {new Vector3(-13.625f, 0, -16.625f), new Vector3(-19.75f, 0, -15.625f), new Vector3(-29.125f, 0, -11.875f), new Vector3(-30.125f, 0, -4.375f), new Vector3(-27.875f, 0, 1.375f), new Vector3(-17.75f, 0, 4.25f), new Vector3(-9.875f, 0, -4.875f), new Vector3(-12.0f, 0, -15.5f)};
        province_3_provincegen.state_color = new Color(254, 210, 35);
        province_3_provincegen.productions = 100;
        province_3_provincegen.education = 100;
        province_3_provincegen.army= 10;
        province_3_provincegen.natural_resources= 100;
        province_3_provincegen.separatism= 50;
        province_3_provincegen.climate = 100;
        province_3_provincegen.sea = 0;
        province_3_provincegen.defensive_ability = 100;
    }
}