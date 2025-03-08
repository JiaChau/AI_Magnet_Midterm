using UnityEngine;
using UnityEngine.SceneManagement;

public class SafeSceneLoader : MonoBehaviour
{
    public void LoadSceneSafely(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("해당 씬이 존재하지 않습니다: " + sceneName);
        }
    }
}
