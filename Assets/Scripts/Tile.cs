using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    public Vector2 position;
    public Color colorX, colorO;
    public GameObject objX, objO;
    public Transform spawn;

    public void SetValue(PlayerType value)
    {
        if (value == PlayerType.X)
        {
            SetColor(colorX);
            SpawnObject(objX);
        }
        else if(value == PlayerType.O)
        {
            SetColor(colorO);
            SpawnObject(objO);
        }
    }

    private void SetColor(Color color)
    {
        GetComponentInChildren<Renderer>().material.color = color;
    }

    private void SpawnObject(GameObject obj)
    {
        Instantiate(obj, spawn);
        obj.transform.localPosition = Vector3.zero;
    }
}
