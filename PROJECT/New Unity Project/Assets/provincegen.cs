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


    private List<Vector3> triangulation_result;
    void Start()
    {
        triangulation triangulation = new triangulation();

        Debug.Log("I`ve started");
        Vector3[] verticles;
        int[] triangles;
        Vector2[] uvcoords;
        triangulation.GetResult(dots, true, new  Vector3(0, -1, 0), out verticles, out triangles, out uvcoords);
        foreach(Vector3 verticle in verticles) { Debug.Log(verticle); }
        Construct(verticles, triangles);
    }

    void Construct(Vector3[] verticles, int[] triangles)
    {
        List<int> list_triangles = new List<int>();
        int original_dots_quantity = dots.Count;
        int original_verticles_quantity = verticles.Length;

        // Debug.Log(original_dots_quantity);
        // Debug.Log(original_verticles_quantity);
        List<Vector3> verticles_modified = new List<Vector3>();

        for (int i = 0; i < original_verticles_quantity; i += 3) { verticles_modified.Add(verticles[i + 2]); verticles_modified.Add(verticles[i + 1]); verticles_modified.Add(verticles[i]); }
        for (int i = 0; i < original_dots_quantity; i++)
        {
            Vector3 original_dot = dots[i];
            verticles_modified.Add(original_dot);
            verticles_modified.Add(BottomDot(original_dot));
            if (i == 0) verticles_modified.Add(dots[original_dots_quantity - 1]);
            else verticles_modified.Add(dots[i - 1]);
            verticles_modified.Add(BottomDot(original_dot));
            verticles_modified.Add(original_dot);
            if (i == original_dots_quantity - 1) verticles_modified.Add(BottomDot(dots[0]));
            else verticles_modified.Add(BottomDot(dots[i + 1]));
        }
        verticles = verticles_modified.ToArray();
        Array.Resize(ref triangles, verticles.Length);
        for (int i = 0; i < triangles.Length; i++) triangles[i] = i;
        
        foreach (Vector3 dot in verticles) { Debug.Log(dot);} foreach (int dot in triangles) { Debug.Log(dot); }
        Mesh mesh = new Mesh();
        mesh.vertices = verticles; mesh.triangles = triangles;
        MeshFilter meshfilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshfilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Unlit/Color"));
        meshRenderer.material.color = state_color;
    }

    Vector3 BottomDot(Vector3 original_dot)
    {
        return new Vector3(original_dot.x, original_dot.y - y_size, original_dot.z);
    }
}
