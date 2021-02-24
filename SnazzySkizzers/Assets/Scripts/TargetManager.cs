using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject[] targets;
    bool tracker = false;
    Animator pantsAnimation;
    

    // Start is called before the first frame update
    void Awake()
    {
        targets = GameObject.FindGameObjectsWithTag("Target");
        pantsAnimation = GameObject.FindWithTag("Clothing").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].GetComponent<SkizzorsCutPoint>().crossCut != true)
            {
                tracker = false;
                Debug.Log("Tracker is false");
            }
            else
            {
                tracker = true;
                Debug.Log("Tracker is true");
            }
        }

        if (tracker == true)
        {
            pantsAnimation.SetTrigger("AllCrossesCut");
        }
    }  
}
