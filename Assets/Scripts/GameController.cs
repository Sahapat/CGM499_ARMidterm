using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum trackableObj
{
    Land,
    Rig
};

public class GameController : MonoBehaviour
{
    public bool isGamePause = false;
    public bool isGameStart = false;
    
    [Header("Mask")]
    [SerializeField]
    private GameObject maskobj;
    [SerializeField]
    private Transform mainObj;
    [Header("Textobj")]
    [SerializeField]
    private Text timeText;

    [Header("ScriptsReference")]
    [SerializeField]
    private GameTime gameTimeScript;
    [SerializeField]
    private UIController uIControllerScript;
    [SerializeField]
    private OilManager oilManagerScript;
    [SerializeField]
    private moneyManager moneyManagerScript;
    [SerializeField]
    private Merchant merchantScript;
    [SerializeField]
    private PlaceImgTarget placeImgTargetScript;

    [Header("Other")]
    [SerializeField]
    private Animator pauseAnim;

    private Vector3 placePos;
    private RaycastHit hit;
    private OilRig currentNode;
    private bool isTouchSomething = false;
    private bool isSelectNode = false;
    private bool canScan = false;
    private bool canSelectNode = false;
    private bool isSetCount = false;

    public bool foundTrackMain;
    public bool foundTrackRig;

    private void Start()
    {
        gameTimeScript.minute = 3;
        oilManagerScript.oilStorage = 0;
        moneyManagerScript.moneyStorage = 500;
        merchantScript.InitTrend();
        merchantScript.unSelected();
        merchantScript.initmerchantprice(6, 6);
        isGameStart = true;
        isGamePause = true;
    }
    private void Update()
    {
        if (isGameStart)
        {
            if (!isGamePause)
            {
                if(gameTimeScript.minute < 0)
                {
                    gameTimeScript.stopCount();
                    gameTimeScript.minute = 0;
                    isGameStart = false;
                    uIControllerScript.endGameShowing(moneyManagerScript.moneyStorage);
                }
                if (!isSetCount)
                {
                    gameTimeScript.startCount(-1);
                    isSetCount = true;
                }
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
                isTouchSomething = (Physics.Raycast(ray, out hit)) ? true : false;
                if (isTouchSomething)
                {
                    switch (hit.collider.tag)
                    {
                        case "FrontLand":
                            canScan = true;
                            if(isSelectNode)
                            {
                                uIControllerScript.selectedUI(1);
                                if (currentNode != null)
                                {
                                    currentNode.setIsSelectNode(1);
                                }
                                canSelectNode = true;
                            }
                            else
                            {
                                if (currentNode != null)
                                {
                                    currentNode.setIsSelectNode(0);
                                }
                            }
                            break;
                        case "OilNode":
                            if (!isSelectNode)
                            {
                                currentNode = placeImgTargetScript.getOilRigScriptByName(hit.collider.name);
                                if (!currentNode.isPlaceNode)
                                {
                                    currentNode.setIsSelectNode(1);
                                    canSelectNode = true;
                                }
                                else
                                {
                                    currentNode.setIsSelectNode(2);
                                    currentNode = null;
                                }
                            }
                            break;
                    }
                }
                else
                {
                    if(isSelectNode)
                    {
                        uIControllerScript.selectedUI(2);
                    }
                    else
                    {
                        if (currentNode != null)
                        {
                            currentNode.setIsSelectNode(0);
                        }
                    }
                    canScan = false;
                    canSelectNode = false;
                }
                merchantScript.merchantPriceUpdate();

                if (merchantScript.selected != selectedFactory.None)
                {
                    int tranferingOil = oilManagerScript.tranferOilForSell();
                    if (tranferingOil != -1)
                    {
                        moneyManagerScript.moneyStorage += merchantScript.sellingOil(tranferingOil);
                    }
                }
                checkSyncTarget();
            }
            else
            {
                gameTimeScript.stopCount();
                isSetCount = false;
            }
            pauseAnim.SetBool("isPause", isGamePause);
            updateText();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            uIControllerScript.newGameShowing();
        }
    }
    public void ScanAndSelect()
    {
        if (uIControllerScript.isOpenScan)
        {
            if (canScan)
            {
                if (moneyManagerScript.cutOffMoney(100))
                {
                    GameObject temp = Instantiate(maskobj, new Vector3(hit.point.x, hit.point.y), Quaternion.identity);
                    temp.transform.parent = mainObj;
                    temp.transform.localPosition = new Vector3(temp.transform.localPosition.x, 6, temp.transform.localPosition.z);
                    uIControllerScript.switchFocus();
                }
                else
                {
                    uIControllerScript.showNotEnoughMoney();
                    uIControllerScript.switchFocus();
                }
            }
        }
        else
        {
            if (canSelectNode)
            {
                if (isSelectNode)
                {
                    GameObject temp = Instantiate(currentNode.getEndNode(), new Vector3(hit.point.x, hit.point.y), Quaternion.identity);
                    temp.transform.parent = mainObj;
                    temp.transform.localPosition = new Vector3(temp.transform.localPosition.x, 6, temp.transform.localPosition.z);
                    currentNode.oilRigPlaceNode(temp);
                    currentNode.setIsSelectNode(2);
                    uIControllerScript.selectedUI(0);
                    currentNode = null;
                    isSelectNode = false;
                }
                else
                {
                    isSelectNode = true;
                    uIControllerScript.selectedUI(1);
                    if (currentNode != null)
                    {
                        currentNode.setIsSelectNode(1);
                    }
                }
            }
            else
            {
                isSelectNode = false;
                uIControllerScript.selectedUI(0);
            }
        }
    }
    private void updateText()
    {
        string timeTxt = string.Empty;
        timeTxt += gameTimeScript.minute + ":" + gameTimeScript.second;
        timeText.text = timeTxt;
    }
    private void checkSyncTarget()
    {
        if(foundTrackMain && foundTrackRig)
        {
            uIControllerScript.setSyncTarget(true);
        }
        else
        {
            uIControllerScript.setSyncTarget(false);
        }
    }
    public void newGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
