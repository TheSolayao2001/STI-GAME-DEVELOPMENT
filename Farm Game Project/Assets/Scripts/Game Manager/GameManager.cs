using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMechanics _playerMechanics;
    [SerializeField] private TextMeshProUGUI _textScorePoints;
    public TextMeshProUGUI GetTextScorePoints { get { return _textScorePoints; } }
    [SerializeField] private TextMeshProUGUI _textMoney;
    public TextMeshProUGUI GetTextMoney { get { return _textMoney; } }
    [SerializeField] private CropSelectionManager _cropSelectionManager;

    [SerializeField] private GameObject _textArrival;
    public GameObject GetTextArrival { get { return _textArrival; } }
    [SerializeField] private TextMeshProUGUI _textArrivalCount;
    public TextMeshProUGUI GetTextArrivalCount { get { return _textArrivalCount; } }
    [SerializeField] private TextMeshProUGUI _textDepartureTime;
    public TextMeshProUGUI GetTextDepartureTime { get { return _textDepartureTime; } }

    void Awake()
    {
        _playerMechanics = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMechanics>();
        _textScorePoints = GameObject.Find("Text - Points").GetComponent<TextMeshProUGUI>();
        _textMoney = GameObject.Find("Text - Piso").GetComponent<TextMeshProUGUI>();
        _cropSelectionManager = GameObject.Find("Crop Selection").GetComponent<CropSelectionManager>();

        _textArrival = GameObject.Find("Text - Arrival");
        _textArrivalCount = GameObject.Find("Text - Arrival Count").GetComponent<TextMeshProUGUI>();
        _textDepartureTime = GameObject.Find("Text - Departure").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerMechanics.GetInteractionState == PlayerMechanics.State.OnHand)
            _cropSelectionManager.gameObject.SetActive(false);
        else _cropSelectionManager.gameObject.SetActive(true);
    }
}
