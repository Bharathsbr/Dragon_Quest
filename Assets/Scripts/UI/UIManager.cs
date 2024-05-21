using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject go;
   [SerializeField] private AudioClip goc;

    [SerializeField] private GameObject pg;

    private void Awake()
    {
        go.SetActive(false);
    }

    #region Game Over
   public void GameOver()
   {
        go.SetActive(true);
        SoundManager.instance.PlaySound(goc);
   }

   public void Restart(){
        SceneManager.LoadScene(0);
   }

   public void MainMenu(){
        SceneManager.LoadScene(0);
   }

    public void Quit(){
        Application.Quit();

        #if Unity_Editor
        UnityEditor.EditorApplication.isPlaying=false;
        #endif
   }
   #endregion

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pg.activeInHierarchy){
                PauseGame(false);
            }else{
                PauseGame(true);
            }
        }
    }

   public void PauseGame(bool status){
        pg.SetActive(status);

        if(status){
            Time.timeScale=0;
        }else{
            Time.timeScale=1;
        }
   }

   public void SoundVolume()
   {
        SoundManager.instance.ChangeSoundVolume(0.2f);
   }

   public void MusicVolume()
   {
        SoundManager.instance.ChangeMusicVolume(0.2f);
   }
}
