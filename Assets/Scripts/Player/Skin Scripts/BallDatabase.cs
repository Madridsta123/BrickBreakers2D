using UnityEngine;

[CreateAssetMenu]
public class BallDatabase : ScriptableObject
{
    public Sprite[] ballSprites;

    public int ballCount
    {
        get
        {
            return ballSprites.Length;
        }
    }

    public Sprite GetBall(int index)
    {
        return ballSprites[index];
    } 
}
