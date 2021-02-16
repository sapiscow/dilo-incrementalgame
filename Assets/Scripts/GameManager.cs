using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager> ();
            }

            return _instance;
        }
    }

    // Fungsi [Range (min, max)] ialah menjaga value agar tetap berada di antara min dan max-nya
    [Range (0f, 1f)]
    public float AutoCollectPercentage = 0.1f;
    public ResourceConfig[] ResourcesConfigs;

    public Transform ResourcesParent;
    public ResourceController ResourcePrefab;

    public Text GoldInfo;
    public Text AutoCollectInfo;

    private List<ResourceController> _activeResources = new List<ResourceController> ();
    private float _collectSecond;

    public double TotalGold { get; private set; }

    private void Start ()
    {
        AddAllResources ();
    }

    private void Update ()
    {
        // Fungsi untuk selalu mengeksekusi CollectPerSecond setiap detik
        _collectSecond += Time.unscaledDeltaTime;
        if (_collectSecond >= 1f)
        {
            CollectPerSecond ();
            _collectSecond = 0f;
        }
    }

    private void AddAllResources ()
    {
        foreach (ResourceConfig config in ResourcesConfigs)
        {
            GameObject obj = Instantiate (ResourcePrefab.gameObject, ResourcesParent, false);
            ResourceController resource = obj.GetComponent<ResourceController> ();

            resource.SetConfig (config);

            _activeResources.Add (resource);
        }
    }

    private void CollectPerSecond ()
    {
        double output = 0;
        foreach (ResourceController resource in _activeResources)
        {
            output += resource.GetOutput ();
        }

        output *= AutoCollectPercentage;
        // Fungsi ToString("F1") ialah membulatkan angka menjadi desimal yang memiliki 1 angka di belakang koma
        AutoCollectInfo.text = $"Auto Collect: { output.ToString ("F1") } / second";

        AddGold (output);
    }

    private void AddGold (double value)
    {
        TotalGold += value;
        GoldInfo.text = $"Gold: { TotalGold.ToString ("0") }";
    }
}

// Fungsi System.Serializable adalah agar object bisa di-serialize dan
// value dapat di-set dari inspector
[System.Serializable]
public struct ResourceConfig
{
    public string Name;
    public double UnlockCost;
    public double UpgradeCost;
    public double Output;
}
