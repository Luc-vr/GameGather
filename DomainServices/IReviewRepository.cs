using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainServices
{
    public interface IReviewRepository
    {
        // Create review for board game night
        Review CreateReview(Review review, int boardGameNightId);
    }
}
