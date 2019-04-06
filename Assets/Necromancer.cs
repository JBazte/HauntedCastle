using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : MonoBehaviour
{

    public GameObject instancePrefab;
    private float i = 0;
    private GameObject child;
    private GameObject child2;
    float rng = 0;
    private void Update()
    {
         i += 1* Time.deltaTime;

         rng = Random.Range(-1f, 1f);

        
        

        if (i > 5)
        {
            if(child == null)
                child = Instantiate(instancePrefab, new Vector3(-5.932319f, -10.45f, 0f), Quaternion.identity);

            if(child2 == null)
                child2 =Instantiate(instancePrefab, new Vector3(11.61f, -8.26f,0f), Quaternion.identity);
            i = 0;
            rng = Random.Range(-1f, 1f);
        }

    }

}
