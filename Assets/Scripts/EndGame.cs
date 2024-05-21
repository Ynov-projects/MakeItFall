using UnityEngine;
using UnityEngine.EventSystems;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            endGamePanel.SetActive(true);
            if(GameManager.lastDevice.name != "Keyboard") EventSystem.current.SetSelectedGameObject(endGamePanel.transform.GetChild(0).gameObject);
            Time.timeScale = 0f;
            PlayerMovement.Instance.enabled = false;
        }
    }
}
