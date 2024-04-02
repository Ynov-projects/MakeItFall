using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ScriptablePotion[] potionScriptables;
    [SerializeField] private Image[] potionImages;

    [SerializeField] private Text potionDisplay;

    private int selectedPotion;

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
        int firstPotion = selectedPotion == 0 ? potionScriptables.Length - 1 : selectedPotion - 1;
        
        potionImages[0].sprite = potionScriptables[firstPotion].Icon;
        potionImages[1].sprite = potionScriptables[(firstPotion + 1) % potionScriptables.Length].Icon;
        potionImages[2].sprite = potionScriptables[(firstPotion + 2) % potionScriptables.Length].Icon;

        potionDisplay.text = potionScriptables[selectedPotion].Name;
    }

    public void ChangePotion(bool sens)
    {
        timer += Time.deltaTime;
        selectedPotion += sens ? 1 : -1;
        if (selectedPotion < 0) selectedPotion = 2;
        selectedPotion = selectedPotion % potionScriptables.Length;
        DisplayPotions();
    }
}
