using AutoMapper;
using CRMEngSystem.Data.Entities.Comment;
using CRMEngSystem.Data.Entities.Contact;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Loaders.Contact;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Comment;
using CRMEngSystem.Models.ViewModels.Comment;
using CRMEngSystem.Models.ViewModels.Contact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Contact
{
    [Authorize]
    public class ContactCommentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public ContactCommentsController(IMapper mapper, IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> ContactComments(int EntityId)
        {
            var entity = await _repositoryFactory.Instantiate<ContactEntity>().GetEntityAsync(new ContactDataLoader(false, false, true, true, true), entity => entity.ContactId, EntityId);
            var comments = _mapper.Map<IEnumerable<CommentDto>>(entity!.Comments).OrderByDescending(entity => entity.DateTimeCreate);
            foreach (var comment in comments)
            {
                comment.ActionName = "ContactComments";
                comment.ControllerName = "ContactComments";
                comment.EntityId = EntityId;
            }
            return View(new ContactCommentsViewModel
            {
                Comments = comments,
                EntityId = entity!.ContactId,
                ActiveTab = "Comments",
                NumberOrders = entity!.ContactOrders!.Count,
                NumberComments = entity.Comments!.Count
            });
        }
        [HttpPost]
        public async Task<IActionResult> ContactComments(CommentListViewModel model)
        {
            await _repositoryFactory.Instantiate<CommentEntity>().AddEntityAsync(new CommentEntity
            {
                AuthorId = (await _userManager.GetUserAsync(User))!.ContactId,
                Text = model.Text,
                DateTimeCreate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time")),
                RecipientContactId = model.EntityId
            });
            return await ContactComments(model.EntityId);
        }
    }
}
