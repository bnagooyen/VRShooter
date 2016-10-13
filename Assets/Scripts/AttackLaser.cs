using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(ControllerEvents))]
[RequireComponent(typeof(LaserPointer))]

public class AttackLaser : MonoBehaviour {

    private LaserPointer laserPointer;
    private ControllerEvents controllerEvents;

    private Color laserDefaultColor;
    private float damagePerFrame = 100f;
    private bool damage = false;

    //need a reference for what is being targeted/attacked 
    private EnemyHealth targetHealth;

	void Awake () {
        laserPointer = GetComponent<LaserPointer>();
        controllerEvents = GetComponent<ControllerEvents>();

        laserDefaultColor = laserPointer.color;
        print(laserDefaultColor);
	}
	
	
    void OnEnable()
    {
        controllerEvents.TriggerPressed += HandleTriggerPress;
        controllerEvents.TriggerReleased += HandleTriggerRelease;

    }

    void OnDisable()
    {
        controllerEvents.TriggerPressed -= HandleTriggerPress;
        controllerEvents.TriggerReleased -= HandleTriggerRelease;

    }

    private void HandleTriggerPress(object sender, ControllerEvents.ControllerInteractionEventArgs e)
    {
        laserPointer.enabled = true;
        laserPointer.PointerIn += HandlePointerIn;
        laserPointer.PointerOut += HandlePointerOut;
    }

   

    private void HandleTriggerRelease(object sender, ControllerEvents.ControllerInteractionEventArgs e)
    {
        laserPointer.enabled = false;
        laserPointer.PointerIn -= HandlePointerIn;
        laserPointer.PointerOut -= HandlePointerOut;


    }

    private void HandlePointerIn(object sender, LaserPointer.PointerEventArgs e)
    {
        laserPointer.pointerModel.GetComponent<MeshRenderer>().material.color = Color.red;
        targetHealth = e.target.gameObject.GetComponent<EnemyHealth>();

        damage = true;

        if (targetHealth)
        {
            damage = true;
        }
            
    }

    private void HandlePointerOut(object sender, LaserPointer.PointerEventArgs e)
    {
        laserPointer.pointerModel.GetComponent<MeshRenderer>().material.color = laserDefaultColor;
        targetHealth = null;
        damage = false;
    }


    void Update () {

        if (damage)
        {
            targetHealth.TakeDamage(damagePerFrame);
        }
	
	}
}
