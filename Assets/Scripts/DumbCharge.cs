using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class DumbCharge : TruckBehaviour {

    public float speed;
    public Transform target;
    public Vector3 vector;

    public bool passedThroughWall = false;
    public bool destroyed = false;
    public bool isCollidingWithWall;
    public bool wasCollidingWithWall;

    private CharacterController2D _controller;

    

    // Use this for initialization
    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        vector = target.transform.position - this.transform.position;
        _controller = GetComponent<CharacterController2D>();
        transform.right = target.position - transform.position;
        _controller.onTriggerEnterEvent += CollideWithWalls;
        _controller.onTriggerStayEvent += IsCollidingWithWall;
        vector = vector.normalized;
	}

    public override void MyUpdate()
    {
        wasCollidingWithWall = isCollidingWithWall;

        if (!passedThroughWall)
        {
            if (this.transform.position.x > Camera.main.BoundsMin().x && this.transform.position.y > Camera.main.BoundsMin().y)
            {
                if (this.transform.position.x < Camera.main.BoundsMax().x && this.transform.position.y < Camera.main.BoundsMax().y)
                {

                    passedThroughWall = true;
                }
            }
        }
        

        if (!destroyed)
        {
            _controller.move(vector * speed * Time.deltaTime);
        }
    }




    public void CollideWithWalls(Collider2D col)
    {
        Debug.Log("Fire collision!");
        if (col.tag == "Wall")
        {
            if (passedThroughWall) { 
                destroyed = true;
                Debug.Log("Wrecked!");
            }
        }
    }

    public void IsCollidingWithWall(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            isCollidingWithWall = true;
        }
        else
        {
            isCollidingWithWall = false;
        }

        
    }

}
