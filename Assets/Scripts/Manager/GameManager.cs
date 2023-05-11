using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Hàm để khởi động lại màn chơi
    public void Restart()
    {
        // Lấy tên của màn chơi hiện tại
        string sceneName = SceneManager.GetActiveScene().name;

        // Tải lại màn chơi theo tên đã lấy
        SceneManager.LoadScene(sceneName);
    }
}
