using System;
using UnityEngine;


public class Easing
{

	public static float Linear(float k)
	{
		return k;
	}

	public static float QuadraticIn(float k)
	{
		return k * k;
	}

	public static float QuadraticOut(float k)
	{
		return k * (2f - k);
	}

	public static float QuadraticInOut(float k)
	{
		if ((k *= 2f) < 1f) return 0.5f * k * k;
		return -0.5f * ((k -= 1f) * (k - 2f) - 1f);
	}

	public static float CubicIn(float k)
	{
		return k * k * k;
	}

	public static float CubicOut(float k)
	{
		return 1f + ((k -= 1f) * k * k);
	}

	public static float CubicInOut(float k)
	{
		if ((k *= 2f) < 1f) return 0.5f * k * k * k;
		return 0.5f * ((k -= 2f) * k * k + 2f);
	}

	public static float QuarticIn(float k)
	{
		return k * k * k * k;
	}

	public static float QuarticOut(float k)
	{
		return 1f - ((k -= 1f) * k * k * k);
	}

	public static float QuarticInOut(float k)
	{
		if ((k *= 2f) < 1f) return 0.5f * k * k * k * k;
		return -0.5f * ((k -= 2f) * k * k * k - 2f);
	}


	public static float QuinticIn(float k)
	{
		return k * k * k * k * k;
	}

	public static float QuinticOut(float k)
	{
		return 1f + ((k -= 1f) * k * k * k * k);
	}

	public static float QuinticInOut(float k)
	{
		if ((k *= 2f) < 1f) return 0.5f * k * k * k * k * k;
		return 0.5f * ((k -= 2f) * k * k * k * k + 2f);
	}


	public static float SinusoidalIn(float k)
	{
		return 1f - Mathf.Cos(k * Mathf.PI / 2f);
	}

	public static float SinusoidalOut(float k)
	{
		return Mathf.Sin(k * Mathf.PI / 2f);
	}

	public static float SinusoidalInOut(float k)
	{
		return 0.5f * (1f - Mathf.Cos(Mathf.PI * k));
	}

	internal static float GetByName(string v, float f)
	{
		var method = typeof(Easing).GetMethod(v);
		var result = method.Invoke(null, new object[] { f });
		return (float)result;
	}

	public static float ExponentialIn(float k)
	{
		return k == 0f ? 0f : Mathf.Pow(1024f, k - 1f);
	}

	public static float ExponentialOut(float k)
	{
		return k == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * k);
	}

	public static float ExponentialInOut(float k)
	{
		if (k == 0f) return 0f;
		if (k == 1f) return 1f;
		if ((k *= 2f) < 1f) return 0.5f * Mathf.Pow(1024f, k - 1f);
		return 0.5f * (-Mathf.Pow(2f, -10f * (k - 1f)) + 2f);
	}


	public static float CircularIn(float k)
	{
		return 1f - Mathf.Sqrt(1f - k * k);
	}

	public static float CircularOut(float k)
	{
		return Mathf.Sqrt(1f - ((k -= 1f) * k));
	}

	public static float CircularInOut(float k)
	{
		if ((k *= 2f) < 1f) return -0.5f * (Mathf.Sqrt(1f - k * k) - 1);
		return 0.5f * (Mathf.Sqrt(1f - (k -= 2f) * k) + 1f);
	}


	public static float ElasticIn(float k)
	{
		if (k == 0) return 0;
		if (k == 1) return 1;
		return -Mathf.Pow(2f, 10f * (k -= 1f)) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f);
	}

	public static float ElasticOut(float k)
	{
		if (k == 0) return 0;
		if (k == 1) return 1;
		return Mathf.Pow(2f, -10f * k) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f) + 1f;
	}

	public static float ElasticInOut(float k)
	{
		if ((k *= 2f) < 1f) return -0.5f * Mathf.Pow(2f, 10f * (k -= 1f)) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f);
		return Mathf.Pow(2f, -10f * (k -= 1f)) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f) * 0.5f + 1f;
	}

	static float s = 1.70158f;
	static float s2 = 2.5949095f;

	public static float BackIn(float k)
	{
		return k * k * ((s + 1f) * k - s);
	}

	public static float BackOut(float k)
	{
		return (k -= 1f) * k * ((s + 1f) * k + s) + 1f;
	}

	public static float BackInOut(float k)
	{
		if ((k *= 2f) < 1f) return 0.5f * (k * k * ((s2 + 1f) * k - s2));
		return 0.5f * ((k -= 2f) * k * ((s2 + 1f) * k + s2) + 2f);
	}

	public static float BounceIn(float k)
	{
		return 1f - BounceOut(1f - k);
	}

	public static float BounceOut(float k)
	{
		if (k < (1f / 2.75f))
		{
			return 7.5625f * k * k;
		}
		else if (k < (2f / 2.75f))
		{
			return 7.5625f * (k -= (1.5f / 2.75f)) * k + 0.75f;
		}
		else if (k < (2.5f / 2.75f))
		{
			return 7.5625f * (k -= (2.25f / 2.75f)) * k + 0.9375f;
		}
		else
		{
			return 7.5625f * (k -= (2.625f / 2.75f)) * k + 0.984375f;
		}
	}
	public static float BounceInOut(float k)
	{
		if (k < 0.5f) return BounceIn(k * 2f) * 0.5f;
		return BounceOut(k * 2f - 1f) * 0.5f + 0.5f;
	}
}
