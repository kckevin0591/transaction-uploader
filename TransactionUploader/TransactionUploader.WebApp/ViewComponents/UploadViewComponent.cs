using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TransactionUploader.WebApp.ViewComponents
{
    public class UploadViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View()).ConfigureAwait(false);
        }
    }
}