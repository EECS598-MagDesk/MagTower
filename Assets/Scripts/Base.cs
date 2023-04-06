using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    private int hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            hp--;
            Destroy(other.gameObject);
        }
    }

    public int getHP()
    {
        return hp;
    }
}
