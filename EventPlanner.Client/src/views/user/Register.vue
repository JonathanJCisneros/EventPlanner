<template>
    <Title title="Register" />
    <div class="register">
        <div v-if="stateMessage" :class="error">{{ stateMessage.message }}</div>
        <form v-on:submit.prevent="submitRegister">
            <div>
                <label for="firstName">First Name</label>
                <input type="text" id="firstName" name="firstName" v-model="formDetails.firstName" />
                <span v-if="formMessages.hasOwnProperty('FirstName')">{{ formMessages.FirstName }}</span>
            </div>
            <div>
                <label for="lastName">Last Name</label>
                <input type="text" id="lastName" name="lastName" v-model="formDetails.lastName" />
                <span v-if="formMessages.hasOwnProperty('LastName')">{{ formMessages.LastName }}</span>
            </div>
            <div>
                <label for="email">Email</label>
                <input type="text" id="email" name="email" v-model="formDetails.email" />
                <span v-if="formMessages.hasOwnProperty('Email')">{{ formMessages.Email }}</span>
            </div>
            <div>
                <label for="phoneNumber">Phone Number</label>
                <input type="text" id="phoneNumber" name="phoneNumber" v-model="formDetails.phoneNumber" @input="formatNumber" />
                <span v-if="formMessages.hasOwnProperty('PhoneNumber')">{{ formMessages.PhoneNumber }}</span>
            </div>
            <div>
                <label for="password">Password</label>
                <input type="password" id="password" name="password" v-model="formDetails.password" />
                <span v-if="formMessages.hasOwnProperty('Password')">{{ formMessages.Password }}</span>
            </div>
            <div>
                <label for="confirmPassword">Confirm Password</label>
                <input type="password" id="confirmPassword" name="confirmPassword" v-model="formDetails.confirmPassword" />
                <span v-if="formMessages.hasOwnProperty('ConfirmPassword')">{{ formMessages.ConfirmPassword }}</span>
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
    import router from '../../routes/routes.ts';

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
        phoneNumber: string,
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
                    phoneNumber: '',
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
            async submitRegister(): Promise<void> {
                if (this.isFormValid()) {
                    await fetch('https://localhost:7134/api/user/register', {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.formDetails)
                    })
                        .then(async (response: Response): Promise<null | FormResponse> => {
                            if (!response.ok) {
                                return null;
                            }

                            return await response.json();
                        })
                        .then(async (response: null | FormResponse): Promise<void> => {
                            if (!data) {
                                await this.handleFail(attempts);

                                return;
                            }

                            if (!data.success) {
                                this.stateMessage = data.message;

                                return;
                            }

                            router.push('/user/account');
                        })
                        .catch(async (error: Error): Promise<void> => {
                            console.log(error);

                            await this.handleFail(attempts);
                        });
                }
            },
            isFormValid(): boolean {
                let errors: Errors = {};

                const firstNameValidation: ValidationResponse<string> = validations.firstName(this.formDetails.firstName, errors);
                this.formDetails.firstName = firstNameValidation.value;
                errors = firstNameValidation.errors;

                const lastNameValidation: ValidationResponse<string> = validations.lastName(this.formDetails.lastName, errors);
                this.formDetails.lastName = lastNameValidation.value;
                errors = lastNameValidation.errors;

                const emailValidation: ValidationResponse<string> = validations.email(this.formDetails.email, errors);
                this.formDetails.email = emailValidation.value;
                errors = emailValidation.errors;

                const phoneNumberValidation: ValidationResponse<string> = vallidations.phoneNumber(this.formDetails.phoneNumber, errors);
                this.formDetails.phoneNumber = phoneNumberValidation.value;
                errors = phoneNumberValidation.errors;

                const passwordValidation: ValidationResponse<string> = validations.password(this.formDetails.password, errors);
                this.formDetails.password = passwordValidation.value;
                errors = passwordValidation.errors;

                const confirmPasswordValidation: ValidationResponse<string> = validations.confirmPassword(this.formDetails.password, this.formDetails.confirmPassword, errors);
                this.formDetails.confirmPassword = confirmPasswordValidation.value;
                errors = confirmPasswordValidation.errors;

                if (Object.keys(errors).length > 0) {
                    this.formMessages = errors;

                    return false;
                }

                return true;
            },
            async handleFail(attempts: number): Promise<void> {
                attempts++;

                if (attempts < 2) {
                    await this.submitRegister(attempts);

                    return;
                }

                this.stateMessage = {
                    success: false,
                    message: 'Oops, we are having trouble registering. Please try again later!'
                };
            },
            formatNumber(): void {
                if (this.formDetails.phoneNumber) {
                    const x: string[] = this.formDetails.phoneNumber.replace(/\D/g, '').match(/(\d{0,3})(\d{0,3})(\d{0,4})/);
                    this.formDetails.phoneNumber = !x[2] ? x[1] : '(' + x[1] + ') ' + x[2] + (x[3] ? '-' + x[3] : '');
                }                
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
