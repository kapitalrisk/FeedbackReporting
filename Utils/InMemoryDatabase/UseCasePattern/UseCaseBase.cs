using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace InMemoryDatabase.UseCasePattern
{
    public abstract class UseCaseBase<TInput, TOutput> : IUseCase<TInput, TOutput>
    {
        public readonly ILogger<UseCaseBase<TInput, TOutput>> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public abstract string ScopeKey { get; }

        public UseCaseBase(ILogger<UseCaseBase<TInput, TOutput>> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<TOutput> ExecuteAsync(TInput input)
        {
            _logger.LogDebug($"StartHandling use case {ScopeKey}");

            try
            {
                _unitOfWork.Begin();
                var result = await ActuallyExecuteAsync(input);
                _unitOfWork.Commit();
                _logger.LogDebug($"Successfully handled use case {ScopeKey}");

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occur trying to handle use case {ScopeKey}");
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public abstract Task<TOutput> ActuallyExecuteAsync(TInput input);
    }
}
