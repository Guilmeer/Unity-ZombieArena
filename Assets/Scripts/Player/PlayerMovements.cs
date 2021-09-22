using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Player playerObj;
    Rigidbody rb;

    [Header("Movement related")]
    [SerializeField] float speed;
    private float speedCut = 1;
    [SerializeField] float lookSpeed;
    Vector3 mouseDirection;
    Vector3 directionToMove;
    Vector3 ySpeed;
    Quaternion targetRotation;
    Quaternion actualRotation;
    bool canDash = true;
    [SerializeField] float dashDelay = 5;
    delegate void MovementDelegate();

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        playerObj = GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        if (!playerObj.Dead)
        {
            LookToCursor();
            // Move Player
            rb.velocity = directionToMove * (speed * speedCut) + ySpeed;
        }
    }
    void Update()
    {
        if (!playerObj.Dead)
        {
            // MovementDelegate can have all functions needed
            MovementDelegate movementDelegate = null;
            movementDelegate += ManageMovement;

            // with LeftShift pressed the Dash function will be called with any other function
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !PlayerAnim.instance.Aiming)
                movementDelegate += Dash;
            movementDelegate();

        }
    }

    // LookToCursor sets variables related to rotation and cursor
    // like mouseDirection, and make character look at the cursor position
    private void LookToCursor()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("FloorRaycast");

        if (Physics.Raycast(mouseRay, out hit, Mathf.Infinity, layerMask))
        {
            mouseDirection = -(transform.position - hit.point);
            Vector3 mouseDirNormalized = mouseDirection.normalized;

            targetRotation = Quaternion.LookRotation(new Vector3(mouseDirNormalized.x, 0, mouseDirNormalized.z));
            actualRotation = Quaternion.Lerp(transform.rotation, targetRotation, lookSpeed);
            transform.rotation = actualRotation;
        }
    }

    // Manages movement related variables. 
    private void ManageMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            PlayerAnim.instance.Running = true;
        }
        else { PlayerAnim.instance.Running = false; }

        directionToMove = new Vector3(horizontal, 0, vertical).normalized;
        ySpeed = new Vector3(0, rb.velocity.y, 0);
    }

    // Manage Dash
    private void Dash()
    {
        canDash = false;
        StartCoroutine(DashCoroutine());
    }
    IEnumerator DashCoroutine()
    {
        float basicspeed = speed;
        speed *= 3;
        yield return new WaitForSeconds(0.2f);
        speed = basicspeed;
        yield return new WaitForSeconds(dashDelay);
        canDash = true;
    }

    public Vector3 GetMouseDirection()
    {
        return mouseDirection;
    }

    public Quaternion GetPlayerRotation()
    {
        return transform.rotation;
    }

    // Sets what percentage of the total player speed will be used (like for aiming down/walking)
    public void SetSpeedAmount(float speedPercentage)
    {
        this.speedCut = speedPercentage;
    }
}