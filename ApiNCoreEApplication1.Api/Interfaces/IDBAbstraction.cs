using Microsoft.AspNetCore.Mvc;

namespace ApiNCoreEApplication1.Api.Interfaces
{
    public interface IDBAbstraction<T> where T : class
    {
        #region Select
        ActionResult<object> Select();
        ActionResult<object> SelectById(int id);
        //IActionResult SelectWhere(string query);
        #endregion

        #region POST
        ActionResult<object> Post([FromBody] T createSample);
        #endregion

        #region PUT
        ActionResult<object> Put(int id, [FromBody] T updateSample);
        #endregion

        #region DELETE
        ActionResult<object> Delete(int id);
        #endregion
    }
}
