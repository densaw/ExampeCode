﻿using System.Web;
using System.Web.Optimization;

namespace PmaPlus
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region NewStyleBundles

            // CSS style (bootstrap/inspinia)
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/style.css",
                      "~/Content/plugins/fileinput/fileinput.min.css"));

            bundles.Add(new StyleBundle("~/Content/css-light").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/animate.css",
                      "~/Content/plugins/fileinput/fileinput.min.css"));
            bundles.Add(new LessBundle("~/Content/less").Include(
                "~/Content/style.less"));

            bundles.Add(new ScriptBundle("~/plugins/fileinput").Include(
                "~/Scripts/plugins/fileinput/fileinput.min.js"
               
                ));

            // Font Awesome icons
            bundles.Add(new StyleBundle("~/font-awesome/css").Include(
                      "~/fonts/font-awesome/css/font-awesome.css"));

            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.1.min.js"));

            //Angular
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                "~/Scripts/angular/angular.js",
                "~/Scripts/angular/angular-route.js",
                 "~/Scripts/plugins/ladda/angular-ladda.min.js"));

            //BootStrap File
            //Angular BootStrap
            bundles.Add(new ScriptBundle("~/plugins/ui-bootstrap").Include(
                "~/Scripts/plugins/bootstrap/bootstrap.file-input.js",
                "~/Scripts/plugins/ui-bootstrap/ui-bootstrap-tpls-0.12.1.js",
                "~/Scripts/plugins/ui-bootstrap/bootstrap.js",
                "~/Scripts/userSripts.js"));

            bundles.Add(new ScriptBundle("~/plugins/angular-file").Include(
                "~/Scripts/angular/angular-file-model.js"));


            //Angular Controllers
            bundles.Add(new ScriptBundle("~/bundles/SysAdminCtr").Include(
                "~/Scripts/controllers/MainApp.js"));
            bundles.Add(new ScriptBundle("~/bundles/ClubAdminCtr").Include(
                "~/Scripts/controllers/ClubControllers.js"));

            // Session
            bundles.Add(new ScriptBundle("~/bundles/jquery-session").Include(
                "~/Scripts/jquery.session.js"));

            // jQueryUI CSS
            bundles.Add(new ScriptBundle("~/Scripts/plugins/jquery-ui/jqueryuiStyles").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.css"));

            // jQueryUI 
            bundles.Add(new StyleBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui/jquery-ui.min.js"));

            // Bootstrap
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            // Inspinia script
            bundles.Add(new ScriptBundle("~/bundles/inspinia").Include(
                      "~/Scripts/plugins/metisMenu/metisMenu.min.js",
                      "~/Scripts/plugins/pace/pace.min.js",
                      "~/Scripts/app/inspinia.min.js"));

            // Inspinia skin config script
            bundles.Add(new ScriptBundle("~/bundles/skinConfig").Include(
                      "~/Scripts/app/skin.config.min.js"));

            // SlimScroll
            bundles.Add(new ScriptBundle("~/plugins/slimScroll").Include(
                      "~/Scripts/plugins/slimscroll/jquery.slimscroll.min.js"));

            // Peity
            bundles.Add(new ScriptBundle("~/plugins/peity").Include(
                      "~/Scripts/plugins/peity/jquery.peity.min.js"));

            // Video responsible
            bundles.Add(new ScriptBundle("~/plugins/videoResponsible").Include(
                      "~/Scripts/plugins/video/responsible-video.js"));

            // Lightbox gallery css styles
            bundles.Add(new StyleBundle("~/Content/plugins/blueimp/css/").Include(
                      "~/Content/plugins/blueimp/css/blueimp-gallery.min.css"));

            // Lightbox gallery
            bundles.Add(new ScriptBundle("~/plugins/lightboxGallery").Include(
                      "~/Scripts/plugins/blueimp/jquery.blueimp-gallery.min.js"));

            // Sparkline
            bundles.Add(new ScriptBundle("~/plugins/sparkline").Include(
                      "~/Scripts/plugins/sparkline/jquery.sparkline.min.js"));

            // Morriss chart css styles
            bundles.Add(new StyleBundle("~/plugins/morrisStyles").Include(
                      "~/Content/plugins/morris/morris-0.4.3.min.css"));

            // Morriss chart
            bundles.Add(new ScriptBundle("~/plugins/morris").Include(
                      "~/Scripts/plugins/morris/raphael-2.1.0.min.js",
                      "~/Scripts/plugins/morris/morris.js"));

            // Flot chart
            bundles.Add(new ScriptBundle("~/plugins/flot").Include(
                      "~/Scripts/plugins/flot/jquery.flot.js",
                      "~/Scripts/plugins/flot/jquery.flot.tooltip.min.js",
                      "~/Scripts/plugins/flot/jquery.flot.resize.js",
                      "~/Scripts/plugins/flot/jquery.flot.pie.js",
                      "~/Scripts/plugins/flot/jquery.flot.time.js",
                      "~/Scripts/plugins/flot/jquery.flot.spline.js"));

            // Rickshaw chart
            bundles.Add(new ScriptBundle("~/plugins/rickshaw").Include(
                      "~/Scripts/plugins/rickshaw/vendor/d3.v3.js",
                      "~/Scripts/plugins/rickshaw/rickshaw.min.js"));

            // ChartJS chart
            bundles.Add(new ScriptBundle("~/plugins/chartJs").Include(
                      "~/Scripts/plugins/chartjs/Chart.min.js"));

            // ChartJS chart Angular
            bundles.Add(new ScriptBundle("~/plugins/ngChartJs").Include(
                      "~/Scripts/plugins/chartjs/tc.ngular.chartjs.min.js"));

            // Spark chart Angular
            bundles.Add(new ScriptBundle("~/plugins/spark").Include(
                      "~/Scripts/plugins/chartjs/ng-sparkline-seriesbar.js"));

            //Pagination
            bundles.Add(new ScriptBundle("~/plugins/pagination").Include(
                "~/Scripts/plugins/pagination/dirPagination.js"));

            // iCheck css styles
            bundles.Add(new StyleBundle("~/Content/plugins/iCheck/iCheckStyles").Include(
                      "~/Content/plugins/iCheck/custom.css"));

            // iCheck
            bundles.Add(new ScriptBundle("~/plugins/iCheck").Include(
                      "~/Scripts/plugins/iCheck/icheck.min.js"));

            // dataTables css styles
            bundles.Add(new StyleBundle("~/Content/plugins/dataTables/dataTablesStyles").Include(
                      "~/Content/plugins/dataTables/dataTables.bootstrap.css",
                      "~/Content/plugins/dataTables/dataTables.responsive.css",
                      "~/Content/plugins/dataTables/dataTables.tableTools.min.css"));

            // dataTables 
            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Scripts/plugins/dataTables/jquery.dataTables.js",
                      "~/Scripts/plugins/dataTables/dataTables.bootstrap.js",
                      "~/Scripts/plugins/dataTables/dataTables.responsive.js",
                      "~/Scripts/plugins/dataTables/dataTables.tableTools.min.js",
                      "~/Scripts/plugins/dataTables/jquery.base64.js",
                      "~/Scripts/plugins/dataTables/jquery.dataTables.js",
                      "~/Scripts/plugins/dataTables/tableExport.js",
                      "~/Scripts/plugins/dataTables/jspdf/jspdf.js",
                      "~/Scripts/plugins/dataTables/jspdf/libs/sprintf.js",
                      "~/Scripts/plugins/dataTables/jspdf/libs/base64.js"));


            // jeditable 
            bundles.Add(new ScriptBundle("~/plugins/jeditable").Include(
                      "~/Scripts/plugins/jeditable/jquery.jeditable.js"));

            // jqGrid styles
            bundles.Add(new StyleBundle("~/Content/plugins/jqGrid/jqGridStyles").Include(
                      "~/Content/plugins/jqGrid/ui.jqgrid.css"));

            // jqGrid 
            bundles.Add(new ScriptBundle("~/plugins/jqGrid").Include(
                      "~/Scripts/plugins/jqGrid/i18n/grid.locale-en.js",
                      "~/Scripts/plugins/jqGrid/jquery.jqGrid.min.js"));

            // codeEditor styles
            bundles.Add(new StyleBundle("~/plugins/codeEditorStyles").Include(
                      "~/Content/plugins/codemirror/codemirror.css",
                      "~/Content/plugins/codemirror/ambiance.css"));

            // codeEditor 
            bundles.Add(new ScriptBundle("~/plugins/codeEditor").Include(
                      "~/Scripts/plugins/codemirror/codemirror.js",
                      "~/Scripts/plugins/codemirror/mode/javascript/javascript.js"));

            // codeEditor 
            bundles.Add(new ScriptBundle("~/plugins/nestable").Include(
                      "~/Scripts/plugins/nestable/jquery.nestable.js"));

            // validate 
            bundles.Add(new ScriptBundle("~/plugins/validate").Include(
                      "~/Scripts/plugins/validate/jquery.validate.min.js"));

            // fullCalendar styles
            bundles.Add(new StyleBundle("~/plugins/fullCalendarStyles").Include(
                      "~/Content/plugins/fullcalendar/fullcalendar.css"));

            // fullCalendar 
            bundles.Add(new ScriptBundle("~/plugins/fullCalendar").Include(
                      "~/Scripts/plugins/fullcalendar/moment.min.js",
                      "~/Scripts/plugins/fullcalendar/fullcalendar.min.js"));

            // vectorMap 
            bundles.Add(new ScriptBundle("~/plugins/vectorMap").Include(
                      "~/Scripts/plugins/jvectormap/jquery-jvectormap-1.2.2.min.js",
                      "~/Scripts/plugins/jvectormap/jquery-jvectormap-world-mill-en.js"));

            // ionRange styles
            bundles.Add(new StyleBundle("~/Content/plugins/ionRangeSlider/ionRangeStyles").Include(
                      "~/Content/plugins/ionRangeSlider/ion.rangeSlider.css",
                      "~/Content/plugins/ionRangeSlider/ion.rangeSlider.skinFlat.css"));

            // ionRange 
            bundles.Add(new ScriptBundle("~/plugins/ionRange").Include(
                      "~/Scripts/plugins/ionRangeSlider/ion.rangeSlider.min.js"));

            // dataPicker styles
            bundles.Add(new StyleBundle("~/plugins/dataPickerStyles").Include(
                      "~/Content/plugins/datapicker/datepicker3.css"));

            // dataPicker 
            bundles.Add(new ScriptBundle("~/plugins/dataPicker").Include(
                      "~/Scripts/plugins/datapicker/bootstrap-datepicker.js"));

            // nouiSlider styles
            bundles.Add(new StyleBundle("~/plugins/nouiSliderStyles").Include(
                      "~/Content/plugins/nouslider/jquery.nouislider.css"));

            // nouiSlider 
            bundles.Add(new ScriptBundle("~/plugins/nouiSlider").Include(
                      "~/Scripts/plugins/nouslider/jquery.nouislider.min.js"));

            // jasnyBootstrap styles
            bundles.Add(new StyleBundle("~/plugins/jasnyBootstrapStyles").Include(
                      "~/Content/plugins/jasny/jasny-bootstrap.min.css"));

            // jasnyBootstrap 
            bundles.Add(new ScriptBundle("~/plugins/jasnyBootstrap").Include(
                      "~/Scripts/plugins/jasny/jasny-bootstrap.min.js"));

            // switchery styles
            bundles.Add(new StyleBundle("~/plugins/switcheryStyles").Include(
                      "~/Content/plugins/switchery/switchery.css"));

            // switchery 
            bundles.Add(new ScriptBundle("~/plugins/switchery").Include(
                      "~/Scripts/plugins/switchery/switchery.js"));

            // chosen styles
            bundles.Add(new StyleBundle("~/Content/plugins/chosen/chosenStyles").Include(
                      "~/Content/plugins/chosen/chosen.css"));

            // chosen 
            bundles.Add(new ScriptBundle("~/plugins/chosen").Include(
                      "~/Scripts/plugins/chosen/chosen.jquery.js"));

            // knob 
            bundles.Add(new ScriptBundle("~/plugins/knob").Include(
                      "~/Scripts/plugins/jsKnob/jquery.knob.js"));

            // wizardSteps styles
            bundles.Add(new StyleBundle("~/plugins/wizardStepsStyles").Include(
                      "~/Content/plugins/steps/jquery.steps.css"));

            // wizardSteps 
            bundles.Add(new ScriptBundle("~/plugins/wizardSteps").Include(
                      "~/Scripts/plugins/staps/jquery.steps.min.js"));

            // dropZone styles
            bundles.Add(new StyleBundle("~/Content/plugins/dropzone/dropZoneStyles").Include(
                      "~/Content/plugins/dropzone/basic.css",
                      "~/Content/plugins/dropzone/dropzone.css"));

            // dropZone 
            bundles.Add(new ScriptBundle("~/plugins/dropZone").Include(
                      "~/Scripts/plugins/dropzone/dropzone.js"));

            // summernote styles
            bundles.Add(new StyleBundle("~/plugins/summernoteStyles").Include(
                      "~/Content/plugins/summernote/summernote.css",
                      "~/Content/plugins/summernote/summernote-bs3.css"));

            // summernote 
            bundles.Add(new ScriptBundle("~/plugins/summernote").Include(
                      "~/Scripts/plugins/summernote/summernote.min.js"));

            // toastr notification 
            bundles.Add(new ScriptBundle("~/plugins/toastr").Include(
                      "~/Scripts/plugins/toastr/toastr.min.js"));

            // toastr notification styles
            bundles.Add(new StyleBundle("~/plugins/toastrStyles").Include(
                      "~/Content/plugins/toastr/toastr.min.css"));

            // color picker
            bundles.Add(new ScriptBundle("~/plugins/colorpicker").Include(
                      "~/Scripts/plugins/colorpicker/bootstrap-colorpicker.min.js"));

            // color picker styles
            bundles.Add(new StyleBundle("~/Content/plugins/colorpicker/colorpickerStyles").Include(
                      "~/Content/plugins/colorpicker/bootstrap-colorpicker.min.css"));

            // image cropper
            bundles.Add(new ScriptBundle("~/plugins/imagecropper").Include(
                      "~/Scripts/plugins/cropper/cropper.min.js"));

            // image cropper styles
            bundles.Add(new StyleBundle("~/plugins/imagecropperStyles").Include(
                      "~/Content/plugins/cropper/cropper.min.css"));

            // jsTree
            bundles.Add(new ScriptBundle("~/plugins/jsTree").Include(
                      "~/Scripts/plugins/jsTree/jstree.min.js"));

            // jsTree styles
            bundles.Add(new StyleBundle("~/Content/plugins/jsTree").Include(
                      "~/Content/plugins/jsTree/style.css"));

            #endregion
        }
    }
}
