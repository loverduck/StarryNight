using UnityEngine;

public class AndroidBackButtonManager : MonoBehaviour
{
    bool isPaused = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!isPaused)
            {
                // if game is not yet paused, ESC will pause it
                isPaused = true;
            }
            else
            {
                // if game is paused and ESC is pressed, it's the second press. QUIT
                Application.Quit();
            }
        }
    }
}