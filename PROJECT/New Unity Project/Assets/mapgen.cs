using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapgen : MonoBehaviour
{
    public GameObject province_template;
    private GameObject province_0;
    private provincegen province_0_provincegen;
    private GameObject province_1;
    private provincegen province_1_provincegen;
    private GameObject province_2;
    private provincegen province_2_provincegen;
    private GameObject province_3;
    private provincegen province_3_provincegen;
    private GameObject province_4;
    private provincegen province_4_provincegen;

    void Start()
    {
        province_0 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_0.name = "province_0";
        province_0_provincegen = province_0.GetComponent<provincegen>();
        province_0_provincegen.name = "";
        province_0_provincegen.dots = new List<Vector3> { new Vector3(-15.0f, 0, -20.875f), new Vector3(-14.375f, 0, -20.0f), new Vector3(-10.5f, 0, -18.0f), new Vector3(-7.0f, 0, -18.125f), new Vector3(-3.375f, 0, -18.75f), new Vector3(1.625f, 0, -19.875f), new Vector3(2.625f, 0, -20.0f), new Vector3(3.875f, 0, -20.125f), new Vector3(4.5f, 0, -20.25f), new Vector3(5.125f, 0, -20.375f), new Vector3(7.25f, 0, -22.5f), new Vector3(6.5f, 0, -23.375f), new Vector3(5.25f, 0, -25.625f), new Vector3(2.5f, 0, -27.875f), new Vector3(0.625f, 0, -29.75f), new Vector3(-1.125f, 0, -31.0f), new Vector3(-1.625f, 0, -31.625f), new Vector3(-3.25f, 0, -33.875f), new Vector3(-5.875f, 0, -36.0f), new Vector3(-7.375f, 0, -36.875f), new Vector3(-9.375f, 0, -37.625f), new Vector3(-12.0f, 0, -38.5f), new Vector3(-17.25f, 0, -38.5f), new Vector3(-19.625f, 0, -36.75f), new Vector3(-18.75f, 0, -31.75f), new Vector3(-18.375f, 0, -28.625f), new Vector3(-19.875f, 0, -25.0f), new Vector3(-18.875f, 0, -21.375f), new Vector3(-16.375f, 0, -20.75f) };
        province_0_provincegen.state_color = new Color(12, 37, 67);
        province_0_provincegen.productions = 146;
        province_0_provincegen.education = 212;
        province_0_provincegen.army = 10;
        province_0_provincegen.natural_resources = 100;
        province_0_provincegen.separatism = 133;
        province_0_provincegen.climate = 100;
        province_0_provincegen.sea = 10;
        province_0_provincegen.defensive_ability = 121;

        province_1 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_1.name = "province_1";
        province_1_provincegen = province_1.GetComponent<provincegen>();
        province_1_provincegen.name = "";
        province_1_provincegen.dots = new List<Vector3> { new Vector3(-18.375f, 0, -17.75f), new Vector3(-18.375f, 0, -13.75f), new Vector3(-18.375f, 0, -11.125f), new Vector3(-17.875f, 0, -7.75f), new Vector3(-17.625f, 0, -4.875f), new Vector3(-17.125f, 0, -2.5f), new Vector3(-16.25f, 0, -0.875f), new Vector3(-15.75f, 0, 1.75f), new Vector3(-14.75f, 0, 3.375f), new Vector3(-15.125f, 0, 5.0f), new Vector3(-14.875f, 0, 8.375f), new Vector3(-14.125f, 0, 9.625f), new Vector3(-5.75f, 0, 11.625f), new Vector3(-3.5f, 0, 11.125f), new Vector3(1.375f, 0, 9.375f), new Vector3(4.375f, 0, 8.625f), new Vector3(5.5f, 0, 7.0f), new Vector3(5.25f, 0, 2.0f), new Vector3(4.75f, 0, -3.0f), new Vector3(5.125f, 0, -10.375f), new Vector3(4.875f, 0, -14.375f), new Vector3(4.875f, 0, -17.25f), new Vector3(4.0f, 0, -17.5f), new Vector3(-2.875f, 0, -15.125f), new Vector3(-7.875f, 0, -14.75f), new Vector3(-12.0f, 0, -16.0f) };
        province_1_provincegen.state_color = new Color(12, 37, 67);
        province_1_provincegen.productions = 100;
        province_1_provincegen.education = 100;
        province_1_provincegen.army = 10;
        province_1_provincegen.natural_resources = 100;
        province_1_provincegen.separatism = 50;
        province_1_provincegen.climate = 100;
        province_1_provincegen.sea = 0;
        province_1_provincegen.defensive_ability = 212;

        province_2 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_2.name = "province_2";
        province_2_provincegen = province_2.GetComponent<provincegen>();
        province_2_provincegen.name = "";
        province_2_provincegen.dots = new List<Vector3> { new Vector3(-14.5f, 0, 13.125f), new Vector3(-15.5f, 0, 14.75f), new Vector3(-15.25f, 0, 19.5f), new Vector3(-15.0f, 0, 22.5f), new Vector3(-15.0f, 0, 28.875f), new Vector3(-14.875f, 0, 31.75f), new Vector3(-13.125f, 0, 33.75f), new Vector3(-11.5f, 0, 35.125f), new Vector3(-9.75f, 0, 36.25f), new Vector3(-6.75f, 0, 36.625f), new Vector3(-2.125f, 0, 36.25f), new Vector3(0.5f, 0, 36.0f), new Vector3(3.375f, 0, 35.375f), new Vector3(7.75f, 0, 30.875f), new Vector3(9.875f, 0, 26.375f), new Vector3(8.875f, 0, 21.625f), new Vector3(6.75f, 0, 17.625f), new Vector3(5.75f, 0, 14.25f), new Vector3(5.25f, 0, 12.625f), new Vector3(-0.625f, 0, 13.75f), new Vector3(-4.75f, 0, 15.125f) };
        province_2_provincegen.state_color = new Color(12, 37, 67);
        province_2_provincegen.productions = 100;
        province_2_provincegen.education = 100;
        province_2_provincegen.army = 10;
        province_2_provincegen.natural_resources = 100;
        province_2_provincegen.separatism = 50;
        province_2_provincegen.climate = 133;
        province_2_provincegen.sea = 0;
        province_2_provincegen.defensive_ability = 256;

        province_3 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_3.name = "province_3";
        province_3_provincegen = province_3.GetComponent<provincegen>();
        province_3_provincegen.name = "";
        province_3_provincegen.dots = new List<Vector3> { new Vector3(-17.375f, 0, 27.75f), new Vector3(-18.375f, 0, 25.25f), new Vector3(-20.375f, 0, 21.625f), new Vector3(-28.75f, 0, 20.875f), new Vector3(-38.625f, 0, 23.25f), new Vector3(-44.25f, 0, 28.5f), new Vector3(-41.875f, 0, 33.375f), new Vector3(-40.0f, 0, 37.375f), new Vector3(-38.625f, 0, 39.875f), new Vector3(-35.125f, 0, 42.875f), new Vector3(-28.0f, 0, 44.125f), new Vector3(-24.875f, 0, 43.125f), new Vector3(-21.5f, 0, 43.125f), new Vector3(-14.0f, 0, 42.0f), new Vector3(-16.375f, 0, 32.625f) };
        province_3_provincegen.state_color = new Color(12, 37, 67);
        province_3_provincegen.productions = 160;
        province_3_provincegen.education = 100;
        province_3_provincegen.army = 60;
        province_3_provincegen.natural_resources = 176;
        province_3_provincegen.separatism = 50;
        province_3_provincegen.climate = 160;
        province_3_provincegen.sea = 0;
        province_3_provincegen.defensive_ability = 100;

        province_4 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_4.name = "province_4";
        province_4_provincegen = province_4.GetComponent<provincegen>();
        province_4_provincegen.name = "";
        province_4_provincegen.dots = new List<Vector3> { new Vector3(30.5f, 0, 19.5f), new Vector3(22.5f, 0, 19.5f), new Vector3(16.25f, 0, 22.625f), new Vector3(12.625f, 0, 26.125f), new Vector3(11.125f, 0, 30.375f), new Vector3(9.625f, 0, 34.875f), new Vector3(10.625f, 0, 38.0f), new Vector3(15.0f, 0, 41.625f), new Vector3(19.5f, 0, 43.125f), new Vector3(30.375f, 0, 44.625f), new Vector3(35.625f, 0, 44.0f), new Vector3(41.875f, 0, 38.375f), new Vector3(42.0f, 0, 26.5f), new Vector3(40.75f, 0, 21.875f) };
        province_4_provincegen.state_color = new Color(12, 37, 67);
        province_4_provincegen.productions = 146;
        province_4_provincegen.education = 100;
        province_4_provincegen.army = 60;
        province_4_provincegen.natural_resources = 176;
        province_4_provincegen.separatism = 50;
        province_4_provincegen.climate = 160;
        province_4_provincegen.sea = 0;
        province_4_provincegen.defensive_ability = 100;

    }
}