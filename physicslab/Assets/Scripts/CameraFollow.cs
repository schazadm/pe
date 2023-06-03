using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothness;
    public Transform targetObject;
    private Vector3 initialOffset;
    private Vector3 cameraPosition;

    public enum RelativePosition { InitialPosition, Position1, Position2 }
    public RelativePosition relativePosition;
    public Vector3 position1;
    public Vector3 position2;

    void Start()
    {
        relativePosition = RelativePosition.InitialPosition;
        initialOffset = transform.position - targetObject.position;
    }

    void FixedUpdate()
    {
        cameraPosition = targetObject.position + CameraOffset(relativePosition);
        transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothness * Time.fixedDeltaTime);
        transform.LookAt(targetObject);
    }

    Vector3 CameraOffset(RelativePosition relativePos)
    {
        Vector3 currentOffset;

        switch (relativePos)
        {
            case RelativePosition.Position1:
                currentOffset = position1;
                break;

            case RelativePosition.Position2:
                currentOffset = position2;
                break;

            default:
                currentOffset = initialOffset;
                break;
        }
        return currentOffset;
    }
}