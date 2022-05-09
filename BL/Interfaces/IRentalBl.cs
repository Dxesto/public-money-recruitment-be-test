using Models;

namespace BL.Interfaces
{
    public interface IRentalBl : IBaseBl<Rental>
    {
        int Create(Rental model);
    }
}
