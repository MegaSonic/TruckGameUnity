using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(TruckBehaviour) )]
public class Truck : MonoBehaviour {

    public TruckBehaviour behaviour;

    void Awake()
    {
        behaviour = GetComponent<TruckBehaviour>();
    }


    void Start()
    {

    }

    void Update()
    {
        behaviour.MyUpdate();
    }

    
}
