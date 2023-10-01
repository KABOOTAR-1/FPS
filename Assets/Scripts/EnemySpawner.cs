using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
   public List<Enemy> m_Spawns = new List<Enemy>();
    int i = 2;
    int x;
    void Start()
    {
         x=m_Spawns.Count;
    }

    // Update is called once per frame
    void Update()
    {
       
        while (i >0)
        {
            int y = Random.Range(0, x );
            i--;
            Instantiate(m_Spawns[y], transform.position + new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10)),Quaternion.identity);
        }
    }
}
