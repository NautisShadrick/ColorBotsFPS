using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunScript : MonoBehaviour
{
    public float dmage = 10f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public Transform gunPoint;
    public Transform gunTip;

    [SerializeField]
    private AudioClip laserClip;

    public GameObject hitParticle;
    public GameObject muzzleFlash;
    public LayerMask Hitables;

    AudioSource As;
    float nextTimeToFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        As = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            As.pitch = Random.Range(0.9f,1.2f);
            As.PlayOneShot(laserClip);

            nextTimeToFire = Time.time + 1f / fireRate;

            GameObject newFlash = Instantiate(muzzleFlash, gunTip.position, gunTip.transform.rotation, gunTip);
            newFlash.transform.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
            Vector3 tempScale = newFlash.transform.localScale;
            tempScale *= Random.Range(0.8f,1.2f);
            newFlash.transform.localScale = tempScale;

            Destroy(newFlash, 0.1f);
            RaycastHit hit;
            if (Physics.Raycast(gunPoint.position, gunPoint.forward, out hit, 500, Hitables))
            {
                GameObject newHitPart = Instantiate(hitParticle,hit.point,hitParticle.transform.rotation);
                newHitPart.transform.LookAt(transform.position,newHitPart.transform.up);
                Destroy(newHitPart, 1f);
            }
        }
    }
}
