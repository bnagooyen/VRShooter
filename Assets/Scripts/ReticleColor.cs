using UnityEngine;
using System.Collections;
using VRStandardAssets.Utils;
using UnityEngine.UI;
using System;

public class ReticleColor : MonoBehaviour {

    [SerializeField]  private VREyeRaycaster eyeRaycaster;
    [SerializeField]  private Image reticleImage;
    private VRInteractiveItem interactiveItem;
    private Color originalReticleColor;
    
	void Awake () {

       
         originalReticleColor = reticleImage.color;
        
        
	 }

    void OnEnable()
    {
        
        eyeRaycaster.OnRaycasthit += hittingObj;

    }

    void OnDisable()
    {
        eyeRaycaster.OnRaycasthit -= hittingObj;

    }

    private void hittingObj(RaycastHit obj)
    {

        if (interactiveItem)
            return;
        

        interactiveItem = obj.collider.GetComponent<VRInteractiveItem>();

        if (interactiveItem)
        {
         
            reticleImage.color = Color.red;
            interactiveItem.OnOut += HandleOut;
        }

    }

    private void HandleOut()
    {
        
        reticleImage.color = originalReticleColor;
        interactiveItem.OnOut -= HandleOut;
        interactiveItem = null;
    }



    
}
