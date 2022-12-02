using UnityEngine;

public class FireController : MonoBehaviour
{
    public float size = 3;
    public float sizeGrow = 0.1f;
    public float sizeMax = 4;


    private void Start()
    {
        name = "FireRing";
        transform.localScale = new Vector3(size,size,size);
    }

    private void Update()
    {
        transform.localScale = new Vector3(size, size, size);
        size += sizeGrow;
        if (size > sizeMax)
            Destroy(gameObject);
        transform.position = GameObject.Find("Player1").transform.position;
    }
}
