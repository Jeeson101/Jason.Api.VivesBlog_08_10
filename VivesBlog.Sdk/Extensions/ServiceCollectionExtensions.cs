using Microsoft.Extensions.DependencyInjection;

namespace VivesBlog.Sdk.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApi(this IServiceCollection services, string apiUrl)
		{
			services.AddHttpClient("PeopleManagerApi", options =>
			{
				options.BaseAddress = new Uri(apiUrl);
			});

			services.AddScoped<PersonSdk>();
			services.AddScoped<ArticleSdk>();

			return services;
		}
	}
}
