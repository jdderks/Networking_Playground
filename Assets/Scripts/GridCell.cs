using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    none = 0,
    grass = 1,
    wall = 2
}

[RequireComponent(typeof(BoxCollider2D))]
public class GridCell : MonoBehaviour
{
    // the x and y coordinates of the cell
    public int x;
    public int y;

    //The type of tile
    [SerializeField] private CellType cellType;

    //child object that holds sprite component
    [SerializeField] private SpriteRenderer spriteRenderer;

    //The walls collider
    [SerializeField] private BoxCollider2D collider;

    //Cellsprite list
    [SerializeField] private CellSprite cellSpriteList;

    public CellType CellType
    {
        get => cellType;
        set
        {
            cellType = value;
        }
    }

    public void SetTile()
    {
        switch (cellType)
        {
            case CellType.none:
                break;
            case CellType.grass:
                spriteRenderer.sprite = cellSpriteList.grassSprite;
                collider.enabled = false;
                break;
            case CellType.wall:
                spriteRenderer.sprite = cellSpriteList.wallSprite;
                collider.enabled = true;
                break;
            default:
                break;
        }
    }
}
