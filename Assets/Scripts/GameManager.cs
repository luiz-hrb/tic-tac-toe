using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Grid grid;
    public MiniMax minimax;
    // X é player; O é a máquina
    public PlayerType playerTurn;

    // UI
    public GameObject divFinal;
    public TextMeshProUGUI finalText;

    void Start()
    {
        playerTurn = PlayerType.X;
        //Debug.Log(tile[0].tile.gameObject.name);
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

    public void ClickedTile(Tile tile)
    {
        //Debug.Log("1 Clicou x no " + tile.position, tile);
        if (playerTurn == PlayerType.X)
        {
            //Debug.Log("2 Clicou x no " + tile.position, tile);
            var position = tile.position;
            var tileValue = GetTileValue(position);
            if (tileValue == PlayerType.None)
            {
                //Debug.Log("3 Clicou x no " + tile.position, tile);
                MakePlayerMove(position);
            }
        }
    }

    private void MakePlayerMove(Vector2 position)
    {
        if (!IsEnd())
        {
            SetTileValue(position, PlayerType.X);
            playerTurn = PlayerType.O;
            MakeMachineMove();
        }
    }

    private void MakeMachineMove()
    {
        if (!IsEnd())
        {
            Vector2 position = minimax.GetMax(grid).position;
            SetTileValue(position, PlayerType.O);
            playerTurn = PlayerType.X;
        }
    }

   private Tile GetTile(Vector2 position)
    {
        foreach (var tile in GetComponentsInChildren<Tile>())
        {
            if (tile.position == position)
                return tile;
        }
        return null;
    }

    private PlayerType GetTileValue(Vector2 position)
    {
        return grid.lines[(int)position.x].itens[(int)position.y].value;
    }

    private void SetTileValue(Vector2 position, PlayerType value)
    {
        Debug.Log("indo pro tile " + position);
        var item = grid.lines[(int)position.x].itens[(int)position.y];
        item.value = value;
        GetTile(position).SetValue(value);
    }

    private bool IsEnd()
    {
        var result = minimax.IsEnd(grid);
        bool isEnd = result != PlayerType.None;
        if (isEnd)
        {
            EndGame(result);
        }
        return isEnd;
    }

    private void EndGame(PlayerType result)
    {
        divFinal.SetActive(true);
        finalText.text = $"Player {result} won!";
        playerTurn = PlayerType.None;
    }
}
