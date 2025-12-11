using System.Collections.Generic;

namespace ViewModel.Contract;

public interface IPagination<T>
{
    public int Page { get; set; }
    public int Page0 { get; }
    public List<T> List { get; }
    public int NbPages { get; }
    public int ClosePages { get; set; }
    public int FarPagesStep { get; set; }

    public string CloseFarPageSeparator { get; set; }

    public int? NextPage { get; }
    public int? PrevPage { get; }
    public Dictionary<int, string> PageToShow();

    public IPagination<T> Next(int nb = 1);
    public IPagination<T> Prev(int nb = 1);
}