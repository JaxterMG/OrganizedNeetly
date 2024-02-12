using System;
using Newtonsoft.Json;
using UnityEngine;

public class RandomNumbersGenerator : MonoBehaviour, ISavable
{
    private int _seed; 
    private int _randomedNumbersCount;

    private struct RandomNumbersGeneratorData
    {
        public int Seed;
        public int RandomedNumbersCount;
        public RandomNumbersGeneratorData(int seed, int randomedNumbersCount)
        {
            Seed = seed;
            RandomedNumbersCount = randomedNumbersCount;
        }
    }
    void Start()
    {
        SaveLoadHandler saveLoadHandler = FindObjectOfType<SaveLoadHandler>();
        saveLoadHandler.RegisterSavable(this);
        if(!saveLoadHandler.HasSaveFile())
        {
            Initialize();
        }
    }
    public void Initialize()
    {
        _seed = DateTime.Now.GetHashCode();
        UnityEngine.Random.InitState(_seed);
    }
    public int RequestRandomNumber(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }
    public void IncreaseRandomCounter()
    {
        _randomedNumbersCount++;
    }

    public void Load(string jsonData)
    {
        var data = JsonConvert.DeserializeObject<RandomNumbersGeneratorData>(jsonData);
        _seed = data.Seed;
        UnityEngine.Random.InitState(_seed);
        _randomedNumbersCount = data.RandomedNumbersCount;
        SkipRandomNumbers();
    }
    private void SkipRandomNumbers()
    {
        for (var i = 0; i < _randomedNumbersCount; i++)
        {
            UnityEngine.Random.Range(0, 10);
        }
    }

    public string Save()
    {
        RandomNumbersGeneratorData randomNumbersGeneratorData = new RandomNumbersGeneratorData(_seed, _randomedNumbersCount);
        return JsonConvert.SerializeObject(randomNumbersGeneratorData);
    }
}
