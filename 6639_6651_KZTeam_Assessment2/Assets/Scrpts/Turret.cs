using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Enemy")
        {
            enemies.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Enemy")
        {
            enemies.Remove(col.gameObject);
        }
    }

    //how many secs per shot
    public float attackRateTime = 1;
    private float timer = 0;

    public GameObject bulletPrefab;
    public Transform firePosition;
    public Transform head;

    public GameObject fireFX;

    private Animator anim;

    void Start()
    {
        timer = attackRateTime;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //face the head to the target
        if (enemies.Count > 0 && enemies[0] != null)
        {
            Vector3 targetPositon = enemies[0].transform.position;
            targetPositon.y = head.position.y;
            head.LookAt(targetPositon);
        }
        //attack part
        timer += Time.deltaTime;
        if (enemies.Count > 0 && timer >= attackRateTime)
        {
            timer = 0;
            attack();
        }
    }

    private void attack()
    {
        if (enemies[0] == null)
        {
            UpdateEnemys();
        }
        if (enemies.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemies[0].transform);
            //create fire fx
            GameObject.Instantiate(fireFX, firePosition.position, firePosition.rotation);
            //start fire Animation
            anim.SetTrigger("FireTrigger");
        }
        else
        {
            //reset the gun for the next group of enemies
            timer = attackRateTime;
        }
    }

    private void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                emptyIndex.Add(i);
            }
        }
        for (int i = 0; i < emptyIndex.Count; i++)
        {
            enemies.RemoveAt(emptyIndex[i] - i);
        }
    }
}
