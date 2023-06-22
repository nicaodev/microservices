using GeekShop.Email.Messages;
using GeekShop.Email.Model.Base.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShop.Email.Model.Repository;

public class EmailRepository : IEmailRepository
{
    private readonly DbContextOptions<MySQLContext> _context;

    public EmailRepository(DbContextOptions<MySQLContext> context)
    {
        _context = context;
    }

    public async Task LogEmail(UpdatePaymentResultMessage message)
    {
        EmailLog email = new EmailLog()
        {
            Email = message.Email,
            SentDate = DateTime.Now,
            Log = $"Ordem: - {message.OrderId} foi criada com sucesso.",
        };

        await using var _db = new MySQLContext(_context);
        _db.Emails.Add(email);
        await _db.SaveChangesAsync();
    }
}