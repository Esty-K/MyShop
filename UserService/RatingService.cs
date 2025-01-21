using Entities;
using Entity;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RatingService : IRatingService
    {
        IRatingRepository repository;

        public RatingService(IRatingRepository ratingRepository)
        {
            this.repository = ratingRepository;
        }
        public async Task Post(Rating rating)
        {
             await repository.Post(rating);
        }
    }
}
