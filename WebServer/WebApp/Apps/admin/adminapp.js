angular.module("app", [
    'ui.bootstrap',
    'ngProgress',
    'ngRoute',
    'ui.router',
    'admin.routes',
    'admin.services',
    'home.services',
    'admin.controllers'

]).directive('datepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        compile: function () {
            return {
                pre: function (scope, element, attrs, ngModelCtrl) {
                    var format, dateObj;
                    format = (!attrs.dpFormat) ? 'dd MM yyyy' : attrs.dpFormat;
                    if (!attrs.initDate && !attrs.dpFormat) {
                        // If there is no initDate attribute than we will get todays date as the default
                        dateObj = new Date();
                        scope[attrs.ngModel] = dateObj.getDate() + '/' + (dateObj.getMonth() + 1) + '/' + dateObj.getFullYear();
                    } else if (!attrs.initDate) {
                        // Otherwise set as the init date
                        scope[attrs.ngModel] = attrs.initDate;
                    } else {
                        // I could put some complex logic that changes the order of the date string I
                        // create from the dateObj based on the format, but I'll leave that for now
                        // Or I could switch case and limit the types of formats...
                    }
                    // Initialize the date-picker
                    $(element).datepicker({
                        format: format,
                    }).on('changeDate', function (ev) {
                        // To me this looks cleaner than adding $apply(); after everything.
                        scope.$apply(function () {
                            ngModelCtrl.$setViewValue(ev.format(format));
                        });
                    });
                }
            }
        }
    }
})
    .directive("bootstrapSlider", ["$parse", function ($parse) {
        return {
            restrict: "A",
            require: "?ngModel",
            link: function (scope, element, attrs, ngModel) {
                // Min value
                var min = 0;
                if (angular.isDefined(attrs.min)) {
                    min = parseInt(attrs.min);
                }

                // Max value
                var max = 100;
                if (angular.isDefined(attrs.max)) {
                    max = parseInt(attrs.max);
                }

                // Step
                var step = 5;
                if (angular.isDefined(attrs.step)) {
                    step = parseInt(attrs.step);
                }

                // Is a range?
                var range = angular.isDefined(attrs.range);

                // Initial value
                var value = range ? [min, max] : min;

                // Tooltip mode
                var tooltipMode = 'hide';
                if (angular.isDefined(attrs.tooltip)) {
                    tooltipMode = attrs.tooltip;
                }

                // Attach slider
                element.slider({
                    min: min,
                    max: max,
                    step: step,
                    range: range,
                    value: value,
                    tooltip: tooltipMode,
                    tooltip_split: angular.isDefined(attrs.splitTooltip)
                });

                // Custom formatter
                if (angular.isDefined(attrs.tooltipFormatter)) {
                    var formatter = $parse(attrs.tooltipFormatter);
                    element.slider('setAttribute', 'formater', function (value) {
                        return formatter(scope, { value: value });
                    });
                }

                if (ngModel && angular.isDefined(attrs.ngModel)) {
                    element.bind('slide', function (e) {
                        var setValue = function () {
                            ngModel.$setViewValue(e.value);
                        };
                        if (!scope.$$phase) {
                            scope.$apply(setValue);
                        }
                        else setValue();
                    });

                    ngModel.$render = function () {
                        if (!ngModel.$isEmpty(ngModel.$viewValue)) {
                            element.slider('setValue', ngModel.$modelValue, false);
                        }
                    };
                }

                // Enabled/disable
                scope.$watch(attrs.ngDisabled, function (disabled) {
                    element.slider(disabled ? 'disable' : 'enable');
                });
            }
        }
    }])
    ;

