using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition2D = new Vector2(mousePosition.x, mousePosition.y);

            PauseMenui[] pauseMenus = FindObjectsOfType<PauseMenui>();

            foreach (PauseMenui pauseMenu in pauseMenus)
            {
                if (pauseMenu != null && pauseMenu.isPaused)
                {
                    pauseMenu.Resume();
                }
            }
        }
    }

}
 