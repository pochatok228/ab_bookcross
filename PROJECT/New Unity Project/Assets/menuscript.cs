using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class menuscript : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        string[] probablymaps = Directory.GetFiles("Assets/Scenes");
        // foreach (string map in maps) {Debug.Log(map);}
        List<string> maps = new List<string>();
        foreach (string map in probablymaps)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
