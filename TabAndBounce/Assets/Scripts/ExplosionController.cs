using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public void OnExplosionFinished()
    {
        GameManager.Instance.GameOver();

        
        Destroy(gameObject);
    }
}
