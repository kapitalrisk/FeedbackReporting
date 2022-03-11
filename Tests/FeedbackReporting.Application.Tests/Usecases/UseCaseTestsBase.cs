using AutoFixture;
using InMemoryDatabase.UseCasePattern;
using Microsoft.Extensions.Logging;
using Moq;

namespace FeedbackReporting.Application.Tests.Usecases
{
    public class UseCaseTestsBase<TUseCase>
    {
        protected Mock<ILogger<TUseCase>> ArrangeLogger() => new Mock<ILogger<TUseCase>>(MockBehavior.Loose);

        protected Mock<IUnitOfWork> ArrangeUnitOfWork()
        {
            var mock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mock.Setup(x => x.Begin());
            mock.Setup(x => x.Commit());
            mock.Setup(x => x.Dispose());

            return mock;
        }

        protected Mock<IUnitOfWork> ArrangeUnitOfWorkWithRollback()
        {
            var mock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            mock.Setup(x => x.Begin());
            mock.Setup(x => x.Rollback());
            mock.Setup(x => x.Dispose());

            return mock;
        }
    }
}
