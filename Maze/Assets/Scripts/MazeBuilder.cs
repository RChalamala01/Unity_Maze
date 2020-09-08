using UnityEngine;

public class MazeBuilder : MonoBehaviour
{

    public Camera cam;

    public GameObject tilePrefab;
    public Transform tileParent;

    public int width;
    public int height;
    public GameObject[,] tileMatrix;


    void Start()
    {
        tileMatrix = new GameObject[height, width];
        cam.transform.position = new Vector3((width - 1) * 0.55f, Mathf.Max(height, width), (height - 1) * 0.55f);
        InstantiateMaze(height, width);
    }

    void InstantiateMaze(int rows, int columns)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(j * 1.1f, 0, i * 1.1f), Quaternion.identity, tileParent);
                tile.transform.name = "tile (" + i + ", " + j + ")";
                tileMatrix[i, j] = tile;
            }
        }

    }

}
