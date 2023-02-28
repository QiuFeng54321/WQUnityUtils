using UnityEngine;

namespace WilliamQiufeng.UnityUtils.Easing
{
    public static class BezierCurve
    {
        public static Vector2 YFromT(Vector2 c1, Vector2 c2, float t)
        {
            return EasingFunction.CubicBezier(Vector2.zero, c1, c2, Vector2.one, t);
        }

        /// <summary>
        ///     https://stackoverflow.com/a/7355667/11225486 <br />
        ///     Solution for Bezier((0, 0), c1, c2, (1, 1)) for given x<br />
        ///     can be used for animation
        /// </summary>
        /// <param name="c1">First control point</param>
        /// <param name="c2">Second control point</param>
        /// <param name="targetX">X to look for</param>
        /// <returns>Y</returns>
        public static float YFromX(Vector2 c1, Vector2 c2, float targetX)
        {
            if (targetX < 0) return 0;
            if (targetX > 1) return 1;
            const float xTolerance = 0.001f; //adjust as you please
            var iterCount = 0;
            //we could do something less stupid, but since the x is monotonic
            //increasing given the problem constraints, we'll do a binary search.

            //establish bounds
            var lower = 0f;
            var upper = 1f;
            var percent = (upper + lower) / 2;

            //get initial x
            var x = YFromT(c1, c2, percent).x;

            //loop until completion
            while (Mathf.Abs(targetX - x) > xTolerance)
            {
                if (targetX > x)
                    lower = percent;
                else
                    upper = percent;

                percent = (upper + lower) / 2;
                x = YFromT(c1, c2, percent).x;
                if (iterCount++ > 100) break;
            }

            //we're within tolerance of the desired x value.
            //return the y value.
            return YFromT(c1, c2, percent).y;
        }
    }
}