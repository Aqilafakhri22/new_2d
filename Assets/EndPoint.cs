using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    public string nextSceneName = "SampleScene";

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // Ganti static bool pake PlayerPrefs (Lebih Aman!)
            // Kita simpan angka 1 (artinya TRUE) dengan nama kunci "BukaLevel"
            PlayerPrefs.SetInt("BukaLevel", 1);
            PlayerPrefs.Save();

            Debug.Log("Piala ditabrak! Surat titipan 'BukaLevel' disimpan.");

            // Pastikan waktu jalan biar gak nge-freeze
            Time.timeScale = 1f;

            SceneManager.LoadScene(nextSceneName);
        }
    }
}