if (typeof (TestAndLearn) == 'undefined') {
    var TestAndLearn = {};
}

TestAndLearn.IndexControls = (function () {
    var _config;
    var _offsetHeight = 280;

    var _testData = [
        {
            SubmittedOn: '05/01/2015',
            Name: 'Coupon Code Test',
            Submitter: 'Akshada',
            Agency: 'Kraft',
            Type: 'Media',
            SuccessMetrics: ['Impressions', 'Cost Per Line'],
            TestingQuarter: 'Q1',
            Year : 2015,
            Rank: 1,
            Hypothesis: 'An initial analysis indicates that people who presented a coupon code are more likely to purchase.',
            AdditionalNotes: '',
            ExpectedBusinessValue: 'More $$$'
            
        },
        {
            SubmittedOn: '05/25/2015',
            Name: 'Coupon Code Test 25%',
            Submitter: 'Akshada',
            Agency:'Kraft',
            Type: 'Media',
            SuccessMetrics: ['Impressions', 'Cost Per Line'],
            TestingQuarter: 'Q1',
            Year:2015,
            Rank: 2,
            Hypothesis: 'An initial analysis indicates that people who presented a coupon code are more likely to purchase if it is 25% or higher.',
            AdditionalNotes: '',
            ExpectedBusinessValue: 'More $$$'
            
        },
        {
            SubmittedOn: '06/01/2015',
            Name: 'Coupon Code Test BOGO',
            Submitter: 'Akshada',
            Agency: 'Kraft',
            Type: 'Media',
            SuccessMetrics: ['Impressions', 'Cost Per Line'],
            TestingQuarter: 'Q1',
            Year: 2015,
            Rank: 1,
            Hypothesis: 'An initial analysis indicates that people who presented a coupon code are more likely to purchase if it is buy one get one free.',
            AdditionalNotes: '',
            ExpectedBusinessValue: 'More $$$'
        },
        {
            SubmittedOn: '06/01/2015',
            Name: 'Another Test',
            Submitter: 'Akshada',
            Agency: 'Kraft',
            Type: 'Media',
            SuccessMetrics: ['Impressions', 'Cost Per Line'],
            TestingQuarter: 'Q1',
            Year: 2015,
            Rank: 1,
            Hypothesis: 'An initial analysis indicates that people who presented a coupon code are more likely to purchase if it is buy one get one free.An initial analysis indicates that people who presented a coupon code are more likely to purchase if it is buy one get one free.An initial analysis indicates that people who presented a coupon code are more likely to purchase if it is buy one get one free.An initial analysis indicates that people who presented a coupon code are more likely to purchase if it is buy one get one free.An initial analysis indicates that people who presented a coupon code are more likely to purchase if it is buy one get one free.An initial analysis indicates that people who presented a coupon code are more likely to purchase if it is buy one get one free.An initial analysis indicates that people who presented a coupon code are more likely to purchase if it is buy one get one free.An initial analysis indicates that people who presented a coupon code are more likely to purchase if it is buy one get one free.',
            AdditionalNotes: '',
            ExpectedBusinessValue: 'More $$$'
        }
        
    ];

    var _sharedColNames = ['Date', 'Test Name', 'Submitted By', 'Agency', 'Test Type', 'Success Metric(s)', 'Quarter', 'Year', 'Rank', 'Hypothesis', 'Additional Notes', 'Expected Business Value'];
    var _sharedColModel = [
                    {
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
                            return '<a href="javascript:void(0)" class="edit-row" id="edit-row-' + obj.Id + '">' + cellvalue + ' </a>';
                        }
                    },
                    {
                        name: 'Submitter',
                        index: 'Submitter',
                        sorttype:"text",
                        search: true,
                        searchoptions: {
                            clearSearch:false
                        },
                        stype:'text',

                    },
                    {
                        name: 'Agency',
                        index: 'Agency',
                        sorttype: 'text',
                        search: true,
                        searchoptions: {
                            clearSearch:false,
                        },
                        stype: 'text',

                    },
                    {
                        name: 'Type',
                        index: 'Type',
                        width: 80,
                        fixed: true,
                        search: true,
                        stype: 'select',
                        searchoptions: { value: ":All;Media:Media;Creative:Creative;Website:Website", clearSearch: false }
                    },
                    {
                        name: 'SuccessMetrics',
                        index: 'SuccessMetrics',
                        sorttype: "string",
                        search: true,
                        searchoptions: {
                            clearSearch: false
                        },
                        stype: 'text'
                    },
                 
                    {
                        name: 'TestingQuarter',
                        index: 'TestingQuarter',
                        sorttype: "string",
                        search: true,
                        searchoptions: {
                            clearSearch: false
                        },
                        stype: 'text',
                        width: 80,
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
                        stype:'text'
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
                        width: 80,
                        fixed: true
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

                    /*{
                        name: '',
                        index: '',
                        search: false,
                        width: 80,
                        fixed: true,
                        formatter: function (cellvalue, options, rowObject) {
                            return '<button class="approve-row btn btn-info" id="approve-row-' + rowObject.Id + '">approve</button>';
                        }
                    },*/

                    {
                        name: 'ExpectedBusinessValue',
                        index: 'ExpectedBusinessValue',
                        sorttype: "string",
                        search: true,
                        searchoptions: {
                            clearSearch: false
                        },
                        stype: 'text'
                    }
    ];

    return {
        init: function (config) {
            _config = config;

            this.initGrid('submitted-test-grid', _testData);
            this.initGrid('approved-test-grid', _testData);
            this.initGrid('closed-test-grid', []);
        },

        initGrid: function (renderTo, data) {
            $('#' + renderTo).jqGrid({
                data: data,
                datatype: "local",
                width: '50%',
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

                    if (data.rows.length == 0) {
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
                            alert('TODO edit ' + id)
                        });

                        $('#' + renderTo + ' .approve-row').unbind('click');
                        $('#' + renderTo + ' .approve-row').click(function (e) {
                            var id = $(this).attr('id').replace('approve-row-', '');
                            alert('TODO approve ' + id)
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

                    $('#' + subgrid_id).html(hypothesis + additionalNotes);
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