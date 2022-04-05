using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision other)
    {
        //if (other.gameObject.layer == 7)
            LevelManager.NextLevel();
    }
}
