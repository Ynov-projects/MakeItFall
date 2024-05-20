using UnityEngine;
using UnityEngine.EventSystems;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;

    private void OnTriggerEnter(Collider other)
    {
        endGamePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(endGamePanel.transform.GetChild(0).gameObject);
    }
}
