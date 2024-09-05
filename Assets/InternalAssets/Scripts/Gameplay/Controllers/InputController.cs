using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Singleton<InputController>
{
    public int Horizontal { get; private set; }
    public int Vertical { get; private set; }

    private Vector2Int mobileInput;

    private void Update()
    {
        Horizontal = 0;
        Vertical = 0;

#if UNITY_EDITOR
        int keyboardX = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal"));
        if (keyboardX != 0)
        {
            Horizontal = keyboardX;
        }

        int keyboardY = Mathf.RoundToInt(Input.GetAxisRaw("Vertical"));
        if(keyboardY != 0)
        {
            Vertical = keyboardY;
        }
#endif

        if(mobileInput.x != 0)
        {
            Horizontal = mobileInput.x;
        }

        if (mobileInput.y != 0)
        {
            Vertical = mobileInput.y;
        }
    }

    public void SetMobileHorizontal(int v)
    {
        mobileInput.x = v;
    }

    public void SetMobileVertical(int v)
    {
        mobileInput.y = v;
    }

}
