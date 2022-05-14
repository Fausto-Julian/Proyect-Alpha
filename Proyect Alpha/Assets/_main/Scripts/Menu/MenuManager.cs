
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Panels"), Space]
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelStart;
    [SerializeField] private GameObject panelCredits;

    [Header("Buttos"), Space]
    [SerializeField] private Button buttonStart;
    [SerializeField] private Button buttonCredits;
    [SerializeField] private Button buttonBackStart;
    [SerializeField] private Button buttonBackCredits;
    [SerializeField] private Button buttonMuted;

    [Header("Sprites"), Space] 
    [SerializeField] private Sprite onAudio;
    [SerializeField] private Sprite offAudio;

    private bool _onAudio;
    private Image _mutedImage;
    
    private void Start()
    {
        panelMenu.SetActive(true);
        panelStart.SetActive(false);
        panelCredits.SetActive(false);
        
        buttonStart.onClick.AddListener(OnButtonStartHandler);
        buttonCredits.onClick.AddListener(OnButtonCreditsHandler);
        buttonBackStart.onClick.AddListener(OnButtonBackHandler);
        buttonBackCredits.onClick.AddListener(OnButtonBackHandler);
        buttonMuted.onClick.AddListener(OnButtonMutedHandler);

        _onAudio = true;
        _mutedImage = buttonMuted.gameObject.GetComponent<Image>();
        _mutedImage.sprite = onAudio;
    }

    private void OnButtonStartHandler()
    {
        panelMenu.SetActive(false);
        panelStart.SetActive(true);
        panelCredits.SetActive(false);
    }
    
    private void OnButtonCreditsHandler()
    {
        panelMenu.SetActive(false);
        panelStart.SetActive(false);
        panelCredits.SetActive(true);
    }
    
    private void OnButtonBackHandler()
    {
        panelMenu.SetActive(true);
        panelStart.SetActive(false);
        panelCredits.SetActive(false);
    }

    private void OnButtonMutedHandler()
    {
        _onAudio = !_onAudio;
        _mutedImage.sprite = _onAudio ? onAudio : offAudio;
    }
}