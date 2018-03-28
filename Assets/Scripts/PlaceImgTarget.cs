using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceImgTarget : MonoBehaviour
{
    [SerializeField]
    private Transform mainImgTarget;
    [SerializeField]
    private int maxSimulateTarget = 4;
    [SerializeField]
    private GameController gameControllerScript;

    private GameObject[] placeTargets;
    private OilRig[] oilRigScripts;

    private void Awake()
    {
        placeTargets = new GameObject[maxSimulateTarget];
        oilRigScripts = new OilRig[maxSimulateTarget];
    }
    public void placeTargetOnLand()
    {
        for(int i = 0;i<placeTargets.Length;i++)
        {
            if(placeTargets[i] != null)
            {
                placeTargets[i].transform.parent = mainImgTarget;
                placeTargets[i].transform.localRotation = new Quaternion(-0.7f,0,0,-0.7f);
                oilRigScripts[i] = placeTargets[i].GetComponent<OilRig>();
                gameControllerScript.foundTrackRig = false;
            }
        }
    }
    public void setRefPlaceTarget(GameObject placeTarget,string name)
    {
        switch(name)
        {
            case "Rig1":
                placeTargets[0] = placeTarget;
                break;
            case "Rig2":
                placeTargets[1] = placeTarget;
                break;
            case "Rig3":
                placeTargets[2] = placeTarget;
                break;
            case "Rig4":
                placeTargets[3] = placeTarget;
                break;
        }
    }
    public void deletePlaceTarget(string name)
    {
        switch (name)
        {
            case "Rig1":
                placeTargets[0] = null;
                break;
            case "Rig2":
                placeTargets[1] = null;
                break;
            case "Rig3":
                placeTargets[2] = null;
                break;
            case "Rig4":
                placeTargets[3] = null;
                break;
        }
    }
    public OilRig getOilRigScriptByName(string name)
    {
        OilRig instance = null;

        switch(name)
        {
            case "OilNode1":
                instance = oilRigScripts[0];
                break;
            case "OilNode2":
                instance = oilRigScripts[1];
                break;
            case "OilNode3":
                instance = oilRigScripts[2];
                break;
            case "OilNode4":
                instance = oilRigScripts[3];
                break;

        }
        return instance;
    }
}
