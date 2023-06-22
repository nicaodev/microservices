using GeekShop.Email.Messages;

namespace GeekShop.Email.Model.Repository;

public interface IEmailRepository
{
    Task LogEmail(UpdatePaymentResultMessage message);
}