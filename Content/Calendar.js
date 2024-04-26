const day = new DayPilot.Calendar("day", {
    viewType: "Day",
    visible: false,
    eventDeleteHandling: "Update",
    onTimeRangeSelected: (args) => {
        app.createAppointmentSlot(args);
    },
    onEventMoved: (args) => {
        app.moveAppointmentSlot(args);
    },
    onEventResized: (args) => {
        app.moveAppointmentSlot(args);
    },
    onEventDeleted: (args) => {
        app.deleteAppointmentSlot(args);
    },
    onBeforeEventRender: (args) => {
        app.renderAppointmentSlot(args);
    },
    onEventClick: (args) => {
        app.editAppointmentSlot(args);
    }
});
day.init();

const week = new DayPilot.Calendar("week", {
    viewType: "Week",
    eventDeleteHandling: "Update",
    onTimeRangeSelected: (args) => {
        app.createAppointmentSlot(args);
    },
    onEventMoved: (args) => {
        app.moveAppointmentSlot(args);
    },
    onEventResized: (args) => {
        app.moveAppointmentSlot(args);
    },
    onEventDeleted: (args) => {
        app.deleteAppointmentSlot(args);
    },
    onBeforeEventRender: (args) => {
        app.renderAppointmentSlot(args);
    },
    onEventClick: (args) => {
        app.editAppointmentSlot(args);
    }
});
week.init();

const month = new DayPilot.Month("month", {
    visible: false,
    eventDeleteHandling: "Update",
    eventMoveHandling: "Disabled",
    eventResizeHandling: "Disabled",
    cellHeight: 150,
    onCellHeaderClick: args => {
        nav.selectMode = "Day";
        nav.select(args.start);
    },
    onEventDelete: args => {
        app.deleteAppointmentSlot(args);
    },
    onBeforeEventRender: args => {
        app.renderAppointmentSlot(args);

        const locale = DayPilot.Locale.find(month.locale);
        const start = new DayPilot.Date(args.data.start).toString(locale.timePattern);
        const name = DayPilot.Util.escapeHtml(args.data.patientName || "");
        args.data.html = `<span class='month-time'>${start}</span> ${name}`;
    },
    onTimeRangeSelected: async (args) => {
        const params = {
            start: args.start.toString(),
            end: args.end.toString(),
            weekends: true
        };

        args.control.clearSelection();

        const { data } = await DayPilot.Http.post("/api/appointments/create", params);
        app.loadEvents();
    },
    onEventClick: (args) => {
        app.editAppointmentSlot(args);
    }
});
month.init();

const nav = new DayPilot.Navigator("nav", {
    selectMode: "Week",
    showMonths: 3,
    skipMonths: 3,
    onTimeRangeSelected: (args) => {
        app.loadEvents(args.day);
    }
});
nav.init();

const app = {
    async loadEvents(date) {
        const start = nav.visibleStart();
        const end = nav.visibleEnd();

        const { data } = await DayPilot.Http.get(`/api/appointments?start=${start}&end=${end}`);

        const options = {
            visible: true,
            events: data
        };

        if (date) {
            options.startDate = date;
        }

        day.hide();
        week.hide();
        month.hide();

        const active = app.active();
        active.update(options);

        nav.update({
            events: data
        });
    },
    // ...
};