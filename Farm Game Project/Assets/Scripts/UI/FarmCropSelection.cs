using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarmCropSelection : MonoBehaviour
{
    [SerializeField] private int _farmCropPrice = 10;

    private bool _isSelected = false;
    private Vector2 _selectionSize;
    private Vector2 _deselectionSize;

    private Button _button;
    private Image _cropImage;
    public Image GetCropImage { get { return _cropImage; } }
    private TextMeshProUGUI _TMPCost;

    [SerializeField] private AudioClip _sfxSelectCrop;
    private AudioSource _audioSFX;

    private CropSelectionManager _cropSelectionManager;
    private RectTransform _rectTransform;
    private PlayerMechanics _player;

    void Awake()
    {
        _button = GetComponent<Button>();
        _cropImage = transform.GetChild(0).GetComponent<Image>();
        _TMPCost = GetComponentInChildren<TextMeshProUGUI>();
        _cropSelectionManager = GetComponentInParent<CropSelectionManager>();
        _rectTransform = GetComponent<RectTransform>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();

        _audioSFX = GameObject.Find("Audio Source - SFX").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _deselectionSize = new Vector2(_rectTransform.rect.width, _rectTransform.rect.height);
        _selectionSize = new Vector2(100f, 100f);
        _TMPCost.text = _farmCropPrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCrop()
    {
        if (!_isSelected)
        {
            if (!_cropSelectionManager.hasStartSelecting) _cropSelectionManager.hasStartSelecting = true;
            _cropSelectionManager.SetSelectedFarmCrop(gameObject);

            _rectTransform.sizeDelta = _selectionSize;
            _player.SetSelectedCropPrice = _farmCropPrice;
            _isSelected = true;

            _audioSFX.clip = _sfxSelectCrop;
            _audioSFX.Play();
        }
    }

    public void DeselectCrop()
    {
        if (_isSelected)
        {
            _rectTransform.sizeDelta = _deselectionSize;
            _isSelected = false;
        }
        
    }
}
