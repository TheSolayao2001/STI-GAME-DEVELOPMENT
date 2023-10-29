using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmPlot : MonoBehaviour
{
    public enum State
    {
        Plot, 
        Seeded, 
        FullGrown
    }
    private State _growthState;
    public State GetGrowthState { get { return _growthState; } }

    [SerializeField] private FarmCropTable _cropTable;
    public FarmCropTable GetCropTable { get { return _cropTable; } }

    [SerializeField] private float _growthTime;
    private GameObject _plantedSeeds;
    private bool _hasPlantedSeeds = false;
    private GameObject _harvestCrop;
    private bool _isHarvestCrop = false;

    private PlayerMechanics _player;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _growthState = State.Plot;
    }

    // Update is called once per frame
    void Update()
    {
        CropGrowth();
    }

    private void CropGrowth()
    {
        switch (_growthState)
        {
            case State.Plot:
                break;

            case State.Seeded:
                if (!_hasPlantedSeeds)
                {
                    _plantedSeeds = Instantiate(_cropTable.selectedFarmCrop.GetSeed, transform);
                    _hasPlantedSeeds = true;
                }

                _growthTime -= Time.deltaTime;
                if (_growthTime <= 0f)
                {
                    Destroy(_plantedSeeds);
                    _hasPlantedSeeds = false;
                    _growthState = State.FullGrown;
                }
                break;

            case State.FullGrown:
                if (!_isHarvestCrop)
                {
                    _harvestCrop = Instantiate(_cropTable.selectedFarmCrop.GetCrop, transform);
                    _isHarvestCrop = true;
                }
                break;
        }
    }

    public void PlantSeeds()
    {
        _growthTime = _cropTable.selectedFarmCrop.GetGrowthTime;
        _growthState = State.Seeded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_player.tag))
        {
            _player.farmPlot = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_player.tag))
        {
            _player.farmPlot = null;
        }
    }
}
