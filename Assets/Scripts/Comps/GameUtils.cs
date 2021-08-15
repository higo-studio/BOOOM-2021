using Unity.Mathematics;

public class GameUtils
{
    public static float3 CalculateLineSurfacePoint(float3 linePos, float3 lineVec, float3 plantPos, float3 plantNormal)
    {
        var l0 = linePos;
        var p0 = plantPos;
        var n = plantNormal;
        var l = lineVec;

        if (math.dot(l, n) == 0)
        {
            return float3.zero;
        }
        var d = math.dot((p0 - l0), n) / math.dot(l, n);
        return d * l + l0;
    }
}