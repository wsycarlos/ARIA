/* ========================================================================
* Copyright © 2013-2014, MoreFunGame. All rights reserved.
*
* 作  者：HanXusheng 时间：1/16/2015 10:31:39 AM
* 文件名：RotateObject
* 版  本：V1.0.0
*
* 修改者：
* 时  间： 
* 修改说明：
* ========================================================================
*/


using UnityEngine;

/// <summary>
/// 功能概述：
/// </summary>
public class RotateObject : MonoBehaviour
{
    Vector3 StartPosition;
    Vector3 previousPosition;
    Vector3 offset;
    Vector3 finalOffset;
    Vector3 eulerAngle;

    bool isSlide;
    float angle;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartPosition = Input.mousePosition;
            previousPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            offset = Input.mousePosition - previousPosition;
            offset.y = 0;
            previousPosition = Input.mousePosition;
            transform.Rotate(Vector3.Cross(offset, Vector3.forward).normalized, offset.magnitude, Space.World);
            foreach (Transform tran in transform)
            {
                tran.Rotate(Vector3.Cross(offset, Vector3.back).normalized, offset.magnitude, Space.World);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            finalOffset = Input.mousePosition - StartPosition;
            finalOffset.y = 0;
            isSlide = true;
            angle = finalOffset.magnitude;
        }

        if (isSlide)
        {
            transform.Rotate(Vector3.Cross(finalOffset, Vector3.forward).normalized, angle * 2 * Time.deltaTime, Space.World);
            foreach (Transform tran in transform)
            {
                tran.Rotate(Vector3.Cross(finalOffset, Vector3.back).normalized, angle * 2 * Time.deltaTime, Space.World);
            }
            if (angle > 0)
            {
                angle -= 5;
            }
            else
            {
                angle = 0;
            }
        }


    }
}
