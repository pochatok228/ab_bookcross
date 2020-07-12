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


    private List<Vector3> triangulation_result;
    void Start()
    {
        Debug.Log("I`ve started");
        Vector3[] verticles;
        int[] triangles;
        Vector2[] uvcoords;
        Triangulation.GetResult(dots, true, Vector3.up, out verticles, out triangles, out uvcoords);
        // for (int i = 0; i < triangles.Length; i++) Debug.Log(Convert.ToString(triangles[i]));
        // for (int i = 0; i < verticles.Length; i++) Debug.Log(Convert.ToString(verticles[i]));
        Mesh mesh = new Mesh();
        mesh.vertices = verticles; mesh.triangles = triangles;



        MeshFilter meshfilter = GameObject.Find(province_name).AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = GameObject.Find(province_name).AddComponent<MeshRenderer>();
        // РЕШИТЬ ПРОБЛЕМУ ОПРЕДЕЛЕНИЯ ОБЪЕКТА ЧЕРЕЗ КОМПОНЕНТ

    }
}
