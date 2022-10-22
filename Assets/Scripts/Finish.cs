using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    [SerializeField] Canvas finishCanvas;

    private void Start()
    {
        Time.timeScale = 1;
        finishCanvas.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinishHandle();
            //LoadNextScene();
        }
    }

    private void FinishHandle()
    {
        finishCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LoadNextScene()
    {
        int totalSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(totalSceneIndex + 1);
    }
}
