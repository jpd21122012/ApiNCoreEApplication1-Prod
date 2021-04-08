using ApiNCoreEApplication1.Entity;

using ApiNCoreEApplication1.Entity.UnitofWork;
namespace ApiNCoreEApplication1.Domain.Service
{
    public class ProductsServiceAsync<Tv, Te> : GenericServiceAsync<Tv, Te>
                                        where Tv : ProductsViewModel
                                        where Te : Product
    {
        public ProductsServiceAsync(IUnitOfWork unitOfWork)
        {
            if (_unitOfWork == null)
                _unitOfWork = unitOfWork;
        }
    }
}