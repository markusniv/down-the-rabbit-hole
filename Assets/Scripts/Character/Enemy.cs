using System.Linq;
using UnityEngine;

public class Enemy : Character
{
    /// <summary>
    /// Health bar game object prefab that is used to spawn healthbar of the enemy when hit
    /// </summary>
    public GameObject healthBar;
    /// <summary>
    /// The variable that stores and tracks the health bar of the enemy
    /// </summary>
    private GameObject myHealthBar;
    /// <summary>
    /// Timer variable used to delete the health bar after a certain period of time
    /// </summary>
    private float healthTimer = 0;

    /// <summary>
    /// Difficulty modifier as percentage. 1 = normal difficulty. 2 = twice as hard.
    /// </summary>
    public float DifficultyModifier = 1;

    /// <summary>
    /// Score that player will be given when this enemy is killed
    /// </summary>
    public DifficultyAware KillScore { get; private set; }

    /// <summary>
    /// Sets random health with <see cref="DifficultyModifier"/> applied
    /// </summary>

    private bool healthBarSpawned = false;
    private void SetRandomHealth()
    {
        MaxHealth = CurrentHealth = Random.Range(100, 300) * DifficultyModifier;
    }

    /// <summary>
    /// Setup for <see cref="KillScore"/>. <see cref="KillScore"/> Scales with <see cref="Character.MaxHealth"/> and <see cref="DifficultyModifier"/>
    /// </summary>
    private void SetKillScore()
    {
        KillScore = new DifficultyAware(
            MaxHealth / 10,
            DifficultyModifier,
            (x) => x.BaseValue * x.DifficultyModifier
            );
    }


    protected override void Start()
    {
        DifficultyModifier = GameController.Instance.CurrentFloor.FloorNumber;
        SetRandomHealth();
        SetKillScore();
        base.Start();
    }

    /// <summary>
    /// Called when health is 0. Enemy will drop all items when it dies.
    /// </summary>
    public override void Die()
    {
        GameController.Instance.Player.Score += KillScore.Value;
        Destroy(myHealthBar);
        base.Die();
        foreach(Item item in Inventory.Items.ToList())
        {
            Inventory.DropItem(item);
        }
        Destroy(gameObject);
    }
    /// <summary>
    /// Check collision with player's weapon to show enemy health bar
    /// </summary>
    /// <param name="collision">A weapon</param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out Weapon weapon)) return;
        // If the health bar already exists, destroy it
        if (myHealthBar != null)
        {
            Destroy(myHealthBar);
        }
        // If the enemy is not dead, spawn health bar and store it in myHealthBar variable
        if (!Dead)
        {
            SpawnHealthbar();
        }

    }

    void SpawnHealthbar()
    {
        healthTimer = 1;
        myHealthBar = Instantiate(healthBar, GameObject.Find("UI").transform);
        myHealthBar.GetComponent<Healthbar>().character = this;
        myHealthBar.GetComponent<Healthbar>().SyncHealth();

        MoveHealthBar();
    }
    /// <summary>
    /// Move the health bar around in the UI canvas so that it moves with the enemy in the game world
    /// </summary>
    private void MoveHealthBar()
    {
        float offsetPosY = transform.position.y + 1f;
        Vector3 offsetPos = new Vector3(transform.position.x, offsetPosY, transform.position.z);

        Vector2 canvasPos;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

        RectTransform canvasRect = GameObject.Find("UI").GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, null, out canvasPos);

        myHealthBar.transform.localPosition = canvasPos;
    }

    /// <summary>
    /// Move the health bar and update the timer for the health bar + destroy it if the timer hits zero
    /// </summary>
    protected override void Update()
    {
        if (this.StatusEffects.Count > 0)
        {
            if ((this.StatusEffects[0].ToString() == "Poisoned") && (!healthBarSpawned))
            {
                SpawnHealthbar();
                healthBarSpawned = true;
            }
        }
        if (healthTimer < 0 && myHealthBar != null)
        {
            healthBarSpawned = false;
            Destroy(myHealthBar);
        }
        else
        {
            if (myHealthBar != null)
            {
                MoveHealthBar();
                healthTimer -= Time.deltaTime;
            }
        }
    }
}