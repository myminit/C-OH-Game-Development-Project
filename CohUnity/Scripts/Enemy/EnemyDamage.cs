using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private Health health;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            health.TakeDamage(_damage);
            new WaitForSeconds(5000f);
        }
    }
}
