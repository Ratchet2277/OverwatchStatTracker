using System;
using System.Collections.Generic;
using System.Linq;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models
{
    public class AddGameViewModel
    {
        public AddGameViewModel(Season currentSeason)
        {
            CurrentSeason = currentSeason;
        }

        public Game Game { get; set; } = new();
        public Season CurrentSeason { get; }

        public List<SelectListItem> MapSelectList
        {
            get
            {
                List<SelectListItem> listItems = new();

                foreach (var type in CurrentSeason.MapPool.OrderBy(m => m.Name).GroupBy(m => m.Type))
                {
                    SelectListGroup group = new() {Name = type.Key.ToString()};

                    listItems.AddRange(
                        type.OrderBy(m => m.Name)
                            .Select(m =>
                                new SelectListItem {Text = m.Name, Value = m.Id.ToString(), Group = group}
                            ).ToList()
                    );
                }

                return listItems;
            }
        }

        public List<SelectListItem> HeroSelectList
        {
            get
            {
                List<SelectListItem> listItems = new();

                foreach (var type in CurrentSeason.HeroPool.OrderBy(m => m.Role.ToString()).GroupBy(m => m.Role))
                {
                    SelectListGroup group = new() {Name = type.Key.ToString(), Disabled = true};

                    listItems.AddRange(
                        type.OrderBy(m => m.Name)
                            .Select(m =>
                                new SelectListItem
                                    {Text = m.Name, Value = m.Id.ToString(), Group = group, Disabled = true}
                            ).ToList()
                    );
                }

                return listItems;
            }
        }

        public List<SelectListItem> GameTypes
        {
            get
            {
                var listGroup = new SelectListGroup
                {
                    Name = "Chose a role"
                };
                return Enum.GetValues(typeof(GameType)).Cast<GameType>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int) v).ToString(),
                    Group = listGroup
                }).ToList();
            }
        }
    }
}