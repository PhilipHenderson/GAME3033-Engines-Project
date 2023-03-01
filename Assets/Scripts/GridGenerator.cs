using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int rows;
    public int cols;
    public float size;
    public GameObject tilePrefab;
    public float tileOffset;
    public int centerRadius;

    private void Start()
    {
        // calculate the center point of the grid
        Vector3 centerPoint = new Vector3((cols - 1) * size / 2, (rows - 1) * size / 2, 0);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // calculate the distance from the current position to the center point
                float distance = Vector2.Distance(new Vector2(col, row), new Vector2(centerPoint.x / size, centerPoint.y / size));

                if (distance <= centerRadius)
                {
                    // skip creating a tile at this position
                    continue;
                }

                GameObject newTile = Instantiate(tilePrefab, transform);
                float xPos = col * size - centerPoint.x + tileOffset;
                float yPos = row * size - centerPoint.y + tileOffset;
                newTile.transform.position = new Vector3(xPos, yPos, 0f);
            }
        }
    }
    public void DestroyTownBuildings()
    {
        GameObject buildingParent = GameObject.Find("Buildings");
        if (buildingParent != null)
        {
            Destroy(buildingParent);
        }
    }
}