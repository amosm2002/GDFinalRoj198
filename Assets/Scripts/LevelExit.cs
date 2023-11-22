using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public AudioClip exitSoundClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(exitSoundClip, transform.position); 
            StartCoroutine(LoadNextLevelAndHandlePlayer(other.gameObject));
        }
    }

    private IEnumerator LoadNextLevelAndHandlePlayer(GameObject player)
    {
        yield return new WaitForSeconds(0.5f);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        string nextSceneName = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);

        if (nextSceneName.Contains("YouWon"))
        {
            Destroy(player);
        }
        else
        {
            yield return new WaitForEndOfFrame();
            Vector2 fixedSpawnLocation = new Vector2(-20.13f, -7.40f);
            player.transform.position = fixedSpawnLocation;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}
