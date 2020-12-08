using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace AWS_SSM_ParameterStore_Web.Controllers
{
    public class ParametersController : Controller
    {
        public List<Parameter> parmeters;

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


        /**
         * Max Results is 10 supported by AmazonSimpleSystemsManagementClient.GetParametersByPathAsync
         * this method will recursively get all the results using the NextToken
         */
        public static async Task<List<Parameter>> GetParametersAsc(List<Parameter> paramData, string token, GetParametersByPathRequest req)
        {
            using (var client = new AmazonSimpleSystemsManagementClient())
            {
                req.NextToken = token;
                var response = await client.GetParametersByPathAsync(req);
                for (int j = 0; j < response.Parameters.Count; j++)
                {
                    paramData.Add(response.Parameters[j]);
                }

                if (response.NextToken != null)
                {
                    var res = await GetParametersAsc(paramData, response.NextToken, req);
                }
            }
            return paramData;
        }
    }
}