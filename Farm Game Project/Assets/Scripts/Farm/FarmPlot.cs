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
    private GameObject _fullGrownCrop;
    private bool _isFullGrown = false;

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
                if (_fullGrownCrop)
                {
                    Destroy(_fullGrownCrop);
                    _isFullGrown = false;
                }
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
                if (!_isFullGrown)
                {
                    _fullGrownCrop = Instantiate(_cropTable.selectedFarmCrop.GetCrop, transform);
                    _isFullGrown = true;
                }
                break;
        }
    }

    public void PlantSeeds()
    {
        _growthTime = _cropTable.selectedFarmCrop.GetGrowthTime;
        _growthState = State.Seeded;
    }

    public void HarvestCrop()
    {
        _growthState = State.Plot;
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
