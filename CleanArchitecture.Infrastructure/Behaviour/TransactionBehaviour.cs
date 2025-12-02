using CleanArchitecture.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Behaviour
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {

        private readonly AppDbContext _dbContext;
        private readonly ILogger<TransactionBehaviour<TRequest, TResponse>> _logger;

        public TransactionBehaviour(AppDbContext dbContext, ILogger<TransactionBehaviour<TRequest, TResponse>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = default(TResponse)!;

            try
            {
                if (_dbContext.Database.CurrentTransaction != null)
                {
                    return await next();
                }

                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync( async () =>
                {
                    using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken).ConfigureAwait(false);

                    _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Request})",
                            transaction.TransactionId, typeof(TRequest).Name, request);

                    response = await next().ConfigureAwait(false);

                    _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}",
                            transaction.TransactionId, typeof(TRequest).Name);

                    await _dbContext.Database.CommitTransactionAsync(cancellationToken).ConfigureAwait(false);


                }).ConfigureAwait(false);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Request})",
                        typeof(TRequest).Name, request);
                throw;
            }
        }
    }
}
