using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage : MonoBehaviour
{
    public static int imageNum;
    public enum LoadAPI
    {
        AssetDataBase,
        Resource,
        WWW,
        UnityWebRequest 
    }

    public enum LoadMethod
    {
        Synchronous,
        Asynchronous
    }
    public LoadAPI LoadApi;
    public LoadMethod loadMethod;
    
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
