using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTergetTrackHandler : DefaultTrackableEventHandler
{
    [SerializeField]
    private trackableObj objType;
    [SerializeField]
    private GameController gameControllerScript;

    [Header("for Rig imgTaget")]
    [SerializeField]
    private PlaceImgTarget placeImgTargetScript;
    [SerializeField]
    private GameObject targetPlace;
    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        switch(objType)
        {
            case trackableObj.Land:
                gameControllerScript.isGamePause = false;
                gameControllerScript.foundTrackMain = true;
                break;
            case trackableObj.Rig:
                if (gameObject.transform.childCount != 0)
                {
                    gameControllerScript.foundTrackRig = true;
                    placeImgTargetScript.setRefPlaceTarget(targetPlace, this.gameObject.name);
                }
                break;
        }
    }
    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        switch (objType)
        {
            case trackableObj.Land:
                gameControllerScript.isGamePause = true;
                gameControllerScript.foundTrackMain = false;
                break;
            case trackableObj.Rig:
                if (gameObject.transform.childCount != 0)
                {
                    gameControllerScript.foundTrackRig = false;
                    placeImgTargetScript.deletePlaceTarget(this.gameObject.name);
                }
                break;
        }
    }
}
