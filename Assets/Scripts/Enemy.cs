using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 1;
    public Base baseObj;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = hp * transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            hp -= 1;
            baseObj.setHP(baseObj.getHP() + 1);
            Destroy(other.gameObject);
        }
    }
}
