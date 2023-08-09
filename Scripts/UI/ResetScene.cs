using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    [SerializeField] string sceneName = "Stage1";


    public void ResetTheScene(string sceneName) { 
        SceneManager.LoadScene(sceneName);
    }
}
