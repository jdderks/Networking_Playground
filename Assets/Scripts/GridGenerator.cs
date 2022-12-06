using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // the grid dimensions
    public Vector2Int dimensions;

    // the prefab for a cell
    public GameObject cellPrefab;

    // the grid cells
    private GridCell[,] cells;

    // Use this for initialization
    public void Init()
    {
        // generate the grid
        GenerateGrid();
    }

    // generates the grid
    public void GenerateGrid()
    {
        // initialize the cells array
        cells = new GridCell[dimensions.x, dimensions.y];

        for (int x = 0; x < dimensions.x; x++)
        {
            for (int y = 0; y < dimensions.y; y++)
            {
                // create a cell at the current position
                Vector3 cellPosition = new Vector3(x, y, 0);
                GameObject cellObject = Instantiate(cellPrefab, cellPosition, Quaternion.identity);

                // add the cell to the grid
                cellObject.transform.parent = transform;

                // create a GridCell instance for the cell
                GridCell cell = new GridCell(x, y, cellObject);

                // add the GridCell to the cells array
                cells[x, y] = cell;
            }
        }
    }
}