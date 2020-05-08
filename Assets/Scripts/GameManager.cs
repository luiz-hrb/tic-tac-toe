using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Grid grid;
    public MiniMax minimax;
    public PlayerType playerTurn;  

    void Start()
    {
        playerTurn = PlayerType.X;
    }

    void Update()
    {

    }

    private bool IsEnd()
    {
        var result = minimax.IsEnd(grid);
        return result != PlayerType.None;
    }
}
