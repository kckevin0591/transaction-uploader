using Microsoft.Extensions.DependencyInjection;
using TransactionUploader.Common;
using TransactionUploader.Repository;

namespace TransactionUploader.Services
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureTransactionUploader(this IServiceCollection services)
        {
            services.AddTransient<IExtractorManager, ExtractorManager>();
            services.AddSingleton<ITransactionRepository, MemoryTransactionRepository>();
            services.AddSingleton<ITransactionUploaderService, TransactionUploaderService>();
            return services;
        }
    }
}