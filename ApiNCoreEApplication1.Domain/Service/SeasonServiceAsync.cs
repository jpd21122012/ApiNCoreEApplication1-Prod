using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class SeasonServiceAsync<Tv, Te> : GenericServiceAsync<Tv, Te>
                                        where Tv : SeasonViewModel
                                        where Te : Season
    {
        public SeasonServiceAsync(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }
    }
}