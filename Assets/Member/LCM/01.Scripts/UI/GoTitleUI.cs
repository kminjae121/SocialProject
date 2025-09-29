using UnityEngine;
using UnityEngine.SceneManagement;

namespace Member.LCM._01.Scripts.UI
{
    public class GoTitleUI : MonoBehaviour
    {
        [SerializeField] private string titleSceneName;

        public void OnClick()
        {
            SceneManager.LoadScene(titleSceneName);
        }
    }
}