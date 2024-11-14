using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance; // Để dễ dàng truy cập từ các script khác
  
    [SerializeField] private float speed;  // Tốc độ di chuyển của người chơi
    public Text countText;                 // Tham chiếu đến Text hiển thị số lượng đồng xu
    public Text winText;                  // Tham chiếu đến Text hiển thị thông báo chiến thắng

    private Rigidbody2D rb2d;              // Tham chiếu đến Rigidbody2D để điều khiển vật lý
    private int count;                     // Biến đếm số lượng đồng xu nhặt được

    private Vector2 startPos;              // Vị trí bắt đầu điểm chạm
    private Vector2 direction;             // Hướng di chuyển từ thao tác chạm

    [SerializeField] private TouchPhaseExample touchPhaseExample; // Tham chiếu đến script TouchPhaseExample

    void Awake()
    {
        // Thiết lập Singleton cho PlayerController
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  // Lấy tham chiếu đến Rigidbody2D
        count = 0;  // Khởi tạo số lượng đồng xu

        // Tìm và gán tham chiếu đến các text
        countText = GameObject.Find("CountText").GetComponent<Text>();
        winText = GameObject.Find("WinText").GetComponent<Text>();

        winText.text = " ";  // Đặt văn bản chiến thắng là rỗng ban đầu
        SetCountText();      // Cập nhật nội dung hiển thị số lượng đồng xu
    }

    void FixedUpdate()
    {
        // Lấy giá trị di chuyển từ Input
        float moveHorizontal = 0;
        float moveVertical = 0;

        var platform = Application.platform;
        if (platform == RuntimePlatform.WindowsEditor)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Moved)
                {
                    float touchSensitivity = 0.1f; // điểu chỉnh giá trị để thay đổi tốc độ
                    moveHorizontal = touch.deltaPosition.x * touchSensitivity ;
                    moveVertical = touch.deltaPosition.y * touchSensitivity;
                }

            }
        }
            // Tạo vector di chuyển
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Áp dụng lực di chuyển lên Rigidbody2D
        rb2d.AddForce(movement * speed);
    }

    // Kiểm tra va chạm với các đối tượng có tag "PickUp"
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);  // Tắt đối tượng đồng xu
            count += 1;  // Tăng số lượng đồng xu

            // Phát âm thanh khi nhặt đồng xu (nếu có)
            if (SoundEffect.soundEffect != null)
            {
                SoundEffect.soundEffect.PlayCollectCoinSound();
            }
            SetCountText();  // Cập nhật lại số lượng đồng xu trên màn hình
        }
    }

    // phương thức di chuyển nhân vật
    public void Move(Vector2 direction)
    {
       // rb2d.velocity = new Vector2(direction.x * speed , direction.y * speed); // di chuyển nhân vật theo cảm ứng
    }
    void SetCountText()
    {
        countText = GameObject.Find("CountText").GetComponent<Text>();
        countText.text = "Count: " + count.ToString();  // Cập nhật số lượng đồng xu
        if (count >= 12)  // Nếu số lượng đồng xu đạt tối thiểu 12, hiển thị thông báo chiến thắng
        {
            winText.text = "You win!";
        }
    }
}
