using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : MonoBehaviour
{

    public GameObject instancePrefab;
    public float Timer = 8;
    private GameObject child;
    private GameObject child2;
    public Transform spawnPoint;
    public Transform spawnPoint2;
    float rng = 0;
    
    private void Update()
    {
         Timer += Time.deltaTime * 1;

         rng = Random.Range(-1f, 1f);

        if (Timer > 8)
        {
            if(child == null){
                child = Instantiate(instancePrefab, spawnPoint.position, Quaternion.identity);
                rng = Random.Range(-1f, 1f);
                Timer = 0;
            }

            if(child2 == null){
                child2 = Instantiate(instancePrefab, spawnPoint2.position, Quaternion.identity);
                rng = Random.Range(-1f, 1f);
                Timer = 0;
            }
        }

    }

}
