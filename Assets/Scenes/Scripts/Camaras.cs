using UnityEngine;

public class Camaras : MonoBehaviour
{
    public Transform target;

    public float yOffset = 6.0f;
    private void LateUpdate()
    {
        float desiredY = target.position.y + yOffset;
        transform.position = new Vector3(target.position.x, desiredY, transform.position.z);

    }
}