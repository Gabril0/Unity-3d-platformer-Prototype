using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    [SerializeField] string sceneName = "Stage1";


    public void ResetTheScene() { 
        SceneManager.LoadScene(sceneName);
    }
}
