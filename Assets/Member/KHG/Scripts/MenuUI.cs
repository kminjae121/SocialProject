using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Image blockPanel;
    public void OnPlayPressed()
    {
        blockPanel.DOFade(1f,1f).OnComplete(()=>SceneManager.LoadScene("GameScene"));
    }
    public void OnExitPressed()
    {
        Application.Quit();
    }
}
