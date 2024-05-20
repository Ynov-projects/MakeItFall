using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;

    private void OnTriggerEnter(Collider other)
    {
        endGamePanel.SetActive(true);
    }
}
