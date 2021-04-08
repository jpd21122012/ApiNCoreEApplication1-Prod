using ApiNCoreEApplication1.Entity;
using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class TournamentsServiceAsync<Tv, Te> : GenericServiceAsync<Tv, Te>
                                        where Tv : TournamentsViewModel
                                        where Te : Tournament
    {
        public TournamentsServiceAsync(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }
    }
}
