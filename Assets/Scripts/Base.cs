using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{

    public int hp = 0;

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
            hp -= other.gameObject.GetComponent<Enemy>().hp;
            Destroy(other.gameObject);
        }
    }

    public int getHP()
    {
        return hp;
    }

    public void setHP(int hp_input)
    {
        if (hp_input < 0)
        {
            hp = 0;
        }
        else
        {
            hp = hp_input;
        }
    }
}
