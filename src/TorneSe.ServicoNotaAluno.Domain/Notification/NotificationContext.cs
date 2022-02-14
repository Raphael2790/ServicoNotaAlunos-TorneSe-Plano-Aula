using System.Collections;
using System.Collections.ObjectModel;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace TorneSe.ServicoNotaAluno.Domain.Notification
{
    public class NotificationContext : IReadOnlyCollection<string>
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        private readonly Collection<string> _notifications = new Collection<string>();

        public bool HasNotifications => _notifications.Any();

        public int Count => _notifications.Count;

        public void Add(string notification) => _notifications.Add(notification);

        public IEnumerator<string> GetEnumerator() => _notifications.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _notifications.GetEnumerator();

        public void AddRange(IEnumerable<string> notifications)
        {
            foreach (var i in notifications)
                _notifications.Add(i);
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(_notifications, _jsonSerializerOptions);
        }
    }
}