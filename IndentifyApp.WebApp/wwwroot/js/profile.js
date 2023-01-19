var profileForm = new Vue({
    el: '#profile-form',
    data: {
        lastName: '',
        firstName: '',
        middleName: '',
        inn: '',
        passportSeries: '',
        passportNumber: '',
        phone: '',
        city: '',
        street: '',
        house: '',
        address: '',
        position: '',
        identificationStatus: '',
        errors: [],
        HUB_CONNECTION: '',
        profileStatus: 'В процессе',
        showProfileStatus: false
    },
    created: function () {
        this.HUB_CONNECTION = new signalR.HubConnectionBuilder()
            .withUrl("/identifyProfile", {
                skipNegotiation: false,
                transport: signalR.HttpTransportType.WebSockets
            })
            .build();
    },
    mounted: function () {
        
        let vue = this;
        
        vue.HUB_CONNECTION.start();

        // подписка по signalR на получение статуса идентификации профиля
        vue.HUB_CONNECTION.on("Receive", function (status) {
            vue.showProfileStatus = true;
            vue.profileStatus = status;
        });
    },
    methods: {
        idetify: function () {

            if (!this.validateBeforeSubmit())
                return;

            var identifyModel = {
                lastName: this.lastName,
                firstName: this.firstName,
                middleName: this.middleName,
                inn: this.inn,
                passportSeries: this.passportSeries,
                passportNumber: this.passportNumber,
                phone: this.phone,
                address: this.city + ", " + this.street + ", " + this.house,
                position: this.position
            };

            // Отправка данных профиля по signalR
            this.HUB_CONNECTION
                .invoke("Send", identifyModel)
                .catch(function (err) {
                    return console.error(err.toSting());
                });

        },
        validateBeforeSubmit: function () {
            this.errors = [];

            if (!this.lastName) {
                this.errors.push('Не заполнено поле "Фамилия".');
            }
            else if (!this.isLetter(this.lastName)) {
                this.errors.push('Поле "Фамилия" должно содержать только буквы.');
            }

            if (!this.firstName) {
                this.errors.push('Не заполнено поле "Имя".');
            }
            else if (!this.isLetter(this.firstName)) {
                this.errors.push('Поле "Имя" должно содержать только буквы.');
            }

            if (!this.middleName) {
                this.errors.push('Не заполнено поле "Отчество".');
            }
            else if (!this.isLetter(this.middleName)) {
                this.errors.push('Поле "Отчество" должно содержать только буквы.');
            }

            if (!this.inn) {
                this.errors.push('Не заполнено поле "ИНН".');
            }
            else if (!this.isNumber(this.inn)) {
                this.errors.push('Поле "ИНН" должно содержать только цифры.');
            }

            if (!this.passportSeries) {
                this.errors.push('Не заполнено поле "Серия паспорта".');
            }
            else if (!this.isNumber(this.passportSeries)) {
                this.errors.push('Поле "Серия паспорта" должно содержать только цифры.');
            }

            if (!this.passportNumber) {
                this.errors.push('Не заполнено поле "Номер паспорта".');
            }
            else if (!this.isNumber(this.passportNumber)) {
                this.errors.push('Поле "Номер паспорта" должно содержать только цифры.');
            }

            if (!this.phone) {
                this.errors.push('Не заполнено поле "Телефон".');
            }

            if (!this.city) {
                this.errors.push('Не заполнено поле "Город".');
            }

            if (!this.street) {
                this.errors.push('Не заполнено поле "Улица".');
            }

            if (!this.house) {
                this.errors.push('Не заполнено поле "Дом".');
            }
            else if (!this.isNumber(this.house)) {
                this.errors.push('Поле "Дом" должно содержать только цифры.');
            }

            if (!this.position) {
                this.errors.push('Не заполнено поле "Должность".');
            }

            if (!this.errors.length)
                return true;

            return false;
        },
        isLetter: function (str) {

            if (str.length == 0)
                return false;

            var pattern = /^[a-zA-Zа-яА-Я]+$/;

            return pattern.test(str);
        },
        isNumber: function (str) {

            var reg = /^\d+$/;

            return reg.test(str);
        }

    }

});