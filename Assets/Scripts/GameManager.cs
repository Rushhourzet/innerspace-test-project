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
    private bool nameHasBeenTyped = false;
    private Text playerNameField => GameObject.Find("Canvas").transform.Find("EnterName Inputfield").Find("Text").GetComponent<Text>(); //I think there was a better way to find this, but I can`t remember it
    private ScoreManager scoreManager => FindObjectOfType<ScoreManager>();
    private void Awake() {
        DontDestroyOnLoad(this);
    }
    public void Play()
    {
        print(playerNameField);
        if(!playerNameField.text.Equals("")) {
            nameHasBeenTyped = true;
            scoreManager.SetName(playerNameField.text);
        }
        if (nameHasBeenTyped) {
            StartCoroutine(LoadScene(gameScene));
        }
        else {
            print("Please Type in your Name!");
        }
    }

    public void GameOver() {

    }

    private IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Loading game!");
        yield return new WaitForSeconds(2);
        EditorSceneManager.LoadScene(sceneName);
    }
}