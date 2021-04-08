using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class ScoresServiceAsync<Tv, Te> : GenericServiceAsync<Tv, Te>
                                        where Tv : ScoresViewModel
                                        where Te : Score
    {
        public ScoresServiceAsync(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }
    }
}
