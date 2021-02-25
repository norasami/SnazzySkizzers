using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject[] targets;
    
    
    

    // Start is called before the first frame update
    void Awake()
    {
        targets = GameObject.FindGameObjectsWithTag("Target");
    }

    // Update is called once per frame
    void Update()
    {
        
    }  
}
