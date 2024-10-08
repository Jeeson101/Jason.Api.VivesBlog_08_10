using System.Net.Http.Json;
using System.Net.Http;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Dto.Request;
using VivesBlog.Dto.Result;

namespace VivesBlog.Sdk
{
	public class ArticleSdk
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ArticleSdk(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		//Find
		public async Task<IList<ArticleResult>> Find()
		{
			var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");

			var route = "Article";
			var response = await httpClient.GetAsync(route);

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<IList<ArticleResult>>();
			if (result is null)
			{
				return new List<ArticleResult>();
			}

			return result;
		}



		//Get
		public async Task<ServiceResult<ArticleResult>> Get(int id)
		{
			var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");

			var route = $"Article/{id}";
			var response = await httpClient.GetAsync(route);

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();
			if (result is null)
			{
				return new ServiceResult<ArticleResult>();
			}

			return result;
		}

		//Create
		public async Task<ServiceResult<ArticleResult>> Create(ArticleRequest request)
		{
			var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");

			var route = "Article";
			var response = await httpClient.PostAsJsonAsync(route, request);

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();
			if (result is null)
			{
				return new ServiceResult<ArticleResult>();
			}

			return result;
		}

		//Update
		public async Task<ServiceResult<ArticleResult>> Update(int id, ArticleRequest request)
		{
			var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");

			var route = $"Article/{id}";
			var response = await httpClient.PutAsJsonAsync(route, request);

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();
			if (result is null)
			{
				result = new ServiceResult<ArticleResult>();
				result.NotFound(nameof(ArticleResult), id);
			}

			return result;
		}

		//Delete
		public async Task<ServiceResult<ArticleResult>> Delete(int id)
		{
			var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");

			var route = $"Article/{id}";
			var response = await httpClient.DeleteAsync(route);

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<ServiceResult<ArticleResult>>();
			if (result is null)
			{
				return new ServiceResult<ArticleResult>();
			}

			return result;
		}
	}
}
