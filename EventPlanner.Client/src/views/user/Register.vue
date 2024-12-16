<template>
    <Title title="Register" />
    <div class="register">
        <div v-if="stateMessage" :class="stateMessage.success ? 'successful' : 'error'">
            {{ stateMessage.message }}
        </div>
        <form v-on:submit.prevent="submitRegister">
            <div>
                <label for="firstName">First Name</label>
                <input type="text" id="firstName" name="firstName" v-model="formDetails.firstName" />
                <span v-if="formMessages.hasOwnProperty('firstName')">{{ formMessages.firstName }}</span>
            </div>
            <div>
                <label for="lastName">Last Name</label>
                <input type="text" id="lastName" name="lastName" v-model="formDetails.lastName" />
                <span v-if="formMessages.hasOwnProperty('lastName')">{{ formMessages.lastName }}</span>
            </div>
            <div>
                <label for="email">Email</label>
                <input type="text" id="email" name="email" v-model="formDetails.email" />
                <span v-if="formMessages.hasOwnProperty('email')">{{ formMessages.email }}</span>
            </div>
            <div>
                <label for="password">Password</label>
                <input type="password" id="password" name="password" v-model="formDetails.password" />
                <span v-if="formMessages.hasOwnProperty('password')">{{ formMessages.password }}</span>
            </div>
            <div>
                <label for="confirmPassword">Confirm Password</label>
                <input type="password" id="confirmPassword" name="confirmPassword" v-model="formDetails.confirmPassword" />
                <span v-if="formMessages.hasOwnProperty('confirmPassword')">{{ formMessages.confirmPassword }}</span>
            </div>
            <div>
                <button type="submit" class="button success">Register</button>
            </div>
        </form>
        <div class="or-separator">Or</div>
        <RouterLink class="button primary" to="/user/login">Login</RouterLink>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import { RouterLink } from 'vue-router';
    import Title from '../../components/Title.vue';

    import type {
        FormResponse,
        Errors,
        ValidationResponse
    } from '../../assets/js/types.ts';

    import validations from '../../assets/js/validations.ts';

    type FormDetails = {
        firstName: string,
        lastName: string,
        email: string,
        password: string,
        confirmPassword: string
    }

    interface Data {
        formDetails: FormDetails,
        formMessages: Errors,
        stateMessage: null | FormResponse
    }

    export default defineComponent({
        data(): Data {
            return {
                formDetails: {
                    firstName: '',
                    lastName: '',
                    email: '',
                    password: '',
                    confirmPassword: ''
                },
                formMessages: {},
                stateMessage: null
            };
        },
        components: {
            Title,
            RouterLink
        },
        methods: {
            async submitRegister(): void {
                if (this.isFormValid()) {
                    await fetch('https://localhost:7134/api/user/register', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.formDetails)
                    })
                        .then(async (response: Response): FormResponse => {
                            if (!response.ok) {
                                return this.stateError();
                            }

                            return await response.json();
                        })
                        .then((response: FormResponse): void => {
                            this.stateMessage = response;
                        })
                        .catch((error: Error): void => {
                            console.log(error);

                            this.stateMessage = this.stateError();
                        });
                }
            },
            isFormValid(): boolean {
                let errors: Errors = {};

                let validationResponse: ValidationResponse = validations.firstName(this.formDetails.firstName, errors);

                this.formDetails.firstName = validationResponse.value;
                errors = validationResponse.errors;

                validationResponse = validations.lastName(this.formDetails.lastName, errors);

                this.formDetails.lastName = validationResponse.value;
                errors = validationResponse.errors;

                validationResponse = validations.email(this.formDetails.email, errors);

                this.formDetails.email = validationResponse.value;
                errors = validationResponse.errors;

                validationResponse = validations.password(this.formDetails.password, errors);

                this.formDetails.password = validationResponse.value;
                errors = validationResponse.errors;

                validationResponse = validations.confirmPassword(this.formDetails.password, this.formDetails.confirmPassword, errors);

                this.formDetails.confirmPassword = validationResponse.value;
                errors = validationResponse.errors;

                if (Object.keys(errors).length > 0) {
                    this.formMessages = errors;

                    return false;
                }

                return true;
            },
            stateError(): FormResponse {
                return {
                    success: false,
                    message: 'Oops, we are having trouble registering. Please try again later!'
                };
            }
        }
    });
</script>

<style scoped>
    .register {
        display: flex;
        align-items: center;
        flex-direction: column;
        gap: 15px;
        margin: 0 auto 25px auto;
        max-width: 450px;
        padding: 25px;
        border: 1px solid #c5c5c5;
        box-shadow: 0px 0px 15px rgba(0, 0, 0, 0.4);
    }

    form {
        width: 100%;
        display: flex;
        align-items: center;
        flex-direction: column;
        gap: 15px;
    }

        form > div {
            display: flex;
            align-items: center;
            flex-direction: column;
            gap: 4px;
        }

            form > div > span {
                color: #dc3545;
                font-size: 0.8rem;
                font-weight: 600;
            }

    label {
        font-weight: 600;
    }

    input {
        padding: .375rem .75rem;
        font-size: 1rem;
        font-weight: 400;
        line-height: 1.5;
        color: #212529;
        background-color: #fff;
        background-clip: padding-box;
        border: 1px solid #ced4da;
        -webkit-appearance: none;
        -moz-appearance: none;
        appearance: none;
        border-radius: .25rem;
        text-align: center;
        width: 100%;
        min-width: 300px;
        max-width: 380px;
    }

    button {
        font-size: 1.15rem;
    }

    a {
        text-decoration-line: none;
    }

        a:hover {
            text-decoration-line: none;
        }
</style>
