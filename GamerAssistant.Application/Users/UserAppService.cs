using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using GamerAssistant.Authorization;
using GamerAssistant.Authorization.Roles;
using GamerAssistant.Authorization.Users;
using GamerAssistant.Roles.Dto;
using GamerAssistant.Users.Dto;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace GamerAssistant.Users
{
    [AbpAuthorize(PermissionNames.Pages_Users)]
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<UserFavorite> _userFavoriteRepository;
        private readonly IRepository<UserFriend> _userFriendRepository;
        private readonly IRepository<UserGame> _userGameRepository;
        private readonly IRepository<UserVote> _userVoteRepository;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            IRepository<Role> roleRepository,
            RoleManager roleManager,
            IRepository<UserFavorite> userFavoriteRepository,
            IRepository<UserFriend> userFriendRepository,
            IRepository<UserGame> userGameRepository,
            IRepository<UserVote> userVoteRepository
            )
            : base(repository)
        {
            _userManager = userManager;
            _roleRepository = roleRepository;
            _roleManager = roleManager;
            _userFavoriteRepository = userFavoriteRepository;
            _userFriendRepository = userFriendRepository;
            _userGameRepository = userGameRepository;
            _userVoteRepository = userVoteRepository;
        }

        public override async Task<UserDto> Get(EntityDto<long> input)
        {
            var user = await base.Get(input);
            var userRoles = await _userManager.GetRolesAsync(user.Id);
            user.Roles = userRoles.Select(ur => ur).ToArray();
            return user;
        }

        public override async Task<UserDto> Create(CreateUserDto input)
        {
            CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.RoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }

            CheckErrors(await _userManager.CreateAsync(user));

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> Update(UpdateUserDto input)
        {
            CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            MapToEntity(input, user);

            CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await Get(input);
        }

        public override async Task Delete(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            return user;
        }

        protected override void MapToEntity(UpdateUserDto input, User user)
        {
            ObjectMapper.Map(input, user);
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = Repository.GetAllIncluding(x => x.Roles).FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(user);
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public IList<UserFavorite> GetFavoritesById(int userId)
        {
            var userFavorites = _userFavoriteRepository.GetAll().ToList();
            if (userFavorites == null)
                return null;

            return userFavorites;
        }

        public IList<UserFriend> GetFriendsById(int userId)
        {
            var userFriends = _userFriendRepository.GetAll().ToList();
            if (userFriends == null)
                return null;

            return userFriends;
        }

        public IList<UserGame> GetGamesById(int userId)
        {
            var userGames = _userGameRepository.GetAll().ToList();
            if (userGames == null)
                return null;

            return userGames;
        }

        public IList<UserVote> GetAllVotesById(int userId)
        {
            var userVotes = _userVoteRepository.GetAll().ToList();
            if (userVotes == null)
                return null;

            return userVotes;
        }

        public IList<UserVote> GetUpcomingVotesById(int userId)
        {
            var userVotes = _userVoteRepository.GetAll().ToList();
            if (userVotes == null)
                return null;

            return userVotes;
        }

        public IList<UserGame> GetUsersByGameId(int gameId)
        {
            var userGames = _userGameRepository.GetAll().Where(x => x.GameId == gameId).ToList();
            if (userGames == null)
                return null;

            return userGames;
        }

        public void AddFavoriteById(UserFavorite favorite)
        {
            _userFavoriteRepository.Insert(favorite);
        }

        public void DeleteFavorite(int gameId)
        {
            _userFavoriteRepository.Delete(gameId);
        }

        public void AddFriendById(UserFriend friend)
        {
            _userFriendRepository.Insert(friend);
        }

        public void DeleteFriendById(int userId)
        {
            _userFriendRepository.Delete(userId);
        }

        public void AddGameToUser(UserGame userGame)
        {
            _userGameRepository.Insert(userGame);
        }

        public void DeleteGameFromUser(int userGameId)
        {
            _userGameRepository.Delete(userGameId);
        }

        public void AddVoteToUser(UserVote userVote)
        {
            _userVoteRepository.Insert(userVote);
        }

        public void UpdateVote(UserVote userVote)
        {
            _userVoteRepository.Update(userVote);
        }

        public void DeleteVoteById(int userVoteId)
        {
            _userVoteRepository.Delete(userVoteId);
        }
    }
}