using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    public GameObject MenuPanel,blockerImage,finalMessageImage;
    public AudioSource source;
    public AudioClip clip;
    public bool defeated=false;

    public void StartGame()
    {
        source.PlayOneShot(clip);
        StartCoroutine(DelayedSceneLoader("GameScene"));
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        source.PlayOneShot(clip);
        StartCoroutine(DelayedSceneLoader("GameScene"));
    }
    public void LoadCreditsScene()
    {
        source.PlayOneShot(clip);
        StartCoroutine(DelayedSceneLoader("CreditsScene"));
    }
    public void LoadStartMenu()
    {
        Time.timeScale = 1f;
        source.PlayOneShot(clip);
        StartCoroutine(DelayedSceneLoader("StartScene"));
    }
    public void QuitGame()
    {
        source.PlayOneShot(clip);
        StartCoroutine("DelayedQuitter");
        
    }
    public void LoadMenu()
    {
        source.PlayOneShot(clip);
        MenuPanel.gameObject.SetActive(true);
        blockerImage.gameObject.SetActive(true);
        
        Time.timeScale = 0f;
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
        }
    }
    public void CloseMenu()
    {
        source.PlayOneShot(clip);
        MenuPanel.gameObject.SetActive(false);
        blockerImage.SetActive(false);
        Time.timeScale = 1f;
    }
    public void CloseCreditsScene()
    {
        source.PlayOneShot(clip);
        StartCoroutine(DelayedSceneLoader("StartScene"));
    }

    public IEnumerator DelayedSceneLoader(string sceneName)
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(sceneName);
    }
    public IEnumerator DelayedQuitter()
    {
        yield return new WaitForSeconds(0.4f);
        Application.Quit();
    }
    private void Update()
    {
        //Requirements for ending Game
        //Upgrade town hall to level 2
        //Upgrade barracks to level 2
        //Defeat enemies
        if(SceneManager.GetActiveScene()== SceneManager.GetSceneByName("GameScene")) 
        {
            if (GameObject.Find("Town Hall").transform.GetComponent<UI_BuildingMenu>().baseLevel == 1 && GameObject.Find("Barracks").transform.GetComponent<UI_BuildingMenu>().baseLevel == 1 && defeated)
            {
                finalMessageImage.SetActive(true);
            }
        }
    }
}
