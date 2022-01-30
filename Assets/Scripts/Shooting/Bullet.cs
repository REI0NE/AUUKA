using UnityEngine;

public class Bullet : MonoCache
{
    [SerializeField] private float _speed = 0f;
    [SerializeField] private Rigidbody _rigidbody = null;

    private float timeLife = 3f, timer = 0f;

    private void Start() => _rigidbody.velocity = transform.right * _speed;


    private void OnEnable() => AddUpdateTick();

    private void OnDisable() => RemoveUpdateTick();

    private void OnDestroy() => OnDisable();

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
            Destroy(gameObject);
    }

    public override void UpdateTick()
    {
        timer += Time.deltaTime;

        if (timer > timeLife)
            Destroy(gameObject);
    }

}
