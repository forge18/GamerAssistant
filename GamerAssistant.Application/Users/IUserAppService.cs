using Abp.Application.Services;
using Abp.Application.Services.Dto;
using GamerAssistant.Roles.Dto;
using GamerAssistant.Users.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamerAssistant.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        //Get favorite list by user id
        IList<UserFavorite> GetFavoritesById(int userId);

        //Get friend list by user id
        IList<UserFriend> GetFriendsById(int userId);

        //Get game list by user id
        IList<UserGame> GetGamesById(int userId);

        //Get source list by user id
        IList<UserSource> GetSourcesById(int userId);

        //Get all votes by user id
        IList<UserVote> GetAllVotesById(int userId);

        //Get all upcoming votes by user id
        IList<UserVote> GetUpcomingVotesById(int userId);

        //Add favorite
        void AddFavoriteById(UserFavorite favorite);

        //Delete favorite
        void DeleteFavorite(int gameId);

        //Add friend
        void AddFriendById(UserFriend friend);

        //Delete friend
        void DeleteFriendById(int userId);

        //Add game
        void AddGameToUser(UserGame userGame);

        //Delete game
        void DeleteGameFromUser(int userGameId);

        //Add source
        void AddSourceToUser(UserSource userSource);

        //Delete Source
        void DeleteSourceById(int userSourceId);

        //Add vote
        void AddVoteToUser(UserVote userVote);

        //Update vote
        void UpdateVote(UserVote userVote);

        //Delete vote
        void DeleteVoteById(int userVoteId);
    }
}