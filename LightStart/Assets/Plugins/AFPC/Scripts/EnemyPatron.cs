using System;
using UnityEngine;

namespace Features.Enemy
{
    public class EnemyPatron : MonoBehaviour
    {
        public float Damage = 20;
        private float _timeTillLife = 20f;
        private float _lifeSpan;
        private void OnCollisionEnter(Collision other)
        {
            if (!other.transform.CompareTag("Player")) return;
            other.transform.GetComponent<Hero>().GetDamage(Damage);
            Dispose();
        }

        private void Update()
        {
            _lifeSpan += Time.deltaTime;
            if (_lifeSpan >= _timeTillLife)
                Dispose();
        }

        private void Dispose()
        {
            Destroy(gameObject);
        }
    }
}