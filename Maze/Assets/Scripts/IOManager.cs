using UnityEngine;

public class IOManager : MonoBehaviour
{

    public Renderer renderer;
    public Material normal;
    public Material hover;
    public Material selected;

    private void OnMouseEnter()
    {
        renderer.material = selected;
    }

    private void OnMouseExit()
    {
        //renderer.material = normal;
    }

    private void OnMouseDown()
    {
        //renderer.material = selected;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            renderer.material = normal;
        }

    }

}
