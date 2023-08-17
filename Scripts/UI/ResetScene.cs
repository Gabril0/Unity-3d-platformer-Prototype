using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public void ResetTheScene(string sceneName) { 
        SceneManager.LoadScene(sceneName);
    }
}
