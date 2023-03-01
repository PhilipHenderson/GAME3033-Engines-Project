using System.Collections.Generic;
using UnityEngine;

public class TownGenerator : MonoBehaviour
{
    public GameObject blockPrefab;
    public Material wallMaterial;

    public GameObject[] buildingPrefabs; // Array of building prefabs for Top Row and BotMidRow
    public GameObject sharedBuildingPrefab; // One shared building prefab for MidTopRow and BotRow
    public Transform[] topRowLocations;
    public Transform[] midTopRowLocations;
    public Transform[] midBotRowLocations;
    public Transform[] botRowLocations;

    public int borderWidth = 10;
    public int townWidth = 30;
    public int townHeight = 30;
    public Vector3 townCenterPosition;

    private GameObject buildingParent;
    private List<GameObject> buildingPool = new List<GameObject>();

    private void Start()
    {
        // Calculate the position of the town center
        Vector3 townCenter = townCenterPosition;

        // Calculate the position of the border center
        Vector3 borderCenter = townCenter - new Vector3(borderWidth, borderWidth, 0) / 2.0f;

        // Generate the border of blocks
        GenerateBorder(borderCenter);

        // Create a parent object for the buildings
        buildingParent = new GameObject("Buildings");

        // Pre-build the buildings in an object pool
        for (int i = 0; i < topRowLocations.Length + midTopRowLocations.Length + midBotRowLocations.Length + botRowLocations.Length; i++)
        {
            // Choose a random building prefab from the available list for Top Row and BotMidRow
            // or use the shared building prefab for MidTopRow and BotRow
            int randomIndex;
            GameObject buildingPrefab;
            if (i < topRowLocations.Length || i >= topRowLocations.Length + midBotRowLocations.Length - 3)
            {
                randomIndex = Random.Range(0, buildingPrefabs.Length);
                buildingPrefab = buildingPrefabs[randomIndex];
            }
            else
            {
                buildingPrefab = sharedBuildingPrefab;
            }

            // Instantiate the building prefab and add it to the pool
            GameObject buildingObject = Instantiate(buildingPrefab, buildingParent.transform);
            buildingObject.SetActive(false);
            buildingPool.Add(buildingObject);
        }

        // Activate the buildings at specified locations
        for (int i = 0; i < topRowLocations.Length; i++)
        {
            // Get a building object from the pool
            GameObject buildingObject = buildingPool[i];

            // Activate the building object and move it to the specified location
            buildingObject.SetActive(true);
            buildingObject.transform.position = topRowLocations[i].position;
        }
        int indexOffset = topRowLocations.Length;

        for (int i = 0; i < midTopRowLocations.Length; i++)
        {
            // Get a building object from the pool
            GameObject buildingObject = buildingPool[indexOffset + i];

            // Activate the building object and move it to the specified location
            buildingObject.SetActive(true);
            buildingObject.transform.position = midTopRowLocations[i].position;

            // Rotate the building to face upwards
            buildingObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        indexOffset += midTopRowLocations.Length;

        for (int i = 0; i < midBotRowLocations.Length; i++)
        {
            // Get a building object from the pool
            GameObject buildingObject = buildingPool[indexOffset + i];

            // Activate the building object and move it to the specified location
            buildingObject.SetActive(true);
            buildingObject.transform.position = midBotRowLocations[i].position;
        }
        indexOffset += midBotRowLocations.Length;

        for (int i = 0; i < botRowLocations.Length; i++)
        {
            // Get a building object from the pool
            GameObject buildingObject = buildingPool[indexOffset + i];

            // Activate the building object and move it to the specified location
            buildingObject.SetActive(true);
            buildingObject.transform.position = botRowLocations[i].position;

            // Rotate the building to face upwards
            buildingObject.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        indexOffset += botRowLocations.Length;
    }

    private void GenerateBorder(Vector3 center)
    {
        // Generate the top and bottom walls
        for (int x = 0; x <= borderWidth + townWidth; x++)
        {
            CreateBlock(new Vector3(x, 0) - new Vector3(borderWidth, 0) / 2.0f + center, wallMaterial);
            CreateBlock(new Vector3(x, townHeight + borderWidth - 1) - new Vector3(borderWidth, 0) / 2.0f + center, wallMaterial);
        }

        // Generate the left and right walls
        for (int y = 1; y < borderWidth + townHeight - 1; y++)
        {
            CreateBlock(new Vector3(0, y) - new Vector3(0, borderWidth) / 2.0f + center, wallMaterial);
            CreateBlock(new Vector3(townWidth + borderWidth - 1, y) - new Vector3(0, borderWidth) / 2.0f + center, wallMaterial);
        }
    }

    private void CreateBlock(Vector3 position, Material material)
    {
        GameObject blockObject = Instantiate(blockPrefab, position, Quaternion.identity);
        Renderer renderer = blockObject.GetComponent<Renderer>();
        renderer.material = material;
    }
}

