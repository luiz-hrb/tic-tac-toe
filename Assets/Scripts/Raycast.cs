using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public GameManager gameManager;

    void FixedUpdate()
    {
        RaycastHit hit;

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            var tile = hit.collider.GetComponent<Tile>();
            if (tile)
            {
                gameManager.ClickedTile(tile);
            }
        }
    }
}
