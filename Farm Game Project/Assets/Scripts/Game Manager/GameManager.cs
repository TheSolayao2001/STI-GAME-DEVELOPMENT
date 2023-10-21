using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMoney;
    public TextMeshProUGUI GetTextMoney { get { return _textMoney; } }

    void Awake()
    {
        _textMoney = GameObject.Find("Text - Piso").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
