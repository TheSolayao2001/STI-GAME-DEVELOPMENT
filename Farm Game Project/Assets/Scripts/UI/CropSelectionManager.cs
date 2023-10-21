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
}
