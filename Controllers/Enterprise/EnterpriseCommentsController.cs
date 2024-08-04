using AutoMapper;
using CRMEngSystem.Data.Entities.Comment;
using CRMEngSystem.Data.Entities.Enterprise;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Loaders.Enterprise;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Comment;
using CRMEngSystem.Models.ViewModels.Comment;
using CRMEngSystem.Models.ViewModels.Enterprise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Enterprise
{
    [Authorize]
    public class EnterpriseCommentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public EnterpriseCommentsController(IMapper mapper, IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> EnterpriseComments(int EntityId)
        {
            var entity = await _repositoryFactory.Instantiate<EnterpriseEntity>().GetEntityAsync(new EnterpriseDataLoader(false, true, true, true), entity => entity.EnterpriseId, EntityId);
            var comments = _mapper.Map<IEnumerable<CommentDto>>(entity!.Comments).OrderByDescending(entity => entity.DateTimeCreate);
            foreach (var comment in comments)
            {
                comment.ActionName = "EnterpriseComments";
                comment.ControllerName = "EnterpriseComments";
                comment.EntityId = EntityId;
            }
            return View(new EnterpriseCommentsViewModel
            {
                Comments = comments,
                EntityId = entity!.EnterpriseId,
                ActiveTab = "Comments",
                NumberOrders = entity!.Orders!.Count,
                NumberContacts = entity!.Contacts!.Count,
                NumberComments = entity.Comments!.Count,
            });
        }
        [HttpPost]
        public async Task<IActionResult> EnterpriseComments(CommentListViewModel model)
        {
            await _repositoryFactory.Instantiate<CommentEntity>().AddEntityAsync(new CommentEntity
            {
                AuthorId = (await _userManager.GetUserAsync(User))!.ContactId,
                Text = model.Text,
                DateTimeCreate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time")),
                RecipientEnterpriseId = model.EntityId
            });
            return await EnterpriseComments(model.EntityId);
        }
    }
}
