using System;
using Unity.Mathematics;
using UnityEngine;

public class HealingEffect : MonoBehaviour
{
    public Vector3 RotationSpeed;
    public float HealValue;
    public bool UseToHeal;
    
    private float xRotation;
    private float yRotation;
    private float zRotation;

    private void Update()
    {
        xRotation += RotationSpeed.x;
        yRotation += RotationSpeed.y;
        zRotation += RotationSpeed.z;
        
        transform.rotation = Quaternion.Euler(new Vector3(xRotation, yRotation, zRotation));
    }

    private void OnCollisionEnter(Collision other)
    { 
        if (!other.transform.CompareTag("Player") || !UseToHeal) return;
        other.transform.GetComponent<Hero>().GetHeal(HealValue);
        Dispose();
    }

    private void Dispose()
    {
        Destroy(gameObject);
    }
}
