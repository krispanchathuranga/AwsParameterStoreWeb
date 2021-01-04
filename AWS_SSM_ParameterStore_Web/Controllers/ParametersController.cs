using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace AWS_SSM_ParameterStore_Web.Controllers
{
    public class ParametersController : Controller
    {
        List<Parameter> parmeters;
        static IAmazonSimpleSystemsManagement SSMClient;

        public ParametersController(IAmazonSimpleSystemsManagement ssmClient)
        {
           SSMClient = ssmClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<PartialViewResult> GetParametersAsync(string name)
        {
            string query = "/";
            if (name != "" | name != null)
            {
                query = query + name;
            }
            parmeters = new List<Parameter>();
            var request = new GetParametersByPathRequest()
            {
                WithDecryption = true,
                Recursive = true,
                Path = query,
                MaxResults = 10
            };

            parmeters = await GetParametersAsc(parmeters, null, request);

            return PartialView("~/Views/Parameters/ParameterData.cshtml", parmeters.ToArray());
        }


        [HttpPost]
        public async Task<JsonResult> UpdateParameterAsync(string pname, string pvalue)
        {
            var req = new PutParameterRequest()
            {
                Name = pname,
                Value = pvalue,
                Overwrite=true
            };
            var res = await UpdateParameter(req);
            return Json(res);
        }

        
    /**
     * Max Results is 10 supported by AmazonSimpleSystemsManagementClient.GetParametersByPathAsync
     * this method will recursively get all the results using the NextToken
     */
    public static async Task<List<Parameter>> GetParametersAsc(List<Parameter> paramData, string token, GetParametersByPathRequest req)
        {
            req.NextToken = token;
            var response = await SSMClient.GetParametersByPathAsync(req);
            for (int j = 0; j < response.Parameters.Count; j++)
            {
                paramData.Add(response.Parameters[j]);
            }

            if (response.NextToken != null)
            {
                var res = await GetParametersAsc(paramData, response.NextToken, req);
            }
            return paramData;
        }

        public static async Task<PutParameterResponse> UpdateParameter(PutParameterRequest req)
        {
            var response = await SSMClient.PutParameterAsync(req);
            return response;
        }

        public static async Task<PutParameterResponse> AddParameter(PutParameterRequest req)
        {
            var response = await SSMClient.PutParameterAsync(req);
            return response;
        }
    }
}