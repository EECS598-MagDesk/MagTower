using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public List<Vector3> spawnPos = new List<Vector3>();
    public float groupSpawnPeriod = 1f;
    public int groupSpawnSize = 20;
    public int bossLevel = 0;
    private bool levelChangeFlag = false;

    private float checkPeriod = 0.5f;

    public GameObject enemyPrefab;
    public GameObject spawnPointPrefab;

    public Base baseObj;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos.Add(new Vector3(14f, 0f, 10f));
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
                    spawnPointList.Add(Instantiate(spawnPointPrefab, spawnPos[i], new Quaternion(0f, 0f, 0f, 0f)));
                }
                prevSize = spawnPos.Count;
                levelChangeFlag = true;
            }
            yield return new WaitForSeconds(checkPeriod);
        }
        yield return null;
    }

    IEnumerator spawnCoroutine()
    {
        while (true)
        {
            int spawnSize = 0;
            if (bossLevel > 0)
            {
                spawnSize = groupSpawnSize;
            }
            else
            {
                spawnSize = Random.Range(groupSpawnSize - 10, groupSpawnSize + 10);
            }
            Vector3 pos = spawnPos[Random.Range(0, spawnPos.Count)];
            for (int i = 0; i < spawnSize; i++)
            {
                pos.x += Random.Range(-4f, 4f);
                pos.z += Random.Range(-4f, 4f);
                GameObject enemy = Instantiate(enemyPrefab, pos, new Quaternion(0f, 0f, 0f, 0f));
                enemy.GetComponent<Enemy>().baseObj = baseObj;
                enemy.GetComponent<Enemy>().hp = Random.RandomRange(1, bossLevel + 2);
                if (levelChangeFlag)
                {
                    levelChangeFlag = false;
                    break;
                }
                else
                {
                    yield return new WaitForSeconds(groupSpawnPeriod);
                }
                
            }
            yield return new WaitForSeconds(0.2f);
        }
        yield return null;
    }
}
