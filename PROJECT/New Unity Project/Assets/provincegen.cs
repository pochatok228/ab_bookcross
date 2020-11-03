using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;




public class provincegen : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    public List<Vector3> dots;
    public string province_name;
    public int population;
    public int productions;
    public int education;
    public int army;
    public int natural_resources;
    public int separatism;
    public int climate;
    public int sea;
    public int defensive_ability;
    public GameObject state;
    public List<GameObject> connections = new List<GameObject>();
    public Vector3 capital_coords;

    public Color state_color;
    private Color current_color = Color.white;
    private intendant intendant;

    private float y_size = 0.1f;

    public Vector3[] verticles;
    public int[] triangles;

    private Material province_material;
    private bool moveup; private bool movedown; private bool finished_up;
    public bool selected;
    private GameObject capital;

    public int Libraries = 0;
    public int Farms = 0;
    public int Factories = 0;
    public int Fortresses = 0;



    void Start() // Manager
    {
        intendant = GameObject.Find("Intendant").GetComponent<intendant>();
        intendant.AddProvince(gameObject);
        // Construct(verticles, triangles);
        GameObject province_capital = GameObject.Find(String.Format("{0}/Capital", gameObject.name));
        province_capital.transform.position = capital_coords;
        capital = GameObject.Find(String.Format("{0}/Capital", gameObject.name));
    }


    public void Update()
    {
        
        if (moveup && gameObject.transform.position.y <= 2f)
        {
            gameObject.transform.position += new Vector3(0, 0.1f, 0);
        }
        if (gameObject.transform.position.y >= 2f)
        {
            moveup = false;
            finished_up = true;
        }
        
        if (movedown && gameObject.transform.position.y > 0 && finished_up && !selected)
        {
            gameObject.transform.position += new Vector3(0, -0.1f, 0);
        }
    }

    public void SetStateColor()
    {
        try
        {
            Color state_color = state.GetComponent<stategen>().state_color;
            ChangeColor(state_color);        }
        catch(Exception)
        {
            ChangeColor(Color.white);
        }
    }
    public void ShowArmyOnTextField() { SetTextFieldValue(Convert.ToString(army)); }


    public void OnPointerEnter(PointerEventData eventData) // Executor
    {
        if (intendant.GetMode() == intendant.CHOISE_MODE)
        {
            intendant.Alert(province_name);
        }
        finished_up = false;
        moveup = true;
        movedown = false;
    }
    public void OnPointerExit(PointerEventData eventData) // Executor
    {
        movedown = true;
        intendant.AlertDefault();
    }
    public void OnPointerClick(PointerEventData eventdata) // Executor
    {
        if (intendant.GetMode() == intendant.CHOISE_MODE)
        {
            try
            {
                intendant.Alert(province_name);
                intendant.ProtagonistState = state;
                intendant.EnterPoliticalCoords();
                foreach (GameObject slider in GameObject.FindGameObjectsWithTag("Slider"))
                {
                    slider.GetComponent<SliderScript>().setState(state);
                }
            }
            catch (Exception)
            {
                intendant.Alert("It is neutral province");
                intendant.EnterPoliticalCoordsCancel();
            }
        }
        else if (intendant.GetMode() == intendant.CONSTRUCION_MODE  && state == intendant.ProtagonistState)
        {
            intendant.SelectProvince(gameObject);
            intendant.OpenMenu(intendant.ConstructionMenu);
            
        }
        else if (intendant.GetMode() == intendant.ARMY_MODE && state == intendant.ProtagonistState)
        {
            intendant.SelectProvince(gameObject);
            intendant.OpenMenu(intendant.ArmyMenu);
        }

        // Debug.Log(province_name + " " + state.GetComponent<stategen>().state_name);
    }
    public void Construct() // Executor
    {
        
        int original_verticles_quantity = verticles.Length; int original_triangles_quantity = triangles.Length;
        List<Vector3> list_of_verticles = new List<Vector3>();
        List<int> list_of_triangles = new List<int>();

        for (int i = 0; i < original_verticles_quantity; i++) list_of_verticles.Add(verticles[i]); // добавляем все вершины
        for (int i = 0; i < original_triangles_quantity; i++) list_of_triangles.Add(triangles[i]); // добавляем все треугольники 
        for (int i = 0; i < original_verticles_quantity; i++) list_of_verticles.Add(BottomDot(verticles[i])); // добавляем нижние точки


        for (int i = 0; i < original_verticles_quantity; i++) // добавляем по два треуголника на угловую вертикаль
        {
            list_of_triangles.Add(i);
            
            if (i == 0) list_of_triangles.Add(original_verticles_quantity * 2 - 1);
            else list_of_triangles.Add(i + original_verticles_quantity - 1);
            list_of_triangles.Add(i + original_verticles_quantity);
            list_of_triangles.Add(i + original_verticles_quantity);
            if (i == original_verticles_quantity - 1) list_of_triangles.Add(0);
            else list_of_triangles.Add(i + 1);
            list_of_triangles.Add(i);

        }

        // Мэш-генерация
        // foreach (Vector3 dot in verticles) { Debug.Log(dot);} foreach (int dot in triangles) { Debug.Log(dot); }
        Mesh mesh = new Mesh();
        mesh.vertices = list_of_verticles.ToArray(); mesh.triangles = list_of_triangles.ToArray();
        MeshFilter meshfilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshfilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard"));
        province_material = meshRenderer.material;
        ChangeColor(Color.white);

        MeshCollider mesh_collider = gameObject.GetComponent<MeshCollider>();
        mesh_collider.sharedMesh = mesh;


        // if (sea != 0) current_color = Color.white;
        // ChangeColor(current_color)
        // province_material.color = current_color;
        // First you want to use the API with multiple materials
        // where 3 should be the index you want to use...
        // var materials = GetComponent<Renderer>().materials;
        // materials[0].SetColor("_EmissionColor", current_color);
        // materials[0].EnableKeyword("_EMISSION");
    } // Executor

    public void SetTextFieldValue(string Text) // Exectuor
    {
        GameObject text_field = GameObject.Find(String.Format("{0}/Capital/TextField", gameObject.name));
        text_field.GetComponent<TMPro.TextMeshPro>().SetText(Text);
        GameObject text_background = GameObject.Find(String.Format("{0}/Capital/TextField/TextBackground", gameObject.name));

        TextBackgroundHeightUpdate upcomponent = text_background.GetComponent<TextBackgroundHeightUpdate>();
        upcomponent.UpdateHeight(Text.Length);
    }
    public void ChangeColor(Color new_color) // Executor
    {
        if (new_color != Color.white)
        {
            float h, s, v;
            Color.RGBToHSV(new_color, out h, out s, out v);
            s = 0.5f; v = 1f; new_color = Color.HSVToRGB(h, s, v);
            // Debug.Log(String.Format("provincegen {0}, {1}, {2}", h, s, v));
        }
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        province_material = meshRenderer.materials[0];
        province_material.color = new_color;
        province_material.SetColor("_EmissionColor", new_color);
        province_material.EnableKeyword("_EMISSION");
    }

    public void SetStrictColor(Color new_color)
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        province_material = meshRenderer.materials[0];
        province_material.color = new_color;
        province_material.SetColor("_EmissionColor", new_color);
        province_material.EnableKeyword("_EMISSION");
    }

    public void SetState(GameObject new_state) { state = new_state; state_color = state.GetComponent<stategen>().state_color; } // Executor
    public string GetStateName()
    {
        try
        {
            return state.name;
        }
        catch(UnassignedReferenceException)
        {
            return "None";
        }
    }

    public void AddConnection(int province_id) // Executor
    {
        connections.Add(GameObject.Find("province_" + Convert.ToString(province_id)));
    }

    public int GetConstructionsQuantity(){return Libraries + Factories + Farms + Fortresses;}

    // вписать сюда сепаратизм 
    public int GetCivilianTax()
    {
        return (int)(population * intendant.ProtagonistState.GetComponent<stategen>().CivilianTax * (1 + (float)education / 100f) * (1 + climate / 100));
    }
    
    public int GetProductionTax()
    {
        return (int)(productions * intendant.ProtagonistState.GetComponent<stategen>().ProductionTax * (1 + (float)education / 100f) * (1 + (float)natural_resources / 100f));
    }

    Vector3 BottomDot(Vector3 original_dot) // helper
    {
        return new Vector3(original_dot.x, original_dot.y - y_size, original_dot.z);
    }
}
