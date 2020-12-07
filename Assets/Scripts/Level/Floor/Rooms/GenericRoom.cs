using UnityEngine;
/// <summary>
/// Most basic room type. Contains only enemies.
/// </summary>
public class GenericRoom : Room
{

    /// <inheritdoc/>
    public override void Create(GridLocation gridLocation)
    {
        // if room is close to center
        if (Mathf.Abs(gridLocation.X) < 5 && Mathf.Abs(gridLocation.Y) < 5)
        {
            MinDoors = 1;
            MaxDoors = 2;
        }
        else
        {
            MinDoors = 0;
            MaxDoors = 1;
        }
        base.Create(gridLocation);
    }
    /// <summary>
    /// Populates this room with enemies
    /// </summary>
    public override void Populate()
    {
        base.Populate();
        var enemyPrefabs = PrefabHelper.GetEnemies();

        var enemyCount = Random.Range(1, 1 + GameController.Instance.CurrentFloor.FloorNumber);

        for(var i = 0; i < enemyCount; i++)
        {
            var newEnemy = GameObject.Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], transform);
            newEnemy.transform.localPosition = new Vector2(Random.Range(2.1f, InnerBounds.size.x), Random.Range(2.1f, InnerBounds.size.y));
            newEnemy.GetComponent<Enemy>().Movement.CurrentRoom = this;
        }

    }
}