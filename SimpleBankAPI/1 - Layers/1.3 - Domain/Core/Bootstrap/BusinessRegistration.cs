using SimpleBankAPI.Application.Repositories;
using SimpleBankAPI.Business;
using SimpleBankAPI.Infrastructure.Kafka;
using SimpleBankAPI.Interfaces;
using SimpleBankAPI.Interfaces.Provider;

namespace SimpleBankAPI._1___Layers._1._3___Domain.Core.Bootstrap
{
    public static class BusinessRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
            .AddTransient<IUsersRepository, UsersRepository>()
            .AddTransient<IUserBusiness, UserBusiness>()
            .AddTransient<IAccountsRepository, AccountsRepository>()
            .AddTransient<IAccountsBusiness, AccountsBusiness>()
            .AddTransient<ITransfersRepository, TransferRepository>()
            .AddTransient<IDocumentsBusiness, DocumentsBusiness>()
            .AddTransient<IDocumentsRepository, DocumentsRepository>()
            .AddTransient<ITokenRepository, TokenRepository>()
            .AddTransient<ITokenBusiness, TokenBusiness>()
            .AddTransient<ITransfersBusiness, TransfersBusiness>()
            .AddTransient<IJwtAuth, JwtAuth>()
            .AddTransient<ICommunicationsService, MailService>()
            .AddTransient<ICommunicationsBusiness, NotificationsBusiness>()
            .AddTransient<IEventProducer, KafkaProducer>()
            .AddHostedService<KafkaConsumer>();

            return services;
        }
    }
}
