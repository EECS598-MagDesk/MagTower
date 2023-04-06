using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{

    public bool debugTest = false;
    public List<Vector3> debugTowerList;

    public GameObject towerPrefab;

    private float transferDistThresh = 3.5f;
    private List<GameObject> towerList = new List<GameObject>();
    private List<Vector3> targetPos = new List<Vector3>();
    private float speed = 1000f;

    private float updatePeriod = 0.05f;
    private float prevTime = 0;

    public InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > prevTime + updatePeriod)
        {
            if (debugTest)
            {
                ReadFromInspector();
            }
            else
            {
                ReadFromInputManager();
            }
            prevTime = Time.time;
        }

        for (int i = 0; i < towerList.Count; i ++)
        {
            towerList[i].transform.position = Vector3.MoveTowards(towerList[i].transform.position, targetPos[i], speed * Time.deltaTime);
        }
    }

    void ReadFromInputManager()
    {
        int[] bucket = new int[towerList.Count];
        List<int> newTowerList = new List<int>();
        for (int i = 0; i < bucket.Length; i++)
        {
            bucket[i] = -1;
        }
        List<Vector3> inputTowerList = inputManager.targets;
        for (int i = 0; i < inputTowerList.Count; i++)
        {
            bool foundFlag = false;
            for (int j = 0; j < towerList.Count; j++)
            {
                if (Vector3.Distance(inputTowerList[i], towerList[j].transform.position) < transferDistThresh)
                {
                    if (bucket[j] == -1)
                    {
                        bucket[j] = i;
                        foundFlag = true;
                        break;
                    }
                }
            }
            if (!foundFlag)
            {
                newTowerList.Add(i);
            }
        }
        for (int i = bucket.Length - 1; i >= 0; i--)
        {
            if (bucket[i] == -1)
            {
                Destroy(towerList[i]);
                towerList.RemoveAt(i);
                targetPos.RemoveAt(i);
            }
            else
            {
                targetPos[i] = inputTowerList[bucket[i]];
            }
        }
        for (int i = 0; i < newTowerList.Count; i++)
        {
            GameObject newTower = Instantiate(towerPrefab, inputTowerList[newTowerList[i]], new Quaternion(0, 0, 0, 0));
            Debug.Log("spawn");
            towerList.Add(newTower);
            targetPos.Add(inputTowerList[newTowerList[i]]);
        }
    }

    void ReadFromInspector()
    {
        int[] bucket = new int[towerList.Count];
        List<int> newTowerList = new List<int>();
        for (int i = 0; i < bucket.Length; i ++)
        {
            bucket[i] = -1;
        }
        for (int i = 0; i < debugTowerList.Count; i++)
        {
            bool foundFlag = false;
            for (int j = 0; j < towerList.Count; j++)
            {
                if (Vector3.Distance(debugTowerList[i], towerList[j].transform.position) < transferDistThresh)
                {
                    if (bucket[j] == -1)
                    {
                        bucket[j] = i;
                        foundFlag = true;
                        break;
                    }
                }
            }
            if (!foundFlag)
            {
                newTowerList.Add(i);
            }
        }
        for (int i = bucket.Length - 1; i >= 0; i--)
        {
            if (bucket[i] == -1)
            {
                Destroy(towerList[i]);
                towerList.RemoveAt(i);
                targetPos.RemoveAt(i);
            }
            else
            {
                targetPos[i] = debugTowerList[bucket[i]];
            }
        }
        for (int i = 0; i < newTowerList.Count; i ++)
        {
            GameObject newTower = Instantiate(towerPrefab, debugTowerList[newTowerList[i]], new Quaternion(0, 0, 0, 0));
            Debug.Log("spawn");
            towerList.Add(newTower);
            targetPos.Add(debugTowerList[newTowerList[i]]);
        }
    }
}
