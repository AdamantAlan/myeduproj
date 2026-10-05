namespace QA.Order.App
{
    public interface IRefundService
    {
        bool Refund(long id);
    }

    public class RefundService : IRefundService
    {
        public bool Refund(long id)
        {
            throw new NotImplementedException();
        }
    }
}
