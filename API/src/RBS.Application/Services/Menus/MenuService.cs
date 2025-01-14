﻿using Common.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using RBS.Application.Models;
using RBS.Domain.Menus;

namespace RBS.Application.Services.Menus
{
    public class MenuService : IMenuService
    {
        //TODO INCLUDE AND THAN INCLUDEE
        private readonly IRepository<Menu> _repository;

        public MenuService(IRepository<Menu> repository)
        {
            _repository = repository;
        }

        public async Task<List<SubMenuModel>> GetMenuItemsBySubMenuId(int restaurantId, int subMenuId, CancellationToken cancellationToken)
        {
            Menu menu = null;
            if (subMenuId == 0)
            {
                menu = await _repository.GetList(cancellationToken).Where(x => x.RestaurantId == restaurantId)
                    .Include(x => x.SubMenus).ThenInclude(x => x.Items).FirstOrDefaultAsync();
            }
            else
            {
                menu = await _repository.GetList(cancellationToken).Where(x => x.RestaurantId == restaurantId)
                    .Include(x => x.SubMenus.Where(x => x.Id == subMenuId)).ThenInclude(x => x.Items).FirstOrDefaultAsync();
            }

            return menu.SubMenus.Select(x => new SubMenuModel(x)).ToList();
        }

        public async Task<MenuModel> GetMenuByRestaurantId(int restaurantId, CancellationToken cancellationToken)
        {
            Menu menu = await _repository.GetList(cancellationToken).Where(x => x.RestaurantId == restaurantId)
                    .Include(x => x.SubMenus).ThenInclude(x => x.Items).FirstOrDefaultAsync();

            return new MenuModel(menu);
        }

        public async Task<List<SubMenuTypeModel>> GetRestaurantSubMenyTypes(int restaurantId, CancellationToken cancellationToken)
        {
            var menu = await _repository.GetList(cancellationToken)
                .Where(x => x.RestaurantId == restaurantId)
                .Include(x => x.SubMenus).FirstOrDefaultAsync();

            if (menu == null)
                return null;

            return menu.SubMenus.Select(x => new SubMenuTypeModel(x)).ToList();
        }
    }
}
