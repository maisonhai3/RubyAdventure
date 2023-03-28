using UnityEngine;

public class FlameManager : MonoBehaviour
{
}

public interface IFlame
{
    void ShowFlame();

    void DestroyFlame();
}

public class BlueFlame : MonoBehaviour, IFlame
{
    public void ShowFlame()
    {
        var blueFlame = Instantiate(Resources.Load("Flame-blue", typeof(GameObject))) as GameObject;
        blueFlame.transform.parent = transform;
    }

    public void DestroyFlame()
    {
        Destroy(gameObject);
    }
}

public class RedFlame : MonoBehaviour, IFlame
{
    public void ShowFlame()
    {
        var redFlame = Instantiate(Resources.Load("Flame-red", typeof(GameObject))) as GameObject;
        redFlame.transform.parent = transform;
    }

    public void DestroyFlame()
    {
        Destroy(gameObject);
        
    }
}