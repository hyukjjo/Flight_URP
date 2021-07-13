using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private float oddX = 0f;
    private float evenX = 0.75f;
    private float offset = 1.5f;

    public GameObject enemyPrefab;
    public EnemyGroup enemyGroup;
    public List<GameObject> enemys = new List<GameObject>();

    private int enemyCount = 0;

    private void Awake()
    {
        enemyGroup = GetComponentInChildren<EnemyGroup>();
        enemyGroup.CallBackEnd += CreateEnemys;
        enemyGroup.enabled = false;
    }

    private void OnDisable()
    {
        enemyGroup.CallBackEnd -= CreateEnemys;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            CreateEnemys();
    }

    public void Init()
    {
        enemyGroup.enabled = false;

        foreach (var enemy in enemys)
        {
            Destroy(enemy);
        }
        enemys.Clear(); 
    }

    public void CreateEnemys()
    {
        Init();

        enemyCount = GameManager.Instance.EnemyCount;
        GameObject enemy;

        for (int i = 0; i < enemyCount; i++)
        {
            enemy = Instantiate(enemyPrefab, enemyGroup.transform);
            enemy.name = "Enemy " + i;
            enemy.tag = "Enemy";
            enemy.transform.localPosition = Vector3.zero;
            enemy.transform.localRotation = Quaternion.identity;
            enemys.Add(enemy);
        }

        SetEnemyPosition();
        enemyGroup.enabled = true;
    }

    public void SetEnemyPosition()
    {
        int halfenemyCount = (int)(enemyCount * 0.5f);

        for (int i = 0; i < enemyCount; i++)
        {
            for (int j = 0; j < enemys.Count; j++)
            {
                if (j == 0)
                    enemys[0].transform.localPosition = new Vector3((halfenemyCount * -offset) + (enemyCount % 2 == 0 ? evenX : oddX), 0f, 0f);
                else
                    enemys[j].transform.localPosition = enemys[j - 1].transform.localPosition + new Vector3(offset, 0f, 0f);
            }
        }
    }

    public void DestroyEnemy()
    {
        enemyGroup.enabled = false;
    }
}
