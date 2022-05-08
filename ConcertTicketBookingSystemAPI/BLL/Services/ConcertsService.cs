using BLL.Dtos.ConcertsDtos;
using BLL.Dtos.ImagesDtos;
using BLL.Dtos.UsersDtos;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ConcertsService : ICommonConcertsService
    {
        private readonly IActionsRepository _actionsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IImagesRepository _imagesRepository;
        private readonly IConcertsRepository _concertsRepository;
        private readonly IClassicConcertsRepository _classicConcertsRepository;
        private readonly IOpenAirConcertsRepository _openAirConcertsRepository;
        private readonly IPartyConcertsRepository _partyConcertsRepository;
        private readonly int maxCensure = 6;
        public ConcertsService(IActionsRepository actionsRepository,
            IUsersRepository usersRepository,
            IImagesRepository imagesRepository,
            IConcertsRepository concertsRepository,
            IClassicConcertsRepository classicConcertsRepository,
            IOpenAirConcertsRepository openAirConcertsRepository,
            IPartyConcertsRepository partyConcertsRepository)
        {
            _actionsRepository = actionsRepository;
            _usersRepository = usersRepository;
            _imagesRepository = imagesRepository;
            _concertsRepository = concertsRepository;
            _classicConcertsRepository = classicConcertsRepository;
            _openAirConcertsRepository = openAirConcertsRepository;
            _partyConcertsRepository = partyConcertsRepository;
        }

        public async Task<int> AddConcertAsync(AddConcertDto dto, Guid userId)
        {
            var currentUser = await _usersRepository.GetByIdAsync(userId);
            var imageId = await _imagesRepository.AddImageAsync(dto.Image, dto.ImageType);
            Concert concert;
            if (dto.ConcertType == ConcertType.ClassicConcert)
            {
                concert = dto.ToClassicConcert(currentUser.UserId, imageId);
                await _classicConcertsRepository.AddAsync((ClassicConcert)concert);
            }
            else if (dto.ConcertType == ConcertType.OpenAirConcert)
            {
                concert = dto.ToOpenAirConcert(currentUser.UserId, imageId);
                await _openAirConcertsRepository.AddAsync((OpenAirConcert)concert);
            }
            else
            {
                concert = dto.ToPartyConcert(currentUser.UserId, imageId);
                await _partyConcertsRepository.AddAsync((PartyConcert)concert);
            }
            await _concertsRepository.SaveChangesAsync();
            await _actionsRepository.AddActionAsync(userId, "Added Concert with id = " +
                concert.ConcertId + " and type = " + dto.ConcertType);
            return concert.ConcertId;
        }

        public async Task<ConcertDto> GetConcertByIdAsync(int concertId, Guid? userId)
        {
            var concert = await _concertsRepository.GetByIdAsync(concertId);
            if (concert is ClassicConcert) return ((ClassicConcert)concert).ToDto();
            else if (concert is OpenAirConcert) return ((OpenAirConcert)concert).ToDto();
            else
            {
                var partyConcert = (PartyConcert)concert;
                if (userId != null)
                {
                    int age = GetAge((await _usersRepository.GetByIdAsync(userId.Value)).BirthDate.Value);
                    if (age >= partyConcert.Censure)  return partyConcert.ToDto();
                }
                else
                {
                    if (maxCensure >= partyConcert.Censure) return partyConcert.ToDto();
                }
                return null;
            }
        }

        public async Task ActivateConcertAsync(int concertId, Guid userId)
        {
            var concert = await _concertsRepository.GetByIdAsync(concertId);
            concert.IsActiveFlag = true;
            await _concertsRepository.SaveChangesAsync();
            await _actionsRepository.AddActionAsync(userId, "Activated concert with id = " + concertId);
        }
        public async Task DeactivateConcertAsync(int concertId, Guid userId)
        {
            var concert = await _concertsRepository.GetByIdAsync(concertId);
            concert.IsActiveFlag = false;
            await _concertsRepository.SaveChangesAsync();
            await _actionsRepository.AddActionAsync(userId, "Deactivated concert with id = " + concertId);
        }

        public async Task<ConcertSelectorDto> GetManyLightConcertsAsync(ConcertSelectParametersDto selectParametersDto, Guid? userId)
        {
            IQueryable<Concert> concerts;
            if (selectParametersDto.ByConcertType != null)
            {
                if (selectParametersDto.ByConcertType == ConcertType.ClassicConcert)
                {
                    var classicConcerts = _classicConcertsRepository.GetQueryable();
                    if (selectParametersDto.ByCompositor != null) classicConcerts = classicConcerts.Where(c =>
                    c.Compositor.ToLower().Contains(selectParametersDto.ByCompositor.ToLower()));
                    if (selectParametersDto.ByConcertName != null) classicConcerts = classicConcerts.Where(c =>
                     c.ConcertName.ToLower().Contains(selectParametersDto.ByConcertName.ToLower()));
                    if (selectParametersDto.ByVoiceType != null) classicConcerts = classicConcerts.Where(c =>
                     c.VoiceType.ToLower().Contains(selectParametersDto.ByVoiceType.ToLower()));
                    concerts = classicConcerts;
                }
                else if (selectParametersDto.ByConcertType == ConcertType.OpenAirConcert)
                {
                    var openAirConcerts = _openAirConcertsRepository.GetQueryable();
                    if (selectParametersDto.ByHeadLiner != null) openAirConcerts = openAirConcerts.Where(c =>
                     c.HeadLiner.ToLower().Contains(selectParametersDto.ByHeadLiner.ToLower()));
                    concerts = openAirConcerts;
                }
                else
                {
                    var partyConcerts = _partyConcertsRepository.GetQueryable();
                    if (userId != null)
                    {
                        int age = GetAge((await _usersRepository.GetByIdAsync(userId.Value)).BirthDate.Value);
                        partyConcerts = partyConcerts.Where(c => age >= c.Censure);
                    }
                    else
                    {
                        partyConcerts = partyConcerts.Where(c => maxCensure >= c.Censure);
                    }
                    concerts = partyConcerts;
                }
            }
            else concerts = _concertsRepository.GetQueryable();
            if (selectParametersDto.DateFrom != null) concerts = concerts.Where(c => c.ConcertDate >= selectParametersDto.DateFrom);
            if (selectParametersDto.DateUntil != null) concerts = concerts.Where(c => c.ConcertDate <= selectParametersDto.DateUntil);
            if (selectParametersDto.ByActivity != null) concerts = concerts.Where(c => c.IsActiveFlag == selectParametersDto.ByActivity);
            if (selectParametersDto.ByUserId != null) concerts = concerts.Where(c => c.UserId == selectParametersDto.ByUserId);
            if (selectParametersDto.ByPerformer != null) concerts = concerts.Where(c => c.Performer.ToLower().
            Contains(selectParametersDto.ByPerformer.ToLower()));
            if (selectParametersDto.UntilPrice != null) concerts = concerts.Where(c => c.Cost <= selectParametersDto.UntilPrice);
            if (selectParametersDto.FromPrice != null) concerts = concerts.Where(c => c.Cost >= selectParametersDto.FromPrice);
            if (selectParametersDto.Sort == SortType.NewFirst) concerts = concerts.OrderByDescending(c => c.ConcertId);
            else if (selectParametersDto.Sort == SortType.ActualFirst) concerts.OrderByDescending(c => c.ConcertDate);
            else concerts = concerts = concerts.OrderBy(c => c.ConcertDate);
            var concertsCount = concerts.Count();
            ConcertSelectorDto selector = new ConcertSelectorDto()
            {
                PagesCount = (concertsCount / selectParametersDto.NeededCount) + 1,
                CurrentPage = selectParametersDto.NextPage,
                Concerts = await concerts.Skip(selectParametersDto.NextPage * selectParametersDto.NeededCount).
                Take(selectParametersDto.NeededCount).ToDtosAsync()
            };
            return selector;
        }

        public Task<bool> IsExistsAsync(int concertId)
        {
            return _concertsRepository.IsExistsAsync(concertId);
        }
        private int GetAge(DateTime birthDate)
        {
            var userBirthDate = birthDate;
            var nowTime = DateTime.UtcNow;
            int age = nowTime.Year - userBirthDate.Year;
            if (nowTime.DayOfYear < userBirthDate.DayOfYear) age++;
            return age;
        }
    }
}
