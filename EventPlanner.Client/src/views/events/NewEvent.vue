<template>
    <Title title="Create an Event" />
    <div class="new-event">
        <div v-if="stateMessage" :class="error">{{ stateMessage.message }}</div>
        <form v-on:submit.prevent="submitEvent">
            <div>
                <label for="name">Name</label>
                <input type="text" id="name" name="name" v-model="formDetails.name" />
                <span v-if="formMessages.hasOwnProperty('Name')">{{ formMessages.Name }}</span>
            </div>
            <div>
                <label for="type">Event Type</label>
                <select id="type" name="type" v-model="formDetails.type">
                    <option value="-1" selected hidden>Please Select</option>
                    <option value="0">Wedding</option>
                    <option value="1">Birthday</option>
                    <option value="2">Business</option>
                </select>
                <span v-if="formMessages.hasOwnProperty('Type')">{{ formMessages.Type }}</span>
            </div>
            <div>
                <label for="description">Description</label>
                <textarea type="text" id="description" name="description" v-model="formDetails.description"></textarea>
                <span v-if="formMessages.hasOwnProperty('Description')">{{ formMessages.Description }}</span>
            </div>
            <div>
                <label for="website">Website</label>
                <input type="text" id="website" name="website" v-model="formDetails.website" />
                <span v-if="formMessages.hasOwnProperty('Website')">{{ formMessages.Website }}</span>
            </div>
            <div>
                <div class="checkbox">
                    <input type="checkbox" id="isPublic" name="isPublic" v-model="formDetails.isPublic" />
                    <label for="isPublic">Public Event</label>
                </div>
                <span v-if="formMessages.hasOwnProperty('IsPublic')">{{ formMessages.IsPublic }}</span>
            </div>
            <div>
                <div class="checkbox">
                    <input type="checkbox" id="isDigital" name="isDigital" v-model="formDetails.isDigital" />
                    <label for="isDigital">Digital Event</label>
                </div>
                <span v-if="formMessages.hasOwnProperty('IsDigital')">{{ formMessages.IsDigital }}</span>
            </div>
            <div>
                <label for="guestMax">Guest Max</label>
                <input type="number" id="guestMax" name="guestMax" v-model="formDetails.guestMax" />
                <span v-if="formMessages.hasOwnProperty('GuestMax')">{{ formMessages.GuestMax }}</span>
            </div>
            <div>
                <label for="startDate">Start Date</label>
                <input type="datetime-local"
                       id="startDate"
                       name="startDate"
                       v-model="formDetails.startDate" />
                <span v-if="formMessages.hasOwnProperty('StartDate')">{{ formMessages.StartDate }}</span>
            </div>
            <div>
                <label for="endDate">End Date</label>
                <input type="datetime-local"
                       id="endDate"
                       name="endDate"
                       v-model="formDetails.endDate" />
                <span v-if="formMessages.hasOwnProperty('EndDate')">{{ formMessages.EndDate }}</span>
            </div>
            <div>
                <div class="checkbox">
                    <input type="checkbox" id="hasLocation" name="hasLocation" :checked="hasLocation" v-on:change="toggleLocation" />
                    <label for="hasLocation">Has Location?</label>
                </div>
            </div>
            <div class="address" v-if="hasLocation">
                <div>
                    <label for="country">Country</label>
                    <select id="country" name="country" v-model="formDetails.address.country" v-on:change="toggleStates">
                        <option value="" selected hidden>Please Select</option>
                        <option v-for="country in countries" :key="country.code" :value="country.code">{{ country.name }}</option>
                    </select>
                    <span v-if="formMessages.hasOwnProperty('Country')">{{ formMessages.Country }}</span>
                </div>
                <div>
                    <label for="streetLineOne">Street Line One</label>
                    <input type="text" id="streetLineOne" name="streetLineOne" v-model="formDetails.address.streetLineOne" />
                    <span v-if="formMessages.hasOwnProperty('StreetLineOne')">{{ formMessages.StreetLineOne }}</span>
                </div>
                <div>
                    <label for="streetLineTwo">Street Line Two</label>
                    <input type="text" id="streetLineTwo" name="streetLineTwo" v-model="formDetails.address.streetLineTwo" />
                </div>
                <div>
                    <label for="streetLineThree">Street Line Three</label>
                    <input type="text" id="streetLineThree" name="streetLineThree" v-model="formDetails.address.streetLineThree" />
                </div>
                <div>
                    <label for="city">City</label>
                    <input type="text" id="city" name="city" v-model="formDetails.address.city" />
                    <span v-if="formMessages.hasOwnProperty('City')">{{ formMessages.City }}</span>
                </div>
                <div>
                    <label for="state">State</label>
                    <select id="state" name="state" v-model="formDetails.address.state">
                        <option value="" selected hidden>
                            {{
                                formDetails.address.country === '' ?
                                    'Please Select Country before State':
                                formDetails.address.state === '' ?
                                    'Please Select': 'Please Select'
                            }}
                        </option>
                        <option v-for="state in states" :key="state.code" :value="state.code">{{ state.name }}</option>
                    </select>
                    <span v-if="formMessages.hasOwnProperty('State')">{{ formMessages.State }}</span>
                </div>
                <div>
                    <label for="postalCode">Postal Code</label>
                    <input type="text" id="postalCode" name="postalCode" v-model="formDetails.address.postalCode" />
                    <span v-if="formMessages.hasOwnProperty('PostalCode')">{{ formMessages.PostalCode }}</span>
                </div>
            </div>
            <div>
                <button type="submit" class="button success" :class="{ 'disabled': submittingEvent }">
                    <template v-if="!submittingEvent">
                        Submit
                    </template>
                    <template v-else>
                        <span class="loader"></span>
                    </template>
                </button>
            </div>
        </form>
    </div>
