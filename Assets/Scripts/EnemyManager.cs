using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public List<Vector3> spawnPos = new List<Vector3>();
    public int level = 0;
    private Dictionary<int, float> levelSpawnPeriodDict = new Dictionary<int, float>()
    {
        { 0, 0.2f },
        { 1, 1f }
    };

    private float checkPeriod = 2f;

    public GameObject enemyPrefab;
    public GameObject spawnPointPrefab;

    public Base baseObj;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnCoroutine());
        StartCoroutine(spawnPointCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnPointCoroutine()
    {
        int prevSize = 0;
        List<GameObject> spawnPointList = new List<GameObject>();
        while (true)
        {
            if (spawnPos.Count != prevSize)
            {
                for (int i = 0; i < spawnPointList.Count; i ++)
                {
                    Destroy(spawnPointList[i]);
                }
                spawnPointList.Clear();
                for (int i = 0; i < spawnPos.Count; i++)
                {
                    Instantiate(spawnPointPrefab, spawnPos[i], new Quaternion(0f, 0f, 0f, 0f));
                }
                prevSize = spawnPos.Count;
            }
            yield return new WaitForSeconds(checkPeriod);
        }
        yield return null;
    }

    IEnumerator spawnCoroutine()
    {
        while (true)
        {
            int spawnSize = Random.Range(10, 30);
            Vector3 pos = spawnPos[Random.Range(0, spawnPos.Count)];
            for (int i = 0; i < spawnSize; i++)
            {
                pos.x += Random.Range(-4f, 4f);
                pos.z += Random.Range(-4f, 4f);
                GameObject enemy = Instantiate(enemyPrefab, pos, new Quaternion(0f, 0f, 0f, 0f));
                enemy.GetComponent<Enemy>().baseObj = baseObj;
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(levelSpawnPeriodDict[level]);
        }
        yield return null;
    }
}
