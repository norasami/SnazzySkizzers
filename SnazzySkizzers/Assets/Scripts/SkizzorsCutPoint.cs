using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkizzorsCutPoint : MonoBehaviour
{
    public bool CuttingTime;
    public Animator crossAnim;
    public bool crossCut = false;

    // Start is called before the first frame update
    void Awake()
    {
        crossAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CuttingTime = true;
        }
    }
}
