using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float destroyTime = 5f;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

