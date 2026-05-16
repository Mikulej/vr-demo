using UnityEngine;

public class Ball : Pickable
{
    public Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }
}
