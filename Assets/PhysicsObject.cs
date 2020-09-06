using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float gravityModifyer = 1f;
    public float minGroundNormalY = .65f;

    protected bool Grounded;
    protected Vector2 GroundNormal;
    protected Vector2 velocity;
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitbufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    private void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
        
    }


    // Update is called once per frame


    private void FixedUpdate()
    {
        velocity += Physics2D.gravity * gravityModifyer * Time.deltaTime;
        Vector2 deltaposition = velocity * Time.deltaTime;
        Vector2 move = Vector2.up * deltaposition.y;
        Grounded = false;

        Movement(move, true);

        
    }

    void Movement(Vector2 move, bool yMovemeny)
    {
        float distance = move.magnitude;

        if(distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitbufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitbufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitbufferList.Count; i++)
            {
                Vector2 currentNormal = hitbufferList[i].normal;
                if(currentNormal.y < minGroundNormalY)
                {
                    Grounded = true;

                    if(yMovemeny)
                    {
                        GroundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
            }
            
        }

        rb2d.position += rb2d.position + move;

    }
}
