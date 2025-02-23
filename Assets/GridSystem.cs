using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int rows = 10;
    public int columns = 10;
    public float cellSize = 1f;
    public GameObject cellPrefab;
    public Color highlightColor = Color.yellow;

    private GameObject[,] gridArray;
    private GameObject highlightedCell;

    void Start()
    {
        CreateGrid();
    }

    void Update()
    {
        HandleMouseInput();
        HighlightCell();
    }

    void CreateGrid()
    {
        gridArray = new GameObject[rows, columns];

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                Vector3 cellPosition = new Vector3(x * cellSize, y * cellSize, 0);
                GameObject cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity);
                cell.transform.parent = transform;
                gridArray[x, y] = cell;
            }
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            int x = Mathf.FloorToInt(mousePosition.x / cellSize);
            int y = Mathf.FloorToInt(mousePosition.y / cellSize);

            if (x >= 0 && x < rows && y >= 0 && y < columns)
            {
                Debug.Log("Clicked on cell: " + x + ", " + y);
                // Add your cell interaction logic here
            }
        }
    }

    void HighlightCell()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.FloorToInt(mousePosition.x / cellSize);
        int y = Mathf.FloorToInt(mousePosition.y / cellSize);

        if (x >= 0 && x < rows && y >= 0 && y < columns)
        {
            if (highlightedCell != null)
            {
                highlightedCell.GetComponent<SpriteRenderer>().color = Color.white;
            }

            highlightedCell = gridArray[x, y];
            highlightedCell.GetComponent<SpriteRenderer>().color = highlightColor;
        }
    }

    public Vector3 GetCellCenter(int x, int y)
    {
        return new Vector3(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2, 0);
    }
}