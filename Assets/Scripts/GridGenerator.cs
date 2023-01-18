using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // the grid dimensions
    [SerializeField] private Vector2Int dimensions;

    //Amount of players
    [SerializeField] private int amountOfPlayers;
    [SerializeField] private int spawnSpacing = 2;

    //Spawnpoints determined by amountOfPlayers
    [SerializeField] private List<Vector2> spawnPoints;

    // the prefab for a cell
    [SerializeField] private GameObject cellPrefab;

    // the grid cells
    private GridCell[,] cells;

    public int AmountOfPlayers { get => amountOfPlayers; set => amountOfPlayers = value; }
    public List<Vector2> SpawnPoints { get => spawnPoints; set => spawnPoints = value; }

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
                GridCell cell = cellObject.GetComponent<GridCell>();

                //Set cell X and Y
                cell.x = x;
                cell.y = y;


                //Selects the outer edges
                if (x == 0 || x == dimensions.x - 1 || y == 0 || y == dimensions.y - 1)
                { 
                    cell.CellType = CellType.wall;
                } 
                else
                {
                    cell.CellType = CellType.grass;
                }
                // add the GridCell to the cells array
                cells[x, y] = cell;

                //Set texture of cell
                cell.SetTile();
            }
        }

    }

    [ContextMenu("Set spawnpoints")]
    public void SetSpawnPoints(int amount)
    {
        SpawnPoints = GetRandomPoints(new Vector2[dimensions.x, dimensions.y], amount, spawnSpacing);
    }

    public List<Vector2> GetRandomPoints(Vector2[,] grid, int numPoints, int spacing)
    {
        List<Vector2> randomPoints = new List<Vector2>();
        int gridWidth = grid.GetLength(0);
        int gridHeight = grid.GetLength(1);
        System.Random random = new System.Random();

        for (int i = 0; i < numPoints; i++)
        {
            int randomX = random.Next(spacing, gridWidth - spacing);
            int randomY = random.Next(spacing, gridHeight - spacing);

            Vector2 point = grid[randomX, randomY];

            while (randomPoints.Contains(point))
            {
                randomX = random.Next(spacing, gridWidth - spacing);
                randomY = random.Next(spacing, gridHeight - spacing);
                point = grid[randomX, randomY];
            }

            randomPoints.Add(new Vector2(randomX, randomY));
        }

        return randomPoints;
    }
}