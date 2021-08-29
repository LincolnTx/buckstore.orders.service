using System;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.DependencyInjection;

namespace buckstore.orders.service.infrastructure.CrossCutting.IoC.Configurations
{
	public static class SwaggerSetup
	{
		public static void AddSwaggerSetup(this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));
			services.AddSwaggerGen(s =>
			{
				s.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "BuckStore Orders Api",
					Description = "Api responsável pelas ações de compra e pedidos do E-Commerce",
					Contact = new OpenApiContact { Name = "Lincoln Teixeira", Email = "lincolnsf98@gmail.com" }
				});
				
				s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Token JWT de autorização Scheme Bearer",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer"
				});
				
				s.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						}, new List<string>()
					}
				});
				
				// s.OperationFilter<RemoveVersionParameterFilter>();
				// s.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
				
				services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
			});
		}
		
		public static void UseSwaggerSetup(this IApplicationBuilder app)
		{
			if (app == null) throw new ArgumentNullException(nameof(app));
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api responsável pelas ações de compra e pedidos do E-Commerce por Lincoln Teixeira");
			});
		}
	}
	
	public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
	{
		public void Configure(SwaggerGenOptions options)
		{
			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Version = "v1",
				Title = "BuckStore Orders Api",
				Description = "Api responsável pelas ações de compra e pedidos do E-Commerce",
				Contact = new OpenApiContact { Name = "Lincoln Teixeira", Email = "lincolnsf98@gmail.com" }
			});
		}
	}
}