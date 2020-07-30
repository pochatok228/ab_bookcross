using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class provincegen : MonoBehaviour
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

    public Color state_color;
    public Color current_color;
    private float y_size = 0.5f;

    public Vector3[] verticles;
    public int[] triangles;


    private List<Vector3> triangulation_result;
    void Start()
    {
        triangulation triangulation = new triangulation();

        Debug.Log("I`ve started");
       
        foreach(Vector3 verticle in verticles) { Debug.Log(verticle); }
        Construct(verticles, triangles);
    }

    void Construct(Vector3[] verticles, int[] triangles)
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
        foreach (Vector3 dot in verticles) { Debug.Log(dot);} foreach (int dot in triangles) { Debug.Log(dot); }
        Mesh mesh = new Mesh();
        mesh.vertices = list_of_verticles.ToArray(); mesh.triangles = list_of_triangles.ToArray();
        MeshFilter meshfilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshfilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Legacy Shaders/Self-Illumin/Bumped Diffuse"));
        meshRenderer.material.SetColor("Main Color", state_color);
        Debug.Log(meshRenderer.material.color);
    }

    Vector3 BottomDot(Vector3 original_dot)
    {
        return new Vector3(original_dot.x, original_dot.y - y_size, original_dot.z);
    }
}
