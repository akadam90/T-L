﻿if (typeof (TestAndLearn) == 'undefined') {
    var TestAndLearn = {};
}

if (typeof (TestAndLearn.Home) == 'undefined') {
    TestAndLearn.Home = {};
}

var debug = {};

TestAndLearn.Home.Calendar = (function () {

    var _config;

    return {
        init: function (config) {
            _config = config;

            $(document).ready(function () {

                $('#calendar').fullCalendar({

                    header: {
                        left: 'prev,next today',
                        center: 'title',
                        right: 'month,basicWeek,basicDay'
                    },
                    height: $(document).height() -280,
                    editable: true,
                    events: {
                        url: _config.getTestsEventsUrl,
                        type: 'GET',
                        data: {
                          client : _config.clientId    
                        },
                        error: function () {
                            alert('there was an error while fetching events!');
                        }
                    },

                    eventClick: function (calEvent, jsEvent, view) {
                        if (calEvent.id.includes("Event")) {
                            var id = calEvent.id.replace('Event', '');
                            TestAndLearn.Home.EventForm.editEvent(id);
                        }
                        else if (calEvent.id.includes("Test")) {
                            var id = calEvent.id.replace('Test', '');
                            TestAndLearn.Home.RunDatesForm.submitRunDates(id);
                        }
                    }
                
                });

            });

        }

    };


})();