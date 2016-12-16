using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    NORMAL = 0,
    DASH = 1
}

public class Player : MonoBehaviour {

    public PlayerState state;
    public float speed;
    public float dashSpeed;
    public float dashTime;


    private Vector3 lastDirection;
    private float dashTimer = 0f;

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

        switch (state)
        {
            case PlayerState.NORMAL:
                this.transform.position += directionNormalized * speed * Time.deltaTime;

                if (Input.GetButtonDown("Jump"))
                {
                    state = PlayerState.DASH;
                }
                break;

            case PlayerState.DASH:
                this.transform.position += lastDirectionNormalized * dashSpeed * Time.deltaTime;

                dashTimer += Time.deltaTime;
                if (dashTimer > dashTime)
                {
                    dashTimer = 0.0f;
                    state = PlayerState.NORMAL;

                }
                break;
        }

        
	}
}
