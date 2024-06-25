using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collision detected with: " + collider.name);
        FlyingEnemy enemy = collider.GetComponent<FlyingEnemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Debug.Log("Damaged: " + collider.name + " for " + damage + " damage.");
        }
        else
        {
            Debug.Log("No FlyingEnemy component found on: " + collider.name);
        }
    }
}
