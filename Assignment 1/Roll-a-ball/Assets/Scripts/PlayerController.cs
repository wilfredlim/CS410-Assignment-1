using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private float jumpCount;
    public float speed = 0;
    public float jumpForce = 2;
    public Vector3 jump;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent <Rigidbody> ();
        jump = new Vector3(0, 2, 0);
        count = 0;
        jumpCount = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }
    
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    private void Update()
    {
        if(isGrounded)
        {
            jumpCount = 0;
        }
        
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded && (jumpCount <=2))
        {
            jumpCount++;
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }


        
            // rb.AddForce(jump * jumpForce * Time.deltaTime, ForceMode.Impulse);
            // isGrounded = false;
        
        Reset();
    }
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
        other.gameObject.SetActive(false);
        count = count + 1;
        SetCountText();
        }
    }

    void Reset()
    {
        if(transform.position.y < -2)
        {
            Debug.Log("reset");
            transform.position =  new Vector3(0f,0f,0f);
        }
    }

    
}
