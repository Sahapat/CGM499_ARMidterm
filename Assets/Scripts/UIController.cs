using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("UI obj")]
    [SerializeField]
    private Button merchant1Btn;
    [SerializeField]
    private Button merchant2Btn;
    [SerializeField]
    private GameObject FocusSelect;
    [SerializeField]
    private GameObject FocusScan;
    [SerializeField]
    private Animator notEnoughMoneyObj;
    [SerializeField]
    private Animator PlaceBtn;
    [SerializeField]
    private Text switchSellStateText;
    [SerializeField]
    private Text switchScanStateText;
    [SerializeField]
    private GameObject newGame;

    [Header("ScriptsReference")]
    [SerializeField]
    private Merchant merchantScritp;

    public bool isOpenScan = false;
    public byte selectedState = 0;

    private void Start()
    {
        isOpenScan = false;
        selectedState = 0;
        updateUI();
    }
    public void setSyncTarget(bool status)
    {
        PlaceBtn.SetBool("isShow", status);
    }
    public void Storage()
    {
        merchantScritp.unSelected();
        updateUI();
    }
    public void newGameShowing()
    {
        newGame.SetActive(!newGame.activeSelf); 
    }
    public void switchFocus()
    {
        if(isOpenScan)
        {
            selectedState = 0;
            isOpenScan = false;
        }
        else
        {
            selectedState = 3;
            isOpenScan = true;
        }
        updateUI();
    }
    public void selectedUI(byte state)
    {
        selectedState = state;
        updateUI();
    }
    public void showNotEnoughMoney()
    {
        notEnoughMoneyObj.SetTrigger("show");
    }
    private void updateUI()
    {
        switch (selectedState)
        {
            case 0:
                FocusScan.SetActive(false);
                FocusSelect.SetActive(true);
                switchScanStateText.text = "Select";
                break;
            case 1:
                FocusScan.SetActive(false);
                FocusSelect.SetActive(true);
                switchScanStateText.text = "Place";
                break;
            case 2:
                FocusScan.SetActive(false);
                FocusSelect.SetActive(true);
                switchScanStateText.text = "Unselect";
                break;
            case 3:
                FocusScan.SetActive(true);
                FocusSelect.SetActive(false);
                switchScanStateText.text = "Scan";
                break;
        }
    }
}
