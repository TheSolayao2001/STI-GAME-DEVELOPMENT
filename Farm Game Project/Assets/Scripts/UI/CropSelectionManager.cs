using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropSelectionManager : MonoBehaviour
{
    public bool hasStartSelecting = false;

    private GameObject _selectedFarmCrop;
    public GameObject GetSelectedFarmCrop { get { return _selectedFarmCrop; } }
    private GameObject _deselectedFarmCrop;

    private PlayerMechanics _player;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCropName();
    }

    public void SetSelectedFarmCrop(GameObject farmCrop)
    {
        _selectedFarmCrop = farmCrop;

        if (_deselectedFarmCrop)
        {
            _deselectedFarmCrop.GetComponent<FarmCropSelection>().DeselectCrop();
            _deselectedFarmCrop = _selectedFarmCrop;
        }
        else
        {
            _deselectedFarmCrop = _selectedFarmCrop;
        }
    }

    private void CheckCropName()
    {
        if (!_selectedFarmCrop) return;
        if (!_player.farmPlot) return;

        switch (_selectedFarmCrop.name)
        {
            case "Button - Rice":
                if (_player.farmPlot.GetGrowthState == FarmPlot.State.Plot) 
                    _player.farmPlot.GetCropTable.selectedFarmCrop = _player.farmPlot.GetCropTable.rice;
                break;

            case "Button - Corn":
                if (_player.farmPlot.GetGrowthState == FarmPlot.State.Plot)
                    _player.farmPlot.GetCropTable.selectedFarmCrop = _player.farmPlot.GetCropTable.corn;
                break;

            case "Button - Carrot":
                if (_player.farmPlot.GetGrowthState == FarmPlot.State.Plot)
                    _player.farmPlot.GetCropTable.selectedFarmCrop = _player.farmPlot.GetCropTable.carrot;
                break;

            case "Button - Calabaza":
                if (_player.farmPlot.GetGrowthState == FarmPlot.State.Plot)
                    _player.farmPlot.GetCropTable.selectedFarmCrop = _player.farmPlot.GetCropTable.calabaza;
                break;

            case "Button - Cake":
                if (_player.farmPlot.GetGrowthState == FarmPlot.State.Plot)
                    _player.farmPlot.GetCropTable.selectedFarmCrop = _player.farmPlot.GetCropTable.cake;
                break;

            default:
                Debug.LogError("OH MAY GOT!");
                break;
        }
    }
}
