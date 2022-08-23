using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrees : MonoBehaviour
{
    public GameObject TreePrefab_01;
    public Transform Min;
    public Transform Max;
    
    void Start()
    {
        for(int i = 0; i < 1000; i++)
        {
            // TreePrefab.transform.SetParent(gameObject.transform, false);
            GameObject TempTrees = Instantiate(TreePrefab_01, new Vector3(Random.Range(Min.position.x, Min.position.z), 110f, Random.Range(Max.position.x, Max.position.z)), Quaternion.Euler(-90f, 0f, 0f));
        }
    }

    void Update()
    {
        
    }
}
