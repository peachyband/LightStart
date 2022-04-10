using UnityEngine;

public class VoidBehaviour : MonoBehaviour
{

    private void OnCollisionStay(Collision other)
    {
        if (!other.transform.CompareTag("Player")) return;
        var hero = other.transform.GetComponent<Hero>();
        hero.GetDamage(20f);
    }
}
