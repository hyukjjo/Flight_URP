using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPos : MonoBehaviour
{
    private float oddX = 0f;
    private float evenX = 0.75f;
    private float offset = 1.5f;

    public List<GameObject> enemys = new List<GameObject>();

    private int enemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            CreateEnemyParant();
    }

    public void Init()
    {
        foreach (var enemy in enemys)
        {
            Destroy(enemy);
        }
        enemys.Clear();
    }

    public void CreateEnemyParant()
    {
        Init();

        enemyCount = GameManager.Instance.Stage;
        GameObject temp = new GameObject();
        GameObject enemy;

        for (int i = 0; i < enemyCount; i++)
        {
            enemy = Instantiate(temp, transform);
            enemy.name = "Enemy " + i;
            enemy.transform.localPosition = Vector3.zero;
            enemy.transform.localRotation = Quaternion.identity;
            enemys.Add(enemy);
        }

        Destroy(temp);
        SetEnemyPosition();
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
}
