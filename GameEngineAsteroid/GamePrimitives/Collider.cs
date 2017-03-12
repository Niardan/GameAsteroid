namespace GameEngineAsteroid.GamePrimitives
{
   // Класс коллайдер отвечает за физический объем объекта, и проверку на столкновения.
    public class Collider
    {
        public readonly GamePolygon GamePolygon;

        public Collider(GamePolygon gamePolygon)
        {
            GamePolygon = gamePolygon;
        }

        public bool IsCollusion(Collider collusionObject)
        {
            return IsCollusion(collusionObject.GamePolygon);
        }
        public bool IsCollusion(GamePolygon collusionObject)
        {
            if (GamePolygon.CenterPolygonAbsolute.DistanceTo(collusionObject.CenterPolygonAbsolute) >
                GamePolygon.MaxRadiusObject + collusionObject.MaxRadiusObject)
            {
                return false;
            }
            return IsCollusionAccurate(collusionObject);

        }

        private bool IsCollusionAccurate(GamePolygon collusionObject)
        {
            var gamePolygonPoints = GamePolygon.GetDrawPoints();
            var collusionPolygonPoints = collusionObject.GetDrawPoints();

            for (int i = 0; i < gamePolygonPoints.Length; i++)
            {
                for (int j = 0; j < collusionPolygonPoints.Length; j++)
                {
                    var a1 = gamePolygonPoints[i];
                    var a2 = gamePolygonPoints[(i + 1 != gamePolygonPoints.Length) ? i + 1 : 0];
                    var b1 = collusionPolygonPoints[j];
                    var b2 = collusionPolygonPoints[(j + 1 != collusionPolygonPoints.Length) ? j + 1 : 0];
                    if (IsCollusionLine(a1, a2, b1, b2))
                        return true;
                }
            }
            return false;
        }

        internal bool IsCollusionLine(GamePoint a1, GamePoint a2, GamePoint b1, GamePoint b2)
        {
            var v1 = (b2.X - b1.X) * (a1.Y - b1.Y) - (b2.Y - b1.Y) * (a1.X - b1.X);
            var v2 = (b2.X - b1.X) * (a2.Y - b1.Y) - (b2.Y - b1.Y) * (a2.X - b1.X);
            var v3 = (a2.X - a1.X) * (b1.Y - a1.Y) - (a2.Y - a1.Y) * (b1.X - a1.X);
            var v4 = (a2.X - a1.X) * (b2.Y - a1.Y) - (a2.Y - a1.Y) * (b2.X - a1.X);

            return (v1 * v2 < 0) && (v3 * v4 < 0);

        }

    }
}
