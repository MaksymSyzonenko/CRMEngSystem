using AutoMapper;
using CRMEngSystem.Data.Entities.Comment;
using CRMEngSystem.Data.Entities.Order;
using CRMEngSystem.Data.Entities.User;
using CRMEngSystem.Data.Loaders.Order;
using CRMEngSystem.Data.Repositories.Factory;
using CRMEngSystem.Dto.Comment;
using CRMEngSystem.Models.ViewModels.Comment;
using CRMEngSystem.Models.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.Order
{
    [Authorize]
    public class OrderCommentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly UserManager<UserEntity> _userManager;
        public OrderCommentsController(IMapper mapper, IRepositoryFactory repositoryFactory, UserManager<UserEntity> userManager)
        {
            _mapper = mapper;
            _repositoryFactory = repositoryFactory;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> OrderComments(int EntityId)
        {
            var entity = await _repositoryFactory.Instantiate<OrderEntity>().GetEntityAsync(new OrderDataLoader(false, false, true, true, true), entity => entity.OrderId, EntityId);
            var comments = _mapper.Map<IEnumerable<CommentDto>>(entity!.Comments).OrderByDescending(entity => entity.DateTimeCreate);
            foreach(var comment in comments)
            {
                comment.ActionName = "OrderComments";
                comment.ControllerName = "OrderComments";
                comment.EntityId = EntityId;
            }
            return View(new OrderCommentsViewModel
            {
                Comments = comments,
                EntityId = entity!.OrderId,
                ActiveTab = "Comments",
                NumberEquipmentPositions = entity.EquipmentOrderPositions != null ? entity.EquipmentOrderPositions.Count : 0,
                NumberContacts = entity.ContactOrders != null ? entity.ContactOrders.Where(contactorder => contactorder.Contact.EnterpriseId != 1).Count() : 0,
                NumberWorkGroup = entity.ContactOrders != null ? entity.ContactOrders.Where(contactorder => contactorder.Contact.EnterpriseId == 1).Count() : 0,
                NumberComments = entity.Comments != null ? entity.Comments.Count : 0
            });
        }
        [HttpPost]
        public async Task<IActionResult> OrderComments(CommentListViewModel model)
        {
            await _repositoryFactory.Instantiate<CommentEntity>().AddEntityAsync(new CommentEntity
            {
                AuthorId = (await _userManager.GetUserAsync(User))!.ContactId,
                Text = model.Text,
                DateTimeCreate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time")),
                RecipientOrderId = model.EntityId
            });
            ModelState.Clear();
            return await OrderComments(model.EntityId);
        }
    }
}
