using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenuBehaviour : MonoBehaviour
{
    public void Options()
    {
        Debug.Log("Options");
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
