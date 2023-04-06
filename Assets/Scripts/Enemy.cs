using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 3;

    // Start is called before the first frame update
    void Start()
    {
        
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
        Debug.Log(other.tag);
        if (other.tag == "Projectile")
        {
            hp -= 1;
            Destroy(other.gameObject);
        }
    }
}
