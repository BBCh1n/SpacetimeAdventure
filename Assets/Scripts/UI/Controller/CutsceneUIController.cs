using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneUIController : MonoBehaviour
{
    public static CutsceneUIController Instance;

    public GameObject cutscenePanel;
    public CanvasGroup canvasGroup;
    public Image cutscene;
    public Image background;
    public Text title;
    public Text intro;
    public Text note;

    public Sprite[] backgroundList;
    public string[] titleList;
    public string[] introList;
    public string[] noteList;

    private Animator anim;
    public bool isActive = false;
    private bool isInAnim = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        cutscenePanel.SetActive(false);
        SetBackgroundActive(false);
    }

    void Update()
    {
        if (isActive && !isInAnim)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Next") || Input.GetButtonDown("Back"))
            {
                TriggerFadeOut();
            }
        }
    }

    public void StartCutscene(int levelIndex)
    {
        int arrayIndex = levelIndex - 1;
        title.text = titleList[arrayIndex];
        intro.text = introList[arrayIndex];
        note.text = noteList[arrayIndex];
        background.sprite = backgroundList[arrayIndex];

        canvasGroup.alpha = 0;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        cutscenePanel.SetActive(true);
        anim.SetTrigger("FadeIn");
        isActive = true;
        isInAnim = true;

        /*
        PlayerController pc = PlayerController.Instance;
        if (pc != null)
        {
            pc.canMove = false;
        }*/
        GameController.Instance.SetPause(true);
    }

    public void TriggerFadeOut()
    {
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        anim.SetTrigger("FadeOut");
        isInAnim = true;
        AudioController.Instance.PlayUINext(true);
    }

    public void OnFadeInAnimEnd()
    {
        SetBackgroundActive(true);
        isInAnim = false;
    }

    public void OnFadeOutAnimEnd()
    {
        cutscenePanel.SetActive(false);
        isActive = false;
        isInAnim = false;
        GameController.Instance.SetPause(false);
    }

    public void SetBackgroundActive(bool isActive)
    {
        background.gameObject.SetActive(isActive);
    }
}
