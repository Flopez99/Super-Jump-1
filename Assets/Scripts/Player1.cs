using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player1 : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    private int superJumpsRemaining;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    [SerializeField] private Transform groundCheckTransform = null;
    [SerializeField] private LayerMask playerMask;

    private float playerSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
      rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate(){

        rigidbodyComponent.velocity = new Vector3(horizontalInput * playerSpeed, rigidbodyComponent.velocity.y, 0);   
        if(Physics.OverlapSphere(groundCheckTransform.position, 0.1f,playerMask).Length == 0){
            return;
        }

        if(jumpKeyWasPressed){

            float jumpPower = 5f;
            if(superJumpsRemaining > 0){
                jumpPower *= 2;
                superJumpsRemaining--;
            }

            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

        scoreText.text = superJumpsRemaining.ToString();

       
    }

    private void OnTriggerEnter(Collider other){

        if(other.gameObject.layer == 7){
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }

    }

}

