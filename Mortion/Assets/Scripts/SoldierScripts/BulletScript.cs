using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float LifeTime=3;
    public bool main = false;

    private void OnTriggerEnter(Collider targetObject)
    {
        if(targetObject.gameObject.tag == "Player")
        {
            PlayerStats.Health -= 20;
            PlayerStats.gotHit = true;
        }

        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!main) {
            LifeTime -= Time.deltaTime;
            if (LifeTime <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        
    }
}
