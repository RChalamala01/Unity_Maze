using UnityEngine;

public class Tile : MonoBehaviour
{
    Renderer tileRenderer;
    public Material path;
    public Material visit;
    public Material wall;
    
    public int isPath = 0;
    public bool visited = false;


    private void Start()
    {
        tileRenderer = gameObject.GetComponent<Renderer>();
    }


    private void Update()
    {
        if (isPath == 1)
        {
            tileRenderer.material = path;
        }
        else
        {
            tileRenderer.material = wall;
        }

        if (visited)
        {
            tileRenderer.material = visit;
        }
    }


    /*
    private void OnMouseEnter()
    {
        if (!isWall)
        {
            tileRenderer.material = hover;
        }
    }

    private void OnMouseExit()
    {
        if (!isWall)
        {
            tileRenderer.material = normal;
        }
        
    }

    private void OnMouseDown()
    {
        isWall = !isWall;
        if (isWall)
        {
            tileRenderer.material = selected;
        }
        else
        {
            tileRenderer.material = normal;
        }
    }
    */

}
