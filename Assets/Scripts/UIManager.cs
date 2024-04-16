using UnityEngine;
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

    public void DisplayPotions()
    {
        int selectedPotion = GameManager.Instance.selectedPotion;

        ScriptablePotion[] potionScriptables = GameManager.Instance.potions;
        int firstPotion = selectedPotion == 0 ? potionScriptables.Length - 1 : selectedPotion - 1;
        
        for (int i = 0; i < 3; i++)
        {
            ScriptablePotion potion = potionScriptables[(firstPotion + i) % potionScriptables.Length];
            potionImages[i].sprite = potion.Icon;
            potionImages[i].color = potion.Quantity == 0 ? Color.grey : Color.white;
            potionTexts[i].text = potion.Quantity < 10 ? potion.Quantity.ToString() : "9+";
        }

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
