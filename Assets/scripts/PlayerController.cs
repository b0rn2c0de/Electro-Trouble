using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    Animator animator;
    Rigidbody2D rigidbody;
    SpriteRenderer sr;
    public LayerMask CastLayers;
    int layermask = 0;
    Vector2 CastPoint;
    Vector2 Direction;
    Camera cam;
    public float VelX = 1, VelY = 1,VelScale = 3;
	void Start () 
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;
        CreateLayerMask();
	}
	
	void Update () 
    {
        Move();
        interact();
	}

    void Move()
    {
        VelX = Input.GetAxisRaw("Horizontal");
        VelY = Input.GetAxisRaw("Vertical");

        rigidbody.velocity = new Vector2(VelX, VelY);
        rigidbody.velocity.Normalize();
        rigidbody.velocity *= VelScale;

        bool IsWalking = (Mathf.Abs(VelX) + Mathf.Abs(VelY)) > 0;
        animator.SetBool("IsWalking",IsWalking);

        if (IsWalking)
        {
            animator.SetFloat("VelY", VelY);
            animator.SetFloat("VelX", VelX);
            if (Mathf.Abs(VelX) > 0)
                Direction = new Vector2(VelX, 0);
            else
                Direction = new Vector2(0, VelY);
        }
        cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    void interact()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CastPoint = new Vector2(transform.position.x + sr.bounds.extents.x * Direction.x, transform.position.y + sr.bounds.extents.y * Direction.y);
            RaycastHit2D hit = Physics2D.Raycast(CastPoint, Direction, 0.16f,CastLayers);
            Vector2 end = CastPoint + Direction * 0.32f;
            Debug.DrawLine(CastPoint, end, Color.red);
            if (hit != null)
            {
                Debug.Log(hit.collider.name);
                (hit.collider.gameObject.GetComponent<electronic>()).ToggleOn(false);
            }

        }
    }

    void CreateLayerMask()
    {
        Debug.Log(CastLayers);
    }
}
