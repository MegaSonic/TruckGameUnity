using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public enum PlayerState
{
    NORMAL = 0,
    DASH = 1
}

public class Player : MonoBehaviour {

    public PlayerState state;
    public float speed;
    public float dashSpeed;
    public float dashLength;
    public float dashCooldown;

    #region Privates
    private Vector3 lastDirection;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    private Rigidbody2D rigid;
    private CharacterController2D _controller;

    #endregion

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        _controller = GetComponent<CharacterController2D>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, v, 0f);

        if (direction != Vector3.zero) lastDirection = direction;

        Vector3 directionNormalized = direction.normalized;
        Vector3 lastDirectionNormalized = lastDirection.normalized;

        float angle = Mathf.Atan2(lastDirection.y, lastDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        dashCooldownTimer += Time.deltaTime;

        switch (state)
        {
            case PlayerState.NORMAL:
                _controller.move(directionNormalized * speed * Time.deltaTime);
                // rigid.MovePosition(this.transform.position + directionNormalized * speed * Time.deltaTime);

                if (Input.GetButtonDown("Jump") && dashCooldownTimer > dashCooldown)
                {
                    state = PlayerState.DASH;
                }
                break;

            case PlayerState.DASH:
                _controller.move(lastDirectionNormalized * dashSpeed * Time.deltaTime);
                // rigid.MovePosition(this.transform.position + lastDirectionNormalized * dashSpeed * Time.deltaTime);

                dashTimer += Time.deltaTime;
                if (dashTimer > dashLength)
                {
                    dashTimer = 0.0f;
                    state = PlayerState.NORMAL;
                    dashCooldownTimer = 0.0f;

                }
                break;
        }

        
	}
}
