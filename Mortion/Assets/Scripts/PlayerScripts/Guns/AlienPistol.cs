using UnityEngine;

public class AlienPistol : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame

    public float damage = 30f;

    public float range = 100f;

    public Camera cam;

    public int totalAmmo = 100;

    private void shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
        }

        totalAmmo--;
    }

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }

    }
}
