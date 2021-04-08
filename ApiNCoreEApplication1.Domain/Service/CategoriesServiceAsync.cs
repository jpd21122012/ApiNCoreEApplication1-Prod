using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class CategoriesServiceAsync<Tv, Te> : GenericServiceAsync<Tv, Te>
                                        where Tv : CategoriesViewModel
                                        where Te : Category
    {
        public CategoriesServiceAsync(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }
    }
}
