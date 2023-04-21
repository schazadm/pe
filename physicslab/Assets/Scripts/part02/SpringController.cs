using UnityEngine;

public class SpringController : MonoBehaviour
{
    public float springConstant;
    public float initialLength;
    public float compressionSpeed;
    private Rigidbody collidingRigidbody;
    private Rigidbody _springRigidbody;
    private bool _isCompressed;
    private bool _isCompressing;

    public GameObject springAnimatedGO;
    private float timer = 0;
    private float animationSpeed = .09f;


    private void Start()
    {
        initialLength = 3f;
        springConstant = 10f;
        compressionSpeed = .1f;
        _springRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.attachedRigidbody) return;
        collidingRigidbody = collider.attachedRigidbody;
        _isCompressed = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.attachedRigidbody)
        {
            _isCompressed = false;
        }
    }

    private void FixedUpdate()
    {
        if (!_isCompressed) return;
        var velocity = collidingRigidbody.velocity;
        // animationSpeed = Mathf.Abs(velocity.x);
        if (velocity.x > 0)
        {
            animateSpring(-compressionSpeed, 3 * compressionSpeed);
        }
        else
        {
            animateSpring(compressionSpeed, -3 * compressionSpeed);
        }

        collidingRigidbody.velocity = new Vector3(velocity.x - compressionSpeed, velocity.y, velocity.z);
        var compressionLength = initialLength - Mathf.Abs(transform.position.x - collidingRigidbody.position.x);
        if (compressionLength < 0) return;
        _isCompressing = compressionLength > 0.01f;
        if (!_isCompressing) return;
        var force = springConstant * compressionLength;
        _springRigidbody.AddForce(force, 0, 0);
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