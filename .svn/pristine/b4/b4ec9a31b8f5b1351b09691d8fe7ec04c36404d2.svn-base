if (typeof (TestAndLearn) == 'undefined') {
    var TestAndLearn = {};
}

if (typeof(TestAndLearn.Home) == 'undefined') {
    TestAndLearn.Home = {};
}

var debug = {};

TestAndLearn.Home.Grids = (function () {
    var _config;
    var _offsetHeight = 280;

    var _sharedColNames = ['Id', 'Date', 'Test Name', 'Submitted By', 'Test Type', 'Success Metric(s)', 'Quarter', 'Year', 'Rank', 'Hypothesis', 'Additional Notes', 'Expected Business Value'];
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

            this.initSubmittedGrid();
            this.initApprovedGrid();
            this.initClosedGrid();
        }

        ,initSubmittedGrid: function () {
            var colNames = _sharedColNames.slice();
                colNames.push('Approve');
            var colModel = _sharedColModel.slice()
                colModel.push({
                name: '',
                index: '',
                search: false,
                width: 80,
                fixed: true,
                formatter: function (cellvalue, options, rowObject) {
                    return '<button class="approve-row btn btn-info" id="approve-row-' + rowObject.TestId + '">approve</button>';
                }
            });

            debug.colNames = colNames;
            debug.colModel = colModel;

            $('#submitted-test-grid').jqGrid({
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
                colNames: colNames,
                colModel: colModel,
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
                        $('#submitted-test-grid .edit-row').unbind('click');

                        $('#submitted-test-grid .edit-row').click(function (e) {
                            var id = $(this).attr('id').replace('edit-row-', '');
                            TestAndLearn.Home.Forms.editTest(id);
                        });

                        $('#submitted-test-grid .approve-row').unbind('click');
                        $('#submitted-test-grid .approve-row').click(function (e) {
                            var id = $(this).attr('id').replace('approve-row-', '');
                            TestAndLearn.Home.Grids.approveTest(id, 2);
                        });

                        $('#submitted-test-grid .edit-rank-row').unbind('click');
                        $('#submitted-test-grid .edit-rank-row').click(function (e) {
                            var id = $(this).attr('id').replace('edit-rank-row-', '');
                            $('#rank-modal').modal('show');
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

            $('#submitted-test-grid').jqGrid('filterToolbar', {
                stringResult: false,
                searchOnEnter: false,
                defaultSearch: 'cn'
            });
        }

        ,initApprovedGrid: function () {
            var colNames = _sharedColNames.slice();
                colNames.push('Close');
            var colModel = _sharedColModel.slice();
                colModel.push({
                name: '',
                index: '',
                search: false,
                width: 80,
                fixed: true,
                formatter: function (cellvalue, options, rowObject) {
                    return '<button class="close-row btn btn-warning" id="close-row-' + rowObject.TestId + '">Close</button>';
                }
            });

            $('#approved-test-grid').jqGrid({
                //data: data,
                url: _config.getTestsUrl,
                postData: {
                    clientId: _config.clientId,
                    testStatusId: _config.approvedStatusId
                },
                datatype: "json",
                mtype: 'GET',
                //width: '50%',
                autowidth: true,
                height: $(document).height() - _offsetHeight,
                //cellsubmit: 'clientArray',
                //editurl: 'clientArray',
                colNames: colNames,
                colModel: colModel,
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
                                'There are no approved tests matching the selceted filters.',
                            '</p>',
                            '</div>'].join(''));

                    }
                    else {
                        $('#approved-test-grid .edit-row').unbind('click');

                        $('#approved-test-grid .edit-row').click(function (e) {
                            var id = $(this).attr('id').replace('edit-row-', '');
                            alert('TODO edit ' + id);
                        });

                        $('#approved-test-grid .close-row').unbind('click');
                        $('#approved-test-grid .close-row').click(function (e) {
                            var id = $(this).attr('id').replace('close-row-', '');
                            $('#input-test-testId').val(id);
                            $('#close-test-modal').modal('show');
                        });

                        $('#approved-test-grid .edit-rank-row').unbind('click');
                        $('#approved-test-grid .edit-rank-row').click(function (e) {
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

            $('#approved-test-grid').jqGrid('filterToolbar', {
                stringResult: false,
                searchOnEnter: false,
                defaultSearch: 'cn'
            });
        }

        ,initClosedGrid: function () {
            var colNames = _sharedColNames.slice();
            colNames.push('Outcome');
            colNames.push('Recommendation');
            var colModel = _sharedColModel.slice();
            colModel.push({ name: 'Outcome', index: 'Outcome', hidden: true });
            colModel.push({ name: 'Recommendation', index: 'Recommendation', hidden: true });

            $('#closed-test-grid').jqGrid({
                //data: data,
                url: _config.getTestsUrl,
                postData: {
                    clientId: _config.clientId,
                    testStatusId: _config.closedStatusId
                },
                datatype: "json",
                mtype: 'GET',
                //width: '50%',
                autowidth: true,
                height: $(document).height() - _offsetHeight,
                //cellsubmit: 'clientArray',
                //editurl: 'clientArray',
                colNames: colNames,
                colModel: colModel,
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
                                'There are no closed tests matching the selected filters.',
                            '</p>',
                            '</div>'].join(''));

                    }
                    else {
                        $('#closed-test-grid .edit-row').unbind('click');

                        $('#closed-test-grid .edit-row').click(function (e) {
                            var id = $(this).attr('id').replace('edit-row-', '');
                            alert('TODO edit ' + id);
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
                        '<div class="bs-callout row">',
                            '<h4>Expected Business Value</h4>',
                            '<p>',
                                row.ExpectedBusinessValue,
                            '</p>',
                        '</div>'].join('');

                    var outcome = [
                        '<div class="bs-callout bs-callout-success row">',
                            '<h4>Outcome</h4>',
                            '<p>',
                                row.Outcome,
                            '</p>',
                        '</div>'].join('');

                    var recommendation = [
                        '<div class="bs-callout bs-callout-warning row">',
                            '<h4>Recommendation</h4>',
                            '<p>',
                                row.Recommendation,
                            '</p>',
                        '</div>'].join('');

                    $('#' + subgrid_id).html(hypothesis + additionalNotes + expectedBusinessValue + outcome + recommendation);
                },
                subGridRowColapsed: function (subgrid_id, row_id) {
                    // this function is called before removing the data 
                    //var subgrid_table_id;
                    //subgrid_table_id = subgrid_id + "_t";
                    //jQuery("#" + subgrid_table_id).remove();
                }
            });

            $('#closed-test-grid').jqGrid('filterToolbar', {
                stringResult: false,
                searchOnEnter: false,
                defaultSearch: 'cn'
            });
        }

        ,approveTest: function (testId) {
            $.ajax({
                url: _config.changeTestStatusUrl,
                type: 'GET',
                data: {
                    testId: testId,
                    statusId: _config.appproveStatusId
                },
                success: function (response) {
                    if (response.Success == true) {
                        JQueryUX.Msg.SlideIn({
                            width: 200,
                            height: 50,
                            msg: response.Message,
                            dockAt: 'tc',
                            wrapperWidthOffset: 8,
                            wrapperHeightOffset: 8
                        });
                        $('#submitted-test-grid').trigger('reloadGrid');
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