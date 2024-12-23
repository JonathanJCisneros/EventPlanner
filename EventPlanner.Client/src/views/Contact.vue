<template>
    <Title title="Contact Us" />
    <div class="container">
        <section>
            <h3>Give us a call!</h3>
            <h6>                
                <a href="tel:4251234567">
                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24">
                        <path d="M21 16.42V19.9561C21 20.4811 20.5941 20.9167 20.0705 20.9537C19.6331 20.9846 19.2763 21 19 21C10.1634 21 3 13.8366 3 5C3 4.72371 3.01545 4.36687 3.04635 3.9295C3.08337 3.40588 3.51894 3 4.04386 3H7.5801C7.83678 3 8.05176 3.19442 8.07753 3.4498C8.10067 3.67907 8.12218 3.86314 8.14207 4.00202C8.34435 5.41472 8.75753 6.75936 9.3487 8.00303C9.44359 8.20265 9.38171 8.44159 9.20185 8.57006L7.04355 10.1118C8.35752 13.1811 10.8189 15.6425 13.8882 16.9565L15.4271 14.8019C15.5572 14.6199 15.799 14.5573 16.001 14.6532C17.2446 15.2439 18.5891 15.6566 20.0016 15.8584C20.1396 15.8782 20.3225 15.8995 20.5502 15.9225C20.8056 15.9483 21 16.1633 21 16.42Z"></path>
                    </svg>
                    <span>(425) 123-4567</span>
                </a>
            </h6>
        </section>
        <h4>Or</h4>
        <section class="form">
            <h3>Leave us a message!</h3>
            <form v-on:submit.prevent="submitInquiry">
                <div>
                    <label for="name">Name</label>
                    <input type="text" id="name" name="name" v-model="formDetails.name" />
                    <span v-if="formMessages.hasOwnProperty('Name')">{{ formMessages.Name }}</span>
                </div>
                <div>
                    <label for="email">Email</label>
                    <input type="text" id="email" name="email" v-model="formDetails.email" />
                    <span v-if="formMessages.hasOwnProperty('Email')">{{ formMessages.Email }}</span>
                </div>
                <div>
                    <label for="subject">Subject</label>
                    <select id="subject" name="subject" v-model.number="formDetails.subject">
                        <option value="-1" selected hidden>Please Select...</option>
                        <option value="0">Question</option>
                        <option value="1">Special Event</option>
                        <option value="2">Partnership</option>
                        <option value="3">Ideas</option>
                    </select>
                    <span v-if="formMessages.hasOwnProperty('Subject')">{{ formMessages.Subject }}</span>
                </div>
                <div>
                    <label for="message">Message</label>
                    <textarea id="message" name="message" rows="4" cols="50" v-model="formDetails.message"></textarea>
                    <span v-if="formMessages.hasOwnProperty('Message')">{{ formMessages.Message }}</span>
                </div>
                <div>
                    <button type="submit" class="button success">Submit</button>
                </div>
                <div v-if="stateMessage" :class="stateMessage.success ? 'successful' : 'error'">
                    {{ stateMessage.message }}
                </div>
            </form>
        </section>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import Title from '../components/Title.vue';

    import type {
        FormResponse,
        Errors,
        ValidationResponse
    } from '../assets/js/types.ts';

    import validations from '../assets/js/validations.ts';

    type FormDetails = {
        name: string,
        email: string,
        subject: number,
        message: string
    }

    interface Data {
        loading: boolean,
        formDetails: FormDetails,
        formMessages: Errors,
        stateMessage: null | FormResponse
    }     

    export default defineComponent({
        data(): Data {
            return {
                loading: true,
                formDetails: {
                    name: '',
                    email: '',
                    subject: -1,
                    message: ''
                },
                formMessages: {},
                stateMessage: null
            };
        },
        components: {
            Title
        },
        methods: {
            async submitInquiry(attempts: number = 0): Promise<void> {
                if (this.isFormValid()) {
                    await fetch('https://localhost:7134/api/contact/submitcontact', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.formDetails)
                    })
                        .then(async (response: Response): Promise<FormResponse> => {
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

                            this.stateMessage = data;
                        })
                        .catch(async (error: Error): Promise<void> => {
                            console.log(error);

                            await this.handleFail(attempts);
                        });                    
                }
            },
            isFormValid(): boolean {
                let errors: Errors = {};

                const nameValidation: ValidationResponse<string> = validations.name(this.formDetails.name, errors);
                this.formDetails.name = nameValidation.value;
                errors = nameValidation.errors;

                const emailValidation: ValidationResponse<string> = validations.email(this.formDetails.email, errors);
                this.formDetails.email = emailValidation.value;
                errors = emailValidation.errors;

                const subjectValidation: ValidationResponse<number> = validations.subject(this.formDetails.subject, errors);
                this.formDetails.subject = subjectValidation.value;
                errors = subjectValidation.errors;

                const messageValidation: ValidationResponse<string> = validations.message(this.formDetails.message, errors);
                this.formDetails.message = messageValidation.value;
                errors = messageValidation.errors;

                if (Object.keys(errors).length > 0) {                    
                    this.formMessages = errors;

                    return false;
                }

                return true;
            },
            async handleFail(attempts: number): Promise<void> {
                attempts++;

                if (attempts < 2) {
                    await this.submitInquiry(attempts);

                    return;
                }

                this.stateMessage = {
                    success: false,
                    message: 'Oops, we are having trouble submitting your message. Please try again later!'
                };
            }
        }
    });
</script>

<style scoped>
    .container {
        text-align: center;
    }

    section {
        margin: 0 auto;
        padding: 15px;
    }

    h3 {
        font-size: 1.5rem;
    }

    h6 {
        display: flex;
        align-items: center;
        justify-content: center;
        margin: 0 auto;
    }

        h6 > a {
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 7px;
            width: 300px;
            padding: 10px;
            border: 2px solid #198754;
            color: white;
            background-color: #198754;
            border-radius: 75px;
            transition: all 250ms ease-in-out;
            text-decoration-line: none;
        }

            h6 > a:hover {
                color: #198754;
                background-color: white;
            }

                h6 > a:hover > svg {
                    fill: #198754;
                }

            h6 > a > svg {
                height: 35px;
                width: 40px;
                fill: white;
            }

            h6 > a > span {
                font-size: 1.5rem;
                text-decoration-line: none;
            }

    h4 {
        font-size: 1.3rem;
    }

    section.form {
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

    input, select, textarea {
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
    }

    input, textarea {
        text-align: center;
        width: 100%;
        min-width: 300px;
        max-width: 380px;
    }

    button {
        font-size: 1.15rem;
    }
</style>
