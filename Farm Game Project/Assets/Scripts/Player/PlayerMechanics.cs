using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;

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
        Harvest
    }
    private State _interactionState;
    public State GetInteractionState { get { return _interactionState; } }

    public delegate void Delegate_1();
    public Delegate_1 OnPlantSeeds;

    public FarmPlot farmPlot;
    private CharacterController2D _controller2D;
    private GameManager _gameManager;

    void Awake()
    {
        _controller2D = GetComponent<CharacterController2D>();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
    }

    private void GeneratePisoPerSecond()
    {
        _generationTime -= 1.5f * Time.deltaTime;
        if (_generationTime <= 0f && _piso < _MAX_PISO_GENERATION)
        {
            _piso += _pisoPerSecond;
            _generationTime = _GENERATE_PISO_PER_SECOND;
        }

        _gameManager.GetTextMoney.text = _piso.ToString();
    }

    private void InteractFarmPlot()
    {
        if (farmPlot)
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
            _interactionState = State.None;
        }
    }

    public void PurchaseThenPlant()
    {
        if (_piso >= _selectedCropPrice)
        {
            _piso -= _selectedCropPrice;
            if (farmPlot) farmPlot.PlantSeeds();
        }
    }
}
