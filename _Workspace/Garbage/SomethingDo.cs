using UnityEngine;

public class SomethingDo
{
    public SomethingDo()
    {
        JustDoIt();
    }

    public void JustDoIt()
    {
        int y = 0;
        int x = 0;

        Vector2 vector2 = new Vector2(x, y);

        GetCache(vector2);
    }

    public void GetCache(Vector2 vector2)
    {
        float next = 0.0f;
        float first = 1.0f;

        float second = next + (first - 1.0f);
        ToVector(next, second);
    }

    public Vector2 ToVector(float x, float y)
    {
        SecondFloor();

        return new Vector2(x, y);
    }

    public void SecondFloor()
    {
        for (int i = 0; i < 5; i++)
        {

        }
    }
}
