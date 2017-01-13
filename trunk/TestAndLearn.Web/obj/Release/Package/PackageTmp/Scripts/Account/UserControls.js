if (typeof (TestAndLearn) == 'undefined') {
    var TestAndLearn = {};
}

TestAndLearn.Account = {};

TestAndLearn.Account.UserControls = (function () {
    var _config;
    var _heightOffset = 210;

    return {
        init: function (config) {
            _config = config;

            this.initGrid();

            this.initForms();

            this.initUserSearch();
        },

        initUserSearch: function () {
            $('#user-search').autocomplete({
                minLength: 4,
                source: _config.searchPublicisUsersUrl,
                focus: function (event, ui) {

                },
                select: function (event, ui) {
                    $('#starcom-username').val(ui.item.SimpleName);
                    $('#starcom-email').val(ui.item.Email);
                    $('#user-search').val(ui.item.Name);
                    return false;
                },
                change: function (event, ui) {

                },
                search: function (event, ui) {

                }
            })
            .data('ui-autocomplete')._renderItem = function (ul, item) {
                return $("<li></li>")
                .data("item.autocomplete", item)
                .append([
                    '<a href="javascript:void(0)">',
                        '<div>',
                            '<span style="font-weight:bold">',
                                item.SimpleName,
                            '</span>',
                            '<span style="margin-left: 5px; color: black">',
                                item.Name,
                            '</span>',
                         '</div>',
                         '<p class="descriptive-text">',
                            item.Email,
                         '</p>',
                    '</a>'
                ].join(''))
                .appendTo(ul);
            };
        },

        initGrid: function () {
            this.Grid = $('#user-grid').jqGrid({
                autowidth: true,
                width: '100%',
                height: $(document).height() - _heightOffset,
                datatype: 'json',
                url: _config.getUsersUrl,
                sortable: false,
                rowNum: 10000000,
                scroll: true,
                viewrecords: true,
                emptyDataText: 'There are no Users.',
                sortname: 'UserId',
                sortorder: 'desc',
                idPrefix: 'user_',
                jsonReader: {
                    repeatitems: false
                },
                colNames: [
                    'UserId',
                    'UserName',
                    'Email',
                    'Role',
                    'Client',
                    ''
                ],
                colModel: [
                  {
                      name: 'UserId',
                      index: 'UserId',
                      jsonmap: 'UserId',
                      key: true,
                      hidden: true
                  }, {
                      name: 'UserName',
                      index: 'UserName',
                      jsonmap: 'UserName',
                      fixed: true,
                      width: 200
                  }, {
                      name: 'Email',
                      index: 'Email',
                      jsonmap: 'Email'
                  }, {
                      name: 'Role',
                      index: 'Role',
                      jsonmap: 'Role',
                      fixed: true,
                      width: 100,
                      align: 'center'
                  }, {
                      name: 'ClientsAsString',
                      index: 'ClientsAsString',
                      jsonmap: 'ClientsAsString'
                  }, {
                      name: '',
                      index: '',
                      jsonmap: '',
                      fixed: true,
                      width: 20,
                      align: 'center',
                      formatter: function (cellvalue, stuff, rowObject) {
                          return '<a href="javascript:void(0)" id="delete-user-' + rowObject.UserName + '" class="delete-user delete-link"></a>';
                      }
                  }
                ],
                loadComplete: function (data) {
                    $(this).parent().children('div.empty-grid').first().remove();

                    if (data.rows.length == 0) {
                        $(this).parent().append([
                            '<div class="empty-grid bs-callout bs-callout-warning row">',
                            '<h4>No Users</h4>',
                            '<p>',
                                'There are users in the system.',
                            '</p>',
                            '</div>'].join(''));

                    }

                    $('.delete-user').click(function () {
                        var id = $(this).attr('id').toString().replace('delete-user-', '');

                        TestAndLearn.Account.UserControls.deleteUser(id);
                    });
                }
            });
        },

        initForms: function () {
            $('#starcom-user-form').ajaxForm({
                beforeSubmit: function () {
                    $('#save-starcom-user-modal .model-content').mask('Saving User...');
                },

                success: function (responseText, statusText, xhr, $form) {
                    if (responseText.Success == true) {
                        $('#save-starcom-user-modal').modal('hide');
                        $('#save-starcom-user-modal .model-content').unmask();
                        $('#user-grid').trigger('reloadGrid');
                        JQueryUX.Msg.SlideIn({
                            width: 200,
                            height: 50,
                            msg: 'User Saved',
                            dockAt: 'tc',
                            wrapperWidthOffset: 8,
                            wrapperHeightOffset: 8
                        });
                    }
                    else {
                        alert(response.Message);
                    }
                }
            });

            $('#client-user-form').ajaxForm({
                beforeSubmit: function () {
                    $('#save-client-user-modal .model-content').mask('Saving User...');
                },

                success: function (responseText, statusText, xhr, $form) {
                    if (responseText.Success == true) {
                        $('#save-client-user-modal').modal('hide');
                        $('#save-client-user-modal .model-content').unmask();
                        $('#user-grid').trigger('reloadGrid');
                        JQueryUX.Msg.SlideIn({
                            width: 200,
                            height: 50,
                            msg: 'User Saved',
                            dockAt: 'tc',
                            wrapperWidthOffset: 8,
                            wrapperHeightOffset: 8
                        });
                    }
                    else {
                        alert(response.Message);
                    }
                }
            });
        },

        deleteUser: function (username) {
            $.ajax({
                url: _config.deleteUserUrl,
                dataType: 'json',
                type: 'POST',
                data: {
                    username : username
                },
                success: function (response) {
                    if (response.Success == true) {
                        JQueryUX.Msg.SlideIn({
                            width: 200,
                            height: 50,
                            msg: 'User Deleted',
                            dockAt: 'tc',
                            wrapperWidthOffset: 8,
                            wrapperHeightOffset: 8
                        });
                        $('#user-grid').trigger('reloadGrid');
                    }
                    else {
                        alert(response.Message);
                    }

                },
                error: function (xhr, status, error) {
                    alert(error);
                }
            });
        }
    };
})();