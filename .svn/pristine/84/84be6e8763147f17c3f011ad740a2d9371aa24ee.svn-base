if (typeof (TestAndLearn) == 'undefined') {
    var TestAndLearn = {};
}

if (typeof(TestAndLearn.Home) == 'undefined') {
    TestAndLearn.Home = {};
}

TestAndLearn.Home.Grids = (function () {
    var _config;
    var _offsetHeight = 280;

    var _sharedColNames = ['Id', 'Date', 'Test Name', 'Submitted By', 'Test Type', 'Success Metric(s)', 'Quarter', 'Year', 'Rank', 'Approve', 'Hypothesis', 'Additional Notes', 'Expected Business Value'];
    var _sharedColModel = [
                     {
                         name: 'TestId',
                         index: 'TestId',
                         hidden: true
                     }, {
                        name: 'SubmittedOn',
                        index: 'SubmittedOn',
                        sorttype: "date",
                        datefmt: 'mm/dd/yyyy',
                        search: true,
                        searchoptions: {
                            clearSearch: false
                        },
                        stype: 'text',
                        width: 100,
                        fixed: true
                    },
                    {
                        name: 'Name',
                        index: 'Name',
                        sorttype: "string",
                        search: true,
                        searchoptions: {
                            clearSearch: false
                        },
                        stype: 'text',
                        formatter: function (cellvalue, options, obj) {
                            return '<a href="javascript:void(0)" class="edit-row" id="edit-row-' + obj.TestId + '">' + cellvalue + ' </a>';
                        }
                    },
                    {
                        name: 'SubmittedBy',
                        index: 'SubmittedBy',
                        sorttype: "text",
                        search: true,
                        searchoptions: {
                            clearSearch: false
                        },
                        stype: 'text',
                        width: 250,
                        fixed: true
                    },
                    {
                        name: 'TestTypes',
                        index: 'TestTypes',
                        width: 200,
                        fixed: true,
                        search: true,
                        stype: 'text',
                        searchoptions: { 
                            clearSearch: false
                        },
                        formatter: function (cellvalue, options, rowObject) {
                            var s = '';
                            for (var i = 0; i < rowObject.TestTypes.length; i++) {
                                s += rowObject.TestTypes[i].Name + ',';
                            }
                            return s;
                        }
                    },
                    {
                        name: 'SuccessMetrics',
                        index: 'SuccessMetrics',
                        width: 200,
                        fixed: true,
                        sorttype: "string",
                        search: true,
                        searchoptions: {
                            clearSearch: false
                        },
                        stype: 'text',
                        formatter: function (cellvalue, options, rowObject) {
                            var s = '';
                            for (var i = 0; i < rowObject.SuccessMetrics.length; i++) {
                                s += rowObject.SuccessMetrics[i].Name + ',';
                            }
                            return s;
                        }
                    },

                    {
                        name: 'Quarter',
                        index: 'Quarter',
                        sorttype: "string",
                        search: true,
                        searchoptions: {
                            clearSearch: false
                        },
                        stype: 'text',
                        width: 60,
                        fixed: true
                    },
                    {
                        name: 'Year',
                        index: 'Year',
                        sorttype: 'int',
                        search: true,
                        searchoptions: {
                            clearSearch: false
                        },
                        stype: 'text',
                        width: 50,
                        fixed: true
                    },

                    {
                        name: 'Rank',
                        index: 'Rank',
                        sorttype: "number",
                        search: true,
                        stype: 'text',
                        searchoptions: {
                            clearSearch: false
                        },
                        width: 50,
                        fixed: true,
                        align: 'center',
                        formatter: function (cellvalue, options, rowObject) {
                            var rank = rowObject.Rank;

                            if (typeof (rowObject.Rank) == 'undefined' || rowObject.Rank == null) {
                                rank = '-'
                            }
                            return '<button class="edit-rank-row btn btn-default" id="edit-rank-row-' + rowObject.TestId + '">' + rank + '</button>';
                        }
                    },

                    {
                        name: '',
                        index: '',
                        search: false,
                        width: 80,
                        fixed: true,
                        formatter: function (cellvalue, options, rowObject) {
                            return '<button class="approve-row btn btn-info" id="approve-row-' + rowObject.TestId + '">approve</button>';
                        }
                    },

                    {
                        name: 'Hypothesis',
                        index: 'Hypothesis',
                        hidden: true
                    },
                    {
                        name: 'AdditionalNotes',
                        index: 'AdditionalNotes',
                        hidden: true
                    },

                    {
                        name: 'ExpectedBusinessValue',
                        index: 'ExpectedBusinessValue',
                        sorttype: "string",
                        hidden: true
                    }
    ];

    return {
        init: function (config) {
            _config = config;

            this.initGrid('submitted-test-grid');
            this.initGrid('approved-test-grid');
            this.initGrid('closed-test-grid');
        },

        initGrid: function (renderTo, data) {
            $('#' + renderTo).jqGrid({
                //data: data,
                url: _config.getTestsUrl,
                postData: {
                    clientId: _config.clientId,
                    testStatusId: _config.submittedStatusId
                },
                datatype: "json",
                mtype: 'GET',
                //width: '50%',
                autowidth: true,
                height: $(document).height() - _offsetHeight,
                //cellsubmit: 'clientArray',
                //editurl: 'clientArray',
                colNames: _sharedColNames,
                colModel: _sharedColModel,
                scroll: true,
                viewrecords: true,
                onSelectRow: function (rowid) {

                },
                afterInsertRow: function (rowid, rowdata, rowelem) {

                },
                afterRestoreRow: function () {

                },
                afterSaveCell: function (rowid, cellname, value, iRow, iCol) {

                },
                loadComplete: function (data) {
                    $(this).parent().children('div.empty-grid').first().remove();

                    if (data.length == 0) {
                        $(this).parent().append([
                            '<div class="empty-grid bs-callout bs-callout-warning row">',
                            '<h4>No Tests</h4>',
                            '<p>',
                                'There are no tests of the selected status and filter.',
                            '</p>',
                            '</div>'].join(''));

                    }
                    else {
                        $('#' + renderTo + ' .edit-row').unbind('click');

                        $('#' + renderTo + ' .edit-row').click(function (e) {
                            var id = $(this).attr('id').replace('edit-row-', '');
                            alert('TODO edit ' + id);
                        });

                        $('#' + renderTo + ' .approve-row').unbind('click');
                        $('#' + renderTo + ' .approve-row').click(function (e) {
                            var id = $(this).attr('id').replace('approve-row-', '');
                            alert('TODO approve ' + id);
                        });

                        $('#edit-rank-row').unbind('click');
                        $('#' + renderTo + ' .edit-rank-row').click(function (e) {
                            var id = $(this).attr('id').replace('edit-rank-row-', '');
                            alert('TODO submit rank for id: ' + id);
                        });
                    }
                },
                subGrid: true,
                subGridRowExpanded: function (subgrid_id, row_id) {
                    var row = $(this).getRowData(row_id);

                    var hypothesis = [
                        '<div class="bs-callout bs-callout-info row">',
                            '<h4>Hypothesis</h4>',
                            '<p>',
                                row.Hypothesis,
                            '</p>',
                        '</div>'].join('');

                    var additionalNotes = [
                        '<div class="bs-callout bs-callout-default row">',
                            '<h4>Additional Notes</h4>',
                            '<p>',
                                row.AdditionalNotes,
                            '</p>',
                        '</div>'].join('');

                    var expectedBusinessValue = [
                        '<div class="bs-callout bs-callout-red row">',
                            '<h4>Expected Business Value</h4>',
                            '<p>',
                                row.ExpectedBusinessValue,
                            '</p>',
                        '</div>'].join('');

                    $('#' + subgrid_id).html(hypothesis + additionalNotes + expectedBusinessValue);
                },
                subGridRowColapsed: function (subgrid_id, row_id) {
                    // this function is called before removing the data 
                    //var subgrid_table_id;
                    //subgrid_table_id = subgrid_id + "_t";
                    //jQuery("#" + subgrid_table_id).remove();
                }
            });

            $('#' + renderTo).jqGrid('filterToolbar', {
                stringResult: false,
                searchOnEnter: false,
                defaultSearch: 'cn'
            });
        }
    };
})();