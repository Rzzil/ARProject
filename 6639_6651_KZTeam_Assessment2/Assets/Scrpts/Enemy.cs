using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float speed = 25;
    //Damage to base
    public int damage;
    public GameObject deadFX;

    private Transform[] positions;
    private int index = 0;

    private Rigidbody rigi;

    // Start is called before the first frame update
    void Awake()
    {
        positions = WayPoints.positions;
        rigi = GetComponent<Rigidbody>();
    }
    void Start()
    {
        GetComponent<BuildManager>().showQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    void Move()
    {
        transform.GetChild(1).LookAt(positions[index].position);
        float tempY = transform.GetChild(1).localEulerAngles.y;
        float tempZ = transform.GetChild(1).localEulerAngles.z;
        transform.GetChild(1).localRotation = Quaternion.Euler(0, tempY, tempZ);
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);

        if (Vector3.Distance(positions[index].position, transform.position) < 1.5f)
        {
            index++;
        }

        if (index > positions.Length - 1)
        {
            ReachDestination();
        }
    }

    void ReachDestination()
    {
        Die();
    }

    public void Die()
    {
        EnemySpawner.instance.SummonEnemy();
        //Instantiate(deadFX, transform.position, transform.rotation);
        //TDSoundManager.instance.playDeadSound();
        Destroy(this.gameObject);
    }
}
