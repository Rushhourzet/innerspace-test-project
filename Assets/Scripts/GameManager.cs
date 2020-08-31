using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UI;

/// <summary> Manages the state of the whole application </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private string gameScene;
    [SerializeField] private string homeScene;
    private bool nameHasBeenTyped = false;
    private Text playerNameField => GameObject.Find("Canvas").transform.Find("EnterName Inputfield").Find("Text").GetComponent<Text>(); //I think there was a better way to find this, but I can`t remember it
    private SessionScore sessionScore => FindObjectOfType<SessionScore>();
    private ScoreBoard scoreBoard => FindObjectOfType<ScoreBoard>();
    private string p_name;

    private bool scoreInitialized = false;
    private void Awake() {
    }
    public void Play()
    {
        if(!playerNameField.text.Equals("")) {
            nameHasBeenTyped = true;
            
        }
        if (nameHasBeenTyped) {

            StartCoroutine(LoadScene(gameScene));
            p_name = playerNameField.text;
            scoreInitialized = true;
        }
        else {
            print("Please Type in your Name!");
        }
    }


    public void GameOver() {
        Time.timeScale = 0f;
        sessionScore.SaveScore();
        EditorSceneManager.LoadScene(homeScene);
        Destroy(transform.parent);
    }

    private IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Loading game!");
        yield return new WaitForSeconds(2);
        EditorSceneManager.LoadScene(sceneName);
    }
}