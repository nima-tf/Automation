using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Button closeButton;

    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private Button interactButton;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private TextMeshProUGUI gamepadInteractAlternateText;
    [SerializeField] private Button gamepadInteractAlternateButton;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;
    [SerializeField] private Button gamepadPauseButton;

    private Action OnCloseButtonAction;

    private void Awake() {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        moveUpButton.onClick.AddListener(() => {  RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => {  RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => {  RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => {  RebindBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => {  RebindBinding(GameInput.Binding.Interact); });
        interactAlternateButton.onClick.AddListener(() => {  RebindBinding(GameInput.Binding.InteractAlternate); });
        pauseButton.onClick.AddListener(() => {  RebindBinding(GameInput.Binding.Pause); });
        gamepadInteractButton.onClick.AddListener(() => {  RebindBinding(GameInput.Binding.Gamepad_Interact); });
        gamepadInteractAlternateButton.onClick.AddListener(() => {  RebindBinding(GameInput.Binding.Gamepad_InteractAlternate); });
        gamepadPauseButton.onClick.AddListener(() => {  RebindBinding(GameInput.Binding.Gamepad_Pause); });

        closeButton.onClick.AddListener(() => {
            Hide();
            OnCloseButtonAction();
        });
    }

    private void Start() {
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause); 
    }

    private void RebindBinding(GameInput.Binding binding) {
        ShowPressToRebindKey();
        GameInput.Instance.RebindBinding(binding, () => {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }

    private void ShowPressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey() {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    public void Show(Action OnCloseButtonAction) {
        this.OnCloseButtonAction = OnCloseButtonAction;

        gameObject.SetActive(true);

        soundEffectsButton.Select();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
