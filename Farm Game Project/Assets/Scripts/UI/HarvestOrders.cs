using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HarvestOrders : MonoBehaviour
{
    [Serializable]
    public struct OrderInfo
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite _sprite;

        public string GetName { get { return _name; } }
        public Sprite GetSprite { get { return _sprite; } }
    }

    [SerializeField] private List<OrderInfo> _orders;
    [SerializeField] private List<Image> _orderImages;
    private List<string> _orderNames;
    public List<string> GetOrderNames { get { return _orderNames; } }

    private bool _canSetOrder = true;
    private bool _clearOrders = false;
    private int _orderIndex = 0;
    private int _checkOrderIndex = 0;

    private PlayerMechanics _player;
    private PickupTruck _truck;

    void Awake()
    {
        _orderNames = new List<string>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
        _truck = GameObject.Find("Pickup Truck").GetComponent<PickupTruck>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _truck.loadCropDel += CheckOrders;
    }

    // Update is called once per frame
    void Update()
    {
        ArrangeHarvestOrders();
    }

    private void ArrangeHarvestOrders()
    {
        switch (_truck.GetScheduleState)
        {
            case PickupTruck.State.Arrive:
                if (_canSetOrder)
                {
                    while (_orderIndex < _orderImages.Count)
                    {
                        int randomIndex = UnityEngine.Random.Range(0, _orders.Count);
                        _orderNames.Add(_orders[randomIndex].GetName);
                        _orderImages[_orderIndex].sprite = _orders[randomIndex].GetSprite;
                        _orderIndex++;
                    }
                    _clearOrders = false;
                    _canSetOrder = false;
                }
                break;

            case PickupTruck.State.Depart:
                _canSetOrder = true;
                _orderIndex = 0;

                if (!_clearOrders)
                {
                    _orderNames.Clear();
                    _clearOrders = true;
                }
                break;
        }
    }

    private void CheckOrders(string loadedCropName)
    {
        while (_checkOrderIndex < _orderNames.Count)
        {
            if (_orderNames[_checkOrderIndex].Equals(loadedCropName))
            {
                _orderNames.RemoveAt(_checkOrderIndex);
                _player.score += 10;
                _checkOrderIndex = 0;
                return;
            }
            else _checkOrderIndex++;
        }
    }
}
