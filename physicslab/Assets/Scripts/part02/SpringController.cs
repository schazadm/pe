using UnityEngine;

public class SpringController : MonoBehaviour
{
    public float springConstant;
    public float springRestingLength;
    public float compressionSpeed;
    private float springForce;
    private Rigidbody rb;
    private Rigidbody collidingCubRb;
    private bool isCompressed;

    // animation
    public GameObject springAnimatedGO;
    private float timer = 0;
    private float animationSpeed = .09f;


    private void Start()
    {
        // initialLength = 5f;
        // springConstant = 1000f;
        // compressionSpeed = .1f;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.attachedRigidbody) return;
        collidingCubRb = collider.attachedRigidbody;
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
        Vector3 velocity = collidingCubRb.velocity;
        float posXDelta = Mathf.Abs(transform.position.x - collidingCubRb.position.x);
        float compressionLength = springRestingLength - posXDelta;
        springForce = -springConstant * compressionLength;
        collidingCubRb.AddForce(springForce, 0, 0);

        if (velocity.x > 0)
        {
            animateSpring(-compressionSpeed, 3 * compressionSpeed);
        }
        else
        {
            animateSpring(compressionSpeed, -3 * compressionSpeed);
        }
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