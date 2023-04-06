using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Vector3 direction = new Vector3(0f, 0f, 0f);
    private float speed = 1f;
    private float range = 0f;
    private float rangeThresh = 20f;
    private GameObject targetGameObject;
    private bool tracking = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setDirection(ref Vector3 dir)
    {
        direction = dir;
    }

    public void setSpeed(float s)
    {
        speed = s;
    }

    public void setRange(float r)
    {
        rangeThresh = r;
    }

    public void setTargetObject(ref GameObject obj)
    {
        targetGameObject = obj;
    }

    public void setTracking(bool t)
    {
        tracking = t;
    }

    // Update is called once per frame
    void Update()
    {
        if (range > rangeThresh)
        {
            Destroy(gameObject);
        }
        float dist = speed * Time.deltaTime;
        range += dist;
        if (tracking)
        {
            if (targetGameObject)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetGameObject.transform.position, dist);
            }
            else
            {
                transform.position += direction * dist;
            }
        }
        else
        {
            transform.position += direction * dist;
        }
    }
}
