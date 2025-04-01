using article_publisher.api.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;


namespace article_publisher.api.Services
{
    public class ArticleQueueService
    {
        private readonly IModel _channel;


        public ArticleQueueService(IConnection connection)
        {
            _channel = connection.CreateModel();
            
            DeclareQueue("article.published");
            DeclareQueue("article.updated");
            DeclareQueue("article.deleted");
            DeclareQueue("draft.saved");
            DeclareQueue("draft.updated");
            DeclareQueue("draft.deleted");
        }

        private void DeclareQueue(string queueName)
        {
            _channel.QueueDeclare(queue: queueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        private void Publish<T>(string queueName, T message)
        {
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            _channel.BasicPublish(exchange: "",
                                  routingKey: queueName,
                                  basicProperties: null,
                                  body: body);
            Console.WriteLine($"[Queue] Published to '{queueName}': {typeof(T).Name}");
        }

        public void PublishArticlePublished(Article article) =>
            Publish("article.published", article);

        public void PublishArticleUpdated(Article article) =>
            Publish("article.updated", article);

        public void PublishArticleDeleted(string articleId) =>
            Publish("article.deleted", new { Id = articleId });

        public void PublishDraftSaved(Draft draft) =>
            Publish("draft.saved", draft);

        public void PublishDraftUpdated(Draft draft) =>
            Publish("draft.updated", draft);

        public void PublishDraftDeleted(string draftId) =>
            Publish("draft.deleted", new { Id = draftId });
    }
}
