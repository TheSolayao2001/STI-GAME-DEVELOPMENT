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
    private int _orderIndex = 0;

    void Awake()
    {
        _orderNames = new List<string>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_canSetOrder)
        {
            while (_orderIndex < _orderImages.Count)
            {
                int randomIndex = UnityEngine.Random.Range(0, _orders.Count);
                _orderNames.Add(_orders[randomIndex].GetName);
                _orderImages[_orderIndex].sprite = _orders[randomIndex].GetSprite;
                _orderIndex++;
            }
            _canSetOrder = false;
        }
    }
}
