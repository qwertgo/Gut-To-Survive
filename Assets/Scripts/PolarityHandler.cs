public static class PolarityHandler
{
    public enum Polarity { negativ, neutral, positiv };


    public static int PolarityToInt(Polarity polarity)
    {
        return (int)polarity - 1;
    }

    public static bool PolarityToBool(Polarity polarity)
    {
        return polarity == Polarity.positiv;
    }

    public static int DirectionFromPolaritys(int polarity1, int polarity2)
    {
        return polarity1 * polarity2;
    }

    public static int DirectionFromPolaritys(Polarity polarity1, Polarity polarity2)
    {
        int pol1 = PolarityToInt(polarity1);
        int pol2 = PolarityToInt(polarity2);
        return DirectionFromPolaritys(pol1, pol2);
    }

    public static float Modulo(float a, float n)
    {
        return ((a % n) + n) % n;
    }
}
