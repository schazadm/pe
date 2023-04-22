using UnityEngine;

public class SpringController : MonoBehaviour
{
    public float springConstant;
    public float initialLength;
    public float compressionSpeed;
    private Rigidbody collidingRigidbody;
    private Rigidbody springRigidbody;
    private bool isCompressed;
    private bool isCompressing;

    public GameObject springAnimatedGO;
    private float timer = 0;
    private float animationSpeed = .09f;


    private void Start()
    {
        // initialLength = 5f;
        // springConstant = 1000f;
        // compressionSpeed = .1f;
        springRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.attachedRigidbody) return;
        collidingRigidbody = collider.attachedRigidbody;
        isCompressed = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.attachedRigidbody)
        {
            isCompressed = false;
        }
    }

    private void FixedUpdate()
    {
        if (!isCompressed) return;
        Vector3 velocity = collidingRigidbody.velocity;
        if (velocity.x > 0)
        {
            animateSpring(-compressionSpeed, 3 * compressionSpeed);
        }
        else
        {
            animateSpring(compressionSpeed, -3 * compressionSpeed);
        }
        collidingRigidbody.velocity = new Vector3(velocity.x - compressionSpeed, velocity.y, velocity.z);
        float compressionLength = initialLength - Mathf.Abs(transform.position.x - collidingRigidbody.position.x);
        if (compressionLength < 0) return;
        isCompressing = compressionLength > 0.01f;
        if (!isCompressing) return;
        float force = -springConstant * compressionLength;
        springRigidbody.AddForce(force, 0, 0);
    }

    private void animateSpring(float scale, float pos)
    {
        if (timer < animationSpeed)
        {
            timer += Time.deltaTime;
        }
        else
        {
            springAnimatedGO.transform.localScale += new Vector3(scale, 0, 0);
            springAnimatedGO.transform.position += new Vector3(pos, 0, 0);
            timer = 0;
        }
    }
}