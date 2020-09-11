using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class MazeBuilder : MonoBehaviour
{

    public Camera cam;

    public Transform tileParent;
    public GameObject tilePrefab;

    public int width;
    public int height;
    public GameObject[,] tileMatrix;

    public Material floorMaterial;

    void Start()
    {
        tileMatrix = new GameObject[height, width];
        InstantiateMaze(height, width);
        DepthFirstRandom(1, 1);
        StartCoroutine(MazeSearch());
        //DepthFirstStack(1, 1);
        //PrimsAlgorithm();
    }



    IEnumerator MazeSearch()
    {
        (int x, int y) = (1, 1);
        (int xEnd, int yEnd) = (height - 2, width - 2);

        List<(int, int)> list = new List<(int, int)>();
        tileMatrix[x, y].GetComponent<Tile>().visited = true;

        bool done = false;
        int counter = 2000;

        while (!done)
        {
            counter--;

            int before = list.Count;

            if (IsPath(x + 1, y) && !list.Contains((x + 1, y))) { list.Add((x + 1, y)); }
            if (IsPath(x - 1, y) && !list.Contains((x - 1, y))) { list.Add((x - 1, y)); }
            if (IsPath(x, y + 1) && !list.Contains((x, y + 1))) { list.Add((x, y + 1)); }
            if (IsPath(x, y - 1) && !list.Contains((x, y - 1))) { list.Add((x, y - 1)); }

            if (before == list.Count) { list.Remove((x, y)); }

            float minDistance = Mathf.Infinity;
            
            for (int i = 0; i < list.Count; i++)
            {
                Debug.Log(list[i]);
                if (EuclideanDistance(list[i], xEnd, yEnd) < minDistance)
                {
                    minDistance = EuclideanDistance(list[i], xEnd, yEnd);
                    (x, y) = list[i];
                }
            }
            tileMatrix[x, y].GetComponent<Tile>().visited = true;
            list.Remove((x, y));
            if (minDistance == 0) { done = true; }
            yield return new WaitForSeconds(0.0001f);
        }
        yield return null;

    }

    int ManhattanDistance((int, int) tile, int xEnd, int yEnd)
    {
        (int x, int y) = tile;
        return Mathf.Abs(xEnd - x) + Mathf.Abs(yEnd - y);
    }

    float EuclideanDistance((int, int) tile, int xEnd, int yEnd)
    {
        (int x, int y) = tile;
        return Mathf.Pow(Mathf.Abs(xEnd - x), 2) + Mathf.Pow(Mathf.Abs(yEnd - y), 2);
    }



    void DepthFirstRandom(int i, int j)
    {
        List<(int, int)> list = new List<(int, int)>();
        list.Add((i, j));
        int x;
        int y;
        int max_nodes = 0;

        while (list.Count != 0)
        {

            int randIndex = Random.Range(0, list.Count - 1);
            if (list.Count > max_nodes) { max_nodes = list.Count; }
            (x, y) = list[randIndex];
            list.Remove((x, y));

            tileMatrix[x, y].GetComponent<Tile>().isPath = 1;

            if (IsViablePath(x + 1, y) && !list.Contains((x + 1, y))) { list.Add((x + 1, y)); }
            else { list.Remove((x + 1, y)); }
            if (IsViablePath(x - 1, y) && !list.Contains((x - 1, y))) { list.Add((x - 1, y)); }
            else { list.Remove((x - 1, y)); }
            if (IsViablePath(x, y + 1) && !list.Contains((x, y + 1))) { list.Add((x, y + 1)); }
            else { list.Remove((x, y + 1)); }
            if (IsViablePath(x, y - 1) && !list.Contains((x, y - 1))) { list.Add((x, y - 1)); }
            else { list.Remove((x, y - 1)); }
        }
        tileMatrix[height - 2, width - 2].GetComponent<Tile>().isPath = 1;
        tileMatrix[height - 3, width - 2].GetComponent<Tile>().isPath = 1;
        tileMatrix[height - 2, width - 3].GetComponent<Tile>().isPath = 1;
        Debug.Log(max_nodes);
    }


    void DepthFirstStack(int i, int j)
    {
        List<(int, int)> stack = new List<(int, int)>();
        stack.Add((i, j));
        int x;
        int y;
        int counter = 1000;

        while (counter > 0 && stack.Count != 0)
        {
            counter--;

            (x, y) = stack[stack.Count - 1];
            Debug.Log(x + "," + y);
            stack.Remove((x, y));
            tileMatrix[x, y].GetComponent<Tile>().isPath = 1;

            if (IsViablePath(x + 1, y) && !stack.Contains((x + 1, y))) { stack.Add((x + 1, y)); }
            else { stack.Remove((x + 1, y)); }
            if (IsViablePath(x - 1, y) && !stack.Contains((x - 1, y))) { stack.Add((x - 1, y)); }
            else { stack.Remove((x - 1, y)); }
            if (IsViablePath(x, y + 1) && !stack.Contains((x, y + 1))) { stack.Add((x, y + 1)); }
            else { stack.Remove((x, y + 1)); }
            if (IsViablePath(x, y - 1) && !stack.Contains((x, y - 1))) { stack.Add((x, y - 1)); }
            else { stack.Remove((x, y - 1)); }
        }
        

    }

    void PrimsAlgorithm()
    {
        tileMatrix[1, 1].GetComponent<Tile>().isPath = 1;
        int i = 1;
        int j = 1;
        int k = 400;

        if (Input.GetKey(KeyCode.Space))
        {
            k--;
            bool change = false;
            int l = 10;
            while (!change && l > 0)
            {
                l--;
                float rand = Random.value;
                if (rand < 0.25f && i != width - 2) { i++; change = true; }
                else if (rand >= 0.25f && rand < 0.5f && j != height - 2) { j++; change = true; }
                else if (rand >= 0.5f && rand < 0.75f && i != 1) { i--; change = true; }
                else if (rand >= 0.75f && j != 1) { j--; change = true; }
            }

            if (IsViablePath(i, j))
            {
                tileMatrix[i, j].GetComponent<Tile>().isPath = 1;
            }
            
        }

    }

    bool IsPath(int i, int j)
    {
        if (i >= height - 1 || i <= 0 || j >= width - 1 || j <= 0)
        {
            return false;
        }
        if (tileMatrix[i, j].GetComponent<Tile>().isPath == 1 && !tileMatrix[i, j].GetComponent<Tile>().visited)
        {
            return true;
        }
        return false;

    }

    // is the tile surrounded by more than one path tile
    bool IsViablePath(int i, int j)
    {
        if (i >= height - 1 || i <= 0 || j >= width - 1|| j <= 0)
        {
            return false;
        }

        int surroundingPaths =
            tileMatrix[i + 1, j].GetComponent<Tile>().isPath +
            tileMatrix[i, j + 1].GetComponent<Tile>().isPath +
            tileMatrix[i - 1, j].GetComponent<Tile>().isPath +
            tileMatrix[i, j - 1].GetComponent<Tile>().isPath;

        if (surroundingPaths == 1 && tileMatrix[i, j].GetComponent<Tile>().isPath != 1)
        {
            return true;
        }

        return false;
    }


    // instantiates floor and tiles for maze
    void InstantiateMaze(int rows, int columns)
    {
        cam.transform.position = new Vector3((width - 1) * 0.55f, Mathf.Max(height, width), (height - 1) * 0.55f);

        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Cube);
        floor.transform.name = "floor";
        floor.transform.position = new Vector3((width - 1) * 0.55f, -0.2f, (height - 1) * 0.55f);
        floor.transform.localScale = new Vector3(width * 1.2f, 0.1f, height * 1.2f);
        var floorRenderer = floor.GetComponent<Renderer>();
        floorRenderer.material = floorMaterial;

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
