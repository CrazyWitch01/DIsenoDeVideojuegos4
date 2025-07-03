using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
         InputSystem_Actions inputActions;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.UI.Enable();
        inputActions.NewPlayer.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void IrMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void IrGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
