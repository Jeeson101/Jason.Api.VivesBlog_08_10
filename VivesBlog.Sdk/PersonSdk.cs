﻿using System.Net.Http.Json;
using System.Net.Http;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;
using VivesBlog.Dto.Request;
using VivesBlog.Dto.Result;

namespace VivesBlog.Sdk
{
	public class PersonSdk(IHttpClientFactory httpClientFactory)
	{
		private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

		//Find
		public async Task<IList<PersonResult>> Find()
		{
			var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");

			var route = "Person";
			var response = await httpClient.GetAsync(route);

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<IList<PersonResult>>();
			if (result is null)
			{
				return new List<PersonResult>();
			}

			return result;
		}

		//Get
		public async Task<ServiceResult<PersonResult>> Get(int id)
		{
			var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");

			var route = $"Person/{id}";
			var response = await httpClient.GetAsync(route);

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();
			if (result is null)
			{
				return new ServiceResult<PersonResult>();
			}

			return result;
		}

		//Create
		public async Task<ServiceResult<PersonResult>> Create(PersonRequest request)
		{
			var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");

			var route = "Person";
			var response = await httpClient.PostAsJsonAsync(route, request);

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();
			if (result is null)
			{
				return new ServiceResult<PersonResult>();
			}

			return result;
		}

		//Update
		public async Task<ServiceResult<PersonResult>> Update(int id, PersonRequest request)
		{
			var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");

			var route = $"Person/{id}";
			var response = await httpClient.PutAsJsonAsync(route, request);

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();
			if (result is null)
			{
				result = new ServiceResult<PersonResult>();
				result.NotFound(nameof(PersonResult), id);
			}

			return result;
		}

		//Delete
		public async Task<ServiceResult<PersonResult>> Delete(int id)
		{
			var httpClient = _httpClientFactory.CreateClient("VivesBlogApi");

			var route = $"Person/{id}";
			var response = await httpClient.DeleteAsync(route);

			response.EnsureSuccessStatusCode();

			var result = await response.Content.ReadFromJsonAsync<ServiceResult<PersonResult>>();
			if (result is null)
			{
				return new ServiceResult<PersonResult>();
			}

			return result;
		}
	}
}
