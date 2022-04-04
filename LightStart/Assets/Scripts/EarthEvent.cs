using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEvent : CutsceneEvent
{
    [SerializeField]
    private GameObject objectToActivate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void startEvent() 
    {
        objectToActivate.SetActive(true);
    }
}
