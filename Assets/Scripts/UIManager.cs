using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image[] potionImages;

    [SerializeField] private Text potionDisplay;

    private float timer;
    [SerializeField] private float timePnlDisappearance;
    [SerializeField] private Animator animator;

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
        if(timer > 0f)
        {
            animator.SetBool("opening", true);
            timer += Time.deltaTime;
        }
        if (timer > timePnlDisappearance)
        {
            animator.SetBool("opening", false);
            timer = 0f;
        }
    }

    private void DisplayPotions()
    {
        int selectedPotion = GameManager.Instance.selectedPotion;

        ScriptablePotion[] potionScriptables = GameManager.Instance.potions;
        int firstPotion = selectedPotion == 0 ? potionScriptables.Length - 1 : selectedPotion - 1;
        
        potionImages[0].sprite = potionScriptables[firstPotion].Icon;
        potionImages[1].sprite = potionScriptables[(firstPotion + 1) % potionScriptables.Length].Icon;
        potionImages[2].sprite = potionScriptables[(firstPotion + 2) % potionScriptables.Length].Icon;

        potionDisplay.text = potionScriptables[selectedPotion].Name;
    }

    public void ChangePotion(bool sens)
    {
        timer += Time.deltaTime;

        int _selectedPotion = GameManager.Instance.selectedPotion;
        _selectedPotion += sens ? 1 : -1; // Suivante ou précédente
        if (_selectedPotion < 0) _selectedPotion = 2; // Si valeur inférieure à 0, on remet à la dernière valeur

        GameManager.Instance.ChangePotion(_selectedPotion);

        DisplayPotions();
    }
}
