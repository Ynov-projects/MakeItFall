using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LauchingManager : MonoBehaviour
{
    [SerializeField] private float waitingTime;
    [SerializeField] private GameObject playButton;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(playButton);
    }

    public void PlayButton()
    {
        StartCoroutine(Play());
    }

    private IEnumerator Play()
    {
        yield return new WaitForSeconds(waitingTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitButton()
    {
        StartCoroutine(Quit());
    }

    private IEnumerator Quit()
    {
        yield return new WaitForSeconds(waitingTime);
        Application.Quit();
    }
}
