using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform minSpawnX;
    [SerializeField]
    private Transform maxSpawnX;
    [SerializeField]
    private Transform minSpawnY;
    [SerializeField]
    private Transform maxSpawnY;
    [SerializeField]
    private int minOilinLevel;
    [SerializeField]
    private int maxOilinLevel;
    [SerializeField]
    private Transform mainTarget;
    [SerializeField]
    private GameObject[] oilObjs;
    private Vector3 spawnPoint;
    private void Start()
    {
        int spawnInLevel = Random.Range(minOilinLevel, maxOilinLevel);
        for(int i =0;i<spawnInLevel;i++)
        {
            Vector3 position = new Vector3(Random.Range(minSpawnX.position.x, maxSpawnX.position.x), Random.Range(minSpawnY.position.y, maxSpawnY.position.y), -5f);
            GameObject temp = Instantiate(oilObjs[Random.Range(0, oilObjs.Length)], position, Quaternion.identity);
            temp.transform.parent = mainTarget;
            temp.transform.localPosition = new Vector3(temp.transform.localPosition.x, 6, temp.transform.localPosition.z);
        }
    }
}
