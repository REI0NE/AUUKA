using System.Collections.Generic;

public class EnemyList
{
    private HashSet<IEnemy> _enemy = new HashSet<IEnemy>();
    public void Add(IEnemy enemy) => _enemy.Add(enemy);
    public void Remove(IEnemy enemy) => _enemy.Remove(enemy);

    public IEnemy GetEnemy(string name)
    {
        IEnemy result = null;

        foreach (IEnemy enemy in _enemy)
            if (enemy.Name().Equals(name))
            {
                result = enemy;
                break;
            }

        return result;
    }
}
