using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Farm Crops")]
public class FarmCropTable : ScriptableObject
{
    public interface ICrops
    {
        GameObject GetSeed { get; }
        GameObject GetCrop { get; }
        float GetGrowthTime { get; }
    }

    #region Crop Table

    [Serializable]
    public struct Rice : ICrops
    {
        [SerializeField] private GameObject _seed;
        [SerializeField] private GameObject _crop;
        [SerializeField] private float _growthTime;

        public GameObject GetSeed { get { return _seed; } }
        public GameObject GetCrop { get { return _crop; } }
        public float GetGrowthTime { get { return _growthTime; } }
    }

    [Serializable]
    public struct Corn : ICrops
    {
        [SerializeField] private GameObject _seed;
        [SerializeField] private GameObject _crop;
        [SerializeField] private float _growthTime;

        public GameObject GetSeed { get { return _seed; } }
        public GameObject GetCrop { get { return _crop; } }
        public float GetGrowthTime { get { return _growthTime; } }
    }

    [Serializable]
    public struct Carrot : ICrops
    {
        [SerializeField] private GameObject _seed;
        [SerializeField] private GameObject _crop;
        [SerializeField] private float _growthTime;

        public GameObject GetSeed { get { return _seed; } }
        public GameObject GetCrop { get { return _crop; } }
        public float GetGrowthTime { get { return _growthTime; } }
    }

    [Serializable]
    public struct Calabaza : ICrops
    {
        [SerializeField] private GameObject _seed;
        [SerializeField] private GameObject _crop;
        [SerializeField] private float _growthTime;

        public GameObject GetSeed { get { return _seed; } }
        public GameObject GetCrop { get { return _crop; } }
        public float GetGrowthTime { get { return _growthTime; } }
    }

    [Serializable]
    public struct Cake : ICrops
    {
        [SerializeField] private GameObject _seed;
        [SerializeField] private GameObject _crop;
        [SerializeField] private float _growthTime;

        public GameObject GetSeed { get { return _seed; } }
        public GameObject GetCrop { get { return _crop; } }
        public float GetGrowthTime { get { return _growthTime; } }
    }

    #endregion

    public Rice rice;
    public Corn corn;
    public Carrot carrot;
    public Calabaza calabaza;
    public Cake cake;

    public ICrops selectedFarmCrop;

}