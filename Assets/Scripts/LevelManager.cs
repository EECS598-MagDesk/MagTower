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
                new Vector3(0f, 0f, 50f),
                new Vector3(0f, 0f, -50f),
            }
        );
        l0.groupSpawnPeriod = 1f;
        l0.groupSpawnSize = 1;
        l0.bossLevel = 0;
        levels.Add(l0);

        LevelInfo l1 = new LevelInfo();
        l1.spawnPos = new List<Vector3>(
            new Vector3[] { 
                new Vector3(35f, 0f, 40f),
                new Vector3(15f, 0f, 50f),
                new Vector3(25f, 0f, 30f),
            }
        );
        l1.groupSpawnPeriod = 0.3f;
        l1.groupSpawnSize = 50;
        l1.bossLevel = 0;
        levels.Add(l1);

        LevelInfo l2 = new LevelInfo();
        l2.spawnPos = new List<Vector3>(
            new Vector3[] {
                new Vector3(-25f, 0f, 60f),
                new Vector3(25f, 0f, -60f),
                new Vector3(-25f, 0f, -60f),
                new Vector3(25f, 0f, 60f),
                new Vector3(8f, 0f, 60f),
            }
        );
        l2.groupSpawnPeriod = 0.1f;
        l2.groupSpawnSize = 20;
        l2.bossLevel = 0;
        levels.Add(l2);

        LevelInfo l3 = new LevelInfo();
        l3.spawnPos = new List<Vector3>(
            new Vector3[] {
                new Vector3(24f, 0f, -60f),
                new Vector3(20f, 0f, -50f),
                new Vector3(15f, 0f, -40f),
                new Vector3(-24f, 0f, -60f),
                new Vector3(-20f, 0f, -50f),
                new Vector3(-15f, 0f, -40f),
            }
        );
        l3.groupSpawnPeriod = 1f;
        l3.groupSpawnSize = 30;
        l3.bossLevel = 2;
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
        l4.groupSpawnSize = 20;
        l4.bossLevel = 5;
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
        if (baseObj.getHP() < 100)
        {
            SetLevel(1);
            currentLevel = 1;
        }
        else if (baseObj.getHP() < 250)
        {
            SetLevel(2);
            currentLevel = 2;
        }
        else if (baseObj.getHP() < 400)
        {
            SetLevel(3);
            currentLevel = 3;
        }
        else if (baseObj.getHP() < 500)
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
