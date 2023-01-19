
var registerForm = new Vue({
    el: '#register-form',
    data: {
        login: '',
        password: '',
        passwordConfirm: '',
        errors: []
    },
    methods: {
        register: async function () {

            if (!this.validateBeforeSubmit())
                return;

            var reqModel = {
                login: this.login,
                password: this.password,
                passwordConfirm: this.passwordConfirm
            };

            await axios.post('/api/account/register', reqModel)
                .then(function (response) {

                    if (response.data.isSuccess) {
                        window.location.href = '/home/profile/' + response.data.id;
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });


        },
        validateBeforeSubmit: function () {
            this.errors = [];

            if (!this.login) {
                this.errors.push('Login required.');
            }
            if (!this.password) {
                this.errors.push('Password required.');
            }
            if (!this.passwordConfirm) {
                this.errors.push('Password confirmation required.');
            }
            if (this.password != this.passwordConfirm) {
                this.errors.push('Passwords do not match.');
            }

            if (!this.errors.length)
                return true;

            return false;
        }
    }

});

