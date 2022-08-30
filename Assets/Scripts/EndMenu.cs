using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
 public GameObject endMenuUI;
 private void Update()
 {
    if (!Timer.TimerIsRunning)
    {
        ShowEndMenu();
    }
 }

 void ShowEndMenu()
 {
  endMenuUI.SetActive(true);
 }

 public void RestartGame()
 {
  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
 }

 public void ContinueLevel()
 {
  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
 }
}
