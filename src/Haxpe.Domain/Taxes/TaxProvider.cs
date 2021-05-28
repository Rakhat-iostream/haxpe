namespace Haxpe.Taxes
{
    public class TaxProvider: ITaxProvider
    {
        public decimal GetTax()
        {
            return 19;
        }
    }
}