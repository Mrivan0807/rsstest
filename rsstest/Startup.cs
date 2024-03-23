namespace rsstest
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // 添加RssController到路由中
                endpoints.MapControllerRoute(
                    name: "rss",
                    pattern: "Rss/{action=Index}/{id?}",
                    defaults: new { controller = "Rss" });
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // 添加MVC服務
        }

    }
}
