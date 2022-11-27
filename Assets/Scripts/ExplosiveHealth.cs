using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveHealth : MonoBehaviour
{
    public float maxHealth = 25;
    public float currentHealth;
    public float explosiveForce = 10f;
    public float explosiveRange = 10f;

    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0f)
        {
            Explode();
        }
    }

    public void Damage(float dmg)
    {
        currentHealth -= dmg;
    }

    void Explode()
    {
        Collider[] rbCols = Physics.OverlapSphere(transform.position, explosiveRange);
        foreach(Collider col in rbCols)
        {
            if (col.gameObject.GetComponent<Rigidbody>())
            {
                col.gameObject.GetComponent<Rigidbody>().AddExplosionForce(explosiveForce, transform.position, explosiveRange);
            }
        }

        GameObject newExplosion = Instantiate(Explosion, transform.position, Explosion.transform.rotation);

        Destroy(gameObject);
    }
}
