using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int phyDmg = 50;
    public int magDmg = 50;

    public float speed = 20;

    private float distanceArriveTarget = 1.2f;

    private Transform target;

    public GameObject hitFX;

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.position);
            Vector3 dir = target.position - transform.position;
            if (dir.magnitude < distanceArriveTarget)
            {
                target.GetComponent<Enemy>().Die();
                //create FX
                GameObject.Instantiate(hitFX,transform.position, transform.rotation);
                //create sound FX
                //TDSoundManager.instance.playHitSound();
                Die();
            }
        }
        //destroy this bullet if target not exists anymore
        else
        {
            Invoke("OnBulletLosingTarget", 0.1f);
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);


    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    void OnBulletLosingTarget()
    {
        Die();
    }
}
