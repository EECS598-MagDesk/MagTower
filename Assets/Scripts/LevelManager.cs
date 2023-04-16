using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    struct LevelInfo
    {
        public List<Vector3> spawnPos;
        public float groupSpawnPeriod;
        public int groupSpawnSize;
        public int bossLevel;
    }

    public EnemyManager enemyManager;
    public Base baseObj;
    private List<LevelInfo> levels = new List<LevelInfo>();
    private int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        LevelInfo l0 = new LevelInfo();
        l0.spawnPos = new List<Vector3>(
            new Vector3[] {
                new Vector3(14f, 0f, 10f),
                new Vector3(-14f, 0f, 10f),
            }
        );
        l0.groupSpawnPeriod = 1f;
        l0.groupSpawnSize = 1;
        l0.bossLevel = 0;
        levels.Add(l0);

        LevelInfo l1 = new LevelInfo();
        l1.spawnPos = new List<Vector3>(
            new Vector3[] { 
                new Vector3(-14f, 0f, -10f),
                new Vector3(14f, 0f, 10f),
                new Vector3(-14f, 0f, 10f),
            }
        );
        l1.groupSpawnPeriod = 0.5f;
        l1.groupSpawnSize = 30;
        l1.bossLevel = 0;
        levels.Add(l1);

        LevelInfo l2 = new LevelInfo();
        l2.spawnPos = new List<Vector3>(
            new Vector3[] {
                new Vector3(-14f, 0f, 10f),
                new Vector3(14f, 0f, -10f),
            }
        );
        l2.groupSpawnPeriod = 0.2f;
        l2.groupSpawnSize = 60;
        l2.bossLevel = 0;
        levels.Add(l2);

        LevelInfo l3 = new LevelInfo();
        l3.spawnPos = new List<Vector3>(
            new Vector3[] {
                new Vector3(14f, 0f, -10f),
                new Vector3(14f, 0f, -19f),
                new Vector3(-14f, 0f, 10f),
            }
        );
        l3.groupSpawnPeriod = 1f;
        l3.groupSpawnSize = 20;
        l3.bossLevel = 1;
        levels.Add(l3);

        LevelInfo l4 = new LevelInfo();
        l4.spawnPos = new List<Vector3>(
            new Vector3[] {
                new Vector3(24f, 0f, 26f),
                new Vector3(-23f, 0f, 18f),
                new Vector3(14f, 0f, -19f),
                new Vector3(-14f, 0f, 10f),
            }
        );
        l4.groupSpawnPeriod = 1f;
        l4.groupSpawnSize = 30;
        l4.bossLevel = 3;
        levels.Add(l4);
    }

    void SetLevel(int level)
    {
        if (level != currentLevel)
        {
            Debug.Log("Set to level " + level.ToString());
            enemyManager.spawnPos = levels[level].spawnPos;
            enemyManager.groupSpawnPeriod = levels[level].groupSpawnPeriod;
            enemyManager.groupSpawnSize = levels[level].groupSpawnSize;
            enemyManager.bossLevel = levels[level].bossLevel;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (baseObj.getHP() < 20)
        {
            SetLevel(1);
            currentLevel = 1;
        }
        else if (baseObj.getHP() < 100)
        {
            SetLevel(2);
            currentLevel = 2;
        }
        else if (baseObj.getHP() < 200)
        {
            SetLevel(3);
            currentLevel = 3;
        }
        else if (baseObj.getHP() < 300)
        {
            SetLevel(4);
            currentLevel = 4;
        }
        else
        {
            SetLevel(0);
            currentLevel = 0;
        }
    }

    public int GetLevel()
    {
        return currentLevel;
    }
}
