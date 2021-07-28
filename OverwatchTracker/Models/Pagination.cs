using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication.Models
{
    public class Pagination<T>
    {
        private readonly int? _pageSize;
        private readonly IEnumerable<T> _query;

        public Pagination(IEnumerable<T> query, int page = 1, int? pageSize = 10)
        {
            if (page < 1)
                throw new ArgumentOutOfRangeException(nameof(page), "Negative or zero are value not allowed here");

            if (pageSize < 10)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Negative or zero value are not allowed here");
            _query = query;
            Page0 = page - 1;
            _pageSize = pageSize;
            if (_pageSize is not null)
                NbPages = (int) Math.Ceiling(_query.Count() / (float) _pageSize);
            else
                NbPages = 1;
            _loadGames();
        }

        //User friendly, start at 1
        public int Page
        {
            get => Page0 + 1;
            set => Page0 = value - 1;
        }

        //Start at 0, for Database operation
        public int Page0 { get; private set; }
        public List<T> List => _loadGames();
        public int NbPages { get; set; }
        public int ClosePages { get; set; } = 3;
        public int FarPagesStep { get; set; } = 10;
        public string CloseFarPageSeparator { get; set; } = "...";

        public int? NextPage => Page < NbPages ? Page + 1 : null;
        public int? PrevPage => Page > 1 ? Page - 1 : null;

        public Dictionary<int, string> PageToShow()
        {
            Dictionary<int, string> allPages = new();

            if (NbPages > 1) allPages.Add(1, "1");

            //Far page under + separator
            if (Page > 2 + ClosePages)
            {
                for (var i = FarPagesStep; i < Page - ClosePages; i += 10) allPages.Add(i, i.ToString());

                allPages.Add(allPages.Last().Key + 1, CloseFarPageSeparator);
            }

            //close page under + current
            if (Page > 1)
                //Max 2 because page one is forced above
                for (var i = Math.Max(2, Page - ClosePages); i <= Page; i++)
                    allPages.Add(i, i.ToString());

            //close page over
            if (Page < NbPages)
                for (var i = Page + 1; i <= Math.Min(NbPages, Page + ClosePages); i++)
                    allPages.Add(i, i.ToString());

            //Far page Over + separator
            if (Page + ClosePages + 1 < NbPages)
            {
                var start = Page + ClosePages + FarPagesStep + 1 -
                            (Page + ClosePages + FarPagesStep + 1) % FarPagesStep;

                allPages.Add(Page + ClosePages + 1, CloseFarPageSeparator);

                for (var i = start; i < NbPages; i += FarPagesStep) allPages.Add(i, i.ToString());
            }

            //Last page
            if (Page + ClosePages < NbPages) allPages.Add(NbPages, NbPages.ToString());

            return allPages;
        }

        private List<T> _loadGames()
        {
            if (_pageSize is null) return _query.ToList();
            return _query.Skip((int) (_pageSize * Page0)).Take((int) _pageSize).ToList();
        }

        public Pagination<T> Next(int nb = 1)
        {
            Page0 = Math.Min(Page0 + nb, NbPages - 1);
            return this;
        }

        public Pagination<T> Prev(int nb = 1)
        {
            Page0 = Math.Max(0, Page0 - nb);
            return this;
        }
    }
}