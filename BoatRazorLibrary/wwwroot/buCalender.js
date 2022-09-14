
export const DisplayCalendar = async() => {
    try {

        const Calendar = tui.Calendar
        
            const container = document.getElementById('calendar');
       
        const options = {
            defaultView: 'month',
            theme: {
                month: {
                    dayName: {
                        borderLeft: 'none',
                        backgroundColor: 'rgba(51, 51, 51, 0.4)',
                    },
                    moreView: {
                        border: '1px solid grey',
                        boxShadow: '0 2px 6px 0 grey',
                        backgroundColor: 'white',
                        width: 120,
                        height: 100,
                    },
                    weekend: {
                        backgroundColor: 'rgba(255, 64, 64, 0.4)',
                    },
                    gridCell: {
                        footerHeight: 11,
                    },
                },
                timezone: {
                    zones: [
                        {
                            timezoneName: 'America/New_York',
                            displayLabel: 'New_York',
                        },
                        {
                            timezoneName: 'America/Los_Angeles',
                            displayLabel: 'Los_Angeles',
                        },
                    ],
                },
                calendars: [
                    {
                        id: 'cal1',
                        name: 'Personal',
                        backgroundColor: '#03bd9e',
                    },
                    {
                        id: 'cal2',
                        name: 'Work',
                        backgroundColor: '#00a9ff',
                    },
                ],
            }           
        }
        const calendar = new Calendar(container, options);
       
    } catch (e) {
        alert("error" + e)
    }

}


