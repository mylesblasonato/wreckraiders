using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    public int mapPrice = 1;
    public int upgradePrice = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetPort(this);

            if (GameManager.Instance.portUI.alpha == 0)
            {
                GameManager.Instance.portUI.alpha = 1;
                InputManager.Instance.HideAndLockCursor(false);
            }
            else
            {
                GameManager.Instance.portUI.alpha = 0;
                InputManager.Instance.HideAndLockCursor(true);
            }
        }
    }
}
