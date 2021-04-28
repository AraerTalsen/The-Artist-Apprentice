using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalOverworldScript : MonoBehaviour
{
    public Enemy[] party = new Enemy[3];//Which enemies will appear in combat
    public GameObject[] lootDrops;

    private Animator anim;
    private Rigidbody2D rb;

    public Animator transition;
    public SpriteRenderer enemySpriteRenderer;

    public float portalSpawnMaxX, portalSpawnMaxY, portalSpawnMinX, portalSpawnMinY;
    public Transform portalTransform;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < party.Length; i++)
        {
            party[i] = Instantiate(party[i]);
        }

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        this.gameObject.transform.position = new Vector3(Random.Range(portalSpawnMinX, portalSpawnMaxX), Random.Range(portalSpawnMinY, portalSpawnMaxY), this.transform.position.z);
    }

    //Start combat if collide with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerMovement.pauseGame = true;
            CombatSystem.enemyParty = party;
            CombatSystem.isPortal = true;
            //LocationRememberer.awokenDim[FindObjectOfType<LocationLoader>().num] = true;
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        CombatSystem.id = GetComponent<Whackable>().id;
        CombatSystem.enemyParty = party;
        StartCoroutine(LoadLevel("Combat"));
    }

    IEnumerator LoadLevel(string levelIndex)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Overworld/SFX/EnterCombat");
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Combat");
    }
}
