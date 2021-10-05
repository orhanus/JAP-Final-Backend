using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IMediaRepository _mediaRepository;

        public RatingService(IRatingRepository ratingRepository, IMediaRepository mediaRepository)
        {
            _ratingRepository = ratingRepository;
            _mediaRepository = mediaRepository;
        }
        public async Task<bool> AddRatingAsync(int mediaId, RatingDto rating)
        {
            if (rating.Score < 1 || rating.Score > 10)
                throw new ArgumentException("Score must have a value between 1 and 10");

            var media = await _mediaRepository.GetMediaByIdAsync(mediaId);

            if (media == null)
                throw new ArgumentException("Media you want to rate does not exist");

            var ratingToAdd = new Rating
            {
                MediaId = mediaId,
                Media = media,
                Score = rating.Score
            };

            return await _ratingRepository.AddRatingAsync(ratingToAdd);
        }

        public async Task<double> GetAverageRatingAsync(int mediaId)
        {
            return await _ratingRepository.GetAverageRatingAsync(mediaId);
        }
    }
}
