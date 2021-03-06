using ApiNCoreEApplication1.Entity;
using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class MatchesServiceAsync<Tv, Te> : GenericServiceAsync<Tv, Te>
                                        where Tv : MatchesViewModel
                                        where Te : Match
    {
        public MatchesServiceAsync(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }
    }
}
