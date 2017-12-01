public class MathExtensions
{
	public static float Remap( float value, float min1, float max1, float min2, float max2 )
	{
		return min2 + ( value - min1 ) * ( max2 - min2 ) / ( max1 - min1 );
	}

	public static float Remap01( float value, float min, float max )
	{
		return 0 + ( value - min ) * ( 1 - 0 ) / ( max - min );
	}
}
