using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public enum PlayerType { None, X, O, Tie }

public class MiniMax : MonoBehaviour
{
    public int gridSize = 3;

    public ReturnAlphaBeta GetMax(Grid grid, int alpha, int beta)
    {
        int maxValue = -2;

        Vector2 position = Vector2.zero;

        var result = IsEnd(grid);

        if (result == PlayerType.X)
            return new ReturnAlphaBeta(-1, Vector2.zero);
        if (result == PlayerType.O)
            return new ReturnAlphaBeta(1, Vector2.zero);
        if (result == PlayerType.Tie)
            return new ReturnAlphaBeta(0, Vector2.zero);


        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (grid.Get(i,j) == PlayerType.None)
                {
                    grid.lines[i].itens[j].value = PlayerType.O;
                    var alphaValue = GetMin(grid, alpha, beta);
                    if (alphaValue.value > maxValue)
                    {
                        maxValue = alphaValue.value;
                        position.x = i;
                        position.y = j;
                    }
                    grid.lines[i].itens[j].value = PlayerType.None;


                    if (maxValue >= beta)
                        return new ReturnAlphaBeta(maxValue, position);

                    if (maxValue > alpha)
                        alpha = maxValue;
                }
            }
        }
        return new ReturnAlphaBeta(maxValue, position);
    }

    public ReturnAlphaBeta GetMin(Grid grid, int alpha, int beta)
    {
        int minValue = 2;

        Vector2 position = Vector2.zero;

        var result = IsEnd(grid);

        if (result == PlayerType.X)
            return new ReturnAlphaBeta(-1, Vector2.zero);
        if (result == PlayerType.O)
            return new ReturnAlphaBeta(1, Vector2.zero);
        if (result == PlayerType.Tie)
            return new ReturnAlphaBeta(0, Vector2.zero);


        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (grid.Get(i,j) == PlayerType.None)
                {
                    grid.lines[i].itens[j].value = PlayerType.X;
                    var alphaValue = GetMax(grid, alpha, beta);
                    if (alphaValue.value < minValue)
                    {
                        minValue = alphaValue.value;
                        position.x = i;
                        position.y = j;
                    }
                    grid.lines[i].itens[j].value = PlayerType.None;


                    if (minValue <= alpha)
                        return new ReturnAlphaBeta(minValue, position);

                    if (minValue < beta)
                        beta = minValue;
                }
            }
        }
        return new ReturnAlphaBeta(minValue, position);
    }

    public PlayerType IsEnd(Grid grid)
    {
        PlayerType type;
        // Vertical win
        for (int i = 0; i < gridSize; i++)
        {
            type = grid.Get(0, i);
            if (type != PlayerType.None)
            {
                if (type == grid.Get(1, i) &&
                    type == grid.Get(2, i))
                {
                    return type;
                }
            }
        }
        // Horizontal win
        for (int i = 0; i < gridSize; i++)
        {
            type = grid.Get(i, 0);
            if (type != PlayerType.None)
            {
                if (type == grid.Get(i, 1) &&
                    type == grid.Get(i, 2))
                {
                    return type;
                }
            }
        }
        // Main diagonal win
        type = grid.Get(0, 0);
        if (type != PlayerType.None)
        {
            if (type == grid.Get(1, 1) &&
                type == grid.Get(2, 2))
            {
                return type;
            }
        }
        // Secundary diagonal win
        type = grid.Get(0, 2);
        if (type != PlayerType.None)
        {
            if (type == grid.Get(1, 1) &&
                type == grid.Get(2, 0))
            {
                return type;
            }
        }
        // If grid have space, return None
        foreach (var line in grid.lines)
        {
            foreach (var item in line.itens)
            {
                if (item.value == PlayerType.None)
                    return PlayerType.None;
            }
        }
        // Have no space, so, it's a tie!
        return PlayerType.Tie;
    }
}

[System.Serializable]
public class ReturnAlphaBeta
{
    public int value;
    public Vector2 position;

    public ReturnAlphaBeta(int value, Vector2 position)
    {
        this.value = value;
        this.position = position;
    }
}


[System.Serializable]
public class Grid
{
    public GridLine[] lines;

    public PlayerType Get(int line, int column)
    {
        return lines[line].itens[column].value;
    }
}

[System.Serializable]
public class GridLine
{
    public GridItem[] itens;
}

[System.Serializable]
public class GridItem
{
    public PlayerType value;
    public Tile tile;
}