</template>

<script lang="ts">
    import { defineComponent } from 'vue';
    import Title from '../../components/Title.vue';

    import type {
        FormResponse,
        ServerValidationResponse
    } from '../../assets/js/shared-types.ts';

    import {
        type State,
        type Country,
        countries
    } from '../../assets/js/geography.ts';

    type Address = {
        streetLineOne: string,
        streetLineTwo: null | string,
        streetLineThree: null | string,
        city: string,
        state: string,
        postalCode: string,
        country: string,
        phoneNumber: string,
        email: string
    };

    type FormDetails = {
        name: string,
        type: number,
        description: string,
        website: null | string,
        isPublic: boolean,
        isDigital: boolean,
        guestMax: number,
        startDate: Date,
        endDate: Date,
        address: null | Address
    };

    type Errors = {
        [key: string]: undefined | string,
        Name?: undefined | string,
        Type?: undefined | string,
        Description?: undefined | string,
        Website?: undefined | string,
        IsPublic?: undefined | string,
        IsDigital?: undefined | string,
        GuestMax?: undefined | string,
        StartDate?: undefined | string,
        EndDate?: undefined | string,
        StreetLineOne?: undefined | string,
        City?: undefined | string,
        State?: undefined | string,
        PostalCode?: undefined | string,
        Country?: undefined | string,
        PhoneNumber?: undefined | string,
        Email?: undefined | string
    };

    interface Data {
        formDetails: FormDetails,
        formMessages: Errors,
        countries: Country[],
        states: State[],
        stateMessage: FormResponse,
        hasLocation: boolean,
        submittingEvent: boolean
    }

    export default defineComponent({
        data(): Data {
            return {
                formDetails: {
                    name: '',
                    type: 0,
                    description: '',
                    website: null,
                    isPublic: false,
                    isDigital: false,
                    guestMax: 0,
                    startDate: new Date(),
                    endDate: new Date(),
                    address: null
                },
                formMessages: {},
                countries: countries,
                states: [],
                stateMessage: null,
                hasLocation: false,
                submittingEvent: false
            };
        },
        components: {
            Title
        },
        methods: {
            async submitEvent(attempts: number = 0): Promise<void> {
                if (Object.keys(this.formMessages).length > 0) {
                    this.formMessages = {} as Errors;
                }                

                if (this.isFormValid()) {
                    this.submittingEvent = true;

                    await fetch('https://localhost:7134/api/event/create', {
                        method: 'POST',
                        headers: {
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify(this.formDetails)
                    })
                        .then(async (response: Response): Promise<ServerValidationResponse | FormResponse> => await response.json())
                        .then(async (data: ServerValidationResponse | FormResponse): Promise<void> => {
                            if (data.hasOwnProperty('errors')) {
                                this.buildServerValidations(data as ServerValidationResponse);

                                this.submittingEvent = false;

                                return;
                            }
                            else if (!data.success) {
                                this.stateMessage = data as UserFormResponse;
                                this.submittingEvent = false;

                                return;
                            }

                            this.formDetails = {
                                name: '',
                                type: 0,
                                description: '',
                                website: null,
                                isPublic: false,
                                isDigital: false,
                                guestMax: 0,
                                startDate: new Date(),
                                endDate: new Date(),
                                address: null
                            } as FormDetails;
                        })
                        .catch(async (error: Error): Promise<void> => {
                            console.log(error);

                            attempts++;

                            if (attempts < 2) {
                                await this.submitRegister(attempts);

                                return;
                            }

                            this.stateMessage = {
                                success: false,
                                message: 'Oops, we are having trouble registering. Please try again later!'
                            } as UserFormResponse;

                            this.registering = false;
                        });
                }                
            },
            isFormValid(): boolean {
                const errors: Errors = {};

                
            },
            buildServerValidations(validations: ServerValidationResponse): void {

            },
            toggleLocation(): void {
                if (this.hasLocation) {
                    this.hasLocation = false;

                    this.formDetails.address = null;
                }
                else {
                    this.formDetails.address = {
                        streetLineOne: '',
                        streetLineTwo: null,
                        streetLineThree: null,
                        city: '',
                        state: '',
                        postalCode: '',
                        country: '',
                        phoneNumber: '',
                        email: ''
                    } as Address;

                    this.hasLocation = true;
                }
            },
            toggleStates(): void {
                this.formDetails.address.state = '';

                if ((this.formDetails.address.country === '' && this.states.length > 0)) {
                    this.states = [] as State[];

                    return;
                }

                this.states = countries.find((x: Country) => x.code === this.formDetails.address.country).states as State[];
            }
        }
    });
</script>

<style scoped>
    .new-event {
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
                text-align: center;
            }

    label {
        font-weight: 600;
    }

    input[type=text], input[type=number], select, textarea {
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

        button.disabled {
            pointer-events: none;
        }

    a {
        text-decoration-line: none;
    }

        a:hover {
            text-decoration-line: none;
        }

        a.disabled {
            border-color: #333;
            background-color: #333;
            pointer-events: none;
        }
</style>
