using FeedbackReporting.Domain.Models.Entities;
using FeedbackReporting.Domain.Models.Ressources;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackReporting.Domain.Repositories
{
    public interface IFeedbackRepository
    {
        Task<int> Insert(FeedbackEntity entityToInsert);
        Task<FeedbackEntity> GetById(int id);
        Task<IEnumerable<FeedbackEntity>> Search(string creatorName, DateTime? dateOfCreation, IEnumerable<int> feedbackIdsFilter);
    }
}
