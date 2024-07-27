using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] PlateCounter platesCounter;
    [SerializeField] Transform CounterTopPoint;
    [SerializeField] Transform plateVisualPrefab;
    private List<GameObject> plateVisualObjectList;
    private void Start()
    {
        plateVisualObjectList = new List<GameObject>();
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpwaned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e)
    {
        GameObject plateGameObject = plateVisualObjectList[plateVisualObjectList.Count - 1];
        plateVisualObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
        
    }

    private void PlatesCounter_OnPlateSpwaned(object sender, EventArgs e)
    {
        
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, CounterTopPoint);
        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0,plateOffsetY*plateVisualObjectList.Count, 0);
        plateVisualObjectList.Add(plateVisualTransform.gameObject);

       
    }
    
}
