using UnityEngine;

public enum FigureColor
{
	RED = 0,
    GREEN,
    BLUE,
    YELLOW,
	GRAY,

	MAX
}

public enum FigureShape
{
    CIRCLE = 0,
    TRIANGLE,
    SQUARE,
    PENTAGON,
    HEXAGON,

    MAX
}

[System.Serializable]
public struct FigureData
{
    public FigureColor color;
    public FigureShape shape;

    public FigureData(FigureColor figureColor, FigureShape figureShape)
	{
		color = figureColor;
		shape = figureShape;
	}

	public static FigureData GetRandomData(int maxIndex)
	{
		FigureColor color = GetRandomFigureColor(maxIndex);
		FigureShape shape = GetRandomFigureShape(maxIndex);

		return new FigureData(color, shape);
	}

	public FigureData GetData(FigureData data, bool isSameColor, bool isSameShape, int maxIndex)
	{
		FigureColor color = GetFigureColor(data.color, isSameColor, maxIndex);
		FigureShape shape = GetFigureShape(data.shape, isSameShape, maxIndex);

		return new FigureData(color, shape);
	}

	private FigureColor GetFigureColor(FigureColor figureColor, bool isSame, int maxIndex)
	{
		if (isSame)
			return figureColor;
		else
		{
			FigureColor newColor = GetRandomFigureColor(maxIndex);
			while (newColor == figureColor)
			{
				newColor = GetRandomFigureColor(maxIndex);
			}

			return newColor;
		}
	}

	private FigureShape GetFigureShape(FigureShape figureShape, bool isSame, int maxIndex)
	{
		if (isSame)
			return figureShape;
		else
		{
			FigureShape newShape = GetRandomFigureShape(maxIndex);
			while (newShape == figureShape)
			{
				newShape = GetRandomFigureShape(maxIndex);
			}

			return newShape;
		}
	}

	private static FigureColor GetRandomFigureColor(int maxIndex)
	{
		return (FigureColor)Random.Range(0, maxIndex);
	}

	private static FigureShape GetRandomFigureShape(int maxIndex)
	{
		return (FigureShape)Random.Range(0, maxIndex);
	}
}