using AutoFixture;
using FeedbackReporting.Application.Tests.ModelBuilders;
using FeedbackReporting.Application.UseCases;
using FeedbackReporting.Domain.Models.Entities;
using FeedbackReporting.Domain.Models.Ressources;
using FeedbackReporting.Domain.Repositories;
using InMemoryDatabase.UseCasePattern;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FeedbackReporting.Application.Tests.Usecases
{
    public class CreateFeedbackUseCaseTests : UseCaseTestsBase<CreateFeedbackUseCase>
    {
        public Fixture _fixture = new Fixture();

        [Fact]
        public async Task Should_Return_Newly_Inserted_Id()
        {
            // Arrange
            var expectedReturnValue = _fixture.Create<int>();
            var inputRessource = new FeedbackRessourceBuilder()
                .WithId(expectedReturnValue)
                .WithDescription(_fixture.Create<string>())
                .WithTitle(_fixture.Create<string>())
                .WithCreatorName(_fixture.Create<string>())
                .WithCreationDate(_fixture.Create<DateTime>())
                .WithPayload(_fixture.Create<string>())
                .Build();
            var feedbackRepo = ArrangeFeedbackRepository(inputRessource, expectedReturnValue);
            var feedbackKeywordRepo = ArrangeFeedbackKeywordsRepository(ArrangeFeedbackKeywords(inputRessource));
            var useCase = ArrangeUseCase(ArrangeUnitOfWork().Object, feedbackRepo.Object, feedbackKeywordRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(inputRessource);

            // Assert
            Assert.Equal(expectedReturnValue, result);
            feedbackRepo.VerifyAll();
            feedbackKeywordRepo.VerifyAll();
        }

        [Fact]
        public async Task Should_ReturnMinusOne_On_Null_Input()
        {
            // Arrange
            var expectedReturnValue = -1;
            var feedbackRepo = ArrangeFeedbackRepository();
            var feedbackKeywordRepo = ArrangeFeedbackKeywordsRepository();
            var useCase = ArrangeUseCase(ArrangeUnitOfWork().Object, feedbackRepo.Object, feedbackKeywordRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(null);

            // Assert
            Assert.Equal(expectedReturnValue, result);
            feedbackRepo.VerifyAll();
            feedbackKeywordRepo.VerifyAll();
        }
        
        [Fact]
        public async Task Should_Rollback_When_Error_Occur()
        {
            // Arrange
            var inputRessource = new FeedbackRessourceBuilder()
                .WithDescription(_fixture.Create<string>())
                .WithTitle(_fixture.Create<string>())
                .WithCreatorName(_fixture.Create<string>())
                .WithCreationDate(_fixture.Create<DateTime>())
                .WithPayload(_fixture.Create<string>())
                .Build();
            var feedbackRepo = ArrangeFeedbackRepositoryThrows();
            var feedbackKeywordRepo = ArrangeFeedbackKeywordsRepository();
            var useCase = ArrangeUseCase(ArrangeUnitOfWorkWithRollback().Object, feedbackRepo.Object, feedbackKeywordRepo.Object);

            // Act

            // Assert
            await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(inputRessource));
            feedbackRepo.VerifyAll();
            feedbackKeywordRepo.VerifyAll();
        }

        #region Arrangers
        private CreateFeedbackUseCase ArrangeUseCase(IUnitOfWork unitOfWork, IFeedbackRepository feedbackRepo, IFeedbackKeywordsRepository feedbackKeywordsRepo)
        {
            return new CreateFeedbackUseCase(ArrangeLogger().Object, unitOfWork, feedbackRepo, feedbackKeywordsRepo);
        }

        private Mock<IFeedbackRepository> ArrangeFeedbackRepository() => new Mock<IFeedbackRepository>(MockBehavior.Strict);
        private Mock<IFeedbackKeywordsRepository> ArrangeFeedbackKeywordsRepository() => new Mock<IFeedbackKeywordsRepository>(MockBehavior.Strict);

        private Mock<IFeedbackRepository> ArrangeFeedbackRepository(FeedbackRessource inputRessource, int returnValue)
        {
            var mock = ArrangeFeedbackRepository();
            mock.Setup(x => x.Insert(It.Is<FeedbackEntity>(x =>
                x.Description == inputRessource.Description &&
                x.CreatorName == inputRessource.CreatorName &&
                x.CreationDate == inputRessource.CreationDate &&
                x.Title == inputRessource.Title &&
                x.Payload == inputRessource.Payload))).ReturnsAsync(returnValue);

            return mock;
        }

        private Mock<IFeedbackKeywordsRepository> ArrangeFeedbackKeywordsRepository(IEnumerable<FeedbackKeywordEntity> insertedEntities)
        {
            var mock = ArrangeFeedbackKeywordsRepository();

            foreach (var insertedEntity in insertedEntities)
                mock.SetupSequence(x => x.Insert(It.Is<FeedbackKeywordEntity>(x => x.FeedbackId == insertedEntity.FeedbackId &&
                    x.HashCode == insertedEntity.HashCode &&
                    x.Keyword == insertedEntity.Keyword))).ReturnsAsync(insertedEntity.FeedbackId);

            return mock;
        }

        private Mock<IFeedbackRepository> ArrangeFeedbackRepositoryThrows()
        {
            var mock = ArrangeFeedbackRepository();
            mock.Setup(x => x.Insert(It.IsAny<FeedbackEntity>())).ThrowsAsync(new Exception("Random database exception"));

            return mock;
        }

        private IEnumerable<FeedbackKeywordEntity> ArrangeFeedbackKeywords(FeedbackRessource ressource)
        {
            var result = new List<FeedbackKeywordEntity>();

            foreach (var word in ressource.Description.Split(' ').Where(x => x.Length > 3))
                result.Add(new FeedbackKeywordEntity { Keyword = word, HashCode = word.GetHashCode(), FeedbackId = ressource.Id });

            return result;
        }
        #endregion
    }
}
