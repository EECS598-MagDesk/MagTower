using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadBaseHP : MonoBehaviour
{

    public Base baseObj;
    public LevelManager levelObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (levelObj.GetLevel() == 0)
        {
            gameObject.GetComponent<Text>().text = "Thank you for playing.";
        }
        else
        {
            gameObject.GetComponent<Text>().text = "Score: " + baseObj.getHP().ToString() + "\nLevel: " + levelObj.GetLevel().ToString();
        }
    }
}
