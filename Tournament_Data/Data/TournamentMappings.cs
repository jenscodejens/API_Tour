using AutoMapper;
using Tournament_Core.Dto;
using Tournament_Core.Entities;

namespace Tournament_Data.Data
{
    public class TournamentMappings : Profile
    {
        public TournamentMappings()
        {
            CreateMap<Tournament, TournamentDto>();
            CreateMap<TournamentDto, Tournament>();
            CreateMap<Game, GameDto>();
        }
    }
}
