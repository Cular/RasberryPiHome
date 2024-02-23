using System.Threading.Tasks;

namespace HomeWatcher.Telegram;

public interface IMessageSender
{
    Task SendAsync();
}