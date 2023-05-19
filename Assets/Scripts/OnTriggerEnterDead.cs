using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnterDead : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        OnCollisionEnterDead death = GetComponent<OnCollisionEnterDead>();

        if(death != null)
        {
            death.enemy.Dead(transform.position);
        }
    }
}
