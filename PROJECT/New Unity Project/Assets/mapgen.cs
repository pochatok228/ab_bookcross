using System.Collections; using System.Collections.Generic; using UnityEngine;

public class mapgen : MonoBehaviour {
    public GameObject province_template;
    private GameObject province_0;
    private provincegen province_0_provincegen;
    private GameObject province_1;
    private provincegen province_1_provincegen;

    void Start()
    {
        province_0 = Instantiate(province_template, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        province_0.name = "province_0";
        province_0_provincegen = province_0.GetComponent<provincegen>();
        province_0_provincegen.name = "";
        province_0_provincegen.verticles = new Vector3[] {new Vector3(26.9375f, 0, -28.1875f), new Vector3(26.8125f, 0, -28.1875f), new Vector3(26.4375f, 0, -28.0f), new Vector3(26.0f, 0, -27.8125f), new Vector3(25.75f, 0, -27.4375f), new Vector3(25.75f, 0, -26.9375f), new Vector3(25.625f, 0, -26.4375f), new Vector3(25.9375f, 0, -26.1875f), new Vector3(26.125f, 0, -26.1875f), new Vector3(26.75f, 0, -26.125f), new Vector3(27.0f, 0, -26.125f), new Vector3(27.3125f, 0, -26.625f), new Vector3(27.625f, 0, -26.75f), new Vector3(28.0625f, 0, -26.6875f), new Vector3(28.0625f, 0, -26.8125f), new Vector3(28.1875f, 0, -27.1875f), new Vector3(28.5f, 0, -27.375f), new Vector3(28.6875f, 0, -27.75f), new Vector3(28.75f, 0, -28.0f), new Vector3(28.375f, 0, -28.25f), new Vector3(28.375f, 0, -28.5f), new Vector3(28.125f, 0, -28.625f), new Vector3(28.0f, 0, -28.75f), new Vector3(27.6875f, 0, -28.75f), new Vector3(27.5f, 0, -28.875f), new Vector3(27.0625f, 0, -28.875f)};
		province_0_provincegen.triangles = new int[] {0, 1, 2, 0, 2, 3, 0, 3, 4, 0, 4, 5, 0, 5, 6, 0, 6, 7, 0, 7, 8, 0, 8, 9, 25, 0, 9, 25, 9, 10, 25, 10, 11, 25, 11, 12, 25, 12, 13, 25, 13, 14, 25, 14, 15, 25, 15, 16, 25, 16, 17, 25, 17, 18, 25, 18, 19, 25, 19, 20, 25, 20, 21, 21, 22, 23, 25, 21, 23, 23, 24, 25};
        province_0_provincegen.state_color = new Color(253, 105, 184);
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
        province_1_provincegen.name = "dsa";
        province_1_provincegen.verticles = new Vector3[] {new Vector3(24.3125f, 0, -27.375f), new Vector3(23.8125f, 0, -27.5625f), new Vector3(23.5f, 0, -27.25f), new Vector3(23.1875f, 0, -26.875f), new Vector3(23.0f, 0, -26.1875f), new Vector3(23.25f, 0, -26.125f), new Vector3(23.25f, 0, -25.875f), new Vector3(23.375f, 0, -25.375f), new Vector3(23.5625f, 0, -25.0625f), new Vector3(23.625f, 0, -25.1875f), new Vector3(24.125f, 0, -25.0f), new Vector3(24.4375f, 0, -24.8125f), new Vector3(24.375f, 0, -24.3125f), new Vector3(24.6875f, 0, -24.0625f), new Vector3(24.9375f, 0, -23.8125f), new Vector3(25.125f, 0, -23.8125f), new Vector3(25.375f, 0, -23.8125f), new Vector3(25.6875f, 0, -23.9375f), new Vector3(25.6875f, 0, -24.3125f), new Vector3(25.9375f, 0, -24.4375f), new Vector3(26.25f, 0, -24.5f), new Vector3(26.8125f, 0, -24.4375f), new Vector3(27.25f, 0, -24.1875f), new Vector3(27.625f, 0, -23.9375f), new Vector3(27.875f, 0, -24.4375f), new Vector3(28.1875f, 0, -24.5f), new Vector3(28.75f, 0, -24.625f), new Vector3(29.0625f, 0, -24.625f), new Vector3(29.1875f, 0, -24.1875f), new Vector3(29.5625f, 0, -24.0625f), new Vector3(29.875f, 0, -24.0625f), new Vector3(29.875f, 0, -23.875f), new Vector3(30.0f, 0, -23.5625f), new Vector3(30.125f, 0, -23.3125f), new Vector3(30.25f, 0, -23.125f), new Vector3(30.5f, 0, -23.125f), new Vector3(30.8125f, 0, -23.125f), new Vector3(30.9375f, 0, -23.1875f), new Vector3(31.0625f, 0, -23.25f), new Vector3(30.875f, 0, -23.3125f), new Vector3(30.6875f, 0, -23.6875f), new Vector3(30.75f, 0, -24.0f), new Vector3(30.5625f, 0, -24.625f), new Vector3(30.4375f, 0, -24.75f), new Vector3(30.125f, 0, -25.0f), new Vector3(29.875f, 0, -25.0f), new Vector3(29.6875f, 0, -25.1875f), new Vector3(29.5f, 0, -25.375f), new Vector3(29.4375f, 0, -25.4375f), new Vector3(29.1875f, 0, -25.625f), new Vector3(29.0625f, 0, -25.625f), new Vector3(28.875f, 0, -25.625f), new Vector3(28.6875f, 0, -25.75f), new Vector3(28.625f, 0, -26.125f), new Vector3(28.375f, 0, -26.375f), new Vector3(28.25f, 0, -26.375f), new Vector3(27.875f, 0, -26.3125f), new Vector3(27.5f, 0, -26.25f), new Vector3(27.125f, 0, -26.0f), new Vector3(26.9375f, 0, -25.75f), new Vector3(26.75f, 0, -25.6875f), new Vector3(26.4375f, 0, -25.75f), new Vector3(26.1875f, 0, -25.875f), new Vector3(25.8125f, 0, -25.6875f), new Vector3(25.375f, 0, -25.875f), new Vector3(25.25f, 0, -26.1875f), new Vector3(25.3125f, 0, -26.875f), new Vector3(25.375f, 0, -27.625f)};
		province_1_provincegen.triangles = new int[] {0, 1, 2, 0, 2, 3, 67, 0, 3, 67, 3, 4, 67, 4, 5, 67, 5, 6, 67, 6, 7, 7, 8, 9, 67, 7, 9, 67, 9, 10, 67, 10, 11, 67, 11, 12, 67, 12, 13, 67, 13, 14, 14, 15, 16, 14, 16, 17, 14, 17, 18, 22, 23, 24, 21, 22, 24, 20, 21, 24, 20, 24, 25, 20, 25, 26, 27, 28, 29, 27, 29, 30, 30, 31, 32, 30, 32, 33, 30, 33, 34, 30, 34, 35, 30, 35, 36, 30, 36, 37, 37, 38, 39, 30, 37, 39, 30, 39, 40, 27, 30, 40, 27, 40, 41, 27, 41, 42, 26, 27, 42, 26, 42, 43, 20, 26, 43, 20, 43, 44, 20, 44, 45, 20, 45, 46, 19, 20, 46, 19, 46, 47, 19, 47, 48, 19, 48, 49, 19, 49, 50, 19, 50, 51, 19, 51, 52, 19, 52, 53, 18, 19, 53, 18, 53, 54, 14, 18, 54, 14, 54, 55, 14, 55, 56, 14, 56, 57, 57, 58, 59, 14, 57, 59, 14, 59, 60, 14, 60, 61, 14, 61, 62, 14, 62, 63, 14, 63, 64, 14, 64, 65, 67, 14, 65, 65, 66, 67};
        province_1_provincegen.state_color = new Color(253, 163, 213);
        province_1_provincegen.productions = 100;
        province_1_provincegen.education = 100;
        province_1_provincegen.army= 10;
        province_1_provincegen.natural_resources= 100;
        province_1_provincegen.separatism= 50;
        province_1_provincegen.climate = 100;
        province_1_provincegen.sea = 0;
        province_1_provincegen.defensive_ability = 100;

    }
}