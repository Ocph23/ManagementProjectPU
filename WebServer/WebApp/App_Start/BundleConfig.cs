﻿using System.Web;
using System.Web.Optimization;

namespace WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

               bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                    "~/bower_components/angular/angular.js",
                    "~/bower_components/angular-route/angular-route.js",
                    "~/bower_components/angular-ui-router/release/angular-ui-router.js"   ,
                    "~/Apps/app.js",
                    "~/Apps/app.config.js",
                    "~/Apps/admin/AdminController.js",
                    "~/Apps/admin/AdminRoute.js"     ,
                    "~/Apps/admin/AdminService.js" ,
                    "~/Apps/Home/home.controller.js"     ,
                    "~/Apps/Home/home.route.js"         ,
                    "~/Apps/Home/home.Service.js"



                    ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}

     