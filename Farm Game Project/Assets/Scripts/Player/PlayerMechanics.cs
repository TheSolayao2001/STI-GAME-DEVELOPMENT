using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;

    public int score = 0;

    [SerializeField] private int _piso = 0;
    private int _pisoPerSecond = 5;
    private const float _GENERATE_PISO_PER_SECOND = 1f;
    private float _generationTime;
    private const int _MAX_PISO_GENERATION = 250;

    private int _selectedCropPrice = 0;
    public int SetSelectedCropPrice { set { _selectedCropPrice = value; } }

    public enum State
    {
        None, 
        Plant, 
        Harvest, 
        OnHand
    }
    private State _interactionState;
    public State GetInteractionState { get { return _interactionState; } }

    private bool _isOnHand = false;
    public PickupTruck pickupTruck;

    private string _harvestCropName;
    private Sprite _harvestCropSprite;
    public Sprite GetHarvestCropSprite { get { return _harvestCropSprite; } }

    public delegate void Delegate_1();
    public Delegate_1 OnPlantSeeds;

    public FarmPlot farmPlot;

    [Space(10)]
    [SerializeField] private AudioClip _sfxPlantCrop;
    [SerializeField] private AudioClip _sfxHarvestCrop;
    [SerializeField] private AudioClip _sfxLoadCropOnTruck;
    private AudioSource _audioSFX;

    private CharacterController2D _controller2D;
    private GameManager _gameManager;

    void Awake()
    {
        _controller2D = GetComponent<CharacterController2D>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        _audioSFX = GameObject.Find("Audio Source - SFX").GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controller2D.SetMoveSpeed = _moveSpeed;
        _generationTime = _GENERATE_PISO_PER_SECOND;
    }

    // Update is called once per frame
    void Update()
    {
        GeneratePisoPerSecond();
        InteractFarmPlot();

        _gameManager.GetTextScorePoints.text = score.ToString();
    }

    private void GeneratePisoPerSecond()
    {
        if (_piso < _MAX_PISO_GENERATION)
        {
            _generationTime -= 1.5f * Time.deltaTime;
            if (_generationTime <= 0f)
            {
                _piso += _pisoPerSecond;
                _generationTime = _GENERATE_PISO_PER_SECOND;
            }
        }

        _gameManager.GetTextMoney.text = _piso.ToString();
    }

    private void InteractFarmPlot()
    {
        if (farmPlot && !_isOnHand)
        {
            switch (farmPlot.GetGrowthState)
            {
                case FarmPlot.State.Plot:
                    _interactionState = State.Plant;
                    break;

                case FarmPlot.State.Seeded:
                    _interactionState = State.None;
                    break;

                case FarmPlot.State.FullGrown:
                    _interactionState = State.Harvest;
                    break;
            }
        }
        else
        {
            if (_isOnHand) _interactionState = State.OnHand;
            else _interactionState = State.None;
        }
    }

    public void PurchaseThenPlant()
    {
        if (_piso >= _selectedCropPrice)
        {
            _piso -= _selectedCropPrice;
            if (farmPlot) farmPlot.PlantSeeds();

            _audioSFX.clip = _sfxPlantCrop;
            _audioSFX.Play();
        }
    }

    public void HarvestCrop()
    {
        if (farmPlot)
        {
            farmPlot.HarvestCrop();
            _harvestCropName = farmPlot.GetCropTable.selectedFarmCrop.GetCropName;
            _harvestCropSprite = farmPlot.GetCropTable.selectedFarmCrop.GetCropSprite;

            _audioSFX.clip = _sfxHarvestCrop;
            _audioSFX.Play();
        }
        _isOnHand = true;
    }

    public void LoadCropOnTruck()
    {
        if (pickupTruck)
        {
            pickupTruck.LoadTruck(_harvestCropName, _harvestCropSprite);
            _isOnHand = false;

            _audioSFX.clip = _sfxLoadCropOnTruck;
            _audioSFX.Play();
        }
    }
}
