using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector3 targetPos;
    private float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] bases = GameObject.FindGameObjectsWithTag("Base");
        if (bases.Length > 0)
        {
            targetPos = bases[Random.Range(0, bases.Length)].transform.position;
        }
    }

    public void setSpeed(float s)
    {
        speed = s;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);
    }
}
