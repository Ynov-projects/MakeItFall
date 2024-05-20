using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image[] potionImages;
    [SerializeField] private Text[] potionTexts;

    [SerializeField] private Text potionDisplay;

    private float timer;
    [SerializeField] private float timePnlDisappearance;
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject commandsMenu;

    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        Instance = this;
    }

    private void Start()
    {
        DisplayPotions();
    }

    private void Update()
    {
        if (timer > 0f)
        {
            animator.SetBool("opening", true);
            timer += Time.deltaTime;
        }
        if (timer > timePnlDisappearance)
        {
            animator.SetBool("opening", false);
            timer = 0f;
        }

        // Gestion des potions visuelles et physiques
        float value = GameManager.input.Gameplay.SwitchPotions.ReadValue<Vector2>().y;
        if (Mathf.Abs(value) >= .7f) ChangePotion(value > 0f);

        if (GameManager.input.Gameplay.Pause.triggered)
            PauseGame();
    }

    public void DisplayPotions()
    {
        int selectedPotion = GameManager.Instance.selectedPotion;

        GameObject[] prefabPotion = GameManager.Instance.prefabPotions;
        int firstPotion = selectedPotion == 0 ? prefabPotion.Length - 1 : selectedPotion - 1;
        
        for (int i = 0; i < 3; i++)
        {
            ScriptablePotion potion = prefabPotion[(firstPotion + i) % prefabPotion.Length].GetComponent<PotionScript>().potion;
            potionImages[i].sprite = potion.Icon;
            potionImages[i].color = potion.Quantity == 0 ? Color.grey : Color.white;
            potionTexts[i].text = potion.Quantity < 10 ? potion.Quantity.ToString() : "9+";
        }

        potionDisplay.text = prefabPotion[selectedPotion].GetComponent<PotionScript>().potion.Name;
    }

    private void ChangePotion(bool sens)
    {
        if (timer == 0 || timer > .5f)
        {
            timer = Time.deltaTime;

            int _selectedPotion = GameManager.Instance.selectedPotion;
            _selectedPotion += sens ? 1 : -1; // Suivante ou précédente
            if (_selectedPotion < 0) _selectedPotion = 2; // Si valeur inférieure à 0, on remet à la dernière valeur

            GameManager.Instance.ChangePotion(_selectedPotion);
            DisplayPotions();
        }
    }

    #region "Interactable"
    private void PauseGame()
    {
        Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
        LaunchMenuPause();
    }

    public void LaunchMenuPause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if(pauseMenu.activeSelf) EventSystem.current.SetSelectedGameObject(pauseMenu.transform.GetChild(0).gameObject);
        PlayerMovement.Instance.enabled = !pauseMenu.activeSelf;
    }

    public void CommandsMenu()
    {
        LaunchMenuPause();
        commandsMenu.SetActive(!commandsMenu.activeSelf);
        if (commandsMenu.activeSelf) EventSystem.current.SetSelectedGameObject(commandsMenu.transform.GetChild(0).gameObject);
        PlayerMovement.Instance.enabled = !commandsMenu.activeSelf;
    }

    public void ResumeGame()
    {
        PauseGame();
    }

    public void RestartGame()
    {
        Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}