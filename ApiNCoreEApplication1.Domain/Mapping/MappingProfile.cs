using ApiNCoreEApplication1.Entity;
using AutoMapper;
using System;

namespace ApiNCoreEApplication1.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Create automap mapping profiles
        /// </summary>
        public MappingProfile()
        {
            CreateMap<AccountViewModel, Account>();
            CreateMap<Account, AccountViewModel>();

            CreateMap<AwardsViewModel, Award>();
            CreateMap<Award, AwardsViewModel>();

            CreateMap<CategoriesViewModel, Category>();
            CreateMap<Category, CategoriesViewModel>();

            CreateMap<EnigmaUsersTypeViewModel, EnigmaUsersType>();
            CreateMap<EnigmaUsersType, EnigmaUsersTypeViewModel>();

            CreateMap<EnigmaUsersViewModel, EnigmaUser>();
            CreateMap<EnigmaUser, EnigmaUsersViewModel>();

            CreateMap<MatchesViewModel, Match>();
            CreateMap<Match, MatchesViewModel>();

            CreateMap<PlayerStatsViewModel, PlayerStat>();
            CreateMap<PlayerStat, PlayerStatsViewModel>();

            CreateMap<PlayersViewModel, Player>();
            CreateMap<Player, PlayersViewModel>();

            CreateMap<ProductsViewModel, Product>();
            CreateMap<Product, ProductsViewModel>();

            CreateMap<ScoresViewModel, Score>();
            CreateMap<Score, ScoresViewModel>();

            CreateMap<SeasonViewModel, Season>();
            CreateMap<Season, SeasonViewModel>();

            CreateMap<TeamsViewModel, Team>();
            CreateMap<Team, TeamsViewModel>();

            CreateMap<TournamentsViewModel, Tournament>();
            CreateMap<Tournament, TournamentsViewModel>();

            CreateMap<UserViewModel, User>()
                .ForMember(dest => dest.DecryptedPassword, opts => opts.MapFrom(src => src.Password))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => string.Join(";", src.Roles)));
            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.Password, opts => opts.MapFrom(src => src.DecryptedPassword))
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.Roles.Split(";", StringSplitOptions.RemoveEmptyEntries)));

            CreateMissingTypeMaps = true;
        }

    }
}
