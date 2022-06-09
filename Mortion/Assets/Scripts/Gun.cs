
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Transform gunBarrell;
    public TrailRenderer bulletTrail;


    public float damage = 50f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;


    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeForFire = 0.2f;//might change due speed of bean

    //ammo related variables
    public static float initialAmmo = 50;
    public static float ammo = 50;

    public Text ammoCounter;

    //sound
    [SerializeField] private new AudioSource audio;


    // initilize the ammo counter on the left of the canvas
    void Awake()
    {
        ammoCalculator();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time>=nextTimeForFire)
        {
            nextTimeForFire = Time.time + 5f / fireRate;
            
            if(ammo > 0)
            {
                Shoot();
                audio.Play();
                ammo--;
            }

            ammoCalculator();

        }
    }
    void Shoot()
    {
        var bullet = Instantiate(bulletTrail, gunBarrell.position, Quaternion.identity);
       bullet.AddPosition((gunBarrell.position)+ fpsCam.transform.forward * 1.5f);
        {
            bullet.transform.position = transform.position + (fpsCam.transform.forward * 200);
        }

        muzzleFlash.Play();
        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name); used for testing no need right now
            SoldierScript target = hit.transform.GetComponent<SoldierScript>();

            if(target != null)
            {
                target.health -= 10;
            }

            if(hit.rigidbody != null)
            {

                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

        

    }

    void ammoCalculator()
    {
        string resolve = ammo + "/" + initialAmmo;
        ammoCounter.text = resolve;
    }

}
