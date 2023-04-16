using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    public GameObject projectilePrefab;
    private float attackPeriod = 0.2f;
    private float findEnemiesPeriod = 0.2f;
    private float attackDistanceThresh = 50f;
    private Vector3 attackTargetDirection;
    private GameObject attackTargetObject;
    private bool attackFlag = false;
    public bool useTracking = false;
    public GameObject attackRangeRing;
    public float attackRangeRingScale;

    public Material towerMat;
    public MeshRenderer towerObj;
    public MeshRenderer cannonObj;

    private float initHeight = 0;

    // Start is called before the first frame update
    void Start()
    {
        initHeight = transform.position.y;
        towerObj.material = towerMat;
        cannonObj.material = towerMat;
        StartCoroutine(findEnemies());
        StartCoroutine(attack());
        StartCoroutine(MaterialCheckingCO());
    }

    IEnumerator MaterialCheckingCO()
    {
        while (true)
        {
            towerObj.material = towerMat;
            cannonObj.material = towerMat;
            yield return new WaitForSeconds(5f);
        }
        yield return true;
    }

    // Update is called once per frame
    void Update()
    {
        float scale = getCurrentProjectileRange() / attackRangeRingScale;
        attackRangeRing.transform.localScale = new Vector3(scale, attackRangeRing.transform.localScale.y, scale);
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
                Vector3 dir = e.transform.position - transform.position;
                float angle = Vector3.Angle(dir, transform.forward);
                if (angle < 330 && angle > 30)
                {
                    continue;
                }
                if (localDist < minDist)
                {
                    minTargetPos = e.transform.position;
                    minDist = localDist;
                    minTargetObj = e;
                }
            }
            if (minDist < getCurrentProjectileRange())
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

    float getCurrentProjectileSpeed()
    {
        float speed = 30f - (transform.position.y) * 2;
        if (speed > 10)
        {
            return speed;
        }
        else
        {
            return 10f;
        }
    }

    float getCurrentProjectileRange()
    {
        float range = 10f + (transform.position.y) * 10;
        if (range > 60)
        {
            return 60;
        }
        else
        {
            return range;
        }
    }

    float getCurrentAttackPeriod()
    {
        float speed = 0.05f + (transform.position.y) * 0.05f;
        if (speed > 1)
        {
            return 1;
        }
        else
        {
            return speed;
        }
    }

    IEnumerator attack()
    {
        while (true)
        {
            if (attackFlag)
            {
                GameObject projectile = Instantiate(projectilePrefab, gameObject.transform.position, new Quaternion(0f, 0f, 0f, 0f));
                projectile.GetComponent<MeshRenderer>().material = towerMat;
                Projectile p = projectile.GetComponent<Projectile>();
                p.setDirection(ref attackTargetDirection);
                p.setSpeed(getCurrentProjectileSpeed());
                p.setRange(getCurrentProjectileRange());
                //if (useTracking)
                if (Random.Range(0, 10) > 3)
                {
                    p.setTracking(true);
                    p.setTargetObject(ref attackTargetObject);
                }
            }
            yield return new WaitForSeconds(getCurrentAttackPeriod());
        }
        yield return null;
    }
}
