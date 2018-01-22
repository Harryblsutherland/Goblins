using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public UnitInfo target;
    public float speed;
    public GameObject ShootingUnit;
    public DamageObject Damage;
    public float rotationStrength;
    

    private Rigidbody rigidBody;
    private void Awake()
    {

        rigidBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        transform.LookAt(target.transform.position);
        //transform.Rotate(-45, 0, 0);

    }
    void Update () {
        if (target == null)
        {
            Destroy(transform.gameObject);
        }
        else
        {
                  transform.LookAt(new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z));
        var targetpoint = target.transform.position;
        targetpoint.y = targetpoint.y + 2;
        transform.LookAt(targetpoint);

        transform.position = Vector3.MoveTowards(transform.position, targetpoint, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetpoint) < 2)
        {
            target.RecieveDamage(Damage);
            Destroy(transform.gameObject);
        }
        }
  
	}
}
