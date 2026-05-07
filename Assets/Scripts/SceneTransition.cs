using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("Nome da Cena para Carregar")]
    public string nomeCena;


    public void OnNextScene()
    {
        SceneManager.LoadScene(nomeCena);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
