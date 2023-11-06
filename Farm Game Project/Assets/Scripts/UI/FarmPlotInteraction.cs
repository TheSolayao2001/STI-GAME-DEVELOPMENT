using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FarmPlotInteraction : MonoBehaviour
{
    [SerializeField] private Sprite _harvestSprite;
    private Button _button;
    private Image _itemImage;
    private TextMeshProUGUI _interactionText;

    private PlayerMechanics _player;
    private CropSelectionManager _cropSelectionManager;

    void Awake()
    {
        _button = GetComponent<Button>();
        _itemImage = transform.GetChild(0).GetComponent<Image>();
        _interactionText = GetComponentInChildren<TextMeshProUGUI>();
        
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
        _cropSelectionManager = GameObject.Find("Crop Selection").GetComponent<CropSelectionManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (_player.GetInteractionState)
        {
            case PlayerMechanics.State.None:
                _button.interactable = false;
                break;

            case PlayerMechanics.State.Plant:
                if (!_cropSelectionManager.hasStartSelecting) _button.interactable = false;
                else
                {
                    _button.interactable = true;
                    _itemImage.sprite = _cropSelectionManager.GetSelectedFarmCrop.GetComponent<FarmCropSelection>().GetCropImage.sprite;
                }

                _interactionText.text = "Plant";
                break;

            case PlayerMechanics.State.Harvest:
                _button.interactable = true;
                _itemImage.sprite = _harvestSprite;
                _interactionText.text = "Harvest";
                break;

            case PlayerMechanics.State.OnHand:
                if (_player.pickupTruck) _button.interactable = true;
                else _button.interactable = false;
                _itemImage.sprite = _player.GetHarvestCropSprite;
                _interactionText.text = "Load In";
                break;
        }
    }

    public void InteractFarmPlot()
    {
        switch (_player.GetInteractionState)
        {
            case PlayerMechanics.State.Plant:
                _player.PurchaseThenPlant();
                break;

            case PlayerMechanics.State.Harvest:
                _player.HarvestCrop();
                break;

            case PlayerMechanics.State.OnHand:
                _player.LoadCropOnTruck();
                break;
        }
    }
}
