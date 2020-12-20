namespace Campaignr.Core.Helpers
{
    public class NumbersHelper
    {
        public static int RoundToNearestX(int input, int roundTo)
        {
            return input % roundTo >= (roundTo / 2) ? input + roundTo - input % roundTo : input - input % roundTo;
        }
    }
}
