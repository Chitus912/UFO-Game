using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPhaseExample : MonoBehaviour
{
    public Vector2 startPos;   // Vị trí bắt đầu khi chạm
    public Vector2 direction;  // Hướng di chuyển của ngón tay

    

    // Update được gọi mỗi frame
    void Update()
    {
        // Kiểm tra nếu có ít nhất một lần chạm trên màn hình
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Lưu vị trí bắt đầu khi ngón tay chạm vào màn hình
                    startPos = touch.position;
                    break;

                case TouchPhase.Moved:
                    // Tính hướng di chuyển từ điểm bắt đầu
                    direction = touch.position - startPos;

                    // Gọi phương thức di chuyển trong PlayerController
                    PlayerController.Instance.Move(direction);
                    break;

                case TouchPhase.Ended:
                    // Khi ngón tay rời màn hình, có thể dừng hoặc thực hiện hành động khác
                    break;
            }
        }
    }
}
