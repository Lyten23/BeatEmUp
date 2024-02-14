using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovement : MonoBehaviour
{
    #region Variables
    [Header("References")] 
    public Rigidbody2D rb2d;
    public Collider2D moveCollider;
    public Transform characterBody;
    [Header("Movement")]
    [SerializeField] private float horizontalVelocity= 5;
    [SerializeField] private float verticalVelocity = 3;
    public Area moveArea;
    [Header("Jump")] public float jumpForce = 5f;
    [Header("Physics")] 
    [SerializeField] private float gravityForce = -9.8f;
    [Header("Privates")] 
    [SerializeField] private bool canMove = true;
    [SerializeField] private Vector2 velocity;
    [SerializeField] private Vector2 bodyMovementStep;
    [SerializeField] private bool canJump;
    public ParticleSystem particleSystem;


    public Vector2 FaceDirection
    {
        get { return new Vector2(transform.localScale.x, 0); }
    }
    public Vector2 GroundVelocity
    {
        get { return velocity; }
    }
    public Vector2 VerticalVelocity
    {
        get { return bodyMovementStep / Time.fixedDeltaTime; }
    }
    public bool IsGrounded
    {
        get { return canJump; }
    }
    [Header("Jump")]
    public float jumpUp = 35;
    public float gravityScale = 10;
    public float fallingGravityScale = 40;
    #endregion
    void Start()
    {
        
    } 
    void Update()
    {
        /*float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        Move(xInput,yInput);*/
        
        /*if (canJump && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        //Movement();
        animator.SetFloat("VerticalVelocity", _bodyVelocity.normalized.y );*/
    }
    private void FixedUpdate()
    {
        rb2d.velocity = velocity;
        FixPosition();
        ApplyGravity();
        ApplyBodyMovement();
    }
    /// <summary>
    /// Método que actualiza la velocidad según el movimiento indicado.
    /// </summary>
    /// <param name="horizontalMovement"></param>
    /// <param name="verticalMovement"></param>
    public void Move(float horizontalMovement, float verticalMovement)
    {
        Vector2 input = new Vector2(horizontalMovement, verticalMovement);
        velocity = input.normalized * new Vector2(horizontalVelocity, verticalVelocity);
        // Flip scale
        Vector2 scale = transform.localScale;
        scale.y = 0;
        if (Vector2.Dot(scale,velocity.normalized)<0)
        {
            FlipScale();
        }
    }

    public void SetVelocity(Vector2 groundVelocity, float verticalVelocity)
    {
        velocity = groundVelocity;
        if (verticalVelocity>0)
        {
            bodyMovementStep = new Vector2(0f, verticalVelocity) * Time.fixedDeltaTime;
            canJump = false;
        }
    }
    /// <summary>
    /// Mueve al personaje en z para solucionar el problema de la profundidad con los elementos bidimensionales.
    /// </summary>
    private void Fixz()
    {
        Vector3 position = transform.position;
        position.z = position.y;
        transform.position = position;
    }
    /// <summary>
    /// Invierte la escala en X
    /// </summary>
    void FlipScale()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    /// <summary>
    /// Arreglo de la posición
    /// </summary>
    public void FixPosition()
    {
        if (moveArea==null) return;
        {
            // Si !moveArea, el componente vector2 viene relleno y con un valor
            if (!moveArea.IsInArea(moveCollider, out Vector2 fixedDirection))
            {
                // Si es moveArea arreglamos la posición
                rb2d.position += fixedDirection;
            }
        }
    }
    /*void Movement()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(xInput, yInput, 0);
        direction.Normalize();
        if (xInput != 0)
        {
            transform.position += direction * (speedX * Time.deltaTime);
            gameObject.transform.localScale = new Vector3(xInput > 0 ? 1 : -1,1,1);
        }
        else if (yInput != 0)
        {
            transform.position += direction * (speedY * Time.deltaTime);
        }
    }*/
    private void LateUpdate()
    {
        Fixz();
        //UpdateAnimation();
    }
    public void Jump()
    {
        bodyMovementStep = new Vector2(0f, jumpForce) * Time.fixedDeltaTime;
        canJump = false;
        
    }
    private void ApplyGravity()
    {
        Vector2 position = characterBody.localPosition;
        if (position.y>0f)
        {
            //En el aire
            // Fórmula de la aceleracióm en este caso: a=m*s2
            bodyMovementStep.y += gravityForce * MathF.Pow(Time.fixedDeltaTime,2);
        }else if (!canJump && bodyMovementStep.y<=0f && position.y<=0)
        {
            //En el suelo
            position.y = 0f;
            characterBody.localPosition = position;
            bodyMovementStep=Vector2.zero;
            canJump = true;
        }
    }
    private void ApplyBodyMovement()
    {
        characterBody.localPosition += (Vector3)bodyMovementStep;
    }
    /*private void UpdateAnimation()
    {
        /*if (velocity.magnitude>0.1f)
        {
            animator.Play("Walk");
        } else if (!canJump)
        {
            animator.SetBool("CanMove",false);
            animator.SetBool("Jumping",true);
            animator.SetFloat("VerticalVelocity", velocity.y);
        }
        else
        {
            animator.Play("Idle");
        }
    }*/
}
