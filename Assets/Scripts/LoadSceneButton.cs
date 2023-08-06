using UnityEngine;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
    public string scene;
    public Condition condition;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadScene);
    }

    public void LoadScene()
    {
        if (condition.Met())
            SceneManager.Instance.LoadScene(scene);
    }
}