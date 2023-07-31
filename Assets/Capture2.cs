using UnityEngine;

public class Capture2 : MonoBehaviour
{
    public KeyCode captureKey = KeyCode.Space; // Ekran görüntüsünü almak için kullanılacak tuş
    public int textureWidth = 1920; // Ekran görüntüsü texture'ının genişliği
    public int textureHeight = 1080; // Ekran görüntüsü texture'ının yüksekliği

    private Camera captureCamera;
    private RenderTexture renderTexture;

    private void Start()
    {
        captureCamera = Camera.main; // Ekran görüntüsünü almak için kamerayı seçebilirsiniz
        renderTexture = new RenderTexture(textureWidth, textureHeight, 64); // 32 ile 32-bit RGBA renk modunu seçiyoruz (8 bit alfa kanalı)
    }



    public void CaptureAndSaveScreenshot()
    {
        // Kamerayı geçici olarak kullanılacak RenderTexture'e bağlayalım
        RenderTexture prevRenderTexture = captureCamera.targetTexture;
        captureCamera.targetTexture = renderTexture;

        // Kamerayı yeniden çizelim
        captureCamera.Render();

        // Ekran görüntüsünü RenderTexture'ten alalım
        Texture2D screenshot = new Texture2D(textureWidth, textureHeight, TextureFormat.RGBA32, false);
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, textureWidth, textureHeight), 0, 0);
        screenshot.Apply();

        // Eski RenderTexture'i tekrar bağlayalım ve temizleyelim
        captureCamera.targetTexture = prevRenderTexture;
        RenderTexture.active = null;

        // Ekran görüntüsünü PNG olarak kaydet
        byte[] bytes = screenshot.EncodeToPNG();
        string filename = "screenshot_" + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/" + filename, bytes);

        Debug.Log("Ekran görüntüsü kaydedildi: " + Application.persistentDataPath + "/" + filename);

        // Texture'i serbest bırak
        Destroy(screenshot);
    }
}
