using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Main Settings")]
    public GameObject mainCanvas; // Tarik objek "Canvas" ke sini

    [Header("UI Panels")]
    public GameObject mainPanel;    // Tarik "MenuUI" ke sini
    public GameObject infoPanel;    // Tarik "Info" ke sini
    public GameObject levelPanel;   // Tarik "Levels" ke sini
    public GameObject settingPanel; // Tarik "Setting" ke sini

    // Variabel static untuk instruksi antar scene
    public static bool shouldOpenLevelPanel = false;

    void Awake()
    {
        // 1. Paksa waktu jalan (biar gak nge-freeze kalo abis pause)
        Time.timeScale = 1f;

        // 2. Pastikan Canvas utama AKTIF
        if (mainCanvas != null)
        {
            mainCanvas.SetActive(true);
        }
    }

    void Start()
    {
        // 1. Cek "surat titipan" dari piala (PlayerPrefs)
        // Kita ambil data bernama "BukaLevel", kalau gak ada isinya 0 (false)
        int instruksiBuka = PlayerPrefs.GetInt("BukaLevel", 0);

        if (instruksiBuka == 1)
        {
            Debug.Log("SISTEM: Menangkap pesan dari piala! Membuka Level Panel...");
            OpenLevelPanel();

            // 2. RESET titipan pesannya jadi 0 lagi
            // Biar kalau kita buka game dari awal gak langsung masuk menu level
            PlayerPrefs.SetInt("BukaLevel", 0);
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("SISTEM: Tidak ada pesan khusus. Membuka Main Menu...");
            ShowOnlyMainPanel();
        }
    }
    // ===== LOGIKA PERPINDAHAN PANEL =====

    public void OpenInfoPanel() => SetPanelActive(infoPanel);
    public void OpenLevelPanel() => SetPanelActive(levelPanel);
    public void OpenSettingPanel() => SetPanelActive(settingPanel);
    public void BackToMain() => ShowOnlyMainPanel();

    // ===== FUNGSI PEMBANTU (Wajib Rapi) =====

    private void ShowOnlyMainPanel()
    {
        SetPanelActive(mainPanel);
    }

    private void SetPanelActive(GameObject panelToActivate)
    {
        // Matikan semua panel biar gak tumpang tindih
        if (mainPanel != null) mainPanel.SetActive(false);
        if (infoPanel != null) infoPanel.SetActive(false);
        if (levelPanel != null) levelPanel.SetActive(false);
        if (settingPanel != null) settingPanel.SetActive(false);

        // Nyalakan panel yang dipilih
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
            Debug.Log("Aktif: " + panelToActivate.name);
        }
        else
        {
            // Kalo panel kosong, balik ke menu utama biar gak blank
            if (mainPanel != null) mainPanel.SetActive(true);
            Debug.LogWarning("Ada slot Panel yang kosong di Inspector MenuManager!");
        }
    }

    // ===== SCENE MANAGEMENT =====

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}