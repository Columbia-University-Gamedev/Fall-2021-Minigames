using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGeneration : MonoBehaviour
{
    int[,] buildingMap;

    [SerializeField] Vector2 buildingRange = new Vector2(2, 10); //(Min, max) number of buildings in succession

    [SerializeField] GameObject[] buildings;

    void Update()
    {

    }
}
