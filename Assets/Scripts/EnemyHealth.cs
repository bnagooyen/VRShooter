using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;
using System;
using UnityEngine.UI;

//Require VRInteractive Item so that we do not have to keep checking for nulls
[RequireComponent(typeof(VRInteractiveItem))]

public class EnemyHealth : MonoBehaviour {

    
    [SerializeField]
    [Range(20f, 500f)]
    private float health = 100;
    private VRInteractiveItem interactiveItem;
    private bool takingDamage = false;
    private float damagePerFrame = .5f;
    public GameObject deathEffect;
    

	void Awake () {

        interactiveItem = GetComponent<VRInteractiveItem>();

    }
    //Handle OnOver and OnOut which are when the Gaze hits the interactive item 
    void OnEnable()
    {
       
        //When Object is enabled add Handler for gaze detection 
        interactiveItem.OnOver += HandleOver;
        interactiveItem.OnOut += HandleOut;
    }

    // when the gaze laves the interactive item 
    void OnDisable()
    {
        interactiveItem.OnOver -= HandleOver;
        interactiveItem.OnOut -= HandleOut;

    }

    private void HandleOut()
    {
        takingDamage = false; 

    }

    private void HandleOver()
    {
        takingDamage = true; 


    }

   
   


    void Update () {

        if (takingDamage)
        {
            TakeDamage(damagePerFrame);
        }

        if (health <= 0)
        {
            Death();
            
        }

	}

    private void Death()
    {
        //Broadcast that you are in fact no longer gazing at the interactive item.  Though something dies and gameObject destroyed, those subscribed don't necessarily know that the object is "out"
        
        interactiveItem.Out();
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
        

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
