using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Transform PlayerTraget;
    public Transform PlayerHead;
    public float StopDistance;
    public FireBulletOnActivate gun;
    public bool Ialive = true;
    public bool udpateManger = false;

    private Quaternion localRotationGun;
    private GameManager gameManager;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        PlayerTraget = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerHead = GameObject.FindGameObjectWithTag("MainCamera").transform;
        gameManager = FindObjectOfType<GameManager>();
        udpateManger = false;
        Ialive = true;
    }
    void Start()
    {

        SetupRagdoll();
        localRotationGun = gun.spawnPoint.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(PlayerTraget.position);
        
        float distance = Vector3.Distance(PlayerTraget.position, transform.position);
        if(distance < StopDistance)
        {
            agent.isStopped= true;
            animator.SetBool("Shoot", true);
        }
        if(Ialive && udpateManger)
        {
            gameManager.currentEnemyLife--;
            Ialive = false;
        }
    }

    public void ThrowGun()
    {
        gun.spawnPoint.localRotation = localRotationGun;
        gun.transform.parent = null;
        Rigidbody rb = gun.GetComponent<Rigidbody>();
        rb.velocity = BallisticVelocityVector(gun.transform.position, PlayerHead.position, 45);
        rb.angularVelocity = Vector3.zero;
        rb.mass = 1;

    }
    


    Vector3 BallisticVelocityVector(Vector3 source, Vector3 target, float angle)
    {
        Vector3 direction = target - source;
        float h = direction.y;
        direction.y = 0;
        float distance = direction.magnitude;
        float a = angle * Mathf.Deg2Rad;
        direction.y = distance * Mathf.Tan(a);
        distance += h / Mathf.Tan(a);

        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return velocity * direction.normalized;

    }
    public void ShootEnemy()
    {
        Vector3 PlayerHeadPostion = PlayerHead.position - Random.Range(0, 0.4f) * Vector3.up;

        gun.spawnPoint.forward = (PlayerHeadPostion - gun.spawnPoint.position).normalized;

        gun.FireBullet();
    }

    public void SetupRagdoll()
    {
        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            item.isKinematic= true;
        }
    }

    public void Dead(Vector3 hitpostion)
    {


        foreach (var item in GetComponentsInChildren<Rigidbody>())
        {
            item.isKinematic = false;
        }

        foreach (var item in Physics.OverlapSphere(hitpostion, 0.3f))
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();

            if (rb)
            {
                rb.AddExplosionForce(1000, hitpostion, 0.3f);
            }
        }
        ThrowGun();
        animator.enabled = false;
        agent.enabled = false;
        udpateManger = true;
        Destroy(gameObject,3);
    }
}
