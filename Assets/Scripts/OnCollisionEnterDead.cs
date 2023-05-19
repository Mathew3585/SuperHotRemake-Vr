using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEnterDead : MonoBehaviour
{
    public string TargetTag;
    public Enemy enemy;

    // Start is called before the first frame update
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == TargetTag)
        {
            enemy.Dead(collision.contacts[0].point);
        }

    }
}
