public class Enemy
{
    public EnemyStats stats;
    public GameObject gameObject; // Reference to the enemy's GameObject

    public Enemy(EnemyStats stats, GameObject gameObject)
    {
        this.stats = stats;
        this.gameObject = gameObject;
    }
}