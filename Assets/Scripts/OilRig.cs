using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilRig : MonoBehaviour
{
    [SerializeField]
    private Sprite[] OilNode;
    [SerializeField]
    private SpriteRenderer spriteNodeRenderer;
    [SerializeField]
    private Animator oilRigAnim;
    [SerializeField]
    private OilManager oilManagerScript;
    [SerializeField]
    private GameController gameControllerScripts;

    [SerializeField]
    private GameObject OilNodePrefab;

    public bool isPlaceNode;
    private bool isWorking;
    private GameObject endNode;
    private WaitForSeconds waitPerFrame;
    private Oil riging;
    private float offsetSize;
    private void Start()
    {
        waitPerFrame = new WaitForSeconds(0.1f);
        setIsSelectNode(0);
    }
    private void Update()
    {
        if (!gameControllerScripts.isGamePause)
        {
            if (isWorking)
            {
                int oilInCome = riging.getOil();
                oilRigAnim.SetBool("isWorking", true);
                if (oilInCome > 0)
                {
                    oilManagerScript.oilStorage += oilInCome;
                }
                else
                {
                    oilRigAnim.SetBool("isWorking", false);
                }
            }
        }
    }
    public void setIsSelectNode(byte state)
    {
        spriteNodeRenderer.sprite = OilNode[state];
    }
    public void setOilWorking(bool status,Oil place)
    {
        oilRigAnim.SetBool("isWorking", status);
        riging = place;
        isWorking = status;
    }
    public GameObject getEndNode()
    {
        return OilNodePrefab;
    }
    public void oilRigPlaceNode(GameObject endNode)
    {
        isPlaceNode = true;
        this.endNode = endNode;
        this.endNode.GetComponent<EndNode>().whoSend = this;
    }
    private IEnumerator RigOilStart()
    {
        yield return waitPerFrame;
    }
}
