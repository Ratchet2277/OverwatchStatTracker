using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DomainModel;
using DomainModel.Types;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication.Models
{
    public class AddGameViewModel
    {
        public Game Game { get; set; } = new();
        public Season CurrentSeason { get; }


        public AddGameViewModel(Season currentSeason)
        {
            CurrentSeason = currentSeason;
        }

        public List<SelectListItem> MapSelectList
        {
            get
            {
                List<SelectListItem> listItems = new();

                foreach (IGrouping<MapType, Map> type in CurrentSeason.MapPool.OrderBy(m => m.Type.ToString()).GroupBy(m => m.Type))
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

                foreach (IGrouping<Role, Hero> type in CurrentSeason.HeroPool.OrderBy(m => m.Role.ToString()).GroupBy(m => m.Role))
                {
                    SelectListGroup group = new() {Name = type.Key.ToString(), Disabled = true};
                    
                    listItems.AddRange(
                        type.OrderBy(m => m.Name)
                            .Select(m => 
                                new SelectListItem {Text = m.Name, Value = m.Id.ToString(), Group = group, Disabled = true}
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
                SelectListGroup listGroup = new SelectListGroup()
                {
                    Name = "Chose a role"
                };
                return Enum.GetValues(typeof(GameType)).Cast<GameType>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int) v).ToString(),
                    Group = listGroup,
                }).ToList();
            }
        }
            
    }
}