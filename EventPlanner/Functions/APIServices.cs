using Azure.Core;
using EventPlannerModels;
using Newtonsoft.Json;
using System.Text;
namespace EventPlanner.Functions
{
    public class APIServices
    {
        private static readonly int timeout = 600;
        private static readonly string baseurl = "https://localhost:7242/api/";
        //private static readonly string baseurl = "https://localhost/EPAPI/api/";
        #region Users
        public static async Task<Token> UsersLogin(LoginUser object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };

            var response = await httpClient.PostAsync(baseurl + "LoginAPI/Login", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<IEnumerable<User>> UsersGetList(string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Users");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<User>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<User> UsersDetails(int? id, string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Users/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<Token> UsersCreate(User object_to_serialize)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            
            var response = await httpClient.PostAsync(baseurl + "LoginAPI/Register", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> UsersEdit(int id, User object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PutAsync(baseurl + "Users/" + id, content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<GeneralResult> UsersDelete(int id, string accessToken)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.DeleteAsync(baseurl + "Users/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        
        #endregion
        #region CatalogItem
        public static async Task<IEnumerable<CatalogItem>> CatalogItemsGetList(string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "CatalogItems");
            

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<CatalogItem>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<CatalogItem> CatalogItemsDetails(int? id, string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "CatalogItems/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<CatalogItem>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> CatalogItemsCreate(CatalogItem object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PostAsync(baseurl + "CatalogItems", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> CatalogItemsEdit(int id, CatalogItem object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PutAsync(baseurl + "CatalogItems/" + id, content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> CatalogItemsDelete(int id, string accessToken)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.DeleteAsync(baseurl + "CatalogItems/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        
        
        #endregion
        #region Seller
        public static async Task<GeneralResult> SellersCreate(Seller object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PostAsync(baseurl + "Sellers", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<IEnumerable<Seller>> SellersGetList(string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "CatalogItems/Sellers");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Seller>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<Seller> SellersDetails(int? id, string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Sellers/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Seller>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> SellersEdit(Seller object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PutAsync(baseurl + "Sellers/" + object_to_serialize.Id, content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<GeneralResult> SellersDelete(int id, string accessToken)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.DeleteAsync(baseurl + "Sellers/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<int?> SellerExists(string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Sellers/Exists");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<int?>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        #endregion
        #region IdentificationType
        public static async Task<IEnumerable<IdentificationType>> IdentificationTypesGetList(string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "IdentificationTypes");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<IdentificationType>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<IdentificationType> IdentificationTypesDetails(int? id, string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "IdentificationTypes/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IdentificationType>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> IdentificationTypesCreate(IdentificationType object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PostAsync(baseurl + "IdentificationTypes", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> IdentificationTypesEdit(IdentificationType object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PutAsync(baseurl + "IdentificationTypes/" + object_to_serialize.Id, content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<GeneralResult> IdentificationTypesDelete(int id, string accessToken)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.DeleteAsync(baseurl + "IdentificationTypes/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        #endregion
        #region Referrals
        public static async Task<IEnumerable<Referral>> ReferralsGetList(string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Referrals");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Referral>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<IEnumerable<Referral>> ReferralsGetListById(string accessToken, int sellerid)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Referrals/RefByUser/"+sellerid);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Referral>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<Referral> ReferralsDetails(int? id, string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Referrals/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Referral>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> ReferralsCreate(Referral object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PostAsync(baseurl + "Referrals", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> ReferralsEdit(Referral object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PutAsync(baseurl + "Referrals/" + object_to_serialize.Id, content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<GeneralResult> ReferralsDelete(int id, string accessToken)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.DeleteAsync(baseurl + "Referrals/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        #endregion
        #region Roles
        public static async Task<IEnumerable<Role>> RolesGetList(string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Roles");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Role>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<Role> RolesDetails(int? id, string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "Roles/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<Role>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        #endregion
        #region ItemCategory
        public static async Task<IEnumerable<ItemCategory>> ItemCategoriesGetList(string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "ItemCategories");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ItemCategory>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<ItemCategory> ItemCategoriesDetails(int? id, string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "ItemCategories/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ItemCategory>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> ItemCategoriesCreate(ItemCategory object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PostAsync(baseurl + "ItemCategories", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> ItemCategoriesEdit(ItemCategory object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PutAsync(baseurl + "ItemCategories/" + object_to_serialize.Id, content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<GeneralResult> ItemCategoriesDelete(int id, string accessToken)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.DeleteAsync(baseurl + "ItemCategories/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        #endregion
        #region VerificationRequest
        public static async Task<IEnumerable<VerificationRequest>> VerificationRequestsGetList(string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "VerificationRequests");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<VerificationRequest>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<VerificationRequest> VerificationRequestsDetails(int? id, string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "VerificationRequests/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<VerificationRequest>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> VerificationRequestsCreate(VerificationRequest object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PostAsync(baseurl + "VerificationRequests", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<GeneralResult> VerificationRequestsEdit(VerificationRequest object_to_serialize, string accessToken)
        {
            var json_ = JsonConvert.SerializeObject(object_to_serialize);
            var content = new StringContent(json_, Encoding.UTF8, "application/json");

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.PutAsync(baseurl + "VerificationRequests/" + object_to_serialize.Id, content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static async Task<GeneralResult> VerificationRequestsDelete(int id, string accessToken)
        {

            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var response = await httpClient.DeleteAsync(baseurl + "VerificationRequests/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        #endregion
        #region VerificationStatuses
        public static async Task<IEnumerable<VerificationStatus>> VerificationStatusesGetList(string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "VerificationStatuses");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<VerificationStatus>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        public static async Task<VerificationStatus> VerificationStatusesDetails(int? id, string accessToken)
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            };

            HttpClient httpClient = new(clientHandler)
            {
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage response = await httpClient.GetAsync(baseurl + "VerificationStatuses/" + id);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<VerificationStatus>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }
        #endregion
    }
}
