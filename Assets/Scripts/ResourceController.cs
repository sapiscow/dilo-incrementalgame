using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public Text ResourceDescription;
    public Text ResourceUpgradeCost;
    public Text ResourceUnlockCost;

    private ResourceConfig _config;

    public void SetConfig (ResourceConfig config)
    {
        _config = config;

        // ToString("0") berfungsi untuk membuang angka di belakang koma
        ResourceDescription.text = $"{ _config.Name }\n+{ _config.Output.ToString ("0") }";
        ResourceUnlockCost.text = $"Unlock Cost\n{ _config.UnlockCost }";
        ResourceUpgradeCost.text = $"Upgrade Cost\n{ _config.UpgradeCost }";
    }

    public double GetOutput ()
    {
        return _config.Output;
    }
}