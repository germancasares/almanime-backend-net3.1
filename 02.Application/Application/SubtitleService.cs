using Application.Interfaces;
using Domain.DTOs;
using Domain.Models;
using Infrastructure.Helpers;
using Persistence.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application
{
    public class SubtitleService : ISubtitleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubtitleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Subtitle GetByID(Guid ID) => _unitOfWork.Subtitles.GetByID(ID);

        public async Task<Subtitle> Create(SubtitleDTO subtitleDTO, Guid identityID)
        {
            var user = _unitOfWork.Users.GetByIdentityID(identityID);
            if (user == null) throw new ArgumentException(nameof(identityID));

            var episode = _unitOfWork.Episodes.GetByAnimeSlugAndNumber(subtitleDTO.AnimeSlug, subtitleDTO.EpisodeNumber);
            if (episode == null) throw new ArgumentException(nameof(subtitleDTO.EpisodeNumber));

            var fansub = _unitOfWork.Fansubs.GetByAcronym(subtitleDTO.FansubAcronym);
            if (fansub == null) throw new ArgumentException(nameof(subtitleDTO.FansubAcronym));

            if (!fansub.Memberships.Any(m => m.UserID == user.ID))
            {
                throw new ArgumentException("User does not belong on the fansub.");
            }

            var subtitle = _unitOfWork.Subtitles.Create(new Subtitle
            {
                EpisodeID = episode.ID,
                FansubID = fansub.ID,
                Status = subtitleDTO.Status,
                Format = subtitleDTO.Subtitle.GetSubtitleFormat(),
            });

            var subtitlePartial = _unitOfWork.SubtitlePartials.Create(new SubtitlePartial
            {
                UserID = user.ID,
                SubtitleID = subtitle.ID,
            });

            _unitOfWork.Save();

            string subtitleUrl;
            try
            {
                subtitleUrl = await _unitOfWork.Storage.UploadSubtitle(subtitleDTO.Subtitle, fansub.ID, subtitle.ID);
            }
            catch (Exception)
            {
                _unitOfWork.Subtitles.Delete(subtitle);
                _unitOfWork.SubtitlePartials.Delete(subtitlePartial);
                _unitOfWork.Save();
                throw;
            }

            string subtitlePartialUrl;
            try
            {
                subtitlePartialUrl = await _unitOfWork.Storage.UploadSubtitlePartial(subtitleDTO.Subtitle, fansub.ID, subtitle.ID, subtitlePartial.ID);
            }
            catch (Exception)
            {
                _unitOfWork.Subtitles.Delete(subtitle);
                _unitOfWork.SubtitlePartials.Delete(subtitlePartial);
                _unitOfWork.Save();

                _unitOfWork.Storage.DeleteSubtitle(fansub.ID, subtitle.ID);
                throw;
            }

            try
            {
                subtitle.Url = subtitleUrl;
                subtitlePartial.Url = subtitlePartialUrl;
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                _unitOfWork.Subtitles.Delete(subtitle);
                _unitOfWork.SubtitlePartials.Delete(subtitlePartial);
                _unitOfWork.Save();

                _unitOfWork.Storage.DeleteSubtitle(fansub.ID, subtitle.ID);
                _unitOfWork.Storage.DeleteSubtitlePartial(fansub.ID, subtitle.ID, subtitlePartial.ID);

                throw;
            }

            return subtitle;
        }
    }
}
