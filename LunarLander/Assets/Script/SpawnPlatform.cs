using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnPlatform : MonoBehaviour
{
    [SerializeField] private GameObject _platform;
    private string[] PointText = { "x2", "x3", "x5" };

    private void Awake()
    {
        Transform[] child = GetComponentsInChildren<Transform>();
        int RandomChild = UnityEngine.Random.Range(5, child.Length);
        for (int i = 1; i < RandomChild; i++)
        {
            GameObject Object = Instantiate(_platform);
            Object.transform.position = child[i].transform.position;
            Object.transform.Rotate(child[i].transform.eulerAngles);
        }
        
    }
}
