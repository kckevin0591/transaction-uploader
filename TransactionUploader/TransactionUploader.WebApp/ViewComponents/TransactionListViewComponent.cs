using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransactionUploader.WebApp.Models;

namespace TransactionUploader.WebApp.ViewComponents
{
    public class TransactionListViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(SearchTransactionViewModel model)
        {
            return await Task.FromResult((IViewComponentResult)View(model)).ConfigureAwait(false);
        }
    }
}