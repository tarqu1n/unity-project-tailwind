using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage;

    public GameObject target;

    void Start()
    {
        transform.LookAt(target.transform);
    }

    private void FixedUpdate()
    {
        if (target)
            transform.LookAt(target.transform);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnHitTarget(Collider collision)
    {
        UnitStateController unitStateController = collision.gameObject.transform.parent.GetComponent<UnitStateController>();
        unitStateController.RecieveDamage(damage);
        Die();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (target && collision.gameObject.GetInstanceID() == target.gameObject.GetInstanceID())
        {
            OnHitTarget(collision);
        }
        else if (!target && collision.gameObject.tag == Config.tagList["Unit"])
        {
            OnHitTarget(collision);
        }
        else if (!target && collision.gameObject.tag == Config.tagList["Ground"])
        {
            Die();
        }
    }
}
