using UnityEngine;
using UnityEngine.SceneManagement;


public class SettingsManager : MonoBehaviour
{
    public static SettingsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ResolutionSetting resolutionSetting = FindObjectOfType<ResolutionSetting>();
        if (SceneManager.GetActiveScene().name == "Settings")
        {
            resolutionSetting.GetResolutions();
            resolutionSetting.AddResolutionsToDropdownMenu();
        }
#if UNITY_WEBGL
        GameObject res = GameObject.FindGameObjectWithTag("CheckType");
        res.SetActive(false);
#endif
    }
}
