using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public GameObject projectilePrefab;
    public float attackPeriod = 0.5f;
    private float findEnemiesPeriod = 0.2f;
    private float attackDistanceThresh = 25f;
    private Vector3 attackTargetDirection;
    private GameObject attackTargetObject;
    private bool attackFlag = false;
    public bool useTracking = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(findEnemies());
        StartCoroutine(attack());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator findEnemies()
    {
        while (true)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            float minDist = float.MaxValue;
            Vector3 minTargetPos = new Vector3(0f, 0f, 0f);
            GameObject minTargetObj = null;
            foreach (GameObject e in enemies) {
                float localDist = Vector3.Distance(e.transform.position, transform.position);
                if (localDist < minDist)
                {
                    minTargetPos = e.transform.position;
                    minDist = localDist;
                    minTargetObj = e;
                }
            }
            if (minDist < attackDistanceThresh)
            {
                attackFlag = true;
                attackTargetDirection = Vector3.Normalize(minTargetPos - transform.position);
                attackTargetObject = minTargetObj;
            }
            else
            {
                attackFlag = false;
            }
            yield return new WaitForSeconds(findEnemiesPeriod);
        }
        yield return null;
    }

    IEnumerator attack()
    {
        while (true)
        {
            if (attackFlag)
            {
                GameObject projectile = Instantiate(projectilePrefab, gameObject.transform.position, new Quaternion(0f, 0f, 0f, 0f));
                Projectile p = projectile.GetComponent<Projectile>();
                p.setDirection(ref attackTargetDirection);
                p.setSpeed(40f);
                p.setRange(20f);
                if (useTracking)
                {
                    p.setTracking(true);
                    p.setTargetObject(ref attackTargetObject);
                }
            }
            yield return new WaitForSeconds(attackPeriod);
        }
        yield return null;
    }
}
