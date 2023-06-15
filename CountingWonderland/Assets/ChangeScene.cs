using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName; // Nama scene yang ingin dipindahkan

    public void ChangeToScene()
    {
        SceneManager.LoadScene(sceneName); // Memuat scene baru berdasarkan nama scene yang ditentukan
    }
}
