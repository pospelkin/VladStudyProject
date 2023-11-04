using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeBehaviour : MonoBehaviour
{
    public Text cubeNumber;
    public int cubeIndex;
    public int positionX;
    public int positionY;

    public int winPositionX;
    public int winPositionY;

    public bool WinPosition()
    {
        if(positionX == winPositionX && positionY == winPositionY)
        {
            return true;
        }
        return false;
    }


}
