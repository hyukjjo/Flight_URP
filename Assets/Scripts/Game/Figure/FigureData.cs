using UnityEngine;

public enum FigureColor
{
	GRAY = 0,
	RED,
    GREEN,
    BLUE,
    YELLOW,

    MAX
}

public enum FigureShape
{
    CIRCLE = 0,
    TRIANGLE,
    SQUARE,
    PENTAGON,
    HEXAGON,

    MAX,
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

	public FigureData GetRandomData()
	{
		FigureColor color = GetRandomFigureColor();
		FigureShape shape = GetRandomFigureShape();

		return new FigureData(color, shape);
	}

	public FigureData GetData(FigureData data, bool isSameColor, bool isSameShape)
	{
		FigureColor color = GetFigureColor(data.color, isSameColor);
		FigureShape shape = GetFigureShape(data.shape, isSameShape);

		return new FigureData(color, shape);
	}

	private FigureColor GetFigureColor(FigureColor figureColor, bool isSame)
	{
		if (isSame)
			return figureColor;
		else
		{
			FigureColor newColor = GetRandomFigureColor();
			while (newColor == figureColor)
			{
				newColor = GetRandomFigureColor();
			}

			return newColor;
		}
	}

	private FigureShape GetFigureShape(FigureShape figureShape, bool isSame)
	{
		if (isSame)
			return figureShape;
		else
		{
			FigureShape newShape = GetRandomFigureShape();
			while (newShape == figureShape)
			{
				newShape = GetRandomFigureShape();
			}

			return newShape;
		}
	}

	private FigureColor GetRandomFigureColor()
	{
		return (FigureColor)Random.Range(0, (int)FigureShape.MAX);
	}

	private FigureShape GetRandomFigureShape()
	{
		return (FigureShape)Random.Range(0, (int)FigureShape.MAX);
	}
}