using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GridCell
{
    // the x and y coordinates of the cell
    public int x;
    public int y;

    // the GameObject representing the cell
    public GameObject gameObject;

    public GridCell(int x, int y, GameObject gameObject)
    {
        this.x = x;
        this.y = y;
        this.gameObject = gameObject;
    }
}
