using System.Threading.Tasks;

namespace InMemoryDatabase.UseCasePattern
{
    public interface IUseCase<TInput, TOutput> : IUseCase
    {
        Task<TOutput> ExecuteAsync(TInput input);
    }

    public interface IUseCase
    {
        string ScopeKey { get; }
    }
}
