using UnityEngine;
using System.Collections;

public class MathUtil
{

    public static float pointLineDistance(Vector2 linePoint1, Vector2 linePoint2, Vector2 externalPoint)
    {
        // n(a,b), ep= externalP  -> |a*ep.x + b*ep.y + c| / sqrt( a^2 + b^2)

        Vector2 v = new Vector2((linePoint1.x - linePoint2.x), (linePoint1.y - linePoint2.y));
        Vector2 n = new Vector2(-v.y, v.x);

        float c = n.x * linePoint2.x + n.y * linePoint2.y;

        float distance = Mathf.Abs(n.x*externalPoint.x + n.y*externalPoint.y - c)/
                         Mathf.Sqrt(n.x*n.x + n.y*n.y);

        return distance;
    }

    public static bool isLineIntersectsCircleOrCloserToNest(Vector2 linePoint1, Vector2 linePoint2, Vector2 center, float radius)
    {
        if (distance(linePoint1, linePoint2) < distance(center, linePoint2))
            return false;
        return radius >= pointLineDistance(linePoint1, linePoint2, center);
    }


    public static void circlesIntersections(Vector2 c1,float r1, Vector2 c2, float r2,
                                    out Vector2 intersection1, out Vector2 intersection2)
    {
        float distance = Vector2.Distance(c1, c2);

        float a = (r1 * r1 - r2 * r2 + distance * distance) / (2 * distance);
        float m = Mathf.Sqrt((r1 * r1 - a * a));


        Vector2 p;
        float px = c1.x + (c2.x - c1.x) * (a / distance); //c1 bol c2 be mutato a hosszu vektor
        float py = c1.y + (c2.y - c1.y) * (a / distance);
        p = new Vector2(px, py);

        float i1x = p.x + (c2.y - c1.y) * (m / distance);  //p bol c1-c2 re merolegesen mutato m hosszu vektor
        float i1y = p.y - (c2.x - c1.x) * (m / distance);

        float i2x = p.x - (c2.y - c1.y) * (m / distance);  
        float i2y = p.y + (c2.x - c1.x) * (m / distance);

        intersection1 = new Vector2(i1x, i1y);
        intersection2 = new Vector2(i2x, i2y);
    }

    public static void getTangentsFromPoint(Vector2 p , Vector2 c, float r, out Vector2 t1, out Vector2 t2)
    {
        float distance = Mathf.Sqrt(Mathf.Pow((p.x - c.x), 2) + Mathf.Pow((p.y - c.y), 2));

        circlesIntersections(p, distance, c, r, out t1, out t2);
    }

    public static Vector2 pickCloserTangent(Vector2 t1, Vector2 t2, Vector2 rock,Vector2 obstacle, Vector2 nest, bool pickCloser)
    {
        float d1 = Vector2.Distance(t1, obstacle);
        float d2 = Vector2.Distance(t2, obstacle);

        if (pickCloser)
            return d1 < d2 ? t1 : t2;
        else
        {
            return d1 < d2 ? t2 : t1;
        }
    }

    public static float distance(Vector2 p1, Vector2 p2)
    {
        return Mathf.Sqrt(Mathf.Pow((p1.x - p2.x), 2) + Mathf.Pow((p1.y - p2.y), 2));
    }
   
}
