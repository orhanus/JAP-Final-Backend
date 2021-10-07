using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Enums;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.DTOs;
using Core.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Core.Services
{
    public class MediaService : IMediaService
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly IMapper _mapper;
        private readonly IRatingRepository _ratingRepository;
        private readonly IActorRepository _actorRepository;

        public MediaService(IMediaRepository mediaRepository, IMapper mapper, IRatingRepository ratingRepository,
            IActorRepository actorRepository)
        {
            _mediaRepository = mediaRepository;
            _mapper = mapper;
            _ratingRepository = ratingRepository;
            _actorRepository = actorRepository;
        }

        public async Task<bool> AddMediaAsync(AddMediaDto addMediaDto)
        {
            Media media = _mapper.Map<Media>(addMediaDto);

            media.Actors = await ExtractExistingAndAddNewActorsAsync(addMediaDto.Actors.ToList());

            return await _mediaRepository.AddMediaAsync(media);
        }

        /// <summary>
        /// Checks if a media with given id exists
        /// If the media exists, method modifies media's properties before saving changes
        /// </summary>
        /// <param name="mediaDto"></param>
        /// <returns></returns>
        public async Task<bool> UpdateMediaAsync(UpdateMediaDto mediaDto)
        {
            var media = await _mediaRepository.GetMediaQuery().FirstOrDefaultAsync(media => media.Id == mediaDto.Id);
            if (media == null)
                throw new ArgumentException("Media with given id does not exist");

            _mapper.Map(mediaDto, media);

            media.Actors = await ExtractExistingAndAddNewActorsAsync(mediaDto.Actors.ToList());

            _mediaRepository.UpdateMedia(media);

            return await _mediaRepository.SaveAllAsync();
        }

        /// <summary>
        /// Returns a paginated list of Media containing Media with matching mediaType and mediaParams
        /// </summary>
        /// <param name="mediaParams"></param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        public async Task<PaginatedResponseDto<MediaDto>> GetMediaAsync(MediaParams mediaParams, MediaType mediaType)
        {
            var mediaQuery = _mediaRepository.GetMediaQuery().AsNoTracking();

            if(mediaParams.SearchParams != null)
                ApplySearchParameters(mediaQuery, mediaParams.SearchParams);

            if (mediaType != MediaType.All)
                mediaQuery = mediaQuery.Where(x => x.Type == mediaType);

            mediaQuery = mediaQuery.OrderByDescending(r => r.Ratings.Average(r => r.Score));

            var pagedList = await PagedList<MediaDto>.CreateAsync(mediaQuery.ProjectTo<MediaDto>(_mapper.ConfigurationProvider), 
                mediaParams.PageNumber, mediaParams.PageSize);

            foreach (var item in pagedList)
            {
                try
                {
                    item.AverageRating = await _ratingRepository.GetAverageRatingAsync(item.Id);
                }
                catch(Exception)
                {
                    item.AverageRating = 0;
                }
            }
            return new PaginatedResponseDto<MediaDto>
            {
                Data = pagedList,
                Pagination = new PaginationDto
                {
                    CurrentPage = pagedList.CurrentPage,
                    TotalCount = pagedList.TotalCount,
                    PageSize = pagedList.PageSize,
                    TotalPages = pagedList.TotalPages
                }
                
            };
        }


        #region Private Methods
        /// <summary>
        /// Checks if an actor already exists in the database
        /// If an actor already exists, gets the actor from the database and adds it to the list
        /// If an actor does not exist, creates a new actor to be added to the database
        /// </summary>
        /// <param name="actorDtos"></param>
        /// <param name="actors"></param>
        /// <returns></returns>
        private async Task<List<Actor>> ExtractExistingAndAddNewActorsAsync(List<ActorDto> actorDtos)
        {
            List<Actor> actors = new();

            foreach (var actor in actorDtos)
            {
                var existingActor = await _actorRepository.GetActorByFullNameAsync(actor.Firstname, actor.Lastname);
                if (existingActor != null)
                    actors.Add(existingActor);
                else
                    actors.Add(new Actor
                    {
                        Firstname = actor.Firstname,
                        Lastname = actor.Lastname
                    });
            }

            return actors;
        }

        /// <summary>
        /// Applies search parameters given as searchParams by extracting keywords out of it
        /// If no keywords exist within searchParams, applies searchParams on Media titles and descriptions
        /// </summary>
        /// <param name="query"></param>
        /// <param name="searchParams"></param>
        /// <returns></returns>
        private IQueryable<Media> ApplySearchParameters(IQueryable<Media> query, string searchParams)
        {
            Dictionary<string, int> keywords = GetKeywords(searchParams);
            if (keywords.Count == 0)
                query = query.Where(obj => obj.Title.ToLower().Contains(searchParams.ToLower())
                    || obj.Description.ToLower().Contains(searchParams.ToLower()));
            else
            {
                if (keywords.ContainsKey("olderthan"))
                    query = query.Where(show => DateTime.Now.Year - show.ReleaseDate.Year >= keywords["olderthan"]);
                if (keywords.ContainsKey("after"))
                    query = query.Where(show => show.ReleaseDate.Year >= keywords["after"]);
                if (keywords.ContainsKey("atleast"))
                    query = query.Where(show => show.Ratings.Average(r => r.Score) >= keywords["atleast"]);
                if (keywords.ContainsKey("stars"))
                    query = query.Where(show => (int)show.Ratings.Average(r => r.Score) == keywords["stars"]);
            }

            return query;
        }

            /// <summary>
            /// Parses a search string looking for keywords.
            /// If found adds them to a dictionary containing keywords as keys and parameters of a keyword as value
            /// </summary>
            /// <param name="searchParams"></param>
            /// <returns></returns>
            private Dictionary<string, int> GetKeywords(string searchParams)
            {
                Dictionary<string, int> Keywords = new Dictionary<string, int>();
                searchParams = searchParams.ToLower();

                try
                {
                    Regex regex = new Regex("older than ([\\d]+) years");
                    Match match = regex.Match(searchParams);
                    Keywords.Add("older than", Int32.Parse(match.Groups[1].Value));
                }
                catch (Exception e) { }

                try
                {
                    Regex regex = new Regex("after ([\\d]{4})");
                    Match match = regex.Match(searchParams);
                    Keywords.Add("after", Int32.Parse(match.Groups[1].Value));
                }
                catch (Exception e) { }

                bool starFilter = false;
                try
                {
                    Regex regex = new Regex("at least ([1-5]) stars");
                    Match match = regex.Match(searchParams);
                    Keywords.Add("atleast", Int32.Parse(match.Groups[1].Value) * 2);
                    starFilter = true;
                }
                catch (Exception e) { }

                if (!starFilter)
                {
                    try
                    {
                        Regex regex = new Regex("([1-5]) stars");
                        Match match = regex.Match(searchParams);
                        Keywords.Add("stars", Int32.Parse(match.Groups[1].Value) * 2);
                    }
                    catch (Exception e) { }
                }

                return Keywords;
            }
        }
        #endregion
}