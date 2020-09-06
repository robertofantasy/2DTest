using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Test : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] RayBuffer = new RaycastHit2D[16];
    protected float ShellRadius = 0.001f;
    public Collider2D sdd;
    public Vector3 start;
    public Vector3 End;


    public float GravityModifier = 1;
    public float MinDistance = 0.001f;

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
        sdd = this.gameObject.GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        velocity += Physics2D.gravity * GravityModifier * Time.deltaTime;
        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 move = Vector2.up * deltaPosition.y;
        Movement(move);
        //Debug.DrawLine(new Vector2(0,0), new Vector2(5,5), Color.red);
        
;    }


    private void Update()
    {
        
        Debug.DrawLine(start, End, Color.red, 100f);
    }

    private void Movement(Vector2 move)
    {
        float Distance = move.magnitude;
        if(Distance > MinDistance)
        {
           int count = rb2d.Cast(move, contactFilter, RayBuffer, Distance + ShellRadius);
        }
        rb2d.position =rb2d.position + move;
    }
}