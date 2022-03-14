using FeedbackReporting.Domain.Models.Entities;
using FeedbackReporting.Domain.Models.Ressources;
using FeedbackReporting.Domain.Repositories;
using InMemoryDatabase;
using InMemoryDatabase.UseCasePattern;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FeedbackReporting.Application.Repositories
{
    public class FeedbackRepository : BaseRepository<FeedbackEntity>, IFeedbackRepository
    {
        public FeedbackRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task<int> Insert(FeedbackEntity entityToInsert)
        {
            await base.InsertAsync(entityToInsert);
            return entityToInsert.Id;
        }

        public async Task<FeedbackEntity> GetById(int id)
        {
            return await base.GetAsync(new FeedbackEntity { Id = id });
        }

        public Task<IEnumerable<FeedbackEntity>> Search(string creatorName, DateTime? dateOfCreation, IEnumerable<int> feedbackIdsFilter)
        {
            var whereClauses = new List<string>();

            if (!String.IsNullOrWhiteSpace(creatorName))
            {
                var creatorNameColumn = _entityType.GetColumnName(nameof(FeedbackEntity.CreatorName));
                whereClauses.Add($"{creatorNameColumn} = @creatorNameParam");
            }
            if (dateOfCreation.HasValue)
            {
                var dateOfCreationColumn = _entityType.GetColumnName(nameof(FeedbackEntity.CreationDate));
                whereClauses.Add($"{dateOfCreationColumn} = @dateOfCreationParam");
            }
            var feedbackIdColumn = _entityType.GetColumnName(nameof(FeedbackEntity.Id));

            foreach (var feedbackId in feedbackIdsFilter)
            {
                whereClauses.Add($"{feedbackIdColumn} = {feedbackId}");
            }
            var whereClause = FormattableStringFactory.Create(string.Join(" or ", whereClauses));
            return base.FindAsync(whereClause, new { creatorNameParam = creatorName, dateOfCreationParam = dateOfCreation });
        }
    }
}
