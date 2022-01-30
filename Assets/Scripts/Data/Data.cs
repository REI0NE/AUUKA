
public class Data
{
    private MonoCacheList _monoCacheList = new MonoCacheList();
    public MonoCacheList MonoCacheList => _monoCacheList;

    private IPlayer _player = null;
    public IPlayer Player { get => _player; set => _player = value; }

    private EnemyList _enemyList = new EnemyList();
    public EnemyList EnemyList => _enemyList;

    private InteractionList _interactionList = new InteractionList();
    public InteractionList InteractionList => _interactionList;

    private LayersGameList _layersGame = new LayersGameList();
    public LayersGameList LayersGameList => _layersGame;

    private Settings _settings = new Settings();
    public Settings Settings => _settings;
}
