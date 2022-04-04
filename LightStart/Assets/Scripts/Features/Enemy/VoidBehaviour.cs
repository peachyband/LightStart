using UnityEngine;

public class VoidBehaviour : MonoBehaviour
{
    
    private void OnCollisionStay(Collision other)
    {
        var hero = other.transform.GetComponent<Hero>();
        hero.lifecycle.Death();
    }
}
