using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("CustomerScene");
    }

    public void Credit()
    {
        SceneManager.LoadScene("Credits");
    }
}
