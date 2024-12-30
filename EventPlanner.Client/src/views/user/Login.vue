<template>
    <Title title="Login" />
    <div class="login">
        <div v-if="stateMessage" class="error">{{ stateMessage.message }}</div>
        <form v-on:submit.prevent="submitLogin">
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
                <button type="submit" class="button success">Login</button>
            </div>
        </form>
        <div class="or-separator">Or</div>
        <RouterLink class="button primary" to="/user/register">Register</RouterLink>
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
        email: string,
        password: string
    };

    interface Data {
        formDetails: FormDetails,
        formMessages: Errors,
        stateMessage: null | FormResponse
    };

    export default defineComponent({
        data(): Data {
            return {
                formDetails: {
                    email: '',
                    password: ''
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
            async submitLogin(): Promise<void> {
                if (this.isFormValid()) {
                    await fetch('https://localhost:7134/api/user/login', {
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
                        .then(async (data: null | FormResponse): Promise<void> => {
                            if (!data) {
                                await this.handleFail(attempts);

                                return;
                            }

                            if (!data.success) {
                                this.stateMessage = data;

                                return;
                            }

                            this.formDetails = {
                                email: '',
                                password: ''
                            };

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

                const emailValidation: ValidationResponse<string> = validations.email(this.formDetails.email, errors);
                this.formDetails.email = emailValidation.value;
                errors = emailValidation.errors;

                const passwordValidation: ValidationResponse<string> = validations.password(this.formDetails.password, errors);
                this.formDetails.password = passwordValidation.value;
                errors = passwordValidation.errors;

                if (Object.keys(errors).length > 0) {
                    this.formMessages = errors;

                    return false;
                }

                return true;
            },
            async handleFail(attempts: number): Promise<void> {
                attempts++;

                if (attempts < 2) {
                    await this.submitLogin(attempts);

                    return;
                }

                this.stateMessage = {
                    success: false,
                    message: 'Oops, we are having trouble logging you in. Please try again later!'
                };
            }
        }
    });
</script>

<style scoped>
    .login {
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
