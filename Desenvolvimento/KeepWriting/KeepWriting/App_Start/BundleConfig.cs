using System.Web;
using System.Web.Optimization;

namespace KeepWriting
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/jquery.magnific-popup.min.js",
                        "~/Scripts/jquery.validate-vsdoc.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/ckeditor").Include("~/Scripts/ckeditor/ckeditor.js"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/js").Include(
                "~/Scripts/animsition.min.js",
                "~/Scripts/aos.js",
                "~/Scripts/countdowntime.js",
                "~/Scripts/daterangepicker.js",
                "~/Scripts/main.js",
                "~/Scripts/main_1.js",
                "~/Scripts/moment.js",
                "~/Scripts/owl.carousel.min.js",
                "~/Scripts/popper.min.js",
                "~/Scripts/select2.min.js",
                "~/Scripts/slick.min.js"));

            //bundles.Add(new StyleBundle("~/Content/css").Include(
                      //"~/Content/bootstrap.css",
                      //"~/Content/site.css",
                      //"~/Content/animate.css",
                      //"~/Content/animsition.css",
                      //"~/Content/blog.css",
                      //"~/Content/blog_responsive.css",
                      //"~/Content/bootstrap-datepicker.css",
                      //"~/Content/bootstrap-theme.css",
                      //"~/Content/bootstrap-theme.css.map",
                      //"~/Content/bootstrap-theme.min.css",
                      //"~/Content/bootstrap-theme.min.css.map",
                      //"~/Content/bootstrap.css.map",
                      //"~/Content/bootstrap.min.css",
                      //"~/Content/bootstrap.min.css.map",
                      //"~/Content/daterangepicker.css",
                      //"~/Content/elegant-fonts.css",
                      //"~/Content/estilo.css",
                      //"~/Content/feed.css",
                      //"~/Content/feed2.css",
                      //"~/Content/font-awesome.min.css",
                      //"~/Content/hamburgers.min.css",
                      //"~/Content/home.css",
                      //"~/Content/icomoon.css",
                      //"~/Content/logincadastro.css",
                      //"~/Content/magnific-popup.css",
                      //"~/Content/main.css"
                      //"~/Content/material-design-iconic-font.min.css",
                      //"~/Content/owl.carousel.min.css",
                      //"~/Content/owl.theme.default.min.css",
                      //"~/Content/perfil.css",
                      //"~/Content/responsive.css",
                      //"~/Content/select2.min.css",
                      //"~/Content/Site.css",
                      //"~/Content/style.css",
                      //"~/Content/util.css"
                      //));

            bundles.Add(new StyleBundle("~/Content/css/_Layout").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/main.css",
                "~/Content/home.css",
                "~/Content/font-awesome.min.css",
                "~/Content/material-design-iconic-font.min.css",
                "~/Content/animate.css",
                "~/Content/hamburgers.min.css",
                "~/Content/animsition.css",
                "~/Content/select2.min.css",
                "~/Content/daterangepicker.css",
                "~/Content/util.css",
                "~/Content/main.css"));        }


    }
}
