var app = {
    alertSuccess: function (message, time) {
        DevExpress.ui.notify({
            message: message,
            width: 350,
            position: {
                my: "center top",
                at: "center top",
                offsite: "-10 -10"
            }
        }, "success", time || 3000);
    },
    alertError: function (message, time) {
        DevExpress.ui.notify({
            message: message,
            width: 350,
            position: {
                my: "center top",
                at: "center top",
                offsite: "-10 -10"
            }
        }, "error", time || 3000);
    },
    alertWarning: function (message, time) {
        DevExpress.ui.notify({
            message: message,
            width: 350,
            position: {
                my: "center top",
                at: "center top",
                offsite: "-10 -10"
            }
        }, "warning", (time || 3000));
    },
    dialogConfirm: function (options) {
        var customDialog = DevExpress.ui.dialog.custom({
            title: options.title || "Dialog Title",
            messageHtml: options.html || "<div></div>",
            buttons: [{
                text: "Confirm",
                icon: "fas fa-check-circle",
                type: options.buttonConfirmType || "danger",
                onClick: function (e) {
                    return true
                }
            }, {
                text: "Cancel",
                icon: "fas fa-times-circle",
                onClick: function (e) {
                    return false
                }
            }]
        });

        customDialog.show().done(function (dialogResult) {
            if (dialogResult) {
                if (typeof (options.onConfirm) === "function") {
                    options.onConfirm();
                }
            } else {
                if (typeof (options.onCancel) === "function") {
                    options.onCancel();
                }
            }

        });
    },
    logoutConfirm: function () {
        this.dialogConfirm({
            title: "Logout",
            html: "<p>Would you like to logout?</p>",
            onConfirm: function () {
                window.location.href = baseUrl + "Account/Logout"
            }
        });
    },
    renderEditorWithCustomHelpText: function (data, itemElement, options) {
        var elemenItemLabel = itemElement.parent().children().first();
        var elemenConentLabel = elemenItemLabel.children().first();
        var spanLast = elemenConentLabel.children().last();

        elemenConentLabel.attr("id", "label-content-help-" + data.component.getItemID() + "-" + + data.dataField);

        var btnId = "label-help-" + data.component.getItemID() + "-" + + data.dataField;

        var btnHelp = $("<span>")
            .dxButton({
                elementAttr: {
                    id: btnId,
                    class: "label-icon-help"
                },
                tabIndex: -1,
                icon: "fas fa-question-circle",
                text: ""
            });

        spanLast.append(btnHelp);

        var tooltip = $("<div>" + options.helpText + "</div>")
            .dxTooltip({
                target: "#" + btnId,
                showEvent: "mouseenter",
                hideEvent: "mouseleave",
                closeOnOutsideClick: false,
                maxWidth: function () {
                    return window.innerWidth / 2.5;
                },
                elementAttr: {
                    class: "label-tooltip-help"
                }
            });

        btnHelp.append(tooltip);

        var editorOptions = Object.assign({}, data.editorOptions);
        var _validationRules = Object.assign({}, options.validationRules);

        editorOptions.value = data.component.option('formData')[data.dataField];
        editorOptions.onValueChanged = function (e) {
            var oldValue = data.component.option('formData')[data.dataField];
            if (oldValue !== e.value) {
                data.component.updateData(data.dataField, e.value);
            }
        }

        var _customEditor = null;

        if (!data.component.customEditor) {
            data.component.customEditor = [];
        }

        switch (data.editorType) {
            case "dxSelectBox":
                _customEditor = $("<div>").dxSelectBox(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxSelectBox("instance");
                break;
            case "dxTagBox":
                _customEditor = $("<div>").dxTagBox(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxTagBox("instance");
                break;
            case "dxSwitch":
                _customEditor = $("<div>").dxSwitch(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxSwitch("instance");
                break;
            case "dxNumberBox":
                _customEditor = $("<div>").dxNumberBox(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxNumberBox("instance");
                break;
            case "dxDateBox":
                _customEditor = $("<div>").dxDateBox(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxDateBox("instance");
                break;
            case "dxTextArea":
                _customEditor = $("<div>").dxTextArea(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxTextArea("instance");
                break;
            default:
                _customEditor = $("<div>").dxTextBox(editorOptions)
                    .dxValidator({ validationRules: _validationRules, validationGroup: options.validationGroup });
                data.component.customEditor[data.dataField] = _customEditor.dxTextBox("instance");
                break;
        }

        itemElement.append(_customEditor);
    },
    sendRequest: function (url, method, data) {
        var that = this;
        var d = $.Deferred();
        var model = data;

        $.ajax(url, {
            type: method,
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(model),
            cache: false,
        }).done(function (result, response) {
            if (!result.isError && result.data) {

                d.resolve();

                that.alertSuccess(response);

                return result.data;
            }

            that.alertError(result.message);
            d.reject(result.message);
        }).fail(function (xhr, response) {
            d.reject(xhr.responseText);
            that.alertError("Error status : " + xhr.status + xhr.responseText);
        });

        return d.promise();
    },
    customStore: function (options, callback) {
        var that = this;
        var store = new DevExpress.data.CustomStore({
            key: options.key,
            load: function (loadOptions) {
                var _url = options.loadUrl;

                if (typeof options.loadUrl === "function") {
                    _url = options.loadUrl();
                }

                return that.loadData(_url, loadOptions, callback);
            },
            insert: function (values) {
                return that.sendRequest(options.insertUrl, "POST", values);
            },
            update: function (key, values) {
                return that.sendRequest(options.updateUrl + "/" + key, "PUT", values);
            },
            remove: function (key) {
                return that.sendRequest(options.deleteUrl + "/" + key, "DELETE");
            },
            byKey: function (key) {
                var d = new $.Deferred();

                $.get(options.byKeyUrl + key)
                    .done(function (result) {
                        d.resolve(result.data);
                    });

                return d.promise();
            }
        });

        return store;
    },
    loadData: function (_url, loadOptions, callback) {
        var deferred = $.Deferred(),
            args = {};

        if (loadOptions) {
            var page = (loadOptions.take) ? (loadOptions.skip / loadOptions.take) + 1 : 1;

            if (typeof (loadOptions.sort) === "string") {
                args.sort = JSON.stringify([{ selector: loadOptions.sort, desc: false }]);
            } else {
                args.sort = JSON.stringify(loadOptions.sort);
            }

            if (loadOptions.filter) {
                args.filter = JSON.stringify(filterParse(loadOptions.filter));
            }

            if (loadOptions.searchExpr) {
                var searchFilter = [];
                loadOptions.searchExpr.split(';').map(function (item, i) {
                    searchFilter.push({
                        filterField: item,
                        filterOperator: loadOptions.searchOperation,
                        filterValue: loadOptions.searchValue
                    });
                });

                args.filter = JSON.stringify(searchFilter);
            }

            args.pageSize = loadOptions.take || 50;
            args.pageNumber = page;
        }

        $.ajax({
            url: _url,
            dataType: "json",
            data: args,
            cache: false,
            success: function (result) {
                var data = result.data;

                if (!result.isError) {
                    deferred.resolve(data.result, {
                        totalCount: data.totalCount,
                        //summary: result.summary,
                        //groupCount: result.groupCount
                    });
                    if (typeof callback === "function") {
                        callback(result);
                    }
                } else
                    deferred.reject(result.message);
            },
            error: function () {
                deferred.reject("Data Loading Error");
            }
        });

        return deferred.promise();
    },
    lookupDataSource: function (options) {
        var that = this;

        return {
            store: new DevExpress.data.CustomStore({
                key: options.key,
                load: function (loadOptions) {
                    return that.loadData(options.url, loadOptions);
                },
                byKey: function (key) {
                    var d = new $.Deferred();

                    $.get(options.byKeyUrl + key)
                        .done(function (result) {
                            d.resolve(result.data);
                        });

                    return d.promise();
                }
            }),
            sort: options.sort,
            paginate: true,
            pageSize: options.pageSize || 10
        }
    },
    apiLoadData: function (url, method, data = null) {
        var json;
        if (data !== null) {
            data = data.values;
        }
        $.ajax(url, {
            type: method,
            data: JSON.stringify(data),
            cache: false,
            //async: false
        }).done(function (result, response) {
            var message = result.message;
            if (!result.isError) {
                var data = result.data.result;
                if (data.length != 0) {
                    DevExpress.ui.notify({
                        message: "Load data successfully!",
                        width: 300,
                        position: { at: 'bottom right', my: "bottom right", offset: '-10 -10' }
                    }, "success", 1500);
                } else {
                    DevExpress.ui.notify({
                        message: "Data is empty!",
                        width: 300,
                        position: { at: 'bottom right', my: "bottom right", offset: '-10 -10' }
                    }, "warning", 1500);
                }
                return json = method === "GET" ? data : true;
            }
            DevExpress.ui.notify(message, "error", 1000);
            return false;
        }).fail(function (xhr, response) {
            DevExpress.ui.notify("Error status : " + xhr.status + xhr.responseText, "error", 2000);
            return json = false;
        });
        return json;
    },
    dataGrid: {
        dataOptions: null,
        instance: null,
        initialize: function (options) {
            this.dataOptions = options;
            this.dataOptions.popupForm = null;

            var _options = this.getOptions(options);

            if (typeof (options.onToolbarPreparing) === "function") {
                _options.onToolbarPreparing = options.onToolbarPreparing;
            }

            if (this.instance) {
                this.instance.option("dataSource", _options.dataSource.bind(this));
                this.instance.option("onToolbarPreparing", _options.onToolbarPreparing.bind(this));
                this.instance.option("columns", _options.columns);
            } else {
                this.instance = $("#" + options.elementId).dxDataGrid(_options).dxDataGrid("instance");
            }

            window[this.elementId] = this.instance;
        },
        getOptions: function (options) {
            var that = this;
            var allowDelete = true;
            if (typeof (that.dataOptions.allowAdd) !== "undefined") {
                allowDelete = that.dataOptions.allowDelete;
            }

            var _columns = [
                {
                    type: "buttons",
                    width: '10%',
                    height: "auto",
                    caption: "Action",
                    buttons: [{
                        hint: "Edit",
                        icon: "edit",
                        onClick: function (e) {
                            var dataOpt = Object.assign({}, options);
                            dataOpt.keyValue = e.row.data.id;
                            dataOpt.title = "Edit " + options.formTitle;
                            dataOpt.instance = that.instance;

                            app.loadPanelForm.show(dataOpt);
                        }
                    }, {
                        hint: "Delete",
                        icon: "trash",
                        visible: allowDelete,
                        onClick: function (e) {
                            app.dialogConfirm({
                                title: "Confirm delete",
                                html: "<i>Are you sure?</i>",
                                onConfirm: function () {
                                    $.ajax({
                                        url: options.deleteUrl,
                                        data: { ID: e.row.data.id },
                                        success: function (respone) {
                                            if (!respone.isError) {
                                                that.instance.refresh();
                                                app.alertSuccess(respone.message || "Delete data success.");
                                            } else {
                                                app.alertError(respone.message || "error when delete data");
                                            }
                                        },
                                        error: function (e) {
                                            app.alertError(e.responseText || "error when delete data");
                                        }
                                    });
                                },
                                onCancel: function () { }
                            });
                        }
                    }]
                }
            ]

            return {
                dataSource: DevExpress.data.AspNet.createStore({
                    key: options.key,
                    loadUrl: options.loadUrl,
                    onBeforeSend: function (method, ajaxOptions) {
                        ajaxOptions.xhrFields = { withCredentials: true };
                    }
                }),
                showBorders: true,
                showRowLines: true,
                remoteOperations: true,
                rowAlternationEnabled: true,
                columnsAutoWidth: true,
                allowColumnReordering: true,
                focusedRowEnabled: true,
                columnHidingEnabled: true,
                hoverStateEnabled: true,
                allowColumnResizing: true,
                columnResizingMode: "widget",
                onToolbarPreparing: that.onToolbarPreparing.bind(that),
                paging: {
                    pageSize: 5
                },
                sorting: {
                    mode: "multiple"
                },
                filterRow: {
                    visible: true,
                    applyFilter: "auto"
                },
                pager: {
                    showPageSizeSelector: true,
                    allowedPageSizes: [5, 10, 25, 50],
                    showInfo: true
                },
                columns: Object.assign(options.columns, _columns)
            };
        },
        onToolbarPreparing: function (e) {
            var that = this;
            var dxGrid = e.component;
            var allowAdd = true;
            if (typeof (that.dataOptions.allowAdd) !== "undefined") {
                allowAdd = that.dataOptions.allowAdd;
            }

            e.toolbarOptions.items.unshift({
                location: "after",
                widget: "dxButton",
                options: {
                    icon: "refresh",
                    onClick: function () {
                        dxGrid.refresh();
                    }
                }
            }, {
                location: "after",
                widget: "dxButton",
                options: {
                    icon: "add",
                    type: "default",
                    visible: allowAdd,
                    onClick: function () {
                        var dataOpt = Object.assign({}, that.dataOptions);
                        dataOpt.keyValue = 0;
                        dataOpt.title = "Add " + that.dataOptions.formTitle;
                        dataOpt.instance = that.instance;

                        app.loadPanelForm.show(dataOpt);
                    }
                }
            });
        }
    },
    popupForm: {
        instance: null,
        dataOptions: null,
        getOptions: function (options) {
            var that = this;
            return {
                width: options.formWidth || '95%',
                height: options.formHeight || '95%',
                contentTemplate: function () {
                    var scrollView = $('<div />');

                    scrollView.append($(options.html));

                    scrollView.dxScrollView({
                        width: '100%',
                        height: '100%'
                    }).dxScrollView("instance");

                    return scrollView;
                },
                showTitle: true,
                title: options.title,
                visible: false,
                dragEnabled: true,
                closeOnOutsideClick: false,
                onShown: function () {
                    if (window[that.dataOptions.formView]) {
                        window[that.dataOptions.formView].dataGrid = that.dataOptions;
                        window[that.dataOptions.formView].initialize(function (e) {

                        });
                    } else {
                        console.warn("form view not set");
                    }
                },
                toolbarItems: [{
                    toolbar: "bottom",
                    widget: "dxButton",
                    location: "after",
                    options: {
                        text: "Save",
                        icon: "save",
                        type: "success",
                        onClick: function (e) {
                            window[that.dataOptions.formView].submit();
                        }
                    }
                }, {
                    toolbar: "bottom",
                    widget: "dxButton",
                    location: "after",
                    options: {
                        text: "Close",
                        icon: "fas fa-times",
                        type: "danger",
                        onClick: function (e) {
                            that.instance.hide();
                        }
                    }
                }]
            }
        },
        show: function (options) {
            this.dataOptions = options;
            var _options = this.getOptions(options);

            if (this.instance) {
                this.instance.option("contentTemplate", _options.contentTemplate.bind(this));
                this.instance.option("toolbarItems", _options.toolbarItems);
                this.instance.option("title", _options.title);
            } else {
                this.instance = $("#popup-panel").dxPopup(_options).dxPopup("instance");
            }
            this.dataOptions.popupForm = this.instance;
            this.instance.show();
        }
    },
    loadPanelForm: {
        instance: null,
        result: null,
        dataOptions: null,
        getOptions: function (options) {
            var that = this;
            var params = {};

            params[options.key || "id"] = options.keyValue;
            that.result = null;

            return {
                shadingColor: "rgba(255,255,255,.8)",
                position: { of: "body" },
                visible: false,
                showIndicator: true,
                showPane: true,
                shading: true,
                closeOnOutsideClick: false,
                onShown: function () {
                    $.ajax({
                        url: options.formUrl,
                        dataType: "html",
                        data: params,
                        success: function (result) {
                            that.result = result;
                            that.instance.hide();
                        },
                        error: function () {
                            that.result = null;
                            that.instance.hide();
                        },
                        //timeout: 5000
                    });
                },
                onHidden: function () {
                    options.html = that.result;
                    app.popupForm.show(that.dataOptions);
                }
            }
        },
        show: function (options) {
            this.dataOptions = options;
            var _options = this.getOptions(options);

            if (this.instance) {
                this.instance.option("onShown", _options.onShown.bind(this));
                this.instance.option("onHidden", _options.onHidden.bind(this));
            } else {
                this.instance = $("#loading-panel").dxLoadPanel(_options).dxLoadPanel("instance");
            }

            this.instance.show();
        }
    },
    loadPanel: {
        instance: null,
        result: null,
        getOptions: function () {
            var that = this;

            return {
                shadingColor: "rgba(255,255,255,.8)",
                container: "body",
                visible: false,
                showIndicator: true,
                showPane: true,
                shading: true,
                closeOnOutsideClick: false,
                onShown: function () {

                },
                onHidden: function () {

                }
            }
        },
        show: function () {
            var _options = this.getOptions();

            if (this.instance) {
                this.instance.option("onShown", _options.onShown.bind(this));
                this.instance.option("onHidden", _options.onHidden.bind(this));
            } else {
                this.instance = $("#loadpanel-global").dxLoadPanel(_options).dxLoadPanel("instance");
            }

            this.instance.show();
        },
        hide: function () {
            this.instance.hide();
        }
    },
    postData: function (url, _params) {
        var d = $.Deferred();
        $.ajax(url, {
            method: "POST",
            data: JSON.stringify(_params),
            cache: false,
            contentType: "application/json",
            beforeSend: function (xhr) {
                app.loadPanel.show();
            }
        }).done(d.resolve)
            .fail(function (xhr) {
                console.log('err', xhr);
                d.reject(xhr.responseJSON ? xhr.responseJSON.message : xhr.statusText)
            }).always(function () {
                app.loadPanel.hide();
            });
        return d.promise();
    },
    popupUnauthorize: {
        instance: null,
        dataOptions: null,
        getOptions: function () {
            return {
                visible: false,
                width: '32%',
                height: '30%',
                title: "Login Expired",
                closeOnOutsideClick: false,
                toolbarItems: [{
                    widget: "dxButton",
                    toolbar: "bottom",
                    location: "center",
                    options: {
                        text: "Ok",
                        onClick: function (e) {
                            window.location.reload();
                        },
                    }
                }, {
                    widget: "dxButton",
                    toolbar: "bottom",
                    location: "center",
                    options: {
                        text: "Logout",
                        type: "danger",
                        onClick: function (e) {
                            window.location.href = baseUrl + "Home/Logout";
                        },
                    }
                }],
                contentTemplate: function (container) {
                    container.append($('<div />').html("Your session has expired. Do you want to extend the session?"));

                    return container;
                }
            }
        },
        show: function () {
            var _options = this.getOptions();

            if (this.instance) {
                this.instance.option("contentTemplate", _options.contentTemplate.bind(this));
                this.instance.option("toolbarItems", _options.toolbarItems);
                this.instance.option("title", _options.title);
            } else {
                this.instance = $("#popup-panel").dxPopup(_options).dxPopup("instance");
            }
            this.instance.show();
        }
    }
}