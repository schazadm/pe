using UnityEngine;

public class SpringCompress : MonoBehaviour
{
    private Rigidbody rb;
    private float timer = 0;
    private float animationRate = 1;
    private float compression = .1f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer < animationRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x - compression, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(transform.position.x + 3 * compression, transform.position.y, transform.position.z);
            timer = 0;
        }
    }
}
