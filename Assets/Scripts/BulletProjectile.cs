using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    //[SerializeField] private Transform vfxHitGreen;
    //[SerializeField] private Transform vfxHitRed;

    private Rigidbody bulletRigidbody;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        float speed = 30f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyTarget>() != null)
        {
            //Hit target
            //Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
        }
        else
        {
            // Hit Something else
            //Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
