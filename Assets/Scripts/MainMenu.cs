using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Other")]
    [SerializeField]
    private Animator fadeAnim;

    private WaitForSeconds waitForFade;

    private void Awake()
    {
        waitForFade = new WaitForSeconds(0.5f);
    }

    public void LoadScene(int index)
    {
        StartCoroutine(fadeAndLoadScene(index));
    }
    public void openTutorial()
    {

    }
    public void Exit()
    {
        Application.Quit();
    }
    private IEnumerator fadeAndLoadScene(int index)
    {
        fadeAnim.SetTrigger("FadeOut");
        yield return waitForFade;
        SceneManager.LoadScene(index);
    }
}
