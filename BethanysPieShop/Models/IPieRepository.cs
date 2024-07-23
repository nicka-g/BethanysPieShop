namespace BethanysPieShop.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies { get; }
        IEnumerable<Pie> PiesOfTheWeek { get; }

        //nullable to prevent throwing error if the searched ID is not found
        //this is dones to avoid/minimize writing if statements
        Pie? GetPieById (int pieId);

        //finding pies based not just on id, but with keywords or criteria
        //that's why it is a list: to return all possible matches
        IEnumerable<Pie> SearchPies(string searchQuery);
    }
}
