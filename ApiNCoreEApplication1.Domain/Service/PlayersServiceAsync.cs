using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class PlayersServiceAsync<Tv, Te> : GenericServiceAsync<Tv, Te>
                                        where Tv : PlayersViewModel
                                        where Te : Player
    {
        public PlayersServiceAsync(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }
    }
}